using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Windows.Input;
using System.Windows.Threading;
using System.Windows;
using System.IO;
using Microsoft.Windows.Controls.Ribbon;
using System.Windows.Controls;
using System.Drawing.Printing;
using System.Threading.Tasks;

namespace HoopsFast
{
	/// <summary>
	/// Base class for Visualize Commands
	/// </summary>
	abstract class Visualize : ICommand
	{
		/// <summary>
		/// MainWindow instance
		/// </summary>
		protected MainWindow _win;

		/// <summary>
		/// Constructs Visualize command
		/// </summary>
		/// <param name="win">MainWindow instance</param>
		public Visualize(MainWindow win)
		{
			if (win == null)
				throw new ArgumentNullException("win");

			_win = win;
		}

		#region ICommand Members

		/// <summary>
		/// Occurs when changes occur that affect whether or not the command should execute.
		/// </summary>
		public event EventHandler CanExecuteChanged
		{
			add { CommandManager.RequerySuggested += value; }
			remove { CommandManager.RequerySuggested -= value; }
		}

		/// <summary>
		/// Defines the method that determines whether the command can execute in its current state.
		/// 
		/// DemoCommands are executable by default
		/// </summary>
		/// <param name="parameter">Data used by the command. If the command does not require
		/// data to be passed, this object can be set to null.</param>
		/// <returns>true if this command can be executed; otherwise, false.</returns>
		public virtual bool CanExecute(object parameter)
		{
			return true;
		}

		/// <summary>
		/// Defines the method to be called when the command is invoked.
		/// 
		/// Override to provide command implementation
		/// </summary>
		/// <param name="parameter">Data used by the command. If the command does not require data
		/// to be passed, this object can be set to null.</param>
		public abstract void Execute(object parameter);

		#endregion // ICommand Members
	}

	class NewVisualize : Visualize
	{
		public NewVisualize(MainWindow win) : base(win) { }

		public override void Execute(object parameter)
		{
			SprocketsWPFControl ctrl = _win.GetSprocketsControl();

			// Restore scene defaults defaults
			ctrl.Canvas.GetWindowKey().GetHighlightControl().UnhighlightEverything();
			_win.CreateNewModel();
			_win.SetupSceneDefaults();

			//_win.InitializeBrowsers();

			// Trigger update
			ctrl.Canvas.Update();
		}
	}


	/// <summary>
	/// Visualize HD geometries
	/// </summary>
	class VisualizeHD : Visualize
	{
		public VisualizeHD(MainWindow win)
			: base(win) { }

		public override void Execute(object parameter)
		{
			if (Hoops.HDKey == null)
			{
				Hoops.CreateHDModel(Fast.oneTurbine);
				Hoops.HDKey.GetVisibilityControl().SetFaces(true);
			}
			else
			{
				bool visible;
				Hoops.HDKey.GetVisibilityControl().ShowFaces(out visible);
				if (visible)
				{
					Hoops.HDKey.GetVisibilityControl().SetFaces(false);
				}
				else
				{
					Hoops.HDKey.GetVisibilityControl().SetFaces(true);
				}
					
			}
			_win.GetCanvas().Update();
		}
	}

	/// <summary>
	/// Visualize AD geometries
	/// </summary>
	class VisualizeAD : Visualize
	{
		public VisualizeAD(MainWindow win)
			: base(win) { }

		public override void Execute(object parameter)
		{
			if (Hoops.ADKey == null)
			{
				Hoops.CreateADModel(Fast.oneTurbine);
				Hoops.ADKey.GetVisibilityControl().SetFaces(true);
			}
			else
			{
				bool visible;
				Hoops.ADKey.GetVisibilityControl().ShowFaces(out visible);
				if (visible)
				{
					Hoops.ADKey.GetVisibilityControl().SetFaces(false);
				}
				else
				{
					Hoops.ADKey.GetVisibilityControl().SetFaces(true);
				}

			}
			_win.GetCanvas().Update();
		}
	}

	/// <summary>
	/// Visualize one blade
	/// </summary>
	class VisualizeBlade : Visualize
	{
		public VisualizeBlade(MainWindow win)
			: base(win) { }

		public override void Execute(object parameter)
		{
			if (Hoops.ADBladeKey == null)
			{

			}
			else
			{
				Hoops.HideEverything();
				Hoops.ADBladeKey.GetVisibilityControl().SetFaces(true);
			}
			_win.GetCanvas().Update();
		}
	}

	/// <summary>
	/// Visualize ED geometries
	/// </summary>
	class VisualizeED : Visualize
	{
		public VisualizeED(MainWindow win)
			: base(win) { }

		public override void Execute(object parameter)
		{
			if (Hoops.EDKey == null)
			{
				Hoops.CreateEDModel(Fast.oneTurbine);
				Hoops.EDKey.GetVisibilityControl().SetFaces(true);
			}
			else
			{
				bool visible;
				Hoops.EDKey.GetVisibilityControl().ShowFaces(out visible);
				if (visible)
				{
					Hoops.EDKey.GetVisibilityControl().SetFaces(false);
				}
				else
				{
					Hoops.EDKey.GetVisibilityControl().SetFaces(true);
				}
			}
			_win.GetCanvas().Update();
		}
	}

	/// <summary>
	/// Visualize SD geometries
	/// </summary>
	class VisualizeSD : Visualize
	{
		public VisualizeSD(MainWindow win)
			: base(win) { }

		public override void Execute(object parameter)
		{
			if (Hoops.SDKey == null)
			{
				Hoops.CreateSDModel(Fast.oneTurbine);
				Hoops.SDKey.GetVisibilityControl().SetFaces(true);
			}
			else
			{
				bool visible;
				Hoops.SDKey.GetVisibilityControl().ShowFaces(out visible);
				if (visible)
				{
					Hoops.SDKey.GetVisibilityControl().SetFaces(false);
				}
				else
				{
					Hoops.SDKey.GetVisibilityControl().SetFaces(true);
				}
			}
			_win.GetCanvas().Update();
		}
	}

	/// <summary>
	/// Visualize MD geometries
	/// </summary>
	class VisualizeMD : Visualize
	{
		public VisualizeMD(MainWindow win)
			: base(win) { }

		public override void Execute(object parameter)
		{
			if (Hoops.MDKey == null)
			{
				Hoops.CreateMDModel(Fast.oneTurbine);
				Hoops.MDKey.GetVisibilityControl().SetLines(true);
			}
			else
			{
				bool visible;
				Hoops.MDKey.GetVisibilityControl().ShowLines(out visible);
				if (visible)
				{
					Hoops.MDKey.GetVisibilityControl().SetLines(false);
				}
				else
				{
					Hoops.MDKey.GetVisibilityControl().SetLines(true);
				}
			}
			_win.GetCanvas().Update();
		}
	}

	/// <summary>
	/// Animation
	/// </summary>
	class VisualizeAnimate : Visualize
	{
		public VisualizeAnimate(MainWindow win)
			: base(win) { }

		public override void Execute(object parameter)
		{
			if (Hoops.Model != null)
			{
				// creating KeyPath for constant transtion segment
				HPS.KeyPath blade1 = new HPS.KeyPath();
				blade1.Append(Hoops.ADBladeKey);
				blade1.Append(Hoops.Model.GetSegmentKey());
				blade1.Append(_win.GetCanvas().GetAttachedLayout().GetAttachedView().GetSegmentKey());
				blade1.Append(_win.GetCanvas().GetWindowKey());

				HPS.KeyPath blade2 = new HPS.KeyPath();
				blade2.Append(Hoops.ADBladeKey2);
				blade2.Append(Hoops.Model.GetSegmentKey());
				blade2.Append(_win.GetCanvas().GetAttachedLayout().GetAttachedView().GetSegmentKey());
				blade2.Append(_win.GetCanvas().GetWindowKey());

				HPS.KeyPath blade3 = new HPS.KeyPath();
				blade3.Append(Hoops.ADBladeKey3);
				blade3.Append(Hoops.Model.GetSegmentKey());
				blade3.Append(_win.GetCanvas().GetAttachedLayout().GetAttachedView().GetSegmentKey());
				blade3.Append(_win.GetCanvas().GetWindowKey());

				HPS.Vector vec = new HPS.Vector(1.0f, 0.0f, 0.0f);

				// specify the rotations we'll be using for this animation
				HPS.Quaternion original_rotation = HPS.Quaternion.ComputeRotation(180.0f, vec);
				HPS.Quaternion rotated_90 = HPS.Quaternion.ComputeRotation(270.0f, vec);
				HPS.Quaternion rotated_180 = HPS.Quaternion.ComputeRotation(360.0f, vec);
				HPS.Quaternion rotated_270 = HPS.Quaternion.ComputeRotation(90.0f, vec);
				HPS.Quaternion rotated_360 = HPS.Quaternion.ComputeRotation(180.0f, vec);
				HPS.QuaternionKeyframe[] rotationKeyframesArray = new HPS.QuaternionKeyframe[5];
				rotationKeyframesArray[0] = new HPS.QuaternionKeyframe(0, original_rotation);
				rotationKeyframesArray[1] = new HPS.QuaternionKeyframe(1, rotated_90);
				rotationKeyframesArray[2] = new HPS.QuaternionKeyframe(2, rotated_180);
				rotationKeyframesArray[3] = new HPS.QuaternionKeyframe(3, rotated_270);
				rotationKeyframesArray[4] = new HPS.QuaternionKeyframe(4, rotated_360);

				HPS.Quaternion original_rotation2 = HPS.Quaternion.ComputeRotation(60.0f, vec);
				HPS.Quaternion rotated2_90 = HPS.Quaternion.ComputeRotation(150.0f, vec);
				HPS.Quaternion rotated2_180 = HPS.Quaternion.ComputeRotation(240.0f, vec);
				HPS.Quaternion rotated2_270 = HPS.Quaternion.ComputeRotation(330.0f, vec);
				HPS.Quaternion rotated2_360 = HPS.Quaternion.ComputeRotation(60.0f, vec); ;
				HPS.QuaternionKeyframe[] rotationKeyframesArray2 = new HPS.QuaternionKeyframe[5];
				rotationKeyframesArray2[0] = new HPS.QuaternionKeyframe(0, original_rotation2);
                rotationKeyframesArray2[1] = new HPS.QuaternionKeyframe(1, rotated2_90);
                rotationKeyframesArray2[2] = new HPS.QuaternionKeyframe(2, rotated2_180);
                rotationKeyframesArray2[3] = new HPS.QuaternionKeyframe(3, rotated2_270);
                rotationKeyframesArray2[4] = new HPS.QuaternionKeyframe(4, rotated2_360);
		
				HPS.Quaternion original_rotation3 = HPS.Quaternion.ComputeRotation(-60.0f, vec);
				HPS.Quaternion rotated3_90 = HPS.Quaternion.ComputeRotation(30.0f, vec);
				HPS.Quaternion rotated3_180 = HPS.Quaternion.ComputeRotation(120.0f, vec);
				HPS.Quaternion rotated3_270 = HPS.Quaternion.ComputeRotation(210.0f, vec);
				HPS.Quaternion rotated3_360 = HPS.Quaternion.ComputeRotation(300.0f, vec);
				HPS.QuaternionKeyframe[] rotationKeyframesArray3 = new HPS.QuaternionKeyframe[5];
				rotationKeyframesArray3[0] = new HPS.QuaternionKeyframe(0, original_rotation3);
                rotationKeyframesArray3[1] = new HPS.QuaternionKeyframe(1, rotated3_90);
                rotationKeyframesArray3[2] = new HPS.QuaternionKeyframe(2, rotated3_180);
                rotationKeyframesArray3[3] = new HPS.QuaternionKeyframe(3, rotated3_270);
                rotationKeyframesArray3[4] = new HPS.QuaternionKeyframe(4, rotated3_360);


                // we set the animation type (in this case, it is a linear animation) in 
                // the QuaternionSampler and add the keyframes from the previous step
                HPS.QuaternionSampler rotation_sampler = new HPS.QuaternionSampler();
				rotation_sampler.SetKeyframes(rotationKeyframesArray);
				rotation_sampler.SetInterpolation(HPS.Sampler.InterpolationType.SphericalLinear);

				HPS.QuaternionSampler rotation_sampler2 = new HPS.QuaternionSampler();
				rotation_sampler2.SetKeyframes(rotationKeyframesArray2);
				rotation_sampler2.SetInterpolation(HPS.Sampler.InterpolationType.SphericalLinear);

				HPS.QuaternionSampler rotation_sampler3 = new HPS.QuaternionSampler();
				rotation_sampler3.SetKeyframes(rotationKeyframesArray3);
				rotation_sampler3.SetInterpolation(HPS.Sampler.InterpolationType.SphericalLinear);


				// using the SprocketPath and a channel, we complete the Animation object
				HPS.SprocketPath sprocketPath = new HPS.SprocketPath(_win.GetCanvas());
				HPS.Animation animation = Hoops.Model.CreateAnimation("rotation");
				animation.AddRotationChannel("blade1", blade1, rotation_sampler);
				animation.AddRotationChannel("blade2", blade2, rotation_sampler2);
				animation.AddRotationChannel("blade3", blade3, rotation_sampler3);


				// access the AnimationControl for the View we want to conduct playback on and supply the Animation from the previous step
				HPS.AnimationControl animationControl = _win.GetCanvas().GetAttachedLayout().GetAttachedView().GetAnimationControl();
				animationControl.SetAnimation(animation);
				animationControl.SetMillisecondsPerTick(5000);
				animationControl.Play();

			}

			_win.GetCanvas().Update();
		}
	}


	/// <summary>
	/// Operator Orbit
	/// </summary>
	class OperatorOrbit : Visualize
	{
		public OperatorOrbit(MainWindow win) : base(win) { }

		public override void Execute(object parameter)
		{
			HPS.View view = _win.GetSprocketsControl().Canvas.GetFrontView();
			view.GetOperatorControl().Pop();
			view.GetOperatorControl().Push(new HPS.OrbitOperator(HPS.MouseButtons.ButtonLeft()));
			_win.GetSprocketsControl().Focus();
		}
	}

	/// <summary>
	/// Operator Pan
	/// </summary>
	class OperatorPan : Visualize
	{
		public OperatorPan(MainWindow win) : base(win) { }

		public override void Execute(object parameter)
		{
			HPS.View view = _win.GetSprocketsControl().Canvas.GetFrontView();
			view.GetOperatorControl().Pop();
			view.GetOperatorControl().Push(new HPS.PanOperator(HPS.MouseButtons.ButtonLeft()));
			_win.GetSprocketsControl().Focus();
		}
	}

	/// <summary>
	/// Operator Zoom
	/// </summary>
	class OperatorZoom : Visualize
	{
		public OperatorZoom(MainWindow win) : base(win) { }

		public override void Execute(object parameter)
		{
			HPS.View view = _win.GetSprocketsControl().Canvas.GetFrontView();
			view.GetOperatorControl().Pop();
			view.GetOperatorControl().Push(new HPS.ZoomOperator(HPS.MouseButtons.ButtonLeft()));
			_win.GetSprocketsControl().Focus();
		}
	}

	/// <summary>
	/// Operator Zoom Box
	/// </summary>
	class OperatorZoomBox : Visualize
	{
		public OperatorZoomBox(MainWindow win) : base(win) { }

		public override void Execute(object parameter)
		{
			HPS.View view = _win.GetSprocketsControl().Canvas.GetFrontView();
			view.GetOperatorControl().Pop();
			view.GetOperatorControl().Push(new HPS.ZoomBoxOperator(HPS.MouseButtons.ButtonLeft()));
			_win.GetSprocketsControl().Focus();
		}
	}

	/// <summary>
	/// Operator Select
	/// </summary>
	class OperatorSelect : Visualize
	{
		public OperatorSelect(MainWindow win) : base(win) { }

		public override void Execute(object parameter)
		{
			HPS.View view = _win.GetSprocketsControl().Canvas.GetFrontView();
			view.GetOperatorControl().Pop();
			view.GetOperatorControl().Push(new SandboxHighlightOperator(_win));
			_win.GetSprocketsControl().Focus();			
		}
	}

