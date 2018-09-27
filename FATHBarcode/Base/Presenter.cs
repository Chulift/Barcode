using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace FATHBarcode.Base
{
    public class Presenter
    {
        protected Presenter(string menuId)
        {
            this.menuId = menuId;
            errFlag = false;
            errMsg = "";
        }

        protected string menuId;
        protected bool errFlag;
        protected string errMsg;
    }
}
