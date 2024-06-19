﻿using DevExpress.Xpo;
using Pikda.Domain.Entites;
using Pikda.Domain.Interfaces;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using DevExpress.Xpo.DB;
using System.Drawing;

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

        public async Task<OcrModel> AddOrcModelAsync(string name)
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                OcrModel newOcrModel = OcrModel.Create(uow, name);
                await uow.SaveAsync(newOcrModel);
                await uow.CommitChangesAsync();

                Console.WriteLine("Id of new orc is " + newOcrModel.Id + " (if it is 0, creation is faild)");
                return newOcrModel;
            }
        }

        public async Task<List<OcrModel>> GetAllOrcModelsAsync()
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                var query = await uow.Query<OcrModel>().ToListAsync();
                return new List<OcrModel>(query);
            }
        }

        public async Task<OcrModel> ChangeImageAsync(int modelId, Image image)
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                var model = await uow.GetObjectByKeyAsync<OcrModel>(modelId);
                model.Image = image;
                await uow.SaveAsync(model);
                await uow.CommitChangesAsync();

                Console.WriteLine(" -> Image updated");

                return model;
            }
        }

        public async Task<OcrModel> ChangeNameAsync(int modelId, string name)
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                var model = await uow.GetObjectByKeyAsync<OcrModel>(modelId);
                model.Name = name;
                await uow.SaveAsync(model);
                await uow.CommitChangesAsync();

                Console.WriteLine(" -> Name updated");

                return model;
            }
        }

        public async Task<OcrModel> AddAreaAsync(int modelId, Area area)
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                var model = await uow.GetObjectByKeyAsync<OcrModel>(modelId);
                model.Areas.Add(area);
                await uow.SaveAsync(model);
                await uow.CommitChangesAsync();

                Console.WriteLine(" -> Area added");

                return model;
            }
        }

        public async Task<OcrModel> DeleteAreaAsync(int modelId, int areaId)
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                var model = await uow.GetObjectByKeyAsync<OcrModel>(modelId);
                var areaToRemove = model.Areas.FirstOrDefault(a => a.Id == areaId);
                if (areaToRemove is null) return model;
                model.Areas.Remove(areaToRemove);

                await uow.SaveAsync(model);
                await uow.CommitChangesAsync();

                Console.WriteLine(" -> Area deleted");

                return model;
            }
        }
    }
}
