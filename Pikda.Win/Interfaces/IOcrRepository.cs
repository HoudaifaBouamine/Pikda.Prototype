using Pikda.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Drawing;
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
        Task<OcrModel> AddOrcModelAsync(string name);

        Task<OcrModel> ChangeImageAsync(int modelId, Image image);

        Task<OcrModel> ChangeNameAsync(int modelId, string name);
        Task<OcrModel> AddAreaAsync(int modelId, Area area);
        Task<OcrModel> DeleteAreaAsync(int modelId, int areaId);
        Task<List<OcrModel>> GetAllOrcModelsAsync();

    }
}
