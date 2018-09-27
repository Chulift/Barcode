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
    public partial class ScanDeliveryOrderTagForm : Form, IScanDeliveryOrderTagView
    {
        MsgWindow _msg;
        public ScanDeliveryOrderTagForm()
        {
            InitializeComponent();
            _msg = new MsgWindow(this);
            this.Closed += (s, args) => _msg.Dispose();
        }

        #region IScanDeliveryOrderTagView Members

        public FATHBarcode.Presenter.ScanDeliveryOrderTagPresenter Presenter
        {
            private get;
            set;
        }

        public void SetTitle(string title)
        {
            this.Text = title;
        }

        public void SetDeliveryOrderTagLabel(string newLabel)
        {
            deliveryOrderTagLabel.Text = newLabel;
        }

        public void OpenFGPickingScreen(string deliveryOrderTag)
        {
            var view = new FGPickingForm();
            var repository = new Model.Object.FGPickingRepository();
            var presenter = new Presenter.FGPickingPresenter(view, repository, deliveryOrderTag);
            presenter.Screen = Presenter.NextScreen;
            view.Closed += (s, args) =>
            {
                _msg = new MsgWindow(this);
                this.Show();
            };
            this.Hide();
            view.Show();
        }

        public void OpenShippingScreen(string deliveryOrderTag)
        {
            var view = new ShippingForm();
            var repository = new Model.Object.ShippingRepository();
            var presenter = new Presenter.ShippingPresenter(view, repository, deliveryOrderTag);
            presenter.Screen = Presenter.NextScreen;
            view.Closed += (s, args) =>
            {
                _msg = new MsgWindow(this);
                this.Show();
            };
            this.Hide();
            _msg.Dispose();
            view.Show();
        }

        #endregion

        #region BaseView Members

        public void ShowMessage(string caption, string message)
        {
            MessageBox.Show(message, caption);
        }

        public void OnScanData(string data)
        {
            if (deliveryOrderTagTextbox.Focused)
            {
                deliveryOrderTagTextbox.Text = data;
                Presenter.ProcessScanDeliveryOrderTag(deliveryOrderTagTextbox.Text);
            }
        }

        #endregion

        private void menuButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void deliveryOrderTagTextbox_GotFocus(object sender, EventArgs e)
        {
            try
            {
                Bt.ScanLib.Control.btScanEnable();
            }
            catch (MissingMethodException)
            {
            }
        }

        private void deliveryOrderTagTextbox_LostFocus(object sender, EventArgs e)
        {
            try
            {
                Bt.ScanLib.Control.btScanDisable();
            }
            catch (MissingMethodException)
            {
            }
        }

        private void ScanDeliveryOrderTagForm_Load(object sender, EventArgs e)
        {
            deliveryOrderTagTextbox.Focus();
        }

        private void ScanDeliveryOrderTagForm_Activated(object sender, EventArgs e)
        {
            deliveryOrderTagTextbox.Focus();
        }

        private void deliveryOrderTagTextbox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                Presenter.ProcessScanDeliveryOrderTag(deliveryOrderTagTextbox.Text);
            }
        }
    }
}