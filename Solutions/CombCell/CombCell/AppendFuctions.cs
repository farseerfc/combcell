using System.IO;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace CombCell
{
    public static class AppendFuctions
    {
        /// <summary>
        /// Render the displaying element into a picture file, supporting bmp,jpeg,png,gif,tiff,wdp
        /// </summary>
        /// <param name="fileName">fileName</param>
        public static void RenderToFile(this FrameworkElement element, string fileName)
        {
            VisualBrush brush = new VisualBrush(element);
            DrawingVisual drawingVisual = new DrawingVisual();
            DrawingContext drawingContext = drawingVisual.RenderOpen();
            drawingContext.DrawRectangle(Brushes.White, null, new Rect(0, 0, element.ActualWidth, element.ActualHeight));
            drawingContext.DrawRectangle(brush, null, new Rect(0, 0, element.ActualWidth, element.ActualHeight));
            drawingContext.Close();

            RenderTargetBitmap bmp = new RenderTargetBitmap((int)element.ActualWidth, (int)element.ActualHeight, 96, 96, PixelFormats.Pbgra32);
            bmp.Render(drawingVisual);

            string[] nameSplit = fileName.Split('.');
            string extName = nameSplit[nameSplit.Length - 1].ToLower();

            using (FileStream stream = new FileStream(fileName, FileMode.Create))
            {
                BitmapEncoder encoder = null;
                switch (extName)
                {
                    case "bmp": encoder = new BmpBitmapEncoder(); break;
                    case "jpg":
                    case "jpeg": encoder = new JpegBitmapEncoder(); break;
                    case "png": encoder = new PngBitmapEncoder(); break;
                    case "gif": encoder = new GifBitmapEncoder(); break;
                    case "tif":
                    case "tiff": encoder = new TiffBitmapEncoder(); break;
                    case "wmp":
                    case "wdp": encoder = new WmpBitmapEncoder(); break;
                }
                encoder.Frames.Add(BitmapFrame.Create(bmp));
                encoder.Save(stream);
            }


        }
    }
}
