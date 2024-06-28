using Pikda.Domain.Entites;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Pikda.Domain.DTOs
{
    public class OcrModelDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Image Image { get; set; }
        public List<AreaDto> Areas { get; set; }

        public static OcrModelDto ToDto(OcrModel model)
        {
            if (model == null) return null;

            return new OcrModelDto
            {
                Id = model.Id,
                Name = model.Name,
                Image = model.Image,
                Areas = model.Areas.Select(a => new AreaDto
                {
                    Id = a.Id,
                    Name = a.Name,
                    XFactor = a.XFactor,
                    YFactor = a.YFactor,
                    WidthFactor = a.WidthFactor,
                    HeightFactor = a.HeightFactor
                }).ToList()
            };
        }
    }

}
