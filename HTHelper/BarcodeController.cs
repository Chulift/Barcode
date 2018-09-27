using System;
using System.Collections.Generic;
using System.Text;

namespace HTHelper
{
    public class BarcodeController : IDisposable
    {
        private System.Windows.Forms.TextBox textBox = null;

        //private static void DisableScanner()
        //{
        //    ReaderFactory.GetReader().DisableScanner();
        //}

        //private static void EnableScanner()
        //{
        //    ReaderFactory.GetReader().EnableScanner();
        //}

        public static void DisableTouchScreen()
        {
            ReaderFactory.GetReader().DisableTouchScreen();
        }

        public static void EnableTouchScreen()
        {
            ReaderFactory.GetReader().EnableTouchScreen();
        }

        public static void EnableScanner()
        {
            ReaderFactory.GetReader().EnableScanner();
        }

        public BarcodeController()
            : this(null)
        {
        }

        public BarcodeController(object textBox)
        {
            if (textBox != null && textBox.GetType() == typeof(System.Windows.Forms.TextBox))
            {
                this.textBox = (System.Windows.Forms.TextBox)textBox;
                this.textBox.Enabled = false;
            }

            ReaderFactory.GetReader().DisableScanner();
        }

        #region IDisposable Members

        public void Dispose()
        {
            ReaderFactory.GetReader().EnableScanner();

            if (textBox != null)
            {
                textBox.Enabled = true;
                textBox.Focus();
            }
        }

        #endregion
    }
}
