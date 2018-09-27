namespace FATHBarcode.View.Object
{
    partial class ScanDeliveryOrderTagForm
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
            this.menuButton = new System.Windows.Forms.Button();
            this.deliveryOrderTagLabel = new System.Windows.Forms.Label();
            this.deliveryOrderTagTextbox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // menuButton
            // 
            this.menuButton.BackColor = System.Drawing.Color.MediumAquamarine;
            this.menuButton.Location = new System.Drawing.Point(12, 14);
            this.menuButton.Name = "menuButton";
            this.menuButton.Size = new System.Drawing.Size(48, 20);
            this.menuButton.TabIndex = 0;
            this.menuButton.Text = "Menu";
            this.menuButton.Click += new System.EventHandler(this.menuButton_Click);
            // 
            // deliveryOrderTagLabel
            // 
            this.deliveryOrderTagLabel.Location = new System.Drawing.Point(12, 50);
            this.deliveryOrderTagLabel.Name = "deliveryOrderTagLabel";
            this.deliveryOrderTagLabel.Size = new System.Drawing.Size(72, 20);
            this.deliveryOrderTagLabel.Text = "DO Tag";
            // 
            // deliveryOrderTagTextbox
            // 
            this.deliveryOrderTagTextbox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.deliveryOrderTagTextbox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.deliveryOrderTagTextbox.Location = new System.Drawing.Point(90, 49);
            this.deliveryOrderTagTextbox.Name = "deliveryOrderTagTextbox";
            this.deliveryOrderTagTextbox.ReadOnly = true;
            this.deliveryOrderTagTextbox.Size = new System.Drawing.Size(100, 23);
            this.deliveryOrderTagTextbox.TabIndex = 2;
            this.deliveryOrderTagTextbox.GotFocus += new System.EventHandler(this.deliveryOrderTagTextbox_GotFocus);
            this.deliveryOrderTagTextbox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.deliveryOrderTagTextbox_KeyDown);
            this.deliveryOrderTagTextbox.LostFocus += new System.EventHandler(this.deliveryOrderTagTextbox_LostFocus);
            // 
            // ScanDeliveryOrderTagForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 294);
            this.Controls.Add(this.deliveryOrderTagTextbox);
            this.Controls.Add(this.deliveryOrderTagLabel);
            this.Controls.Add(this.menuButton);
            this.Name = "ScanDeliveryOrderTagForm";
            this.Text = "ScanDeliveryOrderTagForm";
            this.Load += new System.EventHandler(this.ScanDeliveryOrderTagForm_Load);
            this.Activated += new System.EventHandler(this.ScanDeliveryOrderTagForm_Activated);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button menuButton;
        private System.Windows.Forms.Label deliveryOrderTagLabel;
        private System.Windows.Forms.TextBox deliveryOrderTagTextbox;
    }
}