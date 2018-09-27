using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Security.Cryptography;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Bt.SysLib;
using Bt;
using FATHBarcode.Base;

namespace FATHBarcode.View.Object
{
    public partial class LoginForm : Form, ILoginView
    {
        MsgWindow _msg;
        public LoginForm()
        {
            InitializeComponent();
            // Enable auto scan
            _msg = new MsgWindow(this);
            this.Closed += (s, args) => _msg.Dispose();
        }

        #region ILoginView Members

        public FATHBarcode.Presenter.LoginPresenter Presenter
        {
            private get;
            set;
        }

        public void GoToMenuScreen(FATHBarcode.Model.Object.AuthenticationResponse auth)
        {
            var view = new MenuForm();
            var repository = new Model.Object.MenuRepository();
            var presenter = new Presenter.MenuPresenter(view, repository, auth);
            // Set default cursor;
            Cursor.Current = Cursors.Default;
            view.Closed += (s, args) => this.Close();
            this.Hide();
            view.Show();
        }

        #endregion

        #region BaseView Members

        public void ShowMessage(string caption, string message)
        {
            Cursor.Current = Cursors.Default;
            MessageBox.Show(message, caption);
        }

        public void OnScanData(string data)
        {
            userIdTextBox.Text = data;
            if (!string.IsNullOrEmpty(data.Trim()))
            {
                passwordTextBox.Text = userIdTextBox.Text;//test
                passwordTextBox.Focus();
            }      
        }

        #endregion

        private void userIdTextBox_GotFocus(object sender, EventArgs e)
        {
            try
            {
                Bt.ScanLib.Control.btScanEnable();
            }
            catch (MissingMethodException)
            {
                throw;
            }
        }

        private void userIdTextBox_LostFocus(object sender, EventArgs e)
        {
            try
            {
                Bt.ScanLib.Control.btScanDisable();
            }
            catch (MissingMethodException)
            {
                throw;
            }
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            userIdTextBox.Focus();
        }

        private void loginButton_Click(object sender, EventArgs e)
        {
            Login();
        }

        private void Login()
        {
            Cursor.Current = Cursors.WaitCursor;
            if (isDataValid(userIdTextBox.Text, passwordTextBox.Text))
            {
                Presenter.ProcessScanUserId(userIdTextBox.Text, passwordTextBox.Text);
            }
            else
            {
                ShowMessage("Data invalid", "Please fill the boxs");
            }
            Cursor.Current = Cursors.Default;
        }

        private bool isDataValid(string userId, string password)
        {
            return (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(password));
        }

        private void passwordTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                Login();
            }
        }
    }
}