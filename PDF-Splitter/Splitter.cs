using System;
using System.Drawing; // для ImageFormat
using System.Drawing.Imaging; // для ImageFormat
using System.IO;
using PdfSharp.Pdf;
using PdfSharp.Drawing;
using PdfSharp.Pdf.IO;
using PdfSharp.UniversalAccessibility.Drawing;
using System.Windows;
using iText.IO.Image;
using ImageMagick;

namespace PDF_Splitter
{
    internal class Splitter
    {
        public static void SplitPdfToImages(string pdfPath, string outputPath)
        {

            using (MagickImageCollection images = new MagickImageCollection())
            {
                images.Read(pdfPath);
                using (IMagickImage vertical = images.AppendVertically())
                {
                    vertical.Format = MagickFormat.Png;
                    vertical.Density = new Density(300);
                    vertical.Write(outputPath);
                }
            }

            //using (var pdfDocument = PdfReader.Open(pdfPath, PdfDocumentOpenMode.Import))
            //{
            //    var pdfPage = pdfDocument.Pages[0];
            //    using (var bitmap = new Bitmap((int)pdfPage.Width, (int)pdfPage.Height))
            //    {
            //        using (var graphics = Graphics.FromImage(bitmap))
            //        {
            //            //graphics.DrawImage(pdfPage, System.Drawing.Point.Empty);
            //            // Render page content here...
            //            //pdfPage.Render();
            //            pdfPage.Contents.
            //        }
            //        bitmap.Save(outputPath, ImageFormat.Png);
            //    }

            //}

            //string srcPDF = pdfPath;
            //PdfDocument pdfd = PdfReader.Open(srcPDF);
            //XGraphics xgfx = XGraphics.FromPdfPage(pdfd.Pages[0]);
            //Bitmap b = new Bitmap((int)1920, (int)1080, xgfx.From);

            //// Открываем PDF-файл
            //PdfDocument document = PdfReader.Open(pdfPath);

            //// Получаем первую страницу
            //PdfPage page = document.Pages[0];

            //// Создаем XGraphics для отрисовки на странице
            //XGraphics gfx = XGraphics.FromPdfPage(page);

            //// Определяем размер страницы
            //int width = (int)page.Width.Point;
            //int height = (int)page.Height.Point;

            //// Создаем Bitmap для отрисовки страницы
            //using (Bitmap bitmap = new Bitmap(width, height))
            //{
            //    using (Graphics g = Graphics.FromImage(bitmap))
            //    {
            //        // Отрисовываем страницу на Bitmap
            //        g.DrawImage(xImage, 0, 0, width, height);

            //        // Сохраняем Bitmap в файл
            //        bitmap.Save(imagePath, ImageFormat.Jpeg);
            //    }
            //}
        }
    }
}
