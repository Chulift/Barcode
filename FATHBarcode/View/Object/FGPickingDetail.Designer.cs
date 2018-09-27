namespace FATHBarcode.View.Object
{
    partial class FGPickingDetail
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
            this.label1 = new System.Windows.Forms.Label();
            this.partNoLabel = new System.Windows.Forms.Label();
            this.deleteButton = new System.Windows.Forms.Button();
            this.fgPickingDetailListView = new System.Windows.Forms.ListView();
            this.selectAllCheckBox = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(17, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 20);
            this.label1.Text = "Part No. : ";
            // 
            // partNoLabel
            // 
            this.partNoLabel.Location = new System.Drawing.Point(85, 11);
            this.partNoLabel.Name = "partNoLabel";
            this.partNoLabel.Size = new System.Drawing.Size(76, 20);
            // 
            // deleteButton
            // 
            this.deleteButton.Location = new System.Drawing.Point(167, 11);
            this.deleteButton.Name = "deleteButton";
            this.deleteButton.Size = new System.Drawing.Size(57, 20);
            this.deleteButton.TabIndex = 2;
            this.deleteButton.Text = "Delete";
            this.deleteButton.Click += new System.EventHandler(this.deleteButton_Click);
            // 
            // fgPickingDetailListView
            // 
            this.fgPickingDetailListView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.fgPickingDetailListView.CheckBoxes = true;
            this.fgPickingDetailListView.FullRowSelect = true;
            this.fgPickingDetailListView.Location = new System.Drawing.Point(15, 58);
            this.fgPickingDetailListView.Name = "fgPickingDetailListView";
            this.fgPickingDetailListView.Size = new System.Drawing.Size(209, 128);
            this.fgPickingDetailListView.TabIndex = 3;
            this.fgPickingDetailListView.View = System.Windows.Forms.View.Details;
            // 
            // selectAllCheckBox
            // 
            this.selectAllCheckBox.Location = new System.Drawing.Point(15, 32);
            this.selectAllCheckBox.Name = "selectAllCheckBox";
            this.selectAllCheckBox.Size = new System.Drawing.Size(146, 20);
            this.selectAllCheckBox.TabIndex = 6;
            this.selectAllCheckBox.Text = "Select/UnSelect All";
            this.selectAllCheckBox.Click += new System.EventHandler(this.selectAllCheckBox_Click);
            // 
            // FGPickingDetail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 198);
            this.Controls.Add(this.selectAllCheckBox);
            this.Controls.Add(this.fgPickingDetailListView);
            this.Controls.Add(this.deleteButton);
            this.Controls.Add(this.partNoLabel);
            this.Controls.Add(this.label1);
            this.Name = "FGPickingDetail";
            this.Text = "FG Picking Detail";
            this.Load += new System.EventHandler(this.FGPickingDetail_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label partNoLabel;
        private System.Windows.Forms.Button deleteButton;
        private System.Windows.Forms.ListView fgPickingDetailListView;
        private System.Windows.Forms.CheckBox selectAllCheckBox;
    }
}