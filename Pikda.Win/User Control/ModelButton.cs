using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pikda.Win.User_Control
{
    public partial class ModelButton : UserControl
    {
        public ModelButton(PanelControl modelsList,PictureEditor pictureEditor,Guid id,string modelName)
        {
            InitializeComponent();
            this.Id = id;
            this.ModelName = modelName;
            this.Dock = DockStyle.Top;
            this.ModelsList = modelsList;
            this.PictureEditor = pictureEditor;
            this.UnSelectAllAndSelectThis();
        }

        private void UnSelectAllAndSelectThis()
        {
            foreach (ModelButton modelButton in this.ModelsList.Controls)
                modelButton.MarkAsUnSelected();

            this.MarkAsSelected();
        }
        public void MarkAsSelected()
        {
            this.BackColor = SelectedColor;
            this.IsSelected = true;
            PictureEditor.MarkAsSelected();
        }

        public void MarkAsUnSelected()
        {
            this.BackColor = UnSelectedColor;
            this.IsSelected = false;
            PictureEditor.MarkAsUnSelected();
        }

        private static Color SelectedColor = Color.Green;
        private static Color UnSelectedColor = Color.Gray;
        private PanelControl ModelsList { get; set; }
        private PictureEditor PictureEditor { get; set; }

        public Guid Id { get; private set; }


        /// <summary>
        /// Use MarkAsSelected() or MarkAsUnSelected() to update this value
        /// </summary>
        public bool IsSelected { get; private set; }
        public string ModelName 
        { 
            get
            {
                return this.Button.Text;
            }
            set
            {
                this.Button.Text = value;
            }

        }

        private void Button_Click(object sender, EventArgs e)
        {
            this.UnSelectAllAndSelectThis();
        }
    }
}
