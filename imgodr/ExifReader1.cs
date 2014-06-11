using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace imgodr
{
	sealed class ExifReader1
	{
		private ExifReader1()
		{

		}

		private static System.Collections.IDictionary ReadEXIFData(System.Windows.Media.Imaging.BitmapDecoder decoder)
		{
			if (decoder == null)
			{
				return null;
			}

			if (decoder.Frames == null)
			{
				return null;
			}

			foreach (System.Windows.Media.Imaging.BitmapFrame f in decoder.Frames)
			{
				System.Collections.IDictionary meta = ReadEXIFData(f);
				if (meta != null)
				{
					// 最初に読み取れたものを返却
					return meta;
				}
			}

			return null;
		}

		private static System.Collections.IDictionary ReadEXIFData(System.Windows.Media.Imaging.BitmapFrame frame)
		{
			return ReadEXIFData(Utils.GetMetaData(frame));
		}

		public static System.Collections.IDictionary DumpExifInfo1(string path)
		{
			System.IO.FileStream stream = null;



			try
			{
				stream = new System.IO.FileStream(
					path, System.IO.FileMode.Open, System.IO.FileAccess.Read, System.IO.FileShare.Read);

				// ================================================================
				// [jpeg]
				// ================================================================
				try
				{
					stream.Seek(0, System.IO.SeekOrigin.Begin);
				
					System.Windows.Media.Imaging.BitmapDecoder decoder = new System.Windows.Media.Imaging.JpegBitmapDecoder(
						stream,
						System.Windows.Media.Imaging.BitmapCreateOptions.None,
						System.Windows.Media.Imaging.BitmapCacheOption.None); // Default だとサイズが変わるという噂あり...

					System.Collections.IDictionary meta = ReadEXIFData(decoder);
					
					return meta;
				}
				catch
				{
					// パース失敗
				}

				// ================================================================
				// [png]
				// ================================================================
				try
				{
					stream.Seek(0, System.IO.SeekOrigin.Begin);
					
					System.Windows.Media.Imaging.BitmapDecoder decoder = new System.Windows.Media.Imaging.PngBitmapDecoder(
						stream,
						System.Windows.Media.Imaging.BitmapCreateOptions.None,
						System.Windows.Media.Imaging.BitmapCacheOption.None); // Default だとサイズが変わるという噂あり...

					System.Collections.IDictionary meta = ReadEXIFData(decoder);
					
					return meta;
				}
				catch
				{
					// パース失敗
				}

				// ================================================================
				// [gif]
				// ================================================================
				try
				{
					stream.Seek(0, System.IO.SeekOrigin.Begin);
					
					System.Windows.Media.Imaging.BitmapDecoder decoder = new System.Windows.Media.Imaging.GifBitmapDecoder(
						stream,
						System.Windows.Media.Imaging.BitmapCreateOptions.None,
						System.Windows.Media.Imaging.BitmapCacheOption.None); // Default だとサイズが変わるという噂あり...

					System.Collections.IDictionary meta = ReadEXIFData(decoder);
					
					return meta;
				}
				catch
				{
					// パース失敗
				}
			}
			catch (Exception e)
			{
				// その他の実行時エラー
				Console.WriteLine("[ERROR] {0}", e);
			}
			finally
			{
				if (stream != null)
				{
					stream.Close();
				}
			}

			return null;
		}

		private static System.Collections.IDictionary ReadEXIFData(System.Windows.Media.ImageMetadata meta)
		{
			if (meta == null)
			{
				return null;
			}
			else if (meta is System.Windows.Media.Imaging.BitmapMetadata)
			{
				// 読み取れる情報が少ない。EXIF を読み出した方がより細かい情報が取得できる。
				System.Windows.Media.Imaging.BitmapMetadata bmm = (System.Windows.Media.Imaging.BitmapMetadata)meta;
				System.Collections.IDictionary keys = new Dictionary<string, object>();
	
				keys["ApplicationNamed"] = bmm.ApplicationName;
				keys["Authord"] = bmm.Author;
				keys["CameraManufacturerd"] = bmm.CameraManufacturer;
				keys["CameraModeld"] = bmm.CameraModel;
				keys["CanFreezed"] = bmm.CanFreeze;
				keys["Comment"] = bmm.Comment;
				keys["Copyrightd"] = bmm.Copyright;
				keys["DateTaken"] = bmm.DateTaken;
				keys["DependencyObjectTyped"] = bmm.DependencyObjectType;
				keys["Dispatcherd"] = bmm.Dispatcher;
				keys["Format"] = bmm.Format;
				keys["IsFixedSized"] = bmm.IsFixedSize;
				keys["IsFrozend"] = bmm.IsFrozen;
				keys["IsReadOnlyd"] = bmm.IsReadOnly;
				keys["IsSealedd"] = bmm.IsSealed;
				keys["Keywordsd"] = bmm.Keywords;
				keys["Locationd"] = bmm.Location;
				keys["Ratingd"] = bmm.Rating;
				keys["Subjectd"] = bmm.Subject;
				keys["Titled"] = bmm.Title;
				
				return keys;
			}
			else
			{
				System.Collections.IDictionary keys = new System.Collections.Hashtable();
				
				for (System.Windows.LocalValueEnumerator en = meta.GetLocalValueEnumerator(); en.MoveNext(); )
				{
					System.Windows.DependencyProperty p = en.Current.Property;
					keys[p.Name] = en.Current.Value;
					Console.WriteLine("[{0}]=[{1}]", p.Name, Utils.ToString(keys));
				}
				
				return keys;
			}
		}
	}
}
