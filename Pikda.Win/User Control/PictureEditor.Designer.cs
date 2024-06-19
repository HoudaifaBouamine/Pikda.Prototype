namespace Pikda.Win.User_Control
{
    partial class PictureEditor
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.PictureEdit = new DevExpress.XtraEditors.PictureEdit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureEdit.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // PictureEdit
            // 
            this.PictureEdit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PictureEdit.Location = new System.Drawing.Point(0, 0);
            this.PictureEdit.Name = "PictureEdit";
            this.PictureEdit.Properties.Appearance.Font = new System.Drawing.Font("Arial", 12F);
            this.PictureEdit.Properties.Appearance.Options.UseFont = true;
            this.PictureEdit.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Auto;
            this.PictureEdit.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom;
            this.PictureEdit.Size = new System.Drawing.Size(662, 359);
            this.PictureEdit.TabIndex = 0;
            this.PictureEdit.ImageChanged += new System.EventHandler(this.PictureEdit_ImageChanged);
            this.PictureEdit.ImageLoading += new DevExpress.XtraEditors.Repository.ImageLoadEventHandler(this.PictureEdit_ImageLoading);
            this.PictureEdit.Paint += new System.Windows.Forms.PaintEventHandler(this.PictureEdit_Paint);
            this.PictureEdit.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PictureEdit_MouseDown);
            this.PictureEdit.MouseMove += new System.Windows.Forms.MouseEventHandler(this.PictureEdit_MouseMove);
            this.PictureEdit.MouseUp += new System.Windows.Forms.MouseEventHandler(this.PictureEdit_MouseUp);
            // 
            // PictureEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.PictureEdit);
            this.Name = "PictureEditor";
            this.Size = new System.Drawing.Size(662, 359);
            ((System.ComponentModel.ISupportInitialize)(this.PictureEdit.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PictureEdit PictureEdit;
    }
}
