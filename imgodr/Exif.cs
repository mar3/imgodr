using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace imgodr
{
	internal sealed class Exif : System.Collections.Generic.Dictionary<int, object>
	{
		public Exif()
		{

		}

		public new object this[int key]
		{
			set
			{
				base[key]  = value;
			}
			get
			{
				return base.ContainsKey(key) ? base[key] : null;
			}
		}

		public static Exif Read(string path)
		{
			System.Drawing.Image b = null;



			try
			{
				b = new System.Drawing.Bitmap(path);
				Exif result = new Exif();
				foreach (System.Drawing.Imaging.PropertyItem p in b.PropertyItems)
				{
					object value = ValueOf(p);
					result[p.Id] = value;
				}
				return result;
			}
			catch
			{
				return null;
			}
			finally
			{
				if (b != null)
					b.Dispose();
			}
		}

		private static object ValueOf(System.Drawing.Imaging.PropertyItem item)
		{
			if (item == null)
			{
				return null;
			}

			// =========================================================================
			// DateTimeOriginal(撮影日時)
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

			//if (item.id == 0x9004)
			//{
			//	;
			//}

			return item.Value;
		}
	}
}
