using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace FATHBarcode.Model.Object
{
    public class ScanDeliveryOrderTagRepository : IScanDeliveryOrderTagRepository
    {
        #region IScanDeliveryOrderTagRepository Members

        public bool CheckExistingDeliveryOrderTagInDatabase(string deliveryOrderTag)
        {
            // WS always return data
            return true;
        }

        #endregion
    }
}
