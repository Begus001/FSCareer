using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using FSCareer.Commands;
using FSCareer.Main;
using FSCareer.NewCareer;
using FSCareer.Config;
using Microsoft.Win32;
using FSCareer.Models;

namespace FSCareer.Start
{
	public class StartViewModel : BaseViewModel
	{
		public Window WindowInstance { get; private set; }
		public ICommand Hyperlink => new CmdHyperlink();
		public ICommand NewCareer => new RelayCommand<object>(ExecNewCareer);
		public ICommand LoadCareer => new RelayCommand<object>(ExecLoadCareer);
		public ICommand Exit { get; } = new CmdExit();

		public StartViewModel(Window win)
		{
			WindowInstance = win;
			if (!Directory.Exists(Settings.SaveDir))
			{
				Directory.CreateDirectory(Settings.SaveDir);
				return;
			}

			if (File.Exists(Settings.LastCareer))
			{
				string lastPath;
				using (var f = new StreamReader(Settings.LastCareer))
				{
					lastPath = f.ReadToEnd();
				}
				ExecLoadCareer(Path.Combine(Settings.CareerSaveDir, $"{lastPath}", $"{lastPath}.txt"));
			}
		}

		private void ExecNewCareer(object _)
		{
			var win = new WinNewCareer();
			win.Show();
			WindowInstance.Close();
		}

		private void ExecLoadCareer(object path)
		{
			if ((path as string) == null)
			{
				OpenFileDialog dialog = new OpenFileDialog();
				dialog.InitialDirectory = Settings.CareerSaveDir;
				dialog.Filter = "FSCareer Save File|*.txt|All Files|*.*";
				bool? result = dialog.ShowDialog();
				if (result == true)
				{
					var career = Career.Load(dialog.FileName);
					if (career == null)
						return;

					new WinMain(career).Show();
					WindowInstance.Close();
				}
			}
			else
			{
				Career career = Career.Load(path as string);
				if (career == null)
				{
					var result = MessageBox.Show("Could not load last career. Do you want to create a new one?", "Error", MessageBoxButton.YesNo, MessageBoxImage.Error);
					if (result == MessageBoxResult.Yes)
					{
						ExecNewCareer(null);
						return;
					}
					else
					{
						return;
					}
				}

				new WinMain(career).Show();
				WindowInstance.Close();
			}
		}
	}
}
