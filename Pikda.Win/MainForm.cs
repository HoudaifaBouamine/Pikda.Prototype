using DevExpress.Data.Helpers;
using DevExpress.XtraEditors;
using DevExpress.XtraPrinting.Native;
using Pikda.Domain.Interfaces;
using Pikda.Infrastructure;
using Pikda.Win.Forms;
using Pikda.Win.User_Control;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Pikda.Win
{
    public partial class MainForm : DevExpress.XtraEditors.XtraForm
    {
        private readonly IOcrRepository _ocrRepository;
        public MainForm(IOcrRepository ocrRepository)
        {
            InitializeComponent();
            ContextMenu cm = new ContextMenu();
            cm.MenuItems.Add("Add", new EventHandler(AddNewModel));
            ModelsList.ContextMenu = cm;
            _ocrRepository = ocrRepository;

        }

        const int ModelMenuMaxWidth = 400;
        const int ModelMenuMinWidth = 200;

        private async void AddNewModel(object sender, EventArgs e)
        {
            var modelCreationDialgo = new ModelCreationDialogForm();
            modelCreationDialgo.ShowDialog();

            if (modelCreationDialgo.IsCreationConfirmed)
            {
                var modelName = modelCreationDialgo.ModelName;

                var newOcrModel = await _ocrRepository.AddOrcModelAsync(Name);

                if(newOcrModel.Id == 0)
                {
                    Console.WriteLine("Faild to create model (MainForm)");
                    return;
                }

                var pictureEditor = new PictureEditor(this.ImagesPanel, newOcrModel.Id);
                this.ImagesPanel.Controls.Add(pictureEditor);
                this.ModelsList.Controls.Add(new ModelButton(this.ModelsList, pictureEditor, newOcrModel.Id, modelName));

                foreach(ModelButton modelButton in this.ModelsList.Controls)
                    if(modelButton.Id != newOcrModel.Id)
                        modelButton.BackColor = Color.FromArgb(1,33,33,33);

                foreach (PictureEditor modelPictureEditor in this.ImagesPanel.Controls)
                    if (modelPictureEditor.Id != newOcrModel.Id)
                        modelPictureEditor.Visible = false;
            }

            await _ocrRepository.GetAllOrcModelsAsync();
            
        }

        private void ModelsList_Click(object sender, EventArgs e)
        {
            //foreach (ModelButton model in this.ModelsList.Controls)
            //    if(model.IsSelected)
            //        SelectImageById(model.Id);

            Console.WriteLine("ModelsList Clicked");
        }

        private void SelectImageById(Guid id)
        {
            //foreach (PictureEditor picture in this.ImagesPanel.Controls)
            //    picture.
        }

        private void MainForm_SizeChanged(object sender, EventArgs e)
        {
            var totalWidth = this.Width;

            var partsWidth = (int)(totalWidth * 0.2);

            if (partsWidth < ModelMenuMinWidth)
                partsWidth = ModelMenuMinWidth;

            if (partsWidth > ModelMenuMaxWidth)
                partsWidth = ModelMenuMaxWidth;

            this.ModelsList.Width = this.ModelData.Width = partsWidth;

            this.ResumeLayout(true);
        }

    }
}
