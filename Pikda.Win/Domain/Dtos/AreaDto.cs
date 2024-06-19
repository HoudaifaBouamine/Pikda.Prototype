using DevExpress.Xpo;
using Pikda.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace Pikda.Domain.DTOs
{
    public class AreaDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public float XFactor { get; set; }
        public float YFactor { get; set; }
        public float WidthFactor { get; set; }
        public float HeightFactor { get; set; }

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

        public static AreaDto Create(string name, Rectangle imageRect, Rectangle newRect) =>
            new AreaDto
            {
                Id = default,
                Name = name,
                XFactor = ((float)Math.Min(newRect.X, newRect.X + newRect.Width)) / (imageRect.Width),
                YFactor = ((float)Math.Min(newRect.Y, newRect.Y + newRect.Height)) / (imageRect.Height),
                WidthFactor = (float)Math.Abs(newRect.Width) / (imageRect.Width),
                HeightFactor = (float)Math.Abs(newRect.Height) / (imageRect.Height)
            };
    }
}
