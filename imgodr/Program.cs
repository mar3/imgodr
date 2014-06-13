using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace imgodr
{
	class Program
	{
		static void Main(string[] args)
		{
			try
			{
				string path = 1 <= args.Length ? args[0] : "";
				if (path == "")
				{
					Usage();
					return;
				}

				Application.Find(path);
			}
			catch (Exception e)
			{
				Console.WriteLine("[ERROR] " + e);
			}
		}

		private static void Usage()
		{
			Console.WriteLine("USAGE:");
			Console.WriteLine("    imgodr.exe {directory(s) or file(s)}");
		}
	}
}
