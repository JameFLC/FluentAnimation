using System;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Imaging;
using Windows.Storage.Streams;

public static class ImageSourceConverter
{
    public static async Task<Bitmap?> GetBitmapFromImageSource(ImageSource imageSource)
    {
        if (imageSource is BitmapImage bitmapImage)
        {
            return await ConvertBitmapImageToBitmap(bitmapImage);
        }
        else if (imageSource is WriteableBitmap writeableBitmap)
        {
            return ConvertWriteableBitmapToBitmap(writeableBitmap);
        }


        return null;
    }

    private static async Task<Bitmap?> ConvertBitmapImageToBitmap(BitmapImage bitmapImage)
    {
        try
        {
            using (IRandomAccessStream stream = await RandomAccessStreamReference.CreateFromUri(bitmapImage.UriSource).OpenReadAsync())
            using (Stream netStream = stream.AsStreamForRead())
            {
                return new Bitmap(netStream); // Directly create System.Drawing.Bitmap
            }
        }
        catch (Exception _)
        {
            return null;
        }
    }

    private static Bitmap? ConvertWriteableBitmapToBitmap(WriteableBitmap writeableBitmap)
    {
        try
        {
            using (MemoryStream stream = new MemoryStream())
            {
                byte[] pixels = writeableBitmap.PixelBuffer.ToArray();
                Bitmap bitmap = new Bitmap(writeableBitmap.PixelWidth, writeableBitmap.PixelHeight, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

                var bitmapData = bitmap.LockBits(
                    new Rectangle(0, 0, bitmap.Width, bitmap.Height),
                    System.Drawing.Imaging.ImageLockMode.WriteOnly,
                    System.Drawing.Imaging.PixelFormat.Format32bppArgb);

                System.Runtime.InteropServices.Marshal.Copy(pixels, 0, bitmapData.Scan0, pixels.Length);
                bitmap.UnlockBits(bitmapData);

                return bitmap;
            }
        }
        catch (Exception _)
        {

            return null;
        }
    }
}