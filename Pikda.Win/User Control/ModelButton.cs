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
        public ModelButton(PanelControl modelsList,PictureEditor pictureEditor,int id,string modelName)
        {
            InitializeComponent();
            this.Id = id;
            this.ModelName = modelName;
            this.Dock = DockStyle.Top;
            this.ModelsList = modelsList;
            this.PictureEditor = pictureEditor;
            this.UnSelectAllAndSelectThis();
        }

        #region Public Interface

        /// <summary>
        /// OCR Model Id
        /// </summary>
        public int Id { get; private set; }
        /// <summary>
        /// Use MarkAsSelected() or MarkAsUnSelected() to update this value
        /// </summary>
        public bool IsSelected { get; private set; }
        /// <summary>
        /// OCR Model Name
        /// </summary>
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

        #endregion

        #region Private Behaviors
        private void UnSelectAllAndSelectThis()
        {
            foreach (ModelButton modelButton in this.ModelsList.Controls)
                modelButton.MarkAsUnSelected();

            this.MarkAsSelected();
        }
        private void Button_Click(object sender, EventArgs e)
        {
            this.UnSelectAllAndSelectThis();
        }

        #endregion

        #region Private State

        private static Color SelectedColor = Color.Green;
        private static Color UnSelectedColor = Color.Gray;
        private PanelControl ModelsList { get; set; }
        private PictureEditor PictureEditor { get; set; }

        #endregion

    }
}
