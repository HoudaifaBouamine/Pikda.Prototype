using DevExpress.XtraEditors;
using Pikda.Domain.Interfaces;
using Pikda.Domain.DTOs;
using Pikda.Win.Forms;
using Pikda.Win.User_Control;
using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pikda.Win
{
    public partial class MainForm : DevExpress.XtraEditors.XtraForm
    {
        private readonly IOcrRepository _ocrRepository;
        private const int ModelMenuMaxWidth = 400;
        private const int ModelMenuMinWidth = 200;

        public MainForm(IOcrRepository ocrRepository)
        {
            InitializeComponent();
            InitializeModelsListContextMenu();
            _ocrRepository = ocrRepository;
        }

        private void InitializeModelsListContextMenu()
        {
            ContextMenu cm = new ContextMenu();
            cm.MenuItems.Add("Add", new EventHandler(AddNewModel));
            ModelsList.ContextMenu = cm;
        }

        private async void AddNewModel(object sender, EventArgs e)
        {
            var modelCreationDialog = new ModelCreationDialogForm();
            modelCreationDialog.ShowDialog();

            if (!modelCreationDialog.IsCreationConfirmed)
                return;

            var modelName = modelCreationDialog.ModelName;
            var newOcrModel = await _ocrRepository.AddOrcModelAsync(modelName);

            if (newOcrModel == null || newOcrModel.Id == 0)
            {
                Console.WriteLine("Failed to create model (MainForm)");
                return;
            }

            var pictureEditor = new PictureEditor(_ocrRepository, ImagesPanel, newOcrModel);
            ImagesPanel.Controls.Add(pictureEditor);
            pictureEditor.Dock = DockStyle.Fill;
            var modelButton = new ModelButton(ModelsList, pictureEditor, newOcrModel.Id, modelName);
            ModelsList.Controls.Add(modelButton);

            foreach (ModelButton button in ModelsList.Controls)
            {
                if (button.Id != newOcrModel.Id)
                    button.BackColor = Color.FromArgb(1, 33, 33, 33);
            }

            foreach (PictureEditor editor in ImagesPanel.Controls)
            {
                if (editor.Id != newOcrModel.Id)
                    editor.Visible = false;
            }

            Console.WriteLine("Added Successfully");
        }

        private void ModelsList_Click(object sender, EventArgs e)
        {
            Console.WriteLine("ModelsList Clicked");
            // Implement logic for handling ModelsList click event
        }

        private void MainForm_SizeChanged(object sender, EventArgs e)
        {
            UpdateModelsListWidth();
        }

        private void UpdateModelsListWidth()
        {
            this.SuspendLayout();

            var totalWidth = this.Width;
            var partsWidth = (int)(totalWidth * 0.2);

            if (partsWidth < ModelMenuMinWidth)
                partsWidth = ModelMenuMinWidth;

            if (partsWidth > ModelMenuMaxWidth)
                partsWidth = ModelMenuMaxWidth;

            this.ModelsList.Width = this.ModelData.Width = partsWidth;
            
            this.ResumeLayout(true);
        }

        private async void MainForm_Load(object sender, EventArgs e)
        {
            await LoadModelsAsync();
        }

        private async Task LoadModelsAsync()
        {
            var models = await _ocrRepository.GetAllOrcModelsAsync();

            foreach (var model in models)
            {
                Console.WriteLine($"\n--> model Image : {model.Image}, {model.Image?.Height} X {model.Image?.Width}");

                var pictureEditor = new PictureEditor(_ocrRepository, ImagesPanel, model);
                ImagesPanel.Controls.Add(pictureEditor);
                pictureEditor.Dock = DockStyle.Fill;
                var modelButton = new ModelButton(ModelsList, pictureEditor, model.Id, model.Name);
                ModelsList.Controls.Add(modelButton);
            }
        }

        private void ModelsList_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
