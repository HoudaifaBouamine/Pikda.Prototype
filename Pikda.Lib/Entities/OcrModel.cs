using System.ComponentModel;

namespace Pikda.Domain.Entites
{
    public class OcrModel
    {
        public string Name { get; set; }
        public string ImagePath { get; set; }
        public BindingList<Area> Areas { get; set; } = new BindingList<Area>();

        public static OcrModel Create(string name, string imagePath)
        {
            return new OcrModel
            {
                Name = name,
                ImagePath = imagePath
            };
        }
        protected OcrModel() { }
    }
}
