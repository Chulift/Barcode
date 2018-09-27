using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

using Bt;
using Bt.SysLib;

namespace HTHelper.Scanners
{
    class KeyenceScanner : IBarcodeScanner
    {
        #region IBarcodeScanner Members

        public void DisableScanner()
        {
            Device.btSetKeyEnable(LibDef.BT_KEY_ENABLEALL & ~(LibDef.BT_KEY_STRG | LibDef.BT_KEY_CTRG));
        }

        public void EnableScanner()
        {
            Device.btSetKeyEnable(LibDef.BT_KEY_ENABLEALL);
        }

        public void DisableTouchScreen()
        {
            Device.btSetTouchpanelEnable(0);
        }

        public void EnableTouchScreen()
        {
            Device.btSetTouchpanelEnable(1);
        }

        #endregion
    }
}
