using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace imgodr
{
	internal sealed class Program
	{
		private Program()
		{

		}

		public static void Main(string[] args)
		{
			try
			{
				if(args.Length == 0)
				{
					Usage();
					return;
				}

				Application.Find(args);
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
