using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace imgodr
{
	internal sealed class ExifReader
	{
		private ExifReader()
		{

		}

		public static System.Collections.IDictionary ReadExifInfo(string path)
		{
			System.Drawing.Bitmap b = null;


			
			try
			{
				b = new System.Drawing.Bitmap(path);
				System.Collections.IDictionary result = new System.Collections.Hashtable();
				foreach (System.Drawing.Imaging.PropertyItem p in b.PropertyItems)
				{
					object value = ValueOf(p);
					result["" + p.Id] = value;
				}
				return result;
			}
			catch
			{
				return null;
			}
			finally
			{
				if(b != null)
				{
					b.Dispose();
				}
			}
		}

		private static object ValueOf(System.Drawing.Imaging.PropertyItem item)
		{
			try
			{
				if (item == null)
				{
					return null;
				}

				// =========================================================================
				// 撮影日時
				// =========================================================================
				if (item.Id == 0x9003)
				{
					string value = Encoding.ASCII.GetString(item.Value, 0, 19);

					try
					{
						return DateTime.ParseExact(value, "yyyy:MM:dd HH:mm:ss", null);
					}
					catch
					{
					}

					try
					{
						return DateTime.ParseExact(value, "yyyy:MM:dd H:mm:ss", null);
					}
					catch
					{
					}

					Console.WriteLine("[ERROR] DateTaken タグを解析できませんでした。この書式には対応できません。[" + value + "]");
					return null;
				}

				return item.Value;
			}
			catch(Exception e)
			{
				Console.WriteLine("[ERROR] {0}", e);
				return null;
			}
		}
	}
}
