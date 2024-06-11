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
    public partial class PictureEditor : UserControl
    {
        public PictureEditor(Panel picturePanel,Guid id)
        {
            InitializeComponent();
            this.Dock = DockStyle.Fill;
            this.Id = id;
            this.PicturePanel = picturePanel;
            this.MarkAsSelected();
        }

        private void UnSelectAllAndSelectThis()
        {
            foreach (PictureEditor modelButton in this.PicturePanel.Controls)
                modelButton.MarkAsUnSelected();

            this.MarkAsSelected();
        }

        public void MarkAsSelected()
        {
            this.Visible = true; ;
            this.IsSelected = true;
        }

        public void MarkAsUnSelected()
        {
            this.Visible = false;
            this.IsSelected = false;
        }

        public Guid Id { get; private set; }

        /// <summary>
        /// Use MarkAsSelected() or MarkAsUnSelected() to update this value
        /// </summary>
        public bool IsSelected { get; private set; }
        private Panel PicturePanel { get; set; }

    }
}
