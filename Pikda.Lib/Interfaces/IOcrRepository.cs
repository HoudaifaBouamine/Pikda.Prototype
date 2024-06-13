using Pikda.Domain.Entites;
using System;
using System.Threading.Tasks;

namespace Pikda.Domain.Interfaces
{
    public interface IOcrRepository
    {
        /// <summary>
        /// Save new OrcModel to the storage medium (Database/file...) 
        /// </summary>
        /// <param name="ocrModel">Entity to be store</param>
        /// <returns>If success, the created entity Id (auto increment int) otherwise return NULL</returns>
        Task<int?> AddOrcModel(OcrModel ocrModel);

    }
}
