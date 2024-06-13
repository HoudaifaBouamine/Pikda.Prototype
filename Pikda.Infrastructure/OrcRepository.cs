using Pikda.Domain.Entites;
using Pikda.Domain.Interfaces;
using System;
using System.Threading.Tasks;

namespace Pikda.Infrastructure
{
    public class OrcRepository : IOcrRepository
    {
        public Task<int?> AddOrcModel(OcrModel ocrModel)
        {
            throw new NotImplementedException();
        }
    }
}
