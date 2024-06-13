using System;
using System.Drawing;

namespace Pikda.Domain.Entites
{

    public class Area
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public float XFactor { get; private set; }
        public float YFactor { get; private set; }
        public float WidthFactor { get; private set; }
        public float HeightFactor { get; private set; }

        public Area Create(string name, Rectangle imageRect, Rectangle insiderRectangle) =>
            new Area
            {
                Name = name,
                XFactor = ((float) Math.Min(insiderRectangle.X , insiderRectangle.X + insiderRectangle.Width))  / (imageRect.Width),
                YFactor = ((float) Math.Min(insiderRectangle.Y , insiderRectangle.Y + insiderRectangle.Height)) / (imageRect.Height),
                WidthFactor  = (float) Math.Abs(insiderRectangle.Width)  / (imageRect.Width),
                HeightFactor = (float) Math.Abs(insiderRectangle.Height) / (imageRect.Height)
            };
        

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
