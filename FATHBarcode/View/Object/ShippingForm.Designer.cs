namespace FATHBarcode.View.Object
{
    partial class ShippingForm
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
            this.submitButton = new System.Windows.Forms.Button();
            this.deliveryOrderTagTextbox = new System.Windows.Forms.TextBox();
            this.deliveryOrderTagLabel = new System.Windows.Forms.Label();
            this.menuButton = new System.Windows.Forms.Button();
            this.customerTagLabel = new System.Windows.Forms.Label();
            this.customerTagTextBox = new System.Windows.Forms.TextBox();
            this.fathTagLabel = new System.Windows.Forms.Label();
            this.fathTagTextBox = new System.Windows.Forms.TextBox();
            this.clearButton = new System.Windows.Forms.Button();
            this.shippingDataGrid = new System.Windows.Forms.DataGrid();
            this.label1 = new System.Windows.Forms.Label();
            this.customerTagQtyLabel = new System.Windows.Forms.Label();
            this.customerTagQtyTextbox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // submitButton
            // 
            this.submitButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.submitButton.BackColor = System.Drawing.Color.MediumAquamarine;
            this.submitButton.Location = new System.Drawing.Point(175, 14);
            this.submitButton.Name = "submitButton";
            this.submitButton.Size = new System.Drawing.Size(54, 20);
            this.submitButton.TabIndex = 12;
            this.submitButton.Text = "Submit";
            this.submitButton.Click += new System.EventHandler(this.submitButton_Click);
            // 
            // deliveryOrderTagTextbox
            // 
            this.deliveryOrderTagTextbox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.deliveryOrderTagTextbox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.deliveryOrderTagTextbox.Enabled = false;
            this.deliveryOrderTagTextbox.Location = new System.Drawing.Point(90, 49);
            this.deliveryOrderTagTextbox.Name = "deliveryOrderTagTextbox";
            this.deliveryOrderTagTextbox.Size = new System.Drawing.Size(100, 23);
            this.deliveryOrderTagTextbox.TabIndex = 11;
            // 
            // deliveryOrderTagLabel
            // 
            this.deliveryOrderTagLabel.Location = new System.Drawing.Point(12, 50);
            this.deliveryOrderTagLabel.Name = "deliveryOrderTagLabel";
            this.deliveryOrderTagLabel.Size = new System.Drawing.Size(72, 20);
            this.deliveryOrderTagLabel.Text = "DO Tag";
            // 
            // menuButton
            // 
            this.menuButton.BackColor = System.Drawing.Color.MediumAquamarine;
            this.menuButton.Location = new System.Drawing.Point(12, 14);
            this.menuButton.Name = "menuButton";
            this.menuButton.Size = new System.Drawing.Size(48, 20);
            this.menuButton.TabIndex = 10;
            this.menuButton.Text = "Menu";
            this.menuButton.Click += new System.EventHandler(this.menuButton_Click);
            // 
            // customerTagLabel
            // 
            this.customerTagLabel.Location = new System.Drawing.Point(12, 81);
            this.customerTagLabel.Name = "customerTagLabel";
            this.customerTagLabel.Size = new System.Drawing.Size(96, 20);
            this.customerTagLabel.Text = "Customer Tag";
            // 
            // customerTagTextBox
            // 
            this.customerTagTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.customerTagTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.customerTagTextBox.Location = new System.Drawing.Point(90, 98);
            this.customerTagTextBox.Name = "customerTagTextBox";
            this.customerTagTextBox.ReadOnly = true;
            this.customerTagTextBox.Size = new System.Drawing.Size(138, 23);
            this.customerTagTextBox.TabIndex = 11;
            this.customerTagTextBox.GotFocus += new System.EventHandler(this.customerTagTextBox_GotFocus);
            this.customerTagTextBox.LostFocus += new System.EventHandler(this.customerTagTextBox_LostFocus);
            // 
            // fathTagLabel
            // 
            this.fathTagLabel.Location = new System.Drawing.Point(12, 151);
            this.fathTagLabel.Name = "fathTagLabel";
            this.fathTagLabel.Size = new System.Drawing.Size(96, 20);
            this.fathTagLabel.Text = "FATH Tag";
            // 
            // fathTagTextBox
            // 
            this.fathTagTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.fathTagTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.fathTagTextBox.Location = new System.Drawing.Point(90, 148);
            this.fathTagTextBox.Name = "fathTagTextBox";
            this.fathTagTextBox.ReadOnly = true;
            this.fathTagTextBox.Size = new System.Drawing.Size(138, 23);
            this.fathTagTextBox.TabIndex = 11;
            this.fathTagTextBox.GotFocus += new System.EventHandler(this.fathTagTextBox_GotFocus);
            this.fathTagTextBox.LostFocus += new System.EventHandler(this.fathTagTextBox_LostFocus);
            // 
            // clearButton
            // 
            this.clearButton.BackColor = System.Drawing.Color.MediumAquamarine;
            this.clearButton.Location = new System.Drawing.Point(115, 14);
            this.clearButton.Name = "clearButton";
            this.clearButton.Size = new System.Drawing.Size(54, 20);
            this.clearButton.TabIndex = 12;
            this.clearButton.Text = "Clear";
            this.clearButton.Click += new System.EventHandler(this.clearButton_Click);
            // 
            // shippingDataGrid
            // 
            this.shippingDataGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.shippingDataGrid.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.shippingDataGrid.Location = new System.Drawing.Point(12, 177);
            this.shippingDataGrid.Name = "shippingDataGrid";
            this.shippingDataGrid.Size = new System.Drawing.Size(216, 77);
            this.shippingDataGrid.TabIndex = 16;
            this.shippingDataGrid.KeyDown += new System.Windows.Forms.KeyEventHandler(this.shippingDataGrid_KeyDown);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(30, 101);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 20);
            this.label1.Text = "Part No.";
            // 
            // customerTagQtyLabel
            // 
            this.customerTagQtyLabel.Location = new System.Drawing.Point(30, 125);
            this.customerTagQtyLabel.Name = "customerTagQtyLabel";
            this.customerTagQtyLabel.Size = new System.Drawing.Size(54, 20);
            this.customerTagQtyLabel.Text = "Qty";
            // 
            // customerTagQtyTextbox
            // 
            this.customerTagQtyTextbox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.customerTagQtyTextbox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.customerTagQtyTextbox.Location = new System.Drawing.Point(90, 122);
            this.customerTagQtyTextbox.Name = "customerTagQtyTextbox";
            this.customerTagQtyTextbox.ReadOnly = true;
            this.customerTagQtyTextbox.Size = new System.Drawing.Size(75, 23);
            this.customerTagQtyTextbox.TabIndex = 11;
            this.customerTagQtyTextbox.GotFocus += new System.EventHandler(this.customerTagQtyTextbox_GotFocus);
            this.customerTagQtyTextbox.LostFocus += new System.EventHandler(this.customerTagQtyTextbox_LostFocus);
            // 
            // ShippingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 267);
            this.Controls.Add(this.customerTagQtyLabel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.shippingDataGrid);
            this.Controls.Add(this.clearButton);
            this.Controls.Add(this.submitButton);
            this.Controls.Add(this.fathTagTextBox);
            this.Controls.Add(this.customerTagQtyTextbox);
            this.Controls.Add(this.customerTagTextBox);
            this.Controls.Add(this.deliveryOrderTagTextbox);
            this.Controls.Add(this.fathTagLabel);
            this.Controls.Add(this.customerTagLabel);
            this.Controls.Add(this.deliveryOrderTagLabel);
            this.Controls.Add(this.menuButton);
            this.Name = "ShippingForm";
            this.Text = "Shipping";
            this.Load += new System.EventHandler(this.ShippingForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button submitButton;
        private System.Windows.Forms.TextBox deliveryOrderTagTextbox;
        private System.Windows.Forms.Label deliveryOrderTagLabel;
        private System.Windows.Forms.Button menuButton;
        private System.Windows.Forms.Label customerTagLabel;
        private System.Windows.Forms.TextBox customerTagTextBox;
        private System.Windows.Forms.Label fathTagLabel;
        private System.Windows.Forms.TextBox fathTagTextBox;
        private System.Windows.Forms.Button clearButton;
        private System.Windows.Forms.DataGrid shippingDataGrid;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label customerTagQtyLabel;
        private System.Windows.Forms.TextBox customerTagQtyTextbox;
    }
}