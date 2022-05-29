using FSCareer.Config;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;

namespace FSCareer.Models
{
	public class Career
	{
		private string[] _properties { get; } = Enumerable.Repeat(string.Empty, (int)CareerProperty.CareerPropertiesLength).ToArray();
		public string AirlineName
		{
			get => _properties[(int)CareerProperty.AirlineName];
			set => _properties[(int)CareerProperty.AirlineName] = value;
		}

		public string HomeAirport
		{
			get => _properties[(int)CareerProperty.HomeAirport];

			set => _properties[(int)CareerProperty.HomeAirport] = value;
		}

		public string Difficulty
		{
			get => _properties[(int)CareerProperty.Difficulty];
			set => _properties[(int)CareerProperty.Difficulty] = value;
		}

		public List<Aircraft> LoadOwnedAircraft()
		{
			string path = Path.Combine(Settings.CareerSaveDir, AirlineName, "owned-aircraft.txt");
			List<Aircraft> list = Aircraft.LoadList(path);
			return list;
		}

		public bool Save()
		{
			string path = Path.Combine(Settings.CareerSaveDir, $"{AirlineName}.txt");

			if (File.Exists(path))
				return false;

			if (!Directory.Exists(Settings.CareerSaveDir))
				Directory.CreateDirectory(Settings.CareerSaveDir);

			try
			{
				using (StreamWriter f = new StreamWriter(path, false))
				{
					for (int i = 0; i < (int)CareerProperty.CareerPropertiesLength; i++)
					{
						f.WriteLine($"{(CareerProperty)i}=\"{_properties[i]}\"");
					}
				}

				return true;
			}
			catch
			{
				return false;
			}
		}

		public static Career Load(string path)
		{
			Career career = new Career();
			try
			{
				using (StreamReader f = new StreamReader(path))
				{
					for (int i = 0; i < (int)CareerProperty.CareerPropertiesLength; i++)
					{
						string line = f.ReadLine();
						string name = line.Split('=')[0];
						string val = line.Split('=')[1].Trim().Trim('"');
						CareerProperty prop;
						Enum.TryParse(name, out prop);
						career._properties[(int)prop] = val;
					}
				}
				return career;
			}
			catch
			{
				return null;
			}
		}
	}

	public enum CareerProperty
	{
		AirlineName,
		HomeAirport,
		Difficulty,
		CareerPropertiesLength,
	}
}
