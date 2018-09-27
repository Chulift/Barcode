using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using FATHBarcode.Config;

namespace FATHBarcode.View.Object
{
    public partial class MenuForm : Form, IMenuView
    {
        public MenuForm()
        {
            InitializeComponent();
        }

        #region IMenuView Members

        public FATHBarcode.Presenter.MenuPresenter Presenter
        {
            private get;
            set;
        }

        public void SetMenuTag(int menu, byte tag)
        {
            switch (menu)
            {
                case 0: firstMenuButton.Tag = tag; break;
                case 1: secondMenuButton.Tag = tag; break;
                default:
                    break;
            }
        }

        public void SetMenuText(int menu, string text)
        {
            switch (menu)
            {
                case 0: firstMenuButton.Text = text; break;
                case 1: secondMenuButton.Text = text; break;
                default:
                    break;
            }
        }

        public void SetVisible(int menu, bool visibility)
        {
            switch (menu)
            {
                case 0: firstMenuButton.Visible = visibility; break;
                case 1: secondMenuButton.Visible = visibility; break;
            }
        }

        public void Menu_Click(object sender, EventArgs e)
        {
            var menu = (Button)sender;
            var view = new ScanDeliveryOrderTagForm();
            var repository = new Model.Object.ScanDeliveryOrderTagRepository();
            var presenter = new Presenter.ScanDeliveryOrderTagPresenter(view, repository);
            presenter.NextScreen = ((Settings.Screen)Enum.Parse(typeof(Settings.Screen), menu.Tag.ToString(), false));
            view.Closed += (s, args) => this.Show();
            this.Hide();
            view.ShowDialog();
        }

        #endregion

        #region BaseView Members

        public void ShowMessage(string caption, string message)
        {
            MessageBox.Show(message, caption);
        }

        public void OnScanData(string data)
        {
            
        }

        #endregion

    }
}