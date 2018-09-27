using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

using Calib;

namespace HTHelper.Scanners
{
    class CasioScanner : IBarcodeScanner
    {
        #region IBarcodeScanner Members

        public void DisableScanner()
        {
            // Re-map center trigger key to right trigger key.
            SystemLibNet.Api.SysSetNormalUserDefineKey(SystemLibNet.Def.KEYID_CENTERTRIGGER, new int[] { SystemLibNet.Def.KEYID_RIGHTTRIGGER | SystemLibNet.Def.KEYBD_DEVICE_SILENT });

            // Disabled trigger keys (except for center trigger key)
            SystemLibNet.Api.SysSetEnableTriggerKey(false);

            // Force use defined keys.
            SystemLibNet.Api.SysSetUserDefineKeyState(true);

            //// Lock all keys.
            //SystemLibNet.Api.SysSetAllKeyLock(true);
        }

        public void EnableScanner()
        {
            //SystemLibNet.Api.SysSetAllKeyLock(false);
            SystemLibNet.Api.SysSetUserDefineKeyState(false);
            SystemLibNet.Api.SysSetEnableTriggerKey(true);
            SystemLibNet.Api.SysDeleteUserDefineKey(SystemLibNet.Def.KEYID_CENTERTRIGGER);
        }

        public void DisableTouchScreen()
        {
            SystemLibNet.Api.SysTouchPanelOff();
        }

        public void EnableTouchScreen()
        {
            SystemLibNet.Api.SysTouchPanelOn();
        }

        #endregion
    }
}
