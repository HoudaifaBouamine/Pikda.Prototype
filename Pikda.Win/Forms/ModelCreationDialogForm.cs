using DevExpress.XtraEditors;
using Pikda.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pikda.Win.Forms
{
    public partial class ModelCreationDialogForm : DevExpress.XtraEditors.XtraForm
    {
        public ModelCreationDialogForm()
        {
            InitializeComponent();
            ConfirmButton.Enabled = false;
        }

        private void ConfirmButton_Click(object sender, EventArgs e)
        {
            IsCreationConfirmed = true;
            this.ModelName = this.ModelNameTextBox.Text;

            this.Close();
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            IsCreationConfirmed = false;
            this.Close();
        }

        public string ModelName { get; set; } = "";
        public bool IsCreationConfirmed { get; set; } = false;

        private void ModelNameTextBox_TextChanged(object sender, EventArgs e)
        {
            if(this.ModelNameTextBox.Text.Length <= 0)
            {
                this.ConfirmButton.Enabled = false;
            }

            this.ConfirmButton.Enabled = true;
        }
    }
}