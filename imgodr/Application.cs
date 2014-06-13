using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace imgodr
{
	public sealed class Application
	{
		private Application()
		{

		}

		public static void Find(string path)
		{
			if (System.IO.Directory.Exists(path))
			{
				// 直下のファイルを先に処理します。
				foreach (string e in System.IO.Directory.EnumerateFiles(path))
				{
					Find(e);
				}

				// ディレクトリを掘り下げます。
				foreach (string e in System.IO.Directory.EnumerateDirectories(path))
				{
					Find(e);
				}
			}
			else if (System.IO.File.Exists(path))
			{
				// ファイルを分析し、可能なら名前変更します。
				Process2(path);
			}
			else
			{
				System.Console.WriteLine("パスが存在しません。[" + path + "]");
			}
		}

		private static string MakePath(System.IO.FileInfo info, DateTime date_taken, int i)
		{
			string date_part_format = "yyyy年MM月dd日 HH時mm分ss秒";
			if (i == 0)
			{
				return info.DirectoryName + "\\" + date_taken.ToString(date_part_format) + info.Extension;
			}
			else
			{
				return info.DirectoryName + "\\" + date_taken.ToString(date_part_format) + " (" + i + ")" + info.Extension;
			}
		}

		private static void Process2(string path)
		{
			// (2) ID を用いて EXIF 属性を読み出す実装
			System.Collections.IDictionary meta = ExifReader2.ReadExifInfo(path);
			if (meta == null)
			{
				return;
			}

			DateTime? date_taken = Utils.ParseDate(meta["" + 0x9003]);
			if (date_taken == null)
			{
				return;
			}

			Console.WriteLine("{0} (撮影日時: {1})", path, Utils.ToString(date_taken));

			System.IO.FileInfo info = new System.IO.FileInfo(path);

			for (int i = 0; i < 10000; i++)
			{
				string new_path = MakePath(info, date_taken.Value, i);
				
				if (path == new_path)
				{
					// 変更の必要なし
					break;
				}
	
				if (System.IO.File.Exists(new_path))
				{
					continue;
				}

				System.IO.File.Move(path, new_path);
				break;
			}
		}
	}
}