using Pikda.Domain.DTOs;
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
        Task<OcrModelDto> AddOrcModelAsync(string name);
        Task<OcrModelDto> ChangeImageAsync(int modelId, Image image);
        Task<OcrModelDto> ChangeNameAsync(int modelId, string name);
        Task<OcrModelDto> AddAreaAsync(int modelId, AreaDto area);
        Task<OcrModelDto> DeleteAreaAsync(int modelId, int areaId);
        Task<List<OcrModelDto>> GetAllOrcModelsAsync();

    }
}
