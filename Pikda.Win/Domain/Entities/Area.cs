using DevExpress.Xpo;
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

        public static Area Create(string name, Rectangle imageRect, Rectangle newRect) =>
            new Area
            {
                Id = default,
                Name = name,
                XFactor = ((float) Math.Min(newRect.X , newRect.X + newRect.Width))  / (imageRect.Width),
                YFactor = ((float) Math.Min(newRect.Y , newRect.Y + newRect.Height)) / (imageRect.Height),
                WidthFactor  = (float) Math.Abs(newRect.Width)  / (imageRect.Width),
                HeightFactor = (float) Math.Abs(newRect.Height) / (imageRect.Height)
            };

        public Rectangle ToRectangle(Rectangle currentImageRect)
        {
            return new Rectangle
                (
                    x: (int)(XFactor * currentImageRect.Width),
                    y: (int)(YFactor * currentImageRect.Height),
                    width: (int)(WidthFactor * currentImageRect.Width),
                    height: (int)(HeightFactor * currentImageRect.Height)
                );
        }
        protected Area() { }

        public bool SetName(string name)
        {
            if(string.IsNullOrWhiteSpace(name) || string.IsNullOrEmpty(name)) return false;

            // check for allowed values (FirstName, LastName, ...)

            this.Name = name;
            return true;
        }
    }

}
