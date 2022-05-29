using FSCareer.Commands;
using FSCareer.Main;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using FSCareer.Models;

namespace FSCareer.NewCareer
{
	public class NewCareerViewModel : BaseViewModel
	{
		public Window WindowInstance { get; set; }

		private Career _career = new Career();

		public string AirlineName
		{
			get => _career.AirlineName;
			set => _career.AirlineName = value;
		}

		public string HomeAirport
		{
			get => _career.HomeAirport;
			set => _career.HomeAirport = value;
		}

		public string Difficulty
		{
			get => _career.Difficulty;
			set => _career.Difficulty = value;
		}

		private string _error;
		public string Error
		{
			get => _error;
			set { _error = value; OnPropertyChanged(nameof(Error)); }
		}

		public RelayCommand<object> CmdCreate => new RelayCommand<object>(ExecCmdCreate);

		public RelayCommand<string> CmdSelectDifficulty => new RelayCommand<string>(ExecCmdSelectDifficulty);

		private void ExecCmdCreate(object _)
		{
			if (AirlineName == "")
			{
				Error = "Enter a name for your airline";
				return;
			}

			if (HomeAirport == "")
			{
				Error = "Enter the home airport of your airline";
				return;
			}

			if (Difficulty == "")
			{
				Error = "Set a difficulty for your career";
				return;
			}

			if (!_career.Save())
			{
				Error = "Could not save career";
				return;
			}

			new WinMain(_career).Show();

			WindowInstance.Close();
		}

		private void ExecCmdSelectDifficulty(string difficulty)
		{
			Difficulty = difficulty;
		}
	}
}
