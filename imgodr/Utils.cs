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
				return ToString((System.Collections.IDictionary)unknown);
			if (unknown is DateTime)
				return ToString((DateTime)unknown);
			return unknown.ToString();
		}

		public static string ToString(DateTime date)
		{
			return date.ToString("yyyy-MM-dd HH:mm:ss.fff");
		}

		public static string ToString(System.Collections.IDictionary dic)
		{
			System.Text.StringBuilder buffer = new System.Text.StringBuilder();
			foreach (System.Collections.DictionaryEntry e in dic)
			{
				if (buffer.Length != 0)
					buffer.Append(", ");
				buffer.Append(e.Key);
				buffer.Append("=");
				buffer.Append(ToString(e.Value));
			}
			return buffer.ToString();
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
