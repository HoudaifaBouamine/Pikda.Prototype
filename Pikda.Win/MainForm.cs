using DevExpress.Data.Helpers;
using DevExpress.XtraEditors;
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
        public MainForm()
        {
            InitializeComponent();
            ContextMenu cm = new ContextMenu();
            cm.MenuItems.Add("Add", new EventHandler(AddNewModel));
            ModelsList.ContextMenu = cm;
        }

        const int ModelMenuMaxWidth = 400;
        const int ModelMenuMinWidth = 200;

        private void AddNewModel(object sender, EventArgs e)
        {
            var modelCreationDialgo = new ModelCreationDialogForm();
            modelCreationDialgo.ShowDialog();

            if (modelCreationDialgo.IsCreationConfirmed)
            {
                var modelName = modelCreationDialgo.ModelName;

                Guid id = Guid.NewGuid();
                var pictureEditor = new PictureEditor(this.ImagesPanel, id);
                this.ImagesPanel.Controls.Add(pictureEditor);
                this.ModelsList.Controls.Add(new ModelButton(this.ModelsList, pictureEditor, id, modelName));

                foreach(ModelButton model in this.ModelsList.Controls)
                    if(model.Id != id)
                        model.BackColor = Color.FromArgb(1,33,33,33);

                foreach (PictureEditor model in this.ImagesPanel.Controls)
                    if (model.Id != id)
                        model.Visible = false;
            }

        }

        private void PictureEditor_Paint(object sender, PaintEventArgs e)
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
    }
}
