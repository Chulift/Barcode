using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace FATHBarcode.Base
{
    public interface IView
    {
        void Show();
        void Hide();
        void Close();
        string Text { get; set; }

        void ShowMessage(string caption, string message);

        void OnScanData(string data);
    }
}
