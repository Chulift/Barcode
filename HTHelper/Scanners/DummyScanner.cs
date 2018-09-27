using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace HTHelper.Scanners
{
    class DummyScanner : IBarcodeScanner
    {
        #region IBarcodeScanner Members

        public void DisableScanner()
        {
        }

        public void EnableScanner()
        {
        }

        public void DisableTouchScreen()
        {
            throw new NotImplementedException();
        }

        public void EnableTouchScreen()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
