using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using FSCareer.Models;
using FSCareer.Config;
using System.Windows;
using System.Windows.Data;

namespace FSCareer.Main
{
	public class MainViewModel : BaseViewModel
	{
		public Window WindowInstance { get; private set; }
		public string WindowTitle { get; set; } = "";

		private Career _career;

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

		public List<Aircraft> OwnedAircraftList { get; }

		private Aircraft _selectedAircraft = new Aircraft { Name = "" };
		public Aircraft SelectedAircraft
		{
			get => _selectedAircraft;
			set { _selectedAircraft = value; OnPropertyChanged(nameof(SelectedAircraft)); }
		}

		public MainViewModel(Window win, Career c)
		{
			WindowInstance = win;
			_career = c;

			using (StreamWriter f = new StreamWriter(Settings.LastCareer, false))
			{
				f.Write(_career.AirlineName);
			}

			WindowTitle = $"FSCareer - {_career.AirlineName}";
			List<Aircraft> list = _career.LoadOwnedAircraft();
			//List<Aircraft> list = Aircraft.LoadAll();
			if (list != null)
				OwnedAircraftList = list;

			CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(list);
			view.SortDescriptions.Add(new SortDescription("Price", ListSortDirection.Ascending));
		}
	}
}
