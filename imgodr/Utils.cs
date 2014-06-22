using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace imgodr
{
	internal sealed class Utils
	{
		private Utils()
		{

		}

		public static System.Windows.Media.ImageMetadata GetMetaData(System.Windows.Media.Imaging.BitmapFrame frame)
		{
			return frame == null ? null : frame.Metadata;
		}

		public static string ToString(object unknown)
		{
			if (unknown == null)
				return "";

			if (unknown is System.Collections.IDictionary)
			{
				System.Text.StringBuilder buffer = new System.Text.StringBuilder();
				System.Collections.IDictionary dic = (System.Collections.IDictionary)unknown;
				foreach (string key in dic.Keys)
				{
					if (buffer.Length != 0)
					{
						buffer.Append(", ");
					}
					buffer.Append(key);
					buffer.Append("=");
					buffer.Append(dic[key]);
				}
				return buffer.ToString();
			}

			if (unknown is System.DateTime)
			{
				System.DateTime d = (System.DateTime)unknown;
				return d.ToString("yyyy-MM-dd HH:mm:ss.fff");
			}
			
			return "" + unknown;
		}

		public static DateTime? ParseDate(object unknown)
		{
			if (unknown == null)
				return null;
	
			if (unknown is DateTime)
				return (DateTime)unknown;
			
			try
			{
				return DateTime.ParseExact(unknown.ToString(), "yyyy-MM-dd HH:mm:ss.fff", null);
			}
			catch
			{
			}

			try
			{
				return DateTime.ParseExact(unknown.ToString(), "yyyy/MM/dd HH:mm:ss.fff", null);
			}
			catch
			{
			}
			
			try
			{
				return DateTime.ParseExact(unknown.ToString(), "yyyy-MM-dd HH:mm:ss", null);
			}
			catch
			{
			}

			try
			{
				return DateTime.ParseExact(unknown.ToString(), "yyyy-MM-dd H:mm:ss", null);
			}
			catch
			{
			}

			try
			{
				return DateTime.ParseExact(unknown.ToString(), "yyyy/MM/dd HH:mm:ss", null);
			}
			catch
			{
			}

			try
			{
				return DateTime.ParseExact(unknown.ToString(), "yyyy/MM/dd H:mm:ss", null);
			}
			catch
			{
			}

			try
			{
				return DateTime.ParseExact(unknown.ToString(), "yyyy-MM-dd HH:mm", null);
			}
			catch
			{
			}

			try
			{
				return DateTime.ParseExact(unknown.ToString(), "yyyy/MM/dd HH:mm", null);
			}
			catch
			{
			}

			try
			{
				return DateTime.ParseExact(unknown.ToString(), "yyyy-MM-dd", null);
			}
			catch
			{
			}

			try
			{
				return DateTime.ParseExact(unknown.ToString(), "yyyy/MM/dd", null);
			}
			catch
			{
			}

			return null;
		}
	}
}
