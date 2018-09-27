using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FATHBarcode.View.Object
{
    public partial class ShippingForm : Form , IShippingView
    {
        private MsgWindow _msg;

        public ShippingForm()
        {
            InitializeComponent();
            _msg = new MsgWindow(this);
            this.Closed += (s, args) => _msg.Dispose();
        }

        #region IShippingView Members

        public FATHBarcode.Presenter.ShippingPresenter Presenter
        {
            private get;
            set;
        }

        public bool EnableCustomerTag
        {
            set
            {
                customerTagTextBox.Enabled = value;
                customerTagQtyTextbox.Enabled = value;
                if (value)
                {
                    this.customerTagTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
                    this.customerTagQtyTextbox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
                }
                else
                {
                    this.customerTagTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
                    this.customerTagQtyTextbox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
                }
            }
        }

        public string DeliveryOrderTag
        {
            set { deliveryOrderTagTextbox.Text = value; }
        }

        public void Bind(DataSet ds)
        {
            shippingDataGrid.DataSource = ds.Tables[0];
        }

        #endregion

        #region IView Members


        public void ShowMessage(string caption, string message)
        {
            MessageBox.Show(message, caption);
        }

        public void OnScanData(string data)
        {
            if (customerTagTextBox.Focused)
            {
                customerTagTextBox.Text = data;
                Presenter.ProcessScanCustomerTag(data);
            }
            else if (customerTagQtyTextbox.Focused)
            {
                customerTagQtyTextbox.Text = data;
                Presenter.ProcessScanCustomerQty(data);
            }
            else if (fathTagTextBox.Focused)
            {
                fathTagTextBox.Text = data;
                Presenter.ProcessScanFATHTag(data);
            }//else ni-ten
        }

        #endregion

        #region Private Members

        private void customerTagTextBox_GotFocus(object sender, EventArgs e)
        {
            try
            {
                Bt.ScanLib.Control.btScanEnable();
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        private void fathTagTextBox_GotFocus(object sender, EventArgs e)
        {
            try
            {
                Bt.ScanLib.Control.btScanEnable();
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        private void customerTagTextBox_LostFocus(object sender, EventArgs e)
        {
            try
            {
                Bt.ScanLib.Control.btScanDisable();
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        private void fathTagTextBox_LostFocus(object sender, EventArgs e)
        {
            try
            {
                Bt.ScanLib.Control.btScanDisable(); 
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        private void ShippingForm_Load(object sender, EventArgs e)
        {
            customerTagTextBox.Focus();
        }

        private void customerTagQtyTextbox_GotFocus(object sender, EventArgs e)
        {
            try
            {
                Bt.ScanLib.Control.btScanEnable();
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void customerTagQtyTextbox_LostFocus(object sender, EventArgs e)
        {
            try
            {
                Bt.ScanLib.Control.btScanDisable();
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void shippingDataGrid_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                var fgPicking = new FATHBarcode.Model.Object.FGPicking();
                fgPicking["Delivery Order Tag"] = deliveryOrderTagTextbox.Text;
                fgPicking["Part No."] = shippingDataGrid[shippingDataGrid.CurrentCell.RowNumber, 0].ToString();
                var view = new FGPickingDetail();
                var repository = new FATHBarcode.Model.Object.FGPickingRepository();
                var presenter = new Presenter.FGPickingDetailPresenter(view, repository, fgPicking, Presenter.Screen);
                view.ShowDialog();
                if (presenter.DeleteFrag)
                {
                    Presenter.UpdateAllQty();
                }
            }
        }

        private void submitButton_Click(object sender, EventArgs e)
        {
            if (Presenter.ValidateSubmit())
            {
                DialogResult dr = MessageBox.Show(Properties.Resources.CFM_01, "CFM_01", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                if (dr == DialogResult.Yes)
                {// Call WS
                    if (Presenter.Submit() > 0)
                        this.Close();
                    else
                    {
                        ShowMessage("", "Cannot insert data");
                    }
                }
                else
                {
                }
            }
        }

        private void menuButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion

        private void clearButton_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show(Properties.Resources.CFM_03, "CFM_03", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (dr == DialogResult.Yes)
            {
                if (Presenter.ClearAllData() > 0)
                    this.Close();
            }
        }

    }
}