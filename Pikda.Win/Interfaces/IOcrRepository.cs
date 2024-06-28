using Pikda.Domain.DTOs;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;

namespace Pikda.Domain.Interfaces
{
    /// <summary>
    /// Interface defining operations for interacting with OCR models.
    /// </summary>
    public interface IOcrRepository
    {
        /// <summary>
        /// Asynchronously adds a new OCR model with the specified name to the storage medium.
        /// </summary>
        /// <param name="name">The name of the OCR model.</param>
        /// <returns>If successful, returns the created entity with assigned Id; otherwise, returns null.</returns>
        Task<OcrModelDto> AddOrcModelAsync(string name);

        /// <summary>
        /// Asynchronously changes the image associated with an OCR model.
        /// </summary>
        /// <param name="modelId">The Id of the OCR model.</param>
        /// <param name="image">The new image to be associated with the OCR model.</param>
        /// <returns>If successful, returns the updated OCR model; otherwise, returns null.</returns>
        Task<OcrModelDto> ChangeImageAsync(int modelId, Image image);

        /// <summary>
        /// Asynchronously changes the name of an OCR model.
        /// </summary>
        /// <param name="modelId">The Id of the OCR model.</param>
        /// <param name="name">The new name for the OCR model.</param>
        /// <returns>If successful, returns the updated OCR model; otherwise, returns null.</returns>
        Task<OcrModelDto> ChangeNameAsync(int modelId, string name);

        /// <summary>
        /// Asynchronously adds a new area to an OCR model.
        /// </summary>
        /// <param name="modelId">The Id of the OCR model.</param>
        /// <param name="area">The area to be added.</param>
        /// <returns>If successful, returns the updated OCR model; otherwise, returns null.</returns>
        Task<OcrModelDto> AddAreaAsync(int modelId, AreaDto area);

        /// <summary>
        /// Asynchronously deletes an area from an OCR model.
        /// </summary>
        /// <param name="modelId">The Id of the OCR model.</param>
        /// <param name="areaId">The Id of the area to be deleted.</param>
        /// <returns>If successful, returns the updated OCR model; otherwise, returns null.</returns>
        Task<OcrModelDto> DeleteAreaAsync(int modelId, int areaId);

        /// <summary>
        /// Asynchronously retrieves all OCR models.
        /// </summary>
        /// <returns>A list of all OCR models.</returns>
        Task<List<OcrModelDto>> GetAllOrcModelsAsync();

        /// <summary>
        /// Asynchronously retrieves all areas associated with an OCR model.
        /// </summary>
        /// <param name="modelId">The Id of the OCR model.</param>
        /// <returns>A list of areas associated with the OCR model.</returns>
        Task<List<AreaDto>> GetOcrModelAreas(int modelId);
    }
}
