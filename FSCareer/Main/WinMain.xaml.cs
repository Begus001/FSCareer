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
using System.Windows.Shapes;
using FSCareer.Models;

namespace FSCareer.Main
{
	public partial class WinMain : Window
	{
		public WinMain()
		{
			InitializeComponent();
		}
		public WinMain(Career c)
		{
			InitializeComponent();
			DataContext = new MainViewModel(this, c);
		}
	}
}
