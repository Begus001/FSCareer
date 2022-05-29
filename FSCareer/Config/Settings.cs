using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSCareer.Config
{
	public static class Settings
	{
		public static string SaveDir => @"C:\Users\" + Environment.UserName + @"\AppData\Roaming\FSCareer";
		public static string CareerSaveDir => Path.Combine(SaveDir, "Careers");
		public static string LastCareer => Path.Combine(SaveDir, "last-career.txt");
		public static string AircraftDir => Path.Combine(SaveDir, "Aircraft");
	}
}