	/// <summary>
	/// Visual effect: simple shadow
	/// </summary>
	class EffectSimpleShadow : Visualize
	{
		private bool _enableShadows = false;

		const float opacity = 0.3f;
		const uint resolution = 512;
		const uint blurring = 20;

		public EffectSimpleShadow(MainWindow win) : base(win) { }

		public override void Execute(object parameter)
		{
			_enableShadows = !_enableShadows;

			// Get the Sprockets control from the _mainBorder
			SprocketsWPFControl ctrl = _win.GetSprocketsControl();

			// Get the Sprockets.View segment to set the shadow settings on
			HPS.SegmentKey viewSeg = ctrl.Canvas.GetFrontView().GetSegmentKey();

			// Only recompute plane when enabling shadows
			if (_enableShadows)
			{
				ctrl.Canvas.GetFrontView().SetSimpleShadow(true);

				// Set opacity in simple shadow color
				HPS.RGBAColor color = new HPS.RGBAColor(0, 0, 0, opacity);
				if (viewSeg.GetVisualEffectsControl().ShowSimpleShadowColor(out color))
					color.alpha = opacity;

				// Enable/disable shadow and pass in shadow settings
				viewSeg.GetVisualEffectsControl().SetSimpleShadowColor(color);
			}
			else
				ctrl.Canvas.GetFrontView().SetSimpleShadow(false);

			// Trigger update
			ctrl.Canvas.Update();
		}
	}

	/// <summary>
	/// Visual effect: hidden line
	/// </summary>
	class EffectHiddenLine : Visualize
	{
		public EffectHiddenLine(MainWindow win) : base(win) { }

		public override void Execute(object parameter)
		{
			SprocketsWPFControl ctrl = _win.GetSprocketsControl();
			ctrl.Canvas.GetFrontView().SetRenderingMode(HPS.Rendering.Mode.FastHiddenLine);

			if (_win._enableFrameRate)
			{
				//FrameRate and HiddenLine are incompatible. Turn off FrameRate when selecting HiddenLine
				//_win.GetSprocketsControl().Canvas.SetFrameRate(0);
				//_win._enableFrameRate = false;
				//_win.FrameRateButton.IsChecked = false;
			}

			ctrl.Canvas.GetFrontView().Update();
		}
	}

	/// <summary>
	/// Visual effect: eye dome lighting
	/// </summary>
	class EffectEyeDome : Visualize
	{
		private bool _eyeDomeMode = false;

		public EffectEyeDome(MainWindow win) : base(win) { }

		public override void Execute(object parameter)
		{
			_eyeDomeMode = !_eyeDomeMode;

			SprocketsWPFControl ctrl = _win.GetSprocketsControl();
            HPS.WindowKey window = ctrl.Canvas.GetWindowKey();
            window.GetPostProcessEffectsControl().SetEyeDomeLighting(_eyeDomeMode);

            HPS.VisualEffectsControl visualEffectsControl = window.GetVisualEffectsControl();
            visualEffectsControl.SetEyeDomeLightingEnabled(_eyeDomeMode);

            ctrl.Canvas.Update();
		}
	}

	/// <summary>
	/// Visual effect: eye dome lighting
	/// </summary>
	class EffectReflection : Visualize
	{
		private bool _eyeDomeMode = false;

		public EffectReflection(MainWindow win) : base(win) { }

		public override void Execute(object parameter)
		{
			SprocketsWPFControl ctrl = _win.GetSprocketsControl();
			HPS.SegmentKey myModelKey = ctrl.Canvas.GetFrontView().GetSegmentKey();
			myModelKey.GetVisualEffectsControl().SetSimpleReflection(true, 0.5f, 1U, false, 0, 2.0f);

			// parameters for equation of a plane
			myModelKey.GetVisualEffectsControl().SetSimpleReflectionPlane(new HPS.Plane(0, 0, -1, 0.375f));

			myModelKey.GetVisibilityControl().SetShadows(true);
			myModelKey.GetVisualEffectsControl().SetShadowMaps(true, 16, 2048, true, true);

			ctrl.Canvas.Update();
		}
	}


}
