using System.Windows;
using FSCareer.NewCareer;

namespace FSCareer.Start
{
	public partial class WinStart : Window
	{
		public WinStart()
		{
			InitializeComponent();
			DataContext = new StartViewModel(this);
		}
	}
}
