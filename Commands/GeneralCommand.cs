using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HoopsFast
{
    abstract class GeneralCommand : ICommand
	{
		/// <summary>
		/// MainWindow instance
		/// </summary>
		protected MainWindow _win;

		/// <summary>
		/// Constructs DemoCommand
		/// </summary>
		/// <param name="win">MainWindow instance</param>
		public GeneralCommand(MainWindow win)
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

	/// <summary>
	/// Exit Command handler
	/// </summary>
	class ExitCommand : GeneralCommand
	{
		public ExitCommand(MainWindow win) : base(win) { }

		public override void Execute(object parameter)
		{
			_win.Close();
		}
	}
}
