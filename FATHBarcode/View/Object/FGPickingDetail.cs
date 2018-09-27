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
    public partial class FGPickingDetail : Form, IFGPickingDetailView
    {
        public FGPickingDetail()
        {
            InitializeComponent();
            InitListView();
        }

        private void InitListView()
        {
            fgPickingDetailListView.Columns.Add("Tag No.", 100, HorizontalAlignment.Left);
            fgPickingDetailListView.Columns.Add("Qty.", 50, HorizontalAlignment.Right);
        }

        private void FGPickingDetail_Load(object sender, EventArgs e)
        {
            Presenter.GetPickingDetailList();
        }

        #region IFGPickingDetailView Members

        public FATHBarcode.Presenter.FGPickingDetailPresenter Presenter
        {
            private get;
            set;
        }

        public void Bind(List<FATHBarcode.Model.Object.FGPicking> fgPickingList)
        {
            foreach (var item in fgPickingList)
            {
                var arr = new string[2];
                arr[0] = item["Tag No."].ToString();
                arr[1] = item["Qty."].ToString();
                var itm = new ListViewItem(arr);
                fgPickingDetailListView.Items.Add(itm);
            }
        }

        #endregion

        private void deleteButton_Click(object sender, EventArgs e)
        {
            List<ListViewItem> itemsToRemove = new List<ListViewItem>();
            foreach (ListViewItem item in fgPickingDetailListView.Items)
            {
                if (item.Checked)
                {
                    Presenter.DeleteItem(item.SubItems[0].Text);
                    itemsToRemove.Add(item);
                }
            }
            foreach (var item in itemsToRemove)
            {
                fgPickingDetailListView.Items.Remove(item);
            }
        }

        private void selectAllCheckBox_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem row in fgPickingDetailListView.Items)
            {
                switch (selectAllCheckBox.CheckState)
                {
                    case CheckState.Checked:
                        row.Checked = true;
                        break;
                    case CheckState.Indeterminate:
                        break;
                    case CheckState.Unchecked:
                        row.Checked = false;
                        break;
                    default:
                        break;
                } 
            }
        }
    }
}