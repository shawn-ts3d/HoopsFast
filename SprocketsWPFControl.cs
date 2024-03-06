#define USE_D3D_IMAGE_CANVAS

using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;

namespace HoopsFast
{

    public class MyErrorHandler : HPS.EventHandler
    {
        // Override to provide behavior for an error event
        public override HPS.EventHandler.HandleResult Handle(HPS.Event in_event)
        {
            HPS.ErrorEvent error = new HPS.ErrorEvent(in_event);
            System.Diagnostics.Debug.WriteLine(error.message);
            return HandleResult.Handled;
        }

        ~MyErrorHandler()
        {
            Shutdown();
        }
    }

    public class MyWarningHandler : HPS.EventHandler
    {
        public override HPS.EventHandler.HandleResult Handle(HPS.Event in_event)
        {
            HPS.WarningEvent warning = new HPS.WarningEvent(in_event);
            System.Diagnostics.Debug.WriteLine(warning.message);
            return HandleResult.Handled;
        }

        ~MyWarningHandler()
        {
            Shutdown();
        }
    }

    public class MyFileDropEventArgs : System.EventArgs
    {
        public string Filename
        {
            get;
            set;
        }
    }

#if USE_D3D_IMAGE_CANVAS
    public class SprocketsWPFControl : System.Windows.Controls.Image, IDisposable
    {
        HPS.D3DImageCanvas _canvas;

        public HPS.Canvas Canvas
        {
            get { return _canvas; }
        }

        public EventHandler<MyFileDropEventArgs> FileDropped;

        public SprocketsWPFControl(HPS.Window.Driver driver, string canvasName)
        {
            HPS.OffScreenWindowOptionsKit options = new HPS.OffScreenWindowOptionsKit();
            options.SetPreferredGPU(HPS.GPU.Preference.Default);
            _canvas = new HPS.D3DImageCanvas(options, canvasName);
            Source = _canvas.ImageSource;
            Stretch = Stretch.Fill;
            AllowDrop = true;

            SizeChanged += SprocketsWPFControl_SizeChanged;

            MouseDown += (s, a) => { Mouse.Capture(this); };
            MouseUp += (s, a) => { Mouse.Capture(null); };
            Focusable = true;
            Loaded += (s, e) => { Keyboard.Focus(this); };

            HPS.View view = HPS.Factory.CreateView(canvasName);
            Canvas.AttachViewAsLayout(view);
        }

        private void SprocketsWPFControl_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            _pixelToWindowMatrix = null;
            var size = (Size)TransformToPixels(this, (Point)e.NewSize);
            _canvas.OnSizeChanged(this, size, true);
        }

        ~SprocketsWPFControl()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_canvas != null)
                {
                    _canvas.Dispose();
                    _canvas = null;
                }
            }
        }

        public void Delete()
        {
            Canvas.GetFrontView().Delete();
            Canvas.Delete();
        }

        private static HPS.MouseButtons MapMouseButton(MouseButtonEventArgs e)
        {
            HPS.MouseButtons buttons = new HPS.MouseButtons();

            switch (e.ChangedButton)
            {
                case MouseButton.Left:
                    buttons.Left(true);
                    break;

                case MouseButton.Right:
                    buttons.Right(true);
                    break;

                case MouseButton.Middle:
                    buttons.Middle(true);
                    break;

                case MouseButton.XButton1:
                    buttons.X1(true);
                    break;

                case MouseButton.XButton2:
                    buttons.X2(true);
                    break;
            }

            return buttons;
        }

        private HPS.MatrixKit _pixelToWindowMatrix;

        private HPS.MatrixKit PixelToWindowMatrix
        {
            get
            {
                if (_pixelToWindowMatrix == null)
                {
                    HPS.KeyPath keyPath = new HPS.KeyPath();
                    keyPath.Append(Canvas.GetWindowKey());
                    _pixelToWindowMatrix = new HPS.MatrixKit();
                    keyPath.ComputeTransform(HPS.Coordinate.Space.Pixel, HPS.Coordinate.Space.Window, out _pixelToWindowMatrix);
                }

                return _pixelToWindowMatrix;
            }
        }

        /// <summary>
        /// Helper method for constructing a HPS.MouseEvent
        /// </summary>   
        private HPS.MouseEvent BuildMouseEvent(HPS.MouseEvent.Action action, HPS.MouseButtons buttons, int x, int y, bool down, float scalar, ulong click_count)
        {
            // Convert location to window space
            HPS.Point p = new HPS.Point(x, y, 0);
            p = PixelToWindowMatrix.Transform(p);

            HPS.WindowPoint windowPoint = new HPS.WindowPoint(p.x, p.y, p.z);

            if (action == HPS.MouseEvent.Action.Scroll)
                return new HPS.MouseEvent(action, scalar, windowPoint, MapModifierKeys());
            else
                return new HPS.MouseEvent(action, windowPoint, buttons, MapModifierKeys(), click_count);
        }

        private HPS.KeyboardEvent BuildKeyboardEvent(HPS.KeyboardEvent.Action action, KeyEventArgs e)
        {
            HPS.KeyboardCode[] code = new HPS.KeyboardCode[1];
            HPS.ModifierKeys modifiers = MapModifierKeys();

            code[0] = (HPS.KeyboardCode)KeyInterop.VirtualKeyFromKey(e.Key);
            return new HPS.KeyboardEvent(action, code, modifiers);
        }

        private HPS.KeyboardEvent BuildKeyboardEvent(HPS.KeyboardEvent.Action action, TextCompositionEventArgs e)
        {
            HPS.KeyboardCode[] code = new HPS.KeyboardCode[1];
            HPS.ModifierKeys modifiers = MapModifierKeys();

            code[0] = (HPS.KeyboardCode)(e.Text[e.Text.Length - 1]);
            return new HPS.KeyboardEvent(action, code, modifiers);
        }

        private static HPS.ModifierKeys MapModifierKeys()
        {
            var modifierKeyState = new HPS.ModifierKeys();

            if (Keyboard.IsKeyDown(Key.LeftShift))
                modifierKeyState.LeftShift(true);
            if (Keyboard.IsKeyDown(Key.RightShift))
                modifierKeyState.RightShift(true);

            if (Keyboard.IsKeyDown(Key.LeftCtrl))
                modifierKeyState.LeftControl(true);
            if (Keyboard.IsKeyDown(Key.RightCtrl))
                modifierKeyState.RightControl(true);

            if (Keyboard.IsKeyDown(Key.LeftAlt))
                modifierKeyState.LeftAlt(true);
            if (Keyboard.IsKeyDown(Key.RightAlt))
                modifierKeyState.RightAlt(true);

            if (Keyboard.IsKeyDown(Key.LWin))
                modifierKeyState.LeftMeta(true);
            if (Keyboard.IsKeyDown(Key.RWin))
                modifierKeyState.RightMeta(true);

            if (Keyboard.IsKeyToggled(Key.CapsLock))
                modifierKeyState.CapsLock(true);

            return modifierKeyState;
        }

        private Point TransformToPixels(Visual visual, Point p)
        {
            Matrix matrix;
            var source = PresentationSource.FromVisual(visual);
            if (source != null)
            {
                matrix = source.CompositionTarget.TransformToDevice;
            }
            else
            {
                using (var src = new HwndSource(new HwndSourceParameters()))
                {
                    matrix = src.CompositionTarget.TransformToDevice;
                }
            }

            Point pixels = new Point(matrix.M11 * p.X, matrix.M22 * p.Y);
            return pixels;
        }

        protected override void OnMouseDown(System.Windows.Input.MouseButtonEventArgs e)
        {
            base.OnMouseDown(e);
            var loc = TransformToPixels(this, e.GetPosition(this));
            Canvas.GetWindowKey().GetEventDispatcher().InjectEvent(BuildMouseEvent(HPS.MouseEvent.Action.ButtonDown, MapMouseButton(e), (int)loc.X, (int)loc.Y, false, 0, (ulong)e.ClickCount));
        }

        protected override void OnMouseUp(System.Windows.Input.MouseButtonEventArgs e)
        {
            base.OnMouseUp(e);
            var loc = TransformToPixels(this, e.GetPosition(this));
            Canvas.GetWindowKey().GetEventDispatcher().InjectEvent(BuildMouseEvent(HPS.MouseEvent.Action.ButtonUp, MapMouseButton(e), (int)loc.X, (int)loc.Y, false, 0, 0));
        }

        protected override void OnMouseMove(System.Windows.Input.MouseEventArgs e)
        {
            base.OnMouseMove(e);
            var loc = TransformToPixels(this, e.GetPosition(this));
            Canvas.GetWindowKey().GetEventDispatcher().InjectEvent(BuildMouseEvent(HPS.MouseEvent.Action.Move, new HPS.MouseButtons(), (int)loc.X, (int)loc.Y, false, 0, 0));
        }

        protected override void OnMouseWheel(MouseWheelEventArgs e)
        {
            base.OnMouseWheel(e);
            var loc = e.GetPosition(this);
            Canvas.GetWindowKey().GetEventDispatcher().InjectEvent(BuildMouseEvent(HPS.MouseEvent.Action.Scroll, new HPS.MouseButtons(), (int)loc.X, (int)loc.Y, false, e.Delta, 0));
        }

        protected override void OnDragEnter(DragEventArgs e)
        {
            base.OnDragEnter(e);

            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effects = DragDropEffects.All;
        }

        protected override void OnTextInput(TextCompositionEventArgs e)
        {
            base.OnTextInput(e);
            if (e.Text.Length > 0)
                Canvas.GetWindowKey().GetEventDispatcher().InjectEvent(BuildKeyboardEvent(HPS.KeyboardEvent.Action.KeyDown, e));
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            HPS.ModifierKeys modifiers = MapModifierKeys();
            if (modifiers.Control() || modifiers.Alt())
            {
                //This event will not translate to readable text, so take care of it here rather than in OnTextInput
                Canvas.GetWindowKey().GetEventDispatcher().InjectEvent(BuildKeyboardEvent(HPS.KeyboardEvent.Action.KeyDown, e));
            }
        }

        protected override void OnKeyUp(KeyEventArgs e)
        {
            base.OnKeyUp(e);
            Canvas.GetWindowKey().GetEventDispatcher().InjectEvent(BuildKeyboardEvent(HPS.KeyboardEvent.Action.KeyUp, e));
        }

        protected override void OnDrop(DragEventArgs e)
        {
            base.OnDrop(e);

            if (e.Data.GetDataPresent(DataFormats.FileDrop) && FileDropped != null)
            {
                string[] fileName = (string[])(e.Data.GetData(DataFormats.FileDrop));
                FileDropped(this, new MyFileDropEventArgs()
                {
                    Filename = fileName[0]
                });
            }
        }
    }
