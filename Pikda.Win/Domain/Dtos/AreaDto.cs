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
    }
}
