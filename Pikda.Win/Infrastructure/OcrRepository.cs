using DevExpress.Xpo;
using Pikda.Domain.Entites;
using Pikda.Domain.Interfaces;
using Pikda.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DevExpress.Xpo.DB;

namespace Pikda.Infrastructure
{
    public class OcrRepository : IOcrRepository
    {
        public OcrRepository()
        {
            if (XpoDefault.DataLayer != null) return;

            string appDataPath = Path.Combine(Environment.CurrentDirectory, "..\\..");
            appDataPath = Path.GetFullPath(appDataPath);
            string connectionString = SQLiteConnectionProvider.GetConnectionString(Path.Combine(appDataPath, "OrcModels.db"));
            XpoDefault.DataLayer = XpoDefault.GetDataLayer(connectionString, AutoCreateOption.DatabaseAndSchema);
            Console.WriteLine($"\n\n --> Connection String {connectionString}\n\n");
        }

        public async Task<OcrModelDto> AddOrcModelAsync(string name)
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                OcrModel newOcrModel = OcrModel.Create(uow, name);
                await uow.SaveAsync(newOcrModel);
                await uow.CommitChangesAsync();

                Console.WriteLine("Id of new orc is " + newOcrModel.Id + " (if it is 0, creation is failed)");
                return OcrModelDto.ToDto(newOcrModel);
            }
        }

        public async Task<List<OcrModelDto>> GetAllOrcModelsAsync()
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                var query = await uow.Query<OcrModel>().ToListAsync();
                return query.Select(OcrModelDto.ToDto).ToList();
            }
        }

        public async Task<OcrModelDto> ChangeImageAsync(int modelId, Image image)
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                var model = await uow.GetObjectByKeyAsync<OcrModel>(modelId);
                if (model == null) return null;

                model.Image = image;
                await uow.SaveAsync(model);
                await uow.CommitChangesAsync();

                Console.WriteLine(" -> Image updated");
                return OcrModelDto.ToDto(model);
            }
        }

        public async Task<OcrModelDto> ChangeNameAsync(int modelId, string name)
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                var model = await uow.GetObjectByKeyAsync<OcrModel>(modelId);
                if (model == null) return null;

                model.Name = name;
                await uow.SaveAsync(model);
                await uow.CommitChangesAsync();

                Console.WriteLine(" -> Name updated");
                return OcrModelDto.ToDto(model);
            }
        }

        public async Task<OcrModelDto> AddAreaAsync(int modelId, AreaDto areaDto)
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                var model = await uow.GetObjectByKeyAsync<OcrModel>(modelId);
                if (model == null) return null;

                model.Areas.Add(Area.Create(uow,areaDto));
                await uow.SaveAsync(model);
                await uow.CommitChangesAsync();

                Console.WriteLine(" -> Area added");
                return OcrModelDto.ToDto(model);
            }
        }

        public async Task<OcrModelDto> DeleteAreaAsync(int modelId, int areaId)
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                var model = await uow.GetObjectByKeyAsync<OcrModel>(modelId);
                if (model == null) return null;

                var areaToRemove = model.Areas.FirstOrDefault(a => a.Id == areaId);
                if (areaToRemove != null)
                {
                    model.Areas.Remove(areaToRemove);
                    await uow.SaveAsync(model);
                    await uow.CommitChangesAsync();

                    Console.WriteLine(" -> Area deleted");
                }
                return OcrModelDto.ToDto(model);
            }
        }

        public async Task<List<AreaDto>> GetOcrModelAreas(int modelId)
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                var model = await uow.GetObjectByKeyAsync<OcrModel>(modelId);
                if (model == null) return null;

                var modelDto = OcrModelDto.ToDto(model);

                return modelDto.Areas.ToList();
            }

        }
    }
}
