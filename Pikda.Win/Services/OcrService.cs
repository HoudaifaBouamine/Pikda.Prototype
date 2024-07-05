using System;
using System.Drawing;
using System.IO;
using Tesseract;

namespace Pikda.Service
{
    internal class OcrService
    {
        public const string folderName = "images";
        public const string trainedDataFolderName = "tessdata";
        public string Process(Image image, string lang)
        {
            var name = "CardPic" + Guid.NewGuid();
            var imagePath = Path.Combine("..", "..", folderName, name);// remove "..",".." for production

            using (var fileStream = new FileStream(imagePath, FileMode.Create))
            {
                image.Save(fileStream, image.RawFormat);
            }

            Console.WriteLine($"--> Image Saved at : {imagePath}");

            string tessPath = Path.Combine("..","..",trainedDataFolderName, "");
            string result = "";

            using (var engine = new TesseractEngine(tessPath, lang, EngineMode.Default))
            {
                using (var img = Pix.LoadFromFile(imagePath))
                {
                    var page = engine.Process(img);
                    result = page.GetText();
                }
            }

            return string.IsNullOrWhiteSpace(result) ? "Ocr is finished. Return empty" : result;
        }

        public string ProcessRect(Image image, string lang, Rectangle rect)
        {
            var name = "CardPic" + Guid.NewGuid();
            var imagePath = Path.Combine("..", "..", folderName, name);// remove "..",".." for production

            using (var fileStream = new FileStream(imagePath, FileMode.Create))
            {
                image.Save(fileStream, image.RawFormat);
            }

            Console.WriteLine($"--> Image Saved at : {imagePath}");

            string tessPath = Path.Combine("..", "..", trainedDataFolderName, "");
            string result = "";

            using (var engine = new TesseractEngine(tessPath, lang, EngineMode.Default))
            {
                using (var img = (Bitmap) Bitmap.FromFile(imagePath))
                {
                    Rect rect1 = new Rect(rect.X,rect.Y,rect.Width,rect.Height);
                    var page = engine.Process(img, rect1);
                    result = page.GetText();
                }
            }

            return string.IsNullOrWhiteSpace(result) ? "Ocr is finished. Return empty" : result;
        }
    }
}

