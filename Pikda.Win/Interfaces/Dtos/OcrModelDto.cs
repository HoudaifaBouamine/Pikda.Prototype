using System.Collections.Generic;
using System.Drawing;

namespace Pikda.Domain.DTOs
{
    public class OcrModelDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Image Image { get; set; }
        public List<AreaDto> Areas { get; set; }
    }

}
