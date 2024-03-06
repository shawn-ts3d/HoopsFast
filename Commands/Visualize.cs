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


}
