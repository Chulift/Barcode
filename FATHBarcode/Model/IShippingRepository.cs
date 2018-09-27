using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FATHBarcode.Model.Object;

namespace FATHBarcode.Model
{
    public interface IShippingRepository
    {
        bool DuplicateTagNo(GenericCode data);

        Shipping GetShipping(string deliveryOrderTag);

        List<Shipping> GetShippingItems(string deliveryOrderTag);
        List<Shipping> GetFATHItems(string deliveryOrderTag);

        int UpdateCustomerPathNo(GenericCode code);

        int UpdateCustomerQty(GenericCode code);

        int UpdateFATHTag(GenericCode code);

        bool IsTagNoDuplicate(GenericCode code);

        int SendDataToWebService(string deliveryOrderTag);

        int ClearAllData(string deliveryOrderTag);

        void ClearOperationLog(string deliveryOrderTag, string menuId);
    }
}
