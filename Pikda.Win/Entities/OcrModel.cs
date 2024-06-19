using DevExpress.Xpo;
using System.ComponentModel;
using System.Drawing;

namespace Pikda.Domain.Entites
{
    public class OcrModel : XPLiteObject
    {
        [Key(autoGenerate: true)]
        public int Id { get; set; }
        public string Name { get; set; }
        public Image Image{ get; set; }
        public BindingList<Area> Areas { get; set; } = new BindingList<Area>();

        public static OcrModel Create(Session session,string name) =>
            new OcrModel(session)
            {
                Name = name,
                Image = null
            };

        public void CopyTo(OcrModel destModel)
        {
            destModel.Name = Name;
            destModel.Image = Image;

            destModel.Areas = new BindingList<Area>();
            for (int i = 0; i < Areas.Count; i++)
                destModel.Areas.Add(Areas[i]);
        }

        protected OcrModel(Session session) : base(session) { }
    }
}
