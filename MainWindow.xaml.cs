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
                NewVisualize.Execute(this);

                Fast.fileName_fst = openD.FileName;
                fstModel.filePath = System.IO.Path.GetDirectoryName(openD.FileName);
                fstModel.ParseFstInputFile(Fast.fileName_fst, Fast.status, ref fstInput);

                Fast.fstInput = fstInput;
                Fast.fstModel = fstModel;

                Fast.oneTurbine = new TurbineData();
                Fast.oneTurbine.fst.ParseFstInput(Fast.fileName_fst, Fast.status);

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
            Hoops.Model = HPS.Factory.CreateModel();
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

        private void menuItemFileSave_Click(object sender, RoutedEventArgs e)
        {

        }

        private void menuItemFileSaveAs_Click(object sender, RoutedEventArgs e)
        {

        }

        private void menuItemFstSimCon_Click(object sender, RoutedEventArgs e)
        {
            Fst.Fst_SimCon fst_SimCon = new Fst.Fst_SimCon();
            fst_SimCon.ShowDialog();
        }
    }
}
