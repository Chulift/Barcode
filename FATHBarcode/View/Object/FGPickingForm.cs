using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using FATHBarcode.Model.Object;

namespace FATHBarcode.View.Object
{
    public partial class FGPickingForm : Form, IFGPickingView
    {
        MsgWindow _msg;

        public FGPickingForm()
        {
            InitializeComponent();
            _msg = new MsgWindow(this);
            this.Closed += (s, args) => _msg.Dispose();            
        }

        private void menuButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #region IFGPickingView Members

        public FATHBarcode.Presenter.FGPickingPresenter Presenter
        {
            private get;
            set;
        }

        public string DeliveryOrderTag
        {
            set { deliveryOrderTagTextbox.Text = value; }
        }

        public void Bind(DataSet ds)
        {
            fgPickingDataGrid.DataSource = ds.Tables[0];
        }

        public void FocusCell(int row, int column)
        {
            fgPickingDataGrid.Select(row);
            fgPickingDataGrid.CurrentCell = new DataGridCell(row, column);
        }

        #endregion

        #region BaseView Members

        public void ShowMessage(string caption, string message)
        {
            MessageBox.Show(message, caption);
        }

        public void ComfirmDialog(string caption, string message)
        {
            DialogResult dr = MessageBox.Show(message, caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
            if (dr == DialogResult.Yes)
            {
                // Validate
                // Call WS
            }
            // Clear data
        }

        #endregion

        private void FGPickingForm_Load(object sender, EventArgs e)
        {
            try
            {
                Bt.ScanLib.Control.btScanEnable();
                fgTagTextBox.Focus();
            }
            catch (MissingMethodException)
            {
            }
        }

        private void FGPickingForm_Activated(object sender, EventArgs e)
        {
            try
            {
                Bt.ScanLib.Control.btScanEnable();
                fgTagTextBox.Focus();
            }
            catch (MissingMethodException)
            {
            }
        }

        private void fgPickingDataGrid_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                var fgPicking = new FATHBarcode.Model.Object.FGPicking();
                fgPicking["Delivery Order Tag"] = deliveryOrderTagTextbox.Text;
                fgPicking["Part No."] = fgPickingDataGrid[fgPickingDataGrid.CurrentCell.RowNumber, 1].ToString();
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

        #region IView Members

        public void OnScanData(string data)
        {

            Presenter.ProcessScanFGTag(data);
            fgTagTextBox.Text = Presenter.GetFGTag();
            /*
            if (!string.IsNullOrEmpty(fgTagTextBox.Text.Trim()))
            {
                if (Presenter.InsertFGTag())
                {
                    fgTagTextBox.Text = "";
                    Presenter.ClearCode();
                }
            }
            */
        }

        #endregion

        private void submitButton_Click(object sender, EventArgs e)
        {
            if (Presenter.ValidateSubmit())
            {
                DialogResult dr = MessageBox.Show(Properties.Resources.CFM_01, "CFM_01", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                if (dr == DialogResult.Yes)
                {// Call WS
                    Presenter.Submit();   
                }
                else
                {
                }
            }
        }

        private void clearButton_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show(Properties.Resources.CFM_03, "CFM_03", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (dr == DialogResult.Yes)
            {
                if (Presenter.ClearData())
                    GoBackToScanDeliveryOrderTag();
            }
            else
            {
            }
        }

        public void GoBackToScanDeliveryOrderTag()
        {
            this.Close();
        }
    }
}