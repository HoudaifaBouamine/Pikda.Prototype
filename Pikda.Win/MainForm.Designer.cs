namespace Pikda.Win
{
    partial class MainForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.ModelData = new DevExpress.XtraTreeList.TreeList();
            this.ModelsList = new DevExpress.XtraEditors.PanelControl();
            this.ImagesPanel = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.ModelData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ModelsList)).BeginInit();
            this.SuspendLayout();
            // 
            // ModelData
            // 
            this.ModelData.Dock = System.Windows.Forms.DockStyle.Right;
            this.ModelData.FixedLineWidth = 3;
            this.ModelData.HorzScrollStep = 5;
            this.ModelData.Location = new System.Drawing.Point(814, 0);
            this.ModelData.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.ModelData.MinWidth = 31;
            this.ModelData.Name = "ModelData";
            this.ModelData.Size = new System.Drawing.Size(323, 676);
            this.ModelData.TabIndex = 3;
            this.ModelData.TreeLevelWidth = 28;
            // 
            // ModelsList
            // 
            this.ModelsList.Dock = System.Windows.Forms.DockStyle.Left;
            this.ModelsList.Location = new System.Drawing.Point(0, 0);
            this.ModelsList.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.ModelsList.Name = "ModelsList";
            this.ModelsList.Size = new System.Drawing.Size(323, 676);
            this.ModelsList.TabIndex = 4;
            this.ModelsList.Click += new System.EventHandler(this.ModelsList_Click);
            this.ModelsList.Paint += new System.Windows.Forms.PaintEventHandler(this.ModelsList_Paint);
            // 
            // ImagesPanel
            // 
            this.ImagesPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
            this.ImagesPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ImagesPanel.Location = new System.Drawing.Point(323, 0);
            this.ImagesPanel.Name = "ImagesPanel";
            this.ImagesPanel.Size = new System.Drawing.Size(491, 676);
            this.ImagesPanel.TabIndex = 5;
            // 
            // MainForm
            // 
            this.Appearance.Options.UseFont = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 23F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1137, 676);
            this.Controls.Add(this.ImagesPanel);
            this.Controls.Add(this.ModelsList);
            this.Controls.Add(this.ModelData);
            this.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(6);
            this.MinimumSize = new System.Drawing.Size(900, 450);
            this.Name = "MainForm";
            this.Text = "Pikda";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.SizeChanged += new System.EventHandler(this.MainForm_SizeChanged);
            ((System.ComponentModel.ISupportInitialize)(this.ModelData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ModelsList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private DevExpress.XtraTreeList.TreeList ModelData;
        private DevExpress.XtraEditors.PanelControl ModelsList;
        private System.Windows.Forms.Panel ImagesPanel;
    }
}

