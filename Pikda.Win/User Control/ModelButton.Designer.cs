namespace Pikda.Win.User_Control
{
    partial class ModelButton
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
            this.Button = new DevExpress.XtraEditors.SimpleButton();
            this.SuspendLayout();
            // 
            // Button
            // 
            this.Button.Appearance.Font = new System.Drawing.Font("Arial", 12F);
            this.Button.Appearance.Options.UseFont = true;
            this.Button.Appearance.Options.UseTextOptions = true;
            this.Button.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.Button.Appearance.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.Show;
            this.Button.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.Button.AppearanceDisabled.Font = new System.Drawing.Font("Arial", 12F);
            this.Button.AppearanceDisabled.Options.UseFont = true;
            this.Button.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Button.Location = new System.Drawing.Point(0, 0);
            this.Button.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.Button.Name = "Button";
            this.Button.Padding = new System.Windows.Forms.Padding(40, 0, 0, 0);
            this.Button.Size = new System.Drawing.Size(390, 65);
            this.Button.TabIndex = 2;
            this.Button.Text = "Button";
            this.Button.Click += new System.EventHandler(this.Button_Click);
            // 
            // ModelButton
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.Button);
            this.Name = "ModelButton";
            this.Size = new System.Drawing.Size(390, 65);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton Button;
    }
}
