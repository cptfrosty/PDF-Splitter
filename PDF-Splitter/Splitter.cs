using Spire.Pdf;
using Spire.Pdf.Graphics;
using System;
using System.Drawing; // для ImageFormat
using System.Drawing.Imaging; // для ImageFormat
using System.IO;

namespace PDF_Splitter
{
    internal class Splitter
    {
        public static void SplitPdfToImages(string pdfPath, string outputPath)
        {
            //Создайте экземпляр PdfDocument
            PdfDocument pdf = new PdfDocument();
            //Загрузка образца документа PDF
            pdf.LoadFromFile(pdfPath);
            //Просматривайте каждую страницу в PDF
            for (int i = 0; i < pdf.Pages.Count; i++)
            {
                //Преобразуйте все страницы в изображения и установите разрешение на дюйм для изображений
                Image image = pdf.SaveAsImage(i, PdfImageType.Bitmap, 500, 500);
                //Сохранить изображения в формате PNG в указанную папку 
                String file = String.Format(outputPath + "\\ToImage-{0}.png", i);
                image.Save(file, ImageFormat.Png);
                //Сохранить изображения в формате JPG в указанную папку 
                //String fileJpg = String.Format("\\ToImage-{0}.jpg", i);
                //image.Save(fileJpg, ImageFormat.Jpeg);
            }
        }
    }
}
