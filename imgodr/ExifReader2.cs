using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace imgodr
{
	sealed class ExifReader2
	{
		private ExifReader2()
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
					object value = _ValueOf(p);
					result["" + p.Id] = value;
					// Console.WriteLine("ID=[{0}], TYPE=[{1}], VALUE=[{2}], LEN=[{3}]", i.Id, i.Type, value, i.Len);
				}
				return result;
			}
			catch
			{
				// Console.WriteLine("[ERROR] {0}", e.Message);
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

		/// <summary>
		/// 写真から EXIF 情報を取り出します。
		/// 
		/// ID や TYPE を熟知したうえでの実装になるため、注意深く実装する必要があります。
		/// </summary>
		/// <param name="n"></param>
		/// <returns></returns>
		private static object _ValueOf(System.Drawing.Imaging.PropertyItem item)
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

				if (item.Type == 1)
				{
					//Byte 8bit符号なし整数(1バイト整数型) ※研究中。エンディアンとかどうなってんの...
					long value = 0;
					foreach (byte b in item.Value)
					{
						value = value << 8;
						value = value + b;
					}
					return value;
					//return System.BitConverter.ToUInt32(item.Value, 0);
				}
				else if (item.Type == 2)
				{
					//ASCII	7bitの文字列
					return Encoding.ASCII.GetString(item.Value);
				}
				else if (item.Type == 3)
				{
					//Short	16bit符号なし整数(2バイト短整数型)
					return System.BitConverter.ToUInt16(item.Value, 0);
				}
				else if (item.Type == 4)
				{
					//Long	32bit符号なし整数（8バイト長整数型）
					return System.BitConverter.ToUInt32(item.Value, 0); // エラーが出なくなったので多分正しいっぽい...
				}
				else if (item.Type == 5)
				{
					//Rational	符号無し有理数 ↓みつけたサンプルに倣っているがおかしい...
					return BitConverter.ToInt16(item.Value, 0);
					// return System.BitConverter.ToDouble(item.Value, 0);
				}
				else if (item.Type == 7)
				{
					//Undefined	8bit整数 ↓独自
					return (int)item.Value[0];
					// return System.BitConverter.ToInt16(item.Value, 0);
				}
				else if (item.Type == 10)
				{
					//SRational	符号付き有理数
					return System.BitConverter.ToInt16(item.Value, 0) / 2.0;
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
