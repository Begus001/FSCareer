using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FSCareer.Start
{
	public class ViewModel
	{
		public ICommand Hyperlink { get; } = new Commands.CmdHyperlink();
	}
}