#else
    public class SprocketsWPFControl : WindowsFormsHost
    {
        /// <summary>
        /// Winforms UserControl used for the HWND which HPS will render on to
        /// </summary>
        public Winforms.UserControl _winformsControl;

        /// <summary>
        /// Sprockets Canvas
        /// </summary>
        public HPS.Canvas Canvas { get; private set; }


        public EventHandler<MyFileDropEventArgs> FileDropped;      

        /// <summary>
        /// This class is used primarily to get a HWND (Window Handle) which HPS can render on to.
        /// Deriving a custom UserControl gives more control over when HPS performs a render.
        /// </summary>
        private class InternalWinformsControl : Winforms.UserControl
        {
            private SprocketsWPFControl _owner;

            public InternalWinformsControl(SprocketsWPFControl owner)
            {
                _owner = owner;
            }

            protected override void OnPaintBackground(Winforms.PaintEventArgs e)
            {
                // Empty.  Don't let Windows paint background.
            }

            protected override void OnPaint(Winforms.PaintEventArgs e)
            {
                _owner.Canvas.Update(HPS.Window.UpdateType.Refresh);
            }

            protected override void OnResize(EventArgs e)
            {
                _owner._pixelToWindowMatrix = null;
                _owner.Canvas.Update(HPS.Window.UpdateType.Refresh);
            }
        }

        public SprocketsWPFControl(HPS.Window.Driver driver, string canvasName)
        {
            // Create our Winforms Control
            _winformsControl = new InternalWinformsControl(this);
            _winformsControl.AllowDrop = true;

            _winformsControl.DragEnter += _winformsControl_DragEnter;
            _winformsControl.DragDrop += _winformsControl_DragDrop;

            // Create our Sprockets Canvas with the specified driver
            HPS.ApplicationWindowOptionsKit awok = new HPS.ApplicationWindowOptionsKit();
            awok.SetDriver(driver);
            Canvas = HPS.Factory.CreateCanvas(_winformsControl.Handle, canvasName, awok);

            // Create a new Sprockets View and attach it to our Sprockets.Canvas
            HPS.View view = HPS.Factory.CreateView(canvasName);
            Canvas.AttachViewAsLayout(view);

            // Attach Mouse Handlers to our Winforms Control
            _winformsControl.MouseMove += _winformsControl_MouseMove;
            _winformsControl.LostFocus += _winformsControl_LostFocus;
            _winformsControl.MouseDown += _winformsControl_MouseDown;
            _winformsControl.MouseUp += _winformsControl_MouseUp;
            _winformsControl.MouseWheel += _winformsControl_MouseWheel;
            _winformsControl.KeyDown += _winformsControl_KeyDown;
            _winformsControl.KeyUp += _winformsControl_KeyUp;

            // Attach our Winforms Control as a Child of WindowsFormsHost
            Child = _winformsControl;
        }

        protected override void OnGotFocus(RoutedEventArgs e)
        {
            base.OnGotFocus(e);
            _winformsControl.Focus();
        }

        private void _winformsControl_DragDrop(object sender, Winforms.DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop) && FileDropped != null)
            {
                string[] fileName = (string[])(e.Data.GetData(DataFormats.FileDrop));
                FileDropped(this, new MyFileDropEventArgs()
                {
                    Filename = fileName[0]
                });
            }
        }

        private void _winformsControl_DragEnter(object sender, Winforms.DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = System.Windows.Forms.DragDropEffects.All;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (Canvas != null)
                {
                    Canvas.Dispose();
                    Canvas = null;
                }
                _winformsControl.Dispose();
            }

            base.Dispose(disposing);
        }

        public void Delete()
        {
            Canvas.GetFrontView().Delete();
            Canvas.Delete();
        }

        private static HPS.MouseButtons MapMouseButton(Winforms.MouseEventArgs e)
        {
            HPS.MouseButtons buttons = new HPS.MouseButtons();

            switch (e.Button)
            {
                case Winforms.MouseButtons.Left:
                    buttons.Left(true);
                    break;

                case Winforms.MouseButtons.Right:
                    buttons.Right(true);
                    break;

                case Winforms.MouseButtons.Middle:
                    buttons.Middle(true);
                    break;

                case Winforms.MouseButtons.XButton1:
                    buttons.X1(true);
                    break;

                case Winforms.MouseButtons.XButton2:
                    buttons.X2(true);
                    break;
            }

            return buttons;
        }

        private static HPS.ModifierKeys MapModifierKeys()
        {
            HPS.ModifierKeys modifierKeyState = new HPS.ModifierKeys();

            if (Winforms.Control.ModifierKeys.HasFlag(Winforms.Keys.Shift))
                modifierKeyState.Shift(true);

            if (Winforms.Control.ModifierKeys.HasFlag(Winforms.Keys.Control))
                modifierKeyState.Control(true);

            if (Winforms.Control.ModifierKeys.HasFlag(Winforms.Keys.Alt))
                modifierKeyState.Alt(true);

            return modifierKeyState;
        }

        private HPS.MatrixKit _pixelToWindowMatrix;

        private HPS.MatrixKit PixelToWindowMatrix
        {
            get
            {
                if (_pixelToWindowMatrix == null)
                {
                    HPS.KeyPath keyPath = new HPS.KeyPath();
                    keyPath.Append(Canvas.GetWindowKey());
                    _pixelToWindowMatrix = new HPS.MatrixKit();
                    keyPath.ComputeTransform(HPS.Coordinate.Space.Pixel, HPS.Coordinate.Space.Window, out _pixelToWindowMatrix);
                }

                return _pixelToWindowMatrix;
            }
        }

        /// <summary>
        /// Helper method for constructing a HPS.MouseEvent
        /// </summary>
        /// <param name="action">MouseEvent.Action indicating type of mouse action</param>
        /// <param name="buttons">MouseButtons indicating button pressed</param>
        /// <param name="x">Mouse x position</param>
        /// <param name="y">Mouse y position</param>
        /// <param name="down">Button down state</param>
        /// <param name="scalar">Scalar value (currently used for mouse wheel)</param>
        /// <returns></returns>
        private HPS.MouseEvent BuildMouseEvent(HPS.MouseEvent.Action action, HPS.MouseButtons buttons, int x, int y, bool down, ulong click_count, float scalar)
		{
			// Convert location to window space
			HPS.Point p = new HPS.Point(x, y, 0);
			p = PixelToWindowMatrix.Transform(p);
			
            HPS.WindowPoint windowPoint = new HPS.WindowPoint(p.x, p.y, p.z);

            if (action == HPS.MouseEvent.Action.Scroll)
                return new HPS.MouseEvent(action, scalar, windowPoint, MapModifierKeys(), click_count);
            else
                return new HPS.MouseEvent(action, windowPoint, buttons, MapModifierKeys(), click_count);
		}

        /// <summary>
        /// Helper method for constructing a HPS.KeyboardEvent
        /// </summary>
        /// <param name="action">KeyboardEvent.Action indicating type of keyboard action</param>
        /// <param name="buttons">buttons indicating button pressed</param>
        /// <returns></returns>
        private HPS.KeyboardEvent BuildKeyboardEvent(HPS.KeyboardEvent.Action action, Winforms.KeyEventArgs buttons)
        {
            HPS.KeyboardCode [] code = new HPS.KeyboardCode[1];
            HPS.ModifierKeys modifiers = new HPS.ModifierKeys();
            if (Winforms.Control.ModifierKeys == Winforms.Keys.Shift)
                modifiers.Shift(true);
            if (Winforms.Control.ModifierKeys == Winforms.Keys.Control)
                modifiers.Control(true);
            code[0] = (HPS.KeyboardCode)buttons.KeyCode;
            return new HPS.KeyboardEvent(action, code, modifiers);
        }

        /// <summary>
        /// MouseMove event handler
        /// </summary>
        private void _winformsControl_MouseMove(object sender, Winforms.MouseEventArgs e)
        {
            
			Canvas.GetWindowKey().GetEventDispatcher().InjectEvent(
                BuildMouseEvent(HPS.MouseEvent.Action.Move, new HPS.MouseButtons(), e.X, e.Y, false, 0, 0));
		}

        /// <summary>
        /// MouseDown event handler
        /// </summary>
        private void _winformsControl_MouseDown(object sender, Winforms.MouseEventArgs e)
        {
            System.Windows.Forms.Control c = (System.Windows.Forms.Control)sender;
            c.Capture = true;
			Canvas.GetWindowKey().GetEventDispatcher().InjectEvent(
                BuildMouseEvent(HPS.MouseEvent.Action.ButtonDown, MapMouseButton(e), e.X, e.Y, true, (ulong)e.Clicks, 0));
        }

        /// <summary>
        /// MouseUp event handler
        /// </summary>
        private void _winformsControl_MouseUp(object sender, Winforms.MouseEventArgs e)
        {
            System.Windows.Forms.Control c = (System.Windows.Forms.Control)sender;
            c.Capture = false;
			Canvas.GetWindowKey().GetEventDispatcher().InjectEvent(
                BuildMouseEvent(HPS.MouseEvent.Action.ButtonUp, MapMouseButton(e), e.X, e.Y, false, 0, 0));
        }

        /// <summary>
        /// MouseWheel event handler
        /// </summary>
        private void _winformsControl_MouseWheel(object sender, Winforms.MouseEventArgs e)
        {
			Canvas.GetWindowKey().GetEventDispatcher().InjectEvent(
                BuildMouseEvent(HPS.MouseEvent.Action.Scroll, new HPS.MouseButtons(), e.X, e.Y, false, 0, e.Delta));
        }

        /// <summary>
        /// KeyDown event handler
        /// </summary>
        private void _winformsControl_KeyDown(object sender, Winforms.KeyEventArgs e)
        {
            Canvas.GetWindowKey().GetEventDispatcher().InjectEvent(
                BuildKeyboardEvent(HPS.KeyboardEvent.Action.KeyDown, e));
        }

        /// <summary>
        /// KeyUp event handler
        /// </summary>
        private void _winformsControl_KeyUp(object sender, Winforms.KeyEventArgs e)
        {
            Canvas.GetWindowKey().GetEventDispatcher().InjectEvent(
                BuildKeyboardEvent(HPS.KeyboardEvent.Action.KeyUp, e));
        }

        /// <summary>
        /// LostFocus event handler
        /// </summary>
        private void _winformsControl_LostFocus(object sender, System.EventArgs e)
        {
            Canvas.GetWindowKey().GetEventDispatcher().InjectEvent(new HPS.FocusLostEvent());
        }
    }
#endif





}
