using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

using HTHelper.Scanners;

namespace HTHelper
{
    class ReaderFactory
    {
        private static IBarcodeScanner reader = null;

        public static IBarcodeScanner GetReader()
        {
            if (Environment.OSVersion.Platform != PlatformID.WinCE)
                return new DummyScanner();

            var oemInfo = BarcodeDevice.GetManufacturerInfo();
            oemInfo = oemInfo.ToUpper();

            // HP101: CASIO DT-X8-20E
            if (new string[] { "HP101" }.Contains(oemInfo))
            {
                return new CasioScanner();
            }
            // BT-W: KEYENCE BT-W85G
            else if (new string[] { "BT-W" }.Contains(oemInfo))
            {
                return new KeyenceScanner();
            }

            return new DummyScanner();

        }
    }
}
