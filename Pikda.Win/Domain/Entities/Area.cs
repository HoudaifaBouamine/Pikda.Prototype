using DevExpress.Xpo;
using Pikda.Domain.DTOs;
using System;
using System.Drawing;

namespace Pikda.Domain.Entites
{

    public class Area : XPLiteObject
    {
        [Key(autoGenerate: true), Persistent]
        public int Id { get; private set; }
        [Persistent]
        public string Name { get; private set; }
        [Persistent]
        public float XFactor { get; private set; }
        [Persistent]
        public float YFactor { get; private set; }
        [Persistent]
        public float WidthFactor { get; private set; }
        [Persistent] 
        public float HeightFactor { get; private set; }

        [Association]
        public OcrModel OcrModel { get; set; }

        public static Area Create(Session session, AreaDto areaDto) =>
            new Area(session)
            {
                Name = areaDto.Name,
                XFactor = areaDto.XFactor,
                YFactor = areaDto.YFactor,
                WidthFactor = areaDto.WidthFactor,
                HeightFactor = areaDto.HeightFactor,
            };

        protected Area(Session session) : base(session) { }

        public bool SetName(string name)
        {
            if(string.IsNullOrWhiteSpace(name) || string.IsNullOrEmpty(name)) return false;

            // check for allowed values (FirstName, LastName, ...)

            this.Name = name;
            return true;
        }
    }

}
