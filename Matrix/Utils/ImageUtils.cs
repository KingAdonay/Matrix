﻿using System;
using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;

namespace Matrix.Utils
{

    

    class ImageUtils
    {

        


        #region Image To Bytes

        public static byte[] GetPngFromImageControl ( BitmapImage imageControl )
        {
            var memStream = new MemoryStream ();
            var encoder = new PngBitmapEncoder ();
            encoder.Frames.Add (BitmapFrame.Create (imageControl));
            encoder.Save (memStream);
            return memStream.GetBuffer ();
        }

        public Byte[] BitmapToByte2 ( BitmapImage bitmapImage )
        {

            byte[] data;
            var encoder = new JpegBitmapEncoder ();
            encoder.Frames.Add (BitmapFrame.Create (bitmapImage));
            using(var ms = new MemoryStream ())
            {
                encoder.Save (ms);
                data = ms.ToArray ();
            }
            return data;
        }

        public Byte[] BitmapToByte ( BitmapImage bitmap )
        {
            var stream = bitmap.StreamSource;
            Byte[] buffer;
            if (stream == null || stream.Length <= 0) return null;
            using(var br = new BinaryReader (stream))
            {
                buffer = br.ReadBytes ((Int32)stream.Length);
            }
            return buffer;
        }

        public static byte[] BitmapToByte ( string imagePath )
        {
            FileStream fs = new FileStream (imagePath, FileMode.Open, FileAccess.Read);
            byte[] imgBytes = new byte[fs.Length];
            fs.Read (imgBytes, 0, Convert.ToInt32 (fs.Length));
            string encodeData = Convert.ToBase64String (imgBytes, Base64FormattingOptions.InsertLineBreaks);
            return new[] { Byte.Parse (encodeData) };
        }

        private static byte[] BitmapArrayFromFile ( string imageFilePath )
        {
            if(!File.Exists (imageFilePath)) return null;

            var fs = new FileStream (imageFilePath, FileMode.Open, FileAccess.Read);
            var imgByteArr = new byte[fs.Length];
            fs.Read (imgByteArr, 0, Convert.ToInt32 (fs.Length));
            fs.Close ();
            return imgByteArr;
        }

        public Byte[] BitmapToByte1 ( BitmapImage bitmap )
        {
            var stream = bitmap.StreamSource;
            Byte[] buffer;
            if(stream == null || stream.Length <= 0) return null;
            using(var br = new BinaryReader (stream))
            {
                buffer = br.ReadBytes ((Int32)stream.Length);
            }
            return buffer;
        }


        #endregion




        #region Bytes To Image

        public static BitmapImage DecodePhoto ( byte[] byteVal )
        {
            if(byteVal == null) return null;

            try
            {
                var strmImg = new MemoryStream (byteVal);
                var myBitmapImage = new BitmapImage ();
                myBitmapImage.BeginInit ();
                myBitmapImage.StreamSource = strmImg;
                myBitmapImage.DecodePixelWidth = 200;
                myBitmapImage.EndInit ();
                return myBitmapImage;
            }
            catch(Exception ex)
            {
                MessageBox.Show (ex.Message);
            }

            return null;
        }
        

        public static BitmapImage GetBitmapFromFile ( string path )
        {
            var img = new BitmapImage ();
            img.BeginInit ();
            img.UriSource = new Uri (path, UriKind.RelativeOrAbsolute);
            img.EndInit ();
            return img;
        }


        #endregion



    }
}
