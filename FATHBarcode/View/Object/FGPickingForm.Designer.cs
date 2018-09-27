namespace FATHBarcode.View.Object
{
    partial class FGPickingForm
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
            this.deliveryOrderTagTextbox = new System.Windows.Forms.TextBox();
            this.deliveryOrderTagLabel = new System.Windows.Forms.Label();
            this.menuButton = new System.Windows.Forms.Button();
            this.fgTagLabel = new System.Windows.Forms.Label();
            this.fgTagTextBox = new System.Windows.Forms.TextBox();
            this.submitButton = new System.Windows.Forms.Button();
            this.fgPickingDataGrid = new System.Windows.Forms.DataGrid();
            this.dataGridTableStyle1 = new System.Windows.Forms.DataGridTableStyle();
            this.dataGridTextBoxColumn1 = new System.Windows.Forms.DataGridTextBoxColumn();
            this.dataGridTextBoxColumn2 = new System.Windows.Forms.DataGridTextBoxColumn();
            this.dataGridTextBoxColumn3 = new System.Windows.Forms.DataGridTextBoxColumn();
            this.dataGridTextBoxColumn4 = new System.Windows.Forms.DataGridTextBoxColumn();
            this.dataGridTextBoxColumn5 = new System.Windows.Forms.DataGridTextBoxColumn();
            this.clearButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
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
            this.deliveryOrderTagTextbox.TabIndex = 5;
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
            this.menuButton.TabIndex = 4;
            this.menuButton.Text = "Menu";
            this.menuButton.Click += new System.EventHandler(this.menuButton_Click);
            // 
            // fgTagLabel
            // 
            this.fgTagLabel.Location = new System.Drawing.Point(12, 83);
            this.fgTagLabel.Name = "fgTagLabel";
            this.fgTagLabel.Size = new System.Drawing.Size(72, 20);
            this.fgTagLabel.Text = "FG Tag";
            // 
            // fgTagTextBox
            // 
            this.fgTagTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.fgTagTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.fgTagTextBox.Location = new System.Drawing.Point(90, 82);
            this.fgTagTextBox.Name = "fgTagTextBox";
            this.fgTagTextBox.ReadOnly = true;
            this.fgTagTextBox.Size = new System.Drawing.Size(100, 23);
            this.fgTagTextBox.TabIndex = 5;
            // 
            // submitButton
            // 
            this.submitButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.submitButton.BackColor = System.Drawing.Color.MediumAquamarine;
            this.submitButton.Location = new System.Drawing.Point(174, 14);
            this.submitButton.Name = "submitButton";
            this.submitButton.Size = new System.Drawing.Size(54, 20);
            this.submitButton.TabIndex = 8;
            this.submitButton.Text = "Submit";
            this.submitButton.Click += new System.EventHandler(this.submitButton_Click);
            // 
            // fgPickingDataGrid
            // 
            this.fgPickingDataGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.fgPickingDataGrid.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.fgPickingDataGrid.Location = new System.Drawing.Point(12, 121);
            this.fgPickingDataGrid.Name = "fgPickingDataGrid";
            this.fgPickingDataGrid.Size = new System.Drawing.Size(216, 131);
            this.fgPickingDataGrid.TabIndex = 9;
            this.fgPickingDataGrid.TableStyles.Add(this.dataGridTableStyle1);
            this.fgPickingDataGrid.KeyDown += new System.Windows.Forms.KeyEventHandler(this.fgPickingDataGrid_KeyDown);
            // 
            // dataGridTableStyle1
            // 
            this.dataGridTableStyle1.GridColumnStyles.Add(this.dataGridTextBoxColumn1);
            this.dataGridTableStyle1.GridColumnStyles.Add(this.dataGridTextBoxColumn2);
            this.dataGridTableStyle1.GridColumnStyles.Add(this.dataGridTextBoxColumn3);
            this.dataGridTableStyle1.GridColumnStyles.Add(this.dataGridTextBoxColumn4);
            this.dataGridTableStyle1.GridColumnStyles.Add(this.dataGridTextBoxColumn5);
            this.dataGridTableStyle1.MappingName = "PickingDatatable";
            // 
            // dataGridTextBoxColumn1
            // 
            this.dataGridTextBoxColumn1.Format = "";
            this.dataGridTextBoxColumn1.FormatInfo = null;
            this.dataGridTextBoxColumn1.HeaderText = "Location";
            this.dataGridTextBoxColumn1.MappingName = "Location";
            this.dataGridTextBoxColumn1.NullText = "";
            this.dataGridTextBoxColumn1.Width = 70;
            // 
            // dataGridTextBoxColumn2
            // 
            this.dataGridTextBoxColumn2.Format = "";
            this.dataGridTextBoxColumn2.FormatInfo = null;
            this.dataGridTextBoxColumn2.HeaderText = "Part No.";
            this.dataGridTextBoxColumn2.MappingName = "Part No.";
            this.dataGridTextBoxColumn2.NullText = "";
            this.dataGridTextBoxColumn2.Width = 70;
            // 
            // dataGridTextBoxColumn3
            // 
            this.dataGridTextBoxColumn3.Format = "";
            this.dataGridTextBoxColumn3.FormatInfo = null;
            this.dataGridTextBoxColumn3.HeaderText = "Needed Qty.";
            this.dataGridTextBoxColumn3.MappingName = "Needed Qty.";
            this.dataGridTextBoxColumn3.NullText = "0";
            this.dataGridTextBoxColumn3.Width = 80;
            // 
            // dataGridTextBoxColumn4
            // 
            this.dataGridTextBoxColumn4.Format = "";
            this.dataGridTextBoxColumn4.FormatInfo = null;
            this.dataGridTextBoxColumn4.HeaderText = "Qty.";
            this.dataGridTextBoxColumn4.MappingName = "Qty.";
            this.dataGridTextBoxColumn4.NullText = "0";
            this.dataGridTextBoxColumn4.Width = 80;
            // 
            // dataGridTextBoxColumn5
            // 
            this.dataGridTextBoxColumn5.Format = "";
            this.dataGridTextBoxColumn5.FormatInfo = null;
            this.dataGridTextBoxColumn5.HeaderText = "Completed";
            this.dataGridTextBoxColumn5.MappingName = "Completed";
            this.dataGridTextBoxColumn5.NullText = "N";
            this.dataGridTextBoxColumn5.Width = 90;
            // 
            // clearButton
            // 
            this.clearButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.clearButton.BackColor = System.Drawing.Color.MediumAquamarine;
            this.clearButton.Location = new System.Drawing.Point(114, 14);
            this.clearButton.Name = "clearButton";
            this.clearButton.Size = new System.Drawing.Size(54, 20);
            this.clearButton.TabIndex = 8;
            this.clearButton.Text = "Clear";
            this.clearButton.Click += new System.EventHandler(this.clearButton_Click);
            // 
            // FGPickingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 267);
            this.Controls.Add(this.fgPickingDataGrid);
            this.Controls.Add(this.clearButton);
            this.Controls.Add(this.submitButton);
            this.Controls.Add(this.fgTagTextBox);
            this.Controls.Add(this.deliveryOrderTagTextbox);
            this.Controls.Add(this.fgTagLabel);
            this.Controls.Add(this.deliveryOrderTagLabel);
            this.Controls.Add(this.menuButton);
            this.Name = "FGPickingForm";
            this.Text = "FG Picking";
            this.Load += new System.EventHandler(this.FGPickingForm_Load);
            this.Activated += new System.EventHandler(this.FGPickingForm_Activated);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox deliveryOrderTagTextbox;
        private System.Windows.Forms.Label deliveryOrderTagLabel;
        private System.Windows.Forms.Button menuButton;
        private System.Windows.Forms.Label fgTagLabel;
        private System.Windows.Forms.TextBox fgTagTextBox;
        private System.Windows.Forms.Button submitButton;
        private System.Windows.Forms.DataGrid fgPickingDataGrid;
        private System.Windows.Forms.Button clearButton;
        private System.Windows.Forms.DataGridTableStyle dataGridTableStyle1;
        private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxColumn1;
        private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxColumn2;
        private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxColumn3;
        private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxColumn4;
        private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxColumn5;
    }
}