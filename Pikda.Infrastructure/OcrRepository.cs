using DevExpress.Xpo.DB;
using DevExpress.Xpo;
using Pikda.Domain.Entites;
using Pikda.Domain.Interfaces;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;

namespace Pikda.Infrastructure
{
    public class OcrRepository : IOcrRepository
    {

        public OcrRepository()
        {

            if (XpoDefault.DataLayer != null) return;
            string appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string connectionString = SQLiteConnectionProvider.GetConnectionString(Path.Combine(appDataPath, "OrcModels.db"));
            XpoDefault.DataLayer = XpoDefault.GetDataLayer(connectionString, AutoCreateOption.DatabaseAndSchema);
            
        }

        public Task<int?> AddOrcModel(OcrModel ocrModel)
        {

            using (UnitOfWork uow = new UnitOfWork())
            {
                OcrModel newInfo = OcrModel.Create
                    (
                        session: uow,
                        name: ocrModel.Name
                    );

                uow.CommitChanges();

                Console.WriteLine("Id of new orc is " + newInfo.Id + " (if it is 0, creation is faild)");
                return Task.FromResult(newInfo.Id == 0 ? null : (int?)newInfo.Id);
            }

            //using (UnitOfWork uow = new UnitOfWork())
            //{
            //    Area newInfo = Area.Create
            //        (
            //            session: uow, 
            //            name: ocrModel.Name, 
            //            imageRect: new Rectangle(11, 21, 31, 41),
            //            newRect: new Rectangle(10, 20, 30, 40)
            //        );

            //    uow.CommitChanges();

            //    Console.WriteLine("Id of new orc is " + newInfo.Id + " (if it is 0, creation is faild)");
            //    return Task.FromResult(newInfo.Id == 0? null : (int?)newInfo.Id);
            //}
        }

        public async Task<OcrModel> AddOrcModelAsync(string name)
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                OcrModel newOcrModel = OcrModel.Create
                    (
                        session: uow,
                        name: name
                    );
                
                await uow.CommitChangesAsync();

                Console.WriteLine("Id of new orc is " + newOcrModel.Id + " (if it is 0, creation is faild)");
                return newOcrModel; 
            }
        }

        public async Task<List<OcrModel>> GetAllOrcModelsAsync()
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                var query = await uow.Query<OcrModel>()
                    .Select(ocr => $"[{ocr.Id}] {ocr.Name}")
                    .ToListAsync();

                foreach (var line in query)
                {
                    Console.WriteLine(line);
                }

                return new List<OcrModel>();
            }
        }
    }
}
