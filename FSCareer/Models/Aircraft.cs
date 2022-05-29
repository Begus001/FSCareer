using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using FSCareer.Config;
using System.Diagnostics;

namespace FSCareer.Models
{
	public class Aircraft
	{
		private string[] _properties = Enumerable.Repeat(string.Empty, (int)AircraftProperty.AircraftPropertiesLength).ToArray();

		public string Name
		{
			get => _properties[(int)AircraftProperty.Name];
			set => _properties[(int)AircraftProperty.Name] = value;
		}

		public string Class
		{
			get => _properties[(int)AircraftProperty.Class];
			set => _properties[(int)AircraftProperty.Class] = value;
		}

		public int Price
		{
			get => int.Parse(_properties[(int)AircraftProperty.Price]);
			set => _properties[(int)AircraftProperty.Price] = value.ToString();
		}

		public int PayloadCapacity
		{
			get => int.Parse(_properties[(int)AircraftProperty.PayloadCapacity]);
			set => _properties[(int)AircraftProperty.PayloadCapacity] = value.ToString();
		}

		public int FuelCapacity
		{
			get => int.Parse(_properties[(int)AircraftProperty.FuelCapacity]);
			set => _properties[(int)AircraftProperty.FuelCapacity] = value.ToString();
		}

		public static string[] AircraftClass { get; } =
		{
		"Very Light",
		"Light",
		"Medium",
		"Heavy",
		"Very Heavy",
		};

		public static Aircraft Load(string path)
		{
			Aircraft aircraft = new Aircraft();
			using (var f = new StreamReader(path))
			{
				for (int i = 0; i < (int)AircraftProperty.AircraftPropertiesLength; i++)
				{
					string line = f.ReadLine();
					string name = line.Split('=')[0];
					string val = line.Split('=')[1].Trim().Trim('"');

					AircraftProperty prop;
					Enum.TryParse(name, out prop);
					aircraft._properties[(int)prop] = val;
				}
			}

			return aircraft;
		}

		public static List<Aircraft> LoadList(string path)
		{
			if (!Directory.Exists(Settings.AircraftDir))
			{
				throw new DirectoryNotFoundException();
			}

			if (!File.Exists(path))
			{
				throw new FileNotFoundException();
			}

			List<Aircraft> list = new List<Aircraft>();

			string[] ownedAircraft;

			using (var f = new StreamReader(path))
			{
				ownedAircraft = f.ReadToEnd().Split(new string[] { "\r\n" }, StringSplitOptions.None);
			}

			if (string.IsNullOrEmpty(ownedAircraft[0]))
				return null;

			for (int i = 0; i < ownedAircraft.Length; i++)
			{
				list.Add(Load(Path.Combine(Settings.AircraftDir, $"{ownedAircraft[i]}.txt")));
			}

			return list;
		}

		public static List<Aircraft> LoadAll()
		{
			if (!Directory.Exists(Settings.AircraftDir))
			{
				throw new DirectoryNotFoundException();
			}

			List<Aircraft> list = new List<Aircraft>();

			string[] aircraftFiles = Directory.GetFiles(Settings.AircraftDir, "*.txt");
			for (int i = 0; i < aircraftFiles.Length; i++)
			{
				list.Add(Load(aircraftFiles[i]));
			}

			return list;
		}
	}

	public enum AircraftProperty
	{
		Name,
		Class,
		Price,
		PayloadCapacity,
		FuelCapacity,
		AircraftPropertiesLength,
	}
}
