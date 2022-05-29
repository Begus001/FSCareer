using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;

namespace FSCareer.Commands
{
	public class CmdHyperlink : BaseCommand
	{
		public override void Execute(object parameter)
		{
			System.Diagnostics.Process.Start(parameter as string);
		}
	}

	public class CmdExit : BaseCommand
	{
		public override void Execute(object parameter)
		{
			if (parameter == null)
			{ 
				Application.Current.Shutdown(0);
				return;
			}
			else
			{ 
				Application.Current.Shutdown(Convert.ToInt32(parameter));
				return;
			}
		}
	}
}
