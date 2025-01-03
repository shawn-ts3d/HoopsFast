using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Linq;
using System.Windows.Controls.Ribbon;

using FASTInputDll;
using Microsoft.Win32;
using System.Diagnostics;
using System.Windows.Threading;
using System.Threading;

namespace HoopsFast
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : RibbonWindow
	{
		const string configfile = "hps_wpf_sandbox_config.xml";
        public string samplesDir;
        XElement _config = XElement.Load(configfile);
		HPS.World _hpsWorld;
		MyErrorHandler _errorHandler;
		MyWarningHandler _warningHandler;
        public bool _enableFrameRate;

        public Boolean test { get; set; } = false;

        public MainWindow()
        {
            InitializeComponent();

            try
            {
                // Load sandbox config file
                _config = XElement.Load(configfile);

                // Set default data dir
                samplesDir = _config.Element("directories").Element("samples").Value;
                _enableFrameRate = false;

                InitializeSprockets();
                NewVisualize.Execute(this);
            }
            catch (System.Exception ex)
            {
                string msg = "Error. Caught exception: " + ex.Message;
                MessageBox.Show(msg, "Error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                Application.Current.Shutdown(-1);
            }
        }

        public HPS.Canvas GetCanvas()
        {
            return GetSprocketsControl().Canvas;
        }

        public SprocketsWPFControl GetSprocketsControl()
        {
            return _mainBorder.Child as SprocketsWPFControl;
        }

        public void SetSprocketsControl(SprocketsWPFControl ctrl)
        {
            _mainBorder.Child = ctrl;
        }


        /// <summary>
        /// Initializes Sprockets and creates SprocketsWPFControl
        /// </summary>
        private void InitializeSprockets()
        {
            // Create and initialize Sprockets World
            string materialsDir = _config.Element("directories").Element("materials").Value;
            string fontsDir = _config.Element("directories").Element("fonts").Value;
            _hpsWorld = new HPS.World(HOOPS_LICENSE.KEY);
            _hpsWorld.SetMaterialLibraryDirectory(materialsDir);
            _hpsWorld.SetFontDirectory(fontsDir);


            // Create and attach Sprockets Control
            SprocketsWPFControl ctrl = new SprocketsWPFControl(HPS.Window.Driver.Default3D, "main");
            //ctrl.FileDropped += OnFileDrop;
            SetSprocketsControl(ctrl);

            _errorHandler = new MyErrorHandler();
            _warningHandler = new MyWarningHandler();
        }



        /// <summary>
        /// Sets up defaults for a Sprockets Control attached to the specified WPF border control
        /// </summary>
        public void SetupSceneDefaults()
        {
            // Grab the SprocketsWPFControl from the border element
            SprocketsWPFControl ctrl = GetSprocketsControl();
            if (ctrl == null)
                return;

            // Attach a model
            HPS.View view = ctrl.Canvas.GetFrontView();
            view.AttachModel(Hoops.Model);

            // Set default operators.  Orbit is on top and will be replaced when the operator is changed
            view.GetOperatorControl()
                .Push(new HPS.MouseWheelOperator(), HPS.Operator.Priority.Low)
                .Push(new HPS.ZoomOperator(HPS.MouseButtons.ButtonMiddle()))
                .Push(new HPS.PanOperator(HPS.MouseButtons.ButtonRight()))
                .Push(new HPS.OrbitOperator(HPS.MouseButtons.ButtonLeft()));

            // Subscribe _errorHandler to handle errors
            HPS.Database.GetEventDispatcher().Subscribe(_errorHandler, HPS.Object.ClassID<HPS.ErrorEvent>());

            // Subscribe _warningHandler to handle warnings
            HPS.Database.GetEventDispatcher().Subscribe(_warningHandler, HPS.Object.ClassID<HPS.WarningEvent>());

            view.GetAxisTriadControl().SetVisibility(true);

            // Make sure the segment browser root is correct.
            //SegmentBrowserRootCommand.Execute(null);
        }

        private void menuItemFileOpen_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openD = new OpenFileDialog();
            openD.DefaultExt = ".fst";
            openD.Filter = "FAST Input File (.fst)|*.fst";

            if (openD.ShowDialog() == true)
            {
                Hoops.CreateNewHoopsModel();

                FstModel fstModel = new FstModel();
                FstInput fstInput = new FstInput();
                

                Fast.fileName_fst = openD.FileName;
                fstModel.filePath = System.IO.Path.GetDirectoryName(openD.FileName);   //may not be needed, check
                fstModel.ParseFstInputFile(Fast.fileName_fst, Fast.status, ref fstInput);

                Fast.fstInput = fstInput;
                Fast.fstModel = fstModel;

                Fast.oneTurbine = new TurbineData();
                Fast.oneTurbine.fst.ParseFstInput(Fast.fileName_fst, Fast.status);

                NewVisualize.Execute(this);

                menuTabSimulate.Visibility = Visibility.Visible;
                menuTabVis.Visibility = Visibility.Visible;
                menuTabVis.IsSelected = true;

                //fst
                menuTabFst.Visibility = Visibility.Visible;

                //ED
                if (Fast.oneTurbine.fst.CompElast.value == 1)
                {
                    Fast.oneTurbine.ED.ParseEDInput(fstModel.EDFile, Fast.status, Fast.fstInput);
                    menuTabED.Visibility = Visibility.Visible;
                }

                //Inflow
                if (Fast.oneTurbine.fst.CompInflow.value == 1)
                {
                    //
                }

                //Aero
                if (Fast.oneTurbine.fst.CompAero.value == 2)
                {
                    Fast.oneTurbine.AD.ParseADInput(fstModel.AeroFile, Fast.status, Fast.fstInput);
                    menuTabAD.Visibility = Visibility.Visible;
                }

                //Servo
                if (Fast.oneTurbine.fst.CompServo.value == 1)
                {
                    
                }

                //Hydro
                if (Fast.oneTurbine.fst.CompHydro.value == 1)
                {
                    Fast.oneTurbine.HD.ParseHDInput(fstModel.HydroFile, Fast.status, Fast.fstInput);
                    menuTabHD.Visibility = Visibility.Visible;
                }

                //Sub-structure
                if (Fast.oneTurbine.fst.CompSub.value == 1)
                {
                    Fast.oneTurbine.SD.ParseSDInput(fstModel.SubFile, Fast.status, Fast.fstInput);
                    menuTabSD.Visibility = Visibility.Visible;
                }

                //MoorDyn
                if (Fast.oneTurbine.fst.CompMooring.value == 3)
                {
                    Fast.oneTurbine.MD.ParseMDInput(fstModel.MooringFile, Fast.status, Fast.fstInput);
                    menuTabMD.Visibility = Visibility.Visible;
                }
                
                
                if (Fast.oneTurbine.fst.CompElast.value == 1)
                    VisED.Execute(this);
                if (Fast.oneTurbine.fst.CompAero.value == 2)
                    VisAD.Execute(this);
                if (Fast.oneTurbine.fst.CompHydro.value == 1)
                    VisHD.Execute(this);
                if (Fast.oneTurbine.fst.CompSub.value == 1)
                    VisSD.Execute(this);
                if (Fast.oneTurbine.fst.CompMooring.value == 3)
                    VisMD.Execute(this);


                //HPS.Stream.ExportOptionsKit exportOptionsKit = new HPS.Stream.ExportOptionsKit();
                //exportOptionsKit.SetColorCompression(true, 16); // color compression ranges from 0 to 72
                //HPS.Stream.File.Export("test", Hoops.Model.GetSegmentKey(), exportOptionsKit).Wait();

                Hoops.Model.GetSegmentKey().InsertDistantLight(new HPS.Vector(1, 1, 1));

                SprocketsWPFControl ctrl = GetSprocketsControl();
                HPS.View frontView = GetCanvas().GetFrontView();
                HPS.CameraKit fitWorldCamera;
                ctrl.Canvas.GetFrontView().ComputeFitWorldCamera(out fitWorldCamera);
                ctrl.Canvas.GetFrontView().SmoothTransition(fitWorldCamera);

                ctrl.Canvas.GetFrontView().GetNavigationCubeControl().SetVisibility(true).SetInteractivity(true);

                GetCanvas().Update();
            }
        }

        private void menuItemFileExit_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }
        public void CreateNewModel()
        {
            if (Hoops.Model != null)
            {
                Hoops.Model.Delete();
            }
            if (Fast.oneTurbine != null)
            {
                Hoops.Model = HPS.Factory.CreateModel(System.IO.Path.GetFileNameWithoutExtension(Fast.oneTurbine.fst.fileName));
            }
            else
            {
                Hoops.Model = HPS.Factory.CreateModel();
            }
            Hoops.DefaultCamera = null;
        }

        /// <summary>
        /// OnClosed override used for cleaning up HPS and Sprockets resources.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnClosed(EventArgs e)
        {
            // Cleanup
            if (GetSprocketsControl() != null)
            {
                if (Hoops.Model != null)
                {
                    Hoops.Model.Delete();
                    Hoops.Model.Dispose();
                    Hoops.Model = null;
                }
                GetSprocketsControl().Delete();     // Calls Delete() on View and Canvas
                GetSprocketsControl().Dispose();
                _hpsWorld.Dispose();
                _hpsWorld = null;
            }

            base.OnClosed(e);
        }

        private GeneralCommand _exitCommand;
        private Visualize _newVisualize;
        private Visualize _visHD;
        private Visualize _visAD;
        private Visualize _visBlade;

        private Visualize _visED;
        private Visualize _visSD;
        private Visualize _visMD;

        private Visualize _visAnimate;

        private Visualize _operatorOrbit;
        private Visualize _operatorPan;
        private Visualize _operatorZoom;
        private Visualize _operatorZoomBox;
        private Visualize _operatorSelect;

        private Visualize _effectSimpleShadow;
        private Visualize _effectHiddenLine;
        private Visualize _effectEyeDome;
        private Visualize _effectReflection;

        public ICommand ExitCommand
        {
            get
            {
                if (_exitCommand == null)
                    _exitCommand = new ExitCommand(this);
                return _exitCommand;
            }
        }
        public ICommand NewVisualize
        {
            get
            {
                if (_newVisualize == null)
                    _newVisualize = new NewVisualize(this);
                return _newVisualize;
            }
        }
        public ICommand VisHD
        {
            get
            {
                if (_visHD == null)
                    _visHD = new VisualizeHD(this);
                return _visHD;
            }
        }
        public ICommand VisAD
        {
            get
            {
                if (_visAD == null)
                    _visAD = new VisualizeAD(this);
                return _visAD;
            }
        }
        public ICommand VisBlade
        {
            get
            {
                if (_visBlade == null)
                    _visBlade = new VisualizeBlade(this);
                return _visBlade;
            }
        }
        public ICommand VisED
        {
            get
            {
                if (_visED == null)
                    _visED = new VisualizeED(this);
                return _visED;
            }
        }
        public ICommand VisSD
        {
            get
            {
                if (_visSD == null)
                    _visSD = new VisualizeSD(this);
                return _visSD;
            }
        }
        public ICommand VisMD
        {
            get
            {
                if (_visMD == null)
                    _visMD = new VisualizeMD(this);
                return _visMD;
            }
        }
        public ICommand VisAnimate
        {
            get
            {
                if (_visAnimate == null)
                    _visAnimate = new VisualizeAnimate(this);
                return _visAnimate;
            }
        }

        public ICommand OperatorOrbit
        {
            get
            {
                if (_operatorOrbit == null)
                    _operatorOrbit = new OperatorOrbit(this);
                return _operatorOrbit;
            }
        }

        public ICommand OperatorPan
        {
            get
            {
                if (_operatorPan == null)
                    _operatorPan = new OperatorPan(this);
                return _operatorPan;
            }
        }

        public ICommand OperatorZoom
        {
            get
            {
                if (_operatorZoom == null)
                    _operatorZoom = new OperatorZoom(this);
                return _operatorZoom;
            }
        }

        public ICommand OperatorZoomBox
        {
            get
            {
                if (_operatorZoomBox == null)
                    _operatorZoomBox = new OperatorZoomBox(this);
                return _operatorZoomBox;
            }
        }

        public ICommand OperatorSelect
        {
            get
            {
                if (_operatorSelect == null)
                    _operatorSelect = new OperatorSelect(this);
                return _operatorSelect;
            }
        }

        public ICommand EffectSimpleShadow
        {
            get
            {
                if (_effectSimpleShadow == null)
                    _effectSimpleShadow = new EffectSimpleShadow(this);
                return _effectSimpleShadow;
            }
        }

        public ICommand EffectHiddenLine
        {
            get
            {
                if (_effectHiddenLine == null)
                    _effectHiddenLine = new EffectHiddenLine(this);
                return _effectHiddenLine;
            }
        }

        public ICommand EffectEyeDome
        {
            get
            {
                if (_effectEyeDome == null)
                    _effectEyeDome = new EffectEyeDome(this);
                return _effectEyeDome;
            }
        }

        public ICommand EffectReflection
        {
            get
            {
                if (_effectReflection == null)
                    _effectReflection = new EffectReflection(this);
                return _effectReflection;
            }
        }

        private void menuItemADTower_Click(object sender, RoutedEventArgs e)
        {
            AD.AD_Tower AD_Tower = new AD.AD_Tower();
            AD_Tower.ShowDialog();
        }

        private void menuItemADGeneral_Click(object sender, RoutedEventArgs e)
        {
            AD.AD_GeneralOptions AD_GeneralOptions = new AD.AD_GeneralOptions();
            AD_GeneralOptions.ShowDialog();
        }

        private void menuItemADEnvCon_Click(object sender, RoutedEventArgs e)
        {
            AD.AD_EnvCon AD_EnvCon = new AD.AD_EnvCon();
            AD_EnvCon.ShowDialog();
        }

        private void menuItemADBEMTOptions_Click(object sender, RoutedEventArgs e)
        {
            AD.AD_BEMTOptions AD_BEMTOptions = new AD.AD_BEMTOptions();
            AD_BEMTOptions.ShowDialog();
        }

        private void menuItemADDynamicBEMTOptions_Click(object sender, RoutedEventArgs e)
        {
            AD.AD_DynamicBEMTOptions AD_DynamicBEMTOptions = new AD.AD_DynamicBEMTOptions();
            AD_DynamicBEMTOptions.ShowDialog();
        }

        private void menuItemADOLAFOptions_Click(object sender, RoutedEventArgs e)
        {
            //TODO
        }

        private void menuItemADUnsteadyAeroOptions_Click(object sender, RoutedEventArgs e)
        {
            //TODO
        }

        private void menuItemADAirfoilInfo_Click(object sender, RoutedEventArgs e)
        {
            AD.AD_AirfoilInfo AD_AirfoilInfo = new AD.AD_AirfoilInfo();
            AD_AirfoilInfo.ShowDialog();
        }

        private void menuItemADProperties_Click(object sender, RoutedEventArgs e)
        {
            AD.AD_Properties AD_Properties = new AD.AD_Properties();
            AD_Properties.ShowDialog();
        }


        private void menuItemFileSave_Click(object sender, RoutedEventArgs e)
        {
            Fast.oneTurbine.fst.WriteFstFile(Fast.oneTurbine.fst);
        }

        private void menuItemFileSaveAs_Click(object sender, RoutedEventArgs e)
        {

        }

        private void menuItemFstSimCon_Click(object sender, RoutedEventArgs e)
        {
            Fst.Fst_SimCon fst_SimCon = new Fst.Fst_SimCon();
            fst_SimCon.ShowDialog();
        }

        private void menuItemFstFeature_Click(object sender, RoutedEventArgs e)
        {
            Fst.Fst_Feature fst_Feature = new Fst.Fst_Feature();
            fst_Feature.ShowDialog();
        }

        private void menuItemFstEnvCon_Click(object sender, RoutedEventArgs e)
        {
            Fst.Fst_EnvCon fst_EnvCon = new Fst.Fst_EnvCon();
            fst_EnvCon.ShowDialog();
        }

        private void menuItemFstInputFiles_Click(object sender, RoutedEventArgs e)
        {
            Fst.Fst_InputFiles fst_InputFiles = new Fst.Fst_InputFiles();
            fst_InputFiles.ShowDialog();
        }

        private void menuItemFstOutput_Click(object sender, RoutedEventArgs e)
        {
            Fst.Fst_Output fst_Output = new Fst.Fst_Output();
            fst_Output.ShowDialog();
        }

        private void menuItemFstLinear_Click(object sender, RoutedEventArgs e)
        {
            Fst.Fst_Linearization fst_Linearization = new Fst.Fst_Linearization();
            fst_Linearization.ShowDialog();
        }

        private void menuItemFstVisual_Click(object sender, RoutedEventArgs e)
        {
            Fst.Fst_Visualization fst_Visualization = new Fst.Fst_Visualization();
            fst_Visualization.ShowDialog();
        }

        private void menuItemSimRun_Click(object sender, RoutedEventArgs e)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = "openfast_x64.exe";
            startInfo.Arguments = Fast.fileName_fst;

            try
            {
                // Start the process with the info we specified.
                // Call WaitForExit and then the using statement will close.
                using (Process exeProcess = Process.Start(startInfo))
                {
                    exeProcess.WaitForExit();
                }
            }
            catch
            {
                // Log error.
            }

        }

        private void menuItemSimOpenOutFile_Click(object sender, RoutedEventArgs e)
        {
            var p = new Process();
            var resultFile = System.IO.Path.ChangeExtension(Fast.fileName_fst, ".out");
            if (System.IO.File.Exists(resultFile))
            {
                p.StartInfo = new ProcessStartInfo(resultFile)
                {
                    UseShellExecute = true
                };
                p.Start();
            }
            else
            {
                MessageBox.Show("OpenFAST out file " + resultFile + " does not exist!");
            }
        }

        private void menuItemSimImportResults_Click(object sender, RoutedEventArgs e)
        {
            var outFile = System.IO.Path.ChangeExtension(Fast.fileName_fst, ".out");
            if (System.IO.File.Exists(outFile))
            {
                PostProcess.FstOutResults fst_OutResults = new PostProcess.FstOutResults(outFile);
                fst_OutResults.ShowDialog();
            }
            else
            {
                MessageBox.Show("OpenFAST out file " + outFile + " does not exist!");
            }
        }

        private void menuItemExportHsf_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            dlg.Filter = "HSF|*.hsf";
            dlg.FileName = System.IO.Path.GetFileNameWithoutExtension(Fast.oneTurbine.fst.fileName);
            dlg.ShowDialog();
            if (dlg.FileName != "")
            {
                HPS.SegmentKey exportFromHere;

                if (Hoops.Model.Type() == HPS.Type.None)
                    exportFromHere = GetCanvas().GetFrontView().GetSegmentKey();
                else
                    exportFromHere = Hoops.Model.GetSegmentKey();

                HPS.IOResult status = HPS.IOResult.Failure;
                try
                {
                    HPS.Stream.ExportNotifier notifier = HPS.Stream.File.Export(dlg.FileName, exportFromHere, new HPS.Stream.ExportOptionsKit());
                    DisplayExportProgress(notifier);   //this needs to be changed!
                    status = notifier.Status();
                }
                catch (HPS.IOException ee)
                { MessageBox.Show("HPS::Stream::File::Export threw an exception: " + ee.Message); }
                if (status != HPS.IOResult.Success && status != HPS.IOResult.Canceled)
                    MessageBox.Show("HPS.Stream.Export encountered an error.");
            }
        }

        private bool DisplayExportProgress(HPS.IONotifier notifier)
        {
            bool success = true;
            InvokeUIAction(delegate ()
            {
                //show the progress dialog
                GetSprocketsControl().IsEnabled = false;
                //var dlg = new ProgressBar(_win, notifier, ProgressBar.Operation.Export);
                //dlg.Owner = _win;
                //dlg.ShowDialog();

                //success = dlg.WasSuccessful();
                Thread.Sleep(50000);

                GetSprocketsControl().IsEnabled = true;
            }, true);
            return success;
        }

        private void InvokeUIAction(Action action, bool wait)
        {
            if (wait)
                Dispatcher.Invoke(DispatcherPriority.Normal, new Action(action));
            else
                Dispatcher.BeginInvoke(new Action(action));
        }

        public void Unhighlight()
        {
            var highlightOptions = new HPS.HighlightOptionsKit();
            highlightOptions.SetStyleName("highlight_style").SetNotification(true);

            var canvas = GetSprocketsControl().Canvas;
            canvas.GetWindowKey().GetHighlightControl().Unhighlight(highlightOptions);

            HPS.Database.GetEventDispatcher().InjectEvent(new HPS.HighlightEvent(HPS.HighlightEvent.Action.Unhighlight, new HPS.SelectionResults(), highlightOptions));
            HPS.Database.GetEventDispatcher().InjectEvent(new HPS.ComponentHighlightEvent(HPS.ComponentHighlightEvent.Action.Unhighlight, canvas, 0, new HPS.ComponentPath(), highlightOptions));
        }

        public void Update()
        {
            GetSprocketsControl().Canvas.Update();
        }

        private void menuItemMaterialHD_Click(object sender, RoutedEventArgs e)
        {
            PostProcess.MaterialSettings materialSettings = new PostProcess.MaterialSettings(Hoops.HDKey);
            materialSettings.ShowDialog();
        }

        private void menuItemBackground_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.jpg, *.jpeg, *.png)|*.jpg;*.jpeg;*.png";
            if (openFileDialog.ShowDialog() == true)
            {
                string file_in = openFileDialog.FileName;

                HPS.ImageKit imageKit = new HPS.ImageKit();
                HPS.Image.ImportOptionsKit importOptionsKit = new HPS.Image.ImportOptionsKit();
                importOptionsKit.SetFormat(HPS.Image.Format.Jpeg);
                imageKit = HPS.Image.File.Import(file_in, importOptionsKit);

                HPS.PortfolioKey pKey = HPS.Database.CreatePortfolio();
                HPS.ImageDefinition imageDefinition = pKey.DefineImage("my_image", imageKit);

                GetCanvas().GetWindowKey().GetPortfolioControl().Push(pKey);
                GetCanvas().GetWindowKey().GetSubwindowControl().SetBackground(HPS.Subwindow.Background.Image, imageDefinition.Name());
                GetCanvas().Update();
            }
        }

    }
}
