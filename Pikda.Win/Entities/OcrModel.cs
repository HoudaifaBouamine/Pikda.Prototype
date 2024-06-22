using DevExpress.Xpo;
using System.Drawing;
using DevExpress.Utils.Extensions;

namespace Pikda.Domain.Entites
{
    public class OcrModel : XPLiteObject
    {
        [Key(autoGenerate: true)]
        public int Id { get; set; }
        public string Name { get; set; }

        [Persistent]
        [ValueConverter(typeof(DevExpress.Xpo.Metadata.ImageValueConverter))]
        public Image Image { get; set; }

        [Association]
        public XPCollection<Area> Areas
        {
            get { return GetCollection<Area>("Areas"); }
        }

        public static OcrModel Create(Session session, string name)
        {
            var model = new OcrModel(session)
            {
                Name = name,
                Image = null
            };
            return model;
        }

        public void CopyTo(OcrModel destModel)
        {
            destModel.Name = Name;
            destModel.Image = Image;

            destModel.Areas.Remove(a => true);
            for (int i = 0; i < Areas.Count; i++)
                destModel.Areas.Add(Areas[i]);
        }

        protected OcrModel(Session session) : base(session) { }

        public override string ToString()
        {
            return $"Id = {Id}, Name = {Name}, Image = {Image}, Areas Count = {Areas.Count}, Areas = {Areas}";
        }
    }
}
