﻿using DevExpress.Xpo;
using System;
using System.Drawing;

namespace Pikda.Domain.Entites
{

    public class Area : XPLiteObject
    {
        [Key(autoGenerate: true)]
        public int Id { get; private set; }
        public string Name { get; private set; }
        public float XFactor { get; private set; }
        public float YFactor { get; private set; }
        public float WidthFactor { get; private set; }
        public float HeightFactor { get; private set; }

        public static Area Create(Session session,string name, Rectangle imageRect, Rectangle newRect) =>
            new Area(session)
            {
                Name = name,
                XFactor = ((float) Math.Min(newRect.X , newRect.X + newRect.Width))  / (imageRect.Width),
                YFactor = ((float) Math.Min(newRect.Y , newRect.Y + newRect.Height)) / (imageRect.Height),
                WidthFactor  = (float) Math.Abs(newRect.Width)  / (imageRect.Width),
                HeightFactor = (float) Math.Abs(newRect.Height) / (imageRect.Height)
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
