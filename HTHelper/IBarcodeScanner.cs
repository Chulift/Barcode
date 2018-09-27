using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HTHelper
{
    interface IBarcodeScanner
    {
        void DisableScanner();
        void EnableScanner();
        void DisableTouchScreen();
        void EnableTouchScreen();
    }
}
