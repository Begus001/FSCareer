using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FSCareer.Commands
{
	public class RelayCommand<T> : BaseCommand
	{
		private readonly Action<T> _execute = null;

		public RelayCommand(Action<T> execute)
		{
			_execute = execute;
		}

		public override void Execute(object parameter)
		{
			_execute((T)parameter);
		}
	}
}
