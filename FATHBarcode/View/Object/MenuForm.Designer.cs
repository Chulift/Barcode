namespace FATHBarcode.View.Object
{
    partial class MenuForm
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
            this.firstMenuButton = new System.Windows.Forms.Button();
            this.secondMenuButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // firstMenuButton
            // 
            this.firstMenuButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.firstMenuButton.BackColor = System.Drawing.Color.MediumAquamarine;
            this.firstMenuButton.Location = new System.Drawing.Point(22, 19);
            this.firstMenuButton.Name = "firstMenuButton";
            this.firstMenuButton.Size = new System.Drawing.Size(199, 20);
            this.firstMenuButton.TabIndex = 0;
            this.firstMenuButton.Text = "1. FG Picking";
            this.firstMenuButton.Visible = false;
            this.firstMenuButton.Click += new System.EventHandler(this.Menu_Click);
            // 
            // secondMenuButton
            // 
            this.secondMenuButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.secondMenuButton.BackColor = System.Drawing.Color.MediumAquamarine;
            this.secondMenuButton.Location = new System.Drawing.Point(22, 55);
            this.secondMenuButton.Name = "secondMenuButton";
            this.secondMenuButton.Size = new System.Drawing.Size(199, 20);
            this.secondMenuButton.TabIndex = 0;
            this.secondMenuButton.Text = "2. Shipping";
            this.secondMenuButton.Visible = false;
            this.secondMenuButton.Click += new System.EventHandler(this.Menu_Click);
            // 
            // MenuForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 294);
            this.Controls.Add(this.secondMenuButton);
            this.Controls.Add(this.firstMenuButton);
            this.Name = "MenuForm";
            this.Text = "Menu";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button firstMenuButton;
        private System.Windows.Forms.Button secondMenuButton;
    }
}