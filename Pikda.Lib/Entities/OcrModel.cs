using DevExpress.Xpo;
using System.ComponentModel;

namespace Pikda.Domain.Entites
{
    public class OcrModel : XPLiteObject
    {
        [Key(autoGenerate: true)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImagePath { get; set; }
        public BindingList<Area> Areas { get; set; } = new BindingList<Area>();

        public static OcrModel Create(Session session,string name) =>
            new OcrModel(session)
            {
                Name = name,
                ImagePath = null
            };

        protected OcrModel(Session session) : base(session) { }
    }
}
