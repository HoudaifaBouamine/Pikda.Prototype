using System;
using System.Drawing;
using Tesseract;

internal class OcrService
{
    public const string folderName = "images";
    public const string trainedDataFolderName = "tessdata";

    public async Task<string> Process(IFormFile image, string lang)
    {
        var fileName = image.FileName;
        var imagePath = Path.Combine(folderName, fileName);
        var proImagePath = Path.Combine("proImages", fileName);

        using (var stream = new FileStream(imagePath, FileMode.Create))
        {
            image.CopyTo(stream);
        }

        Console.WriteLine($"--> Image Saved at : {imagePath}");

        var result = _process(imagePath, lang);

        return string.IsNullOrWhiteSpace(result) ? "Ocr is finished. Return empty" : result;
    }

    public string _process(string imagePath,string lang)
    {
        string tessPath = Path.Combine(trainedDataFolderName, "");
        string result = "";

        using (var engine = new TesseractEngine(tessPath, lang, EngineMode.Default))
        {
            var img = Pix.LoadFromFile(imagePath);


            img = img.ConvertTo8(0);// 0 to not be collermaped (required by BinarizeSauvolaTiled), 1 to grayscale image  
            img = img.BinarizeSauvolaTiled(50, 0.35f, 1, 1);

            //var btmFromPix = PixConverter.ToBitmap(img);

            //using (var fileStream = new FileStream(imagePath, FileMode.Create))
            //{
            //    btmFromPix.Save(proImagePath);
            //}

            var page = engine.Process(img);

            result = page.GetText();
        }

        return result;
    }
    public string Process(Image image,string imageName, string lang)
    {
        var fileName = imageName;
        var imagePath = Path.Combine(folderName, fileName);
        var proImagePath = Path.Combine("proImages", fileName);

        image.Save(imagePath);

        Console.WriteLine($"--> Image Saved at : {imagePath}");

        var result = _process(imagePath, lang);

        return string.IsNullOrWhiteSpace(result) ? "Ocr is finished. Return empty" : result;
    }

    public static Image ConvertIFormFileToImage(IFormFile formFile)
    {
        if (formFile == null || formFile.Length == 0)
        {
            throw new ArgumentException("Invalid file.");
        }

        using (var stream = new MemoryStream())
        {
            formFile.CopyTo(stream);
            stream.Position = 0; // Reset stream position
            return Image.FromStream(stream);
        }
    }

}


