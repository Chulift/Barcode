using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FATHBarcode.Model.Object;

namespace FATHBarcode.Model
{
    public interface IFGPickingRepository
    {
        List<FGPicking> GetPickingDetailList(FGPicking baseFGPicking);

        int Remove(FGPicking fGPicking);

        FGPicking GetFGPicking(string deliveryOrderTag);

        List<FGPicking> GetPickedItems(string _deliveryOrderTag);
        List<FGPicking> GetFATHList(FGPicking _baseFGPicking);
        bool IsTagNoDuplicate(FATHBarcode.Model.Object.GenericCode fathTag);

        int InsertFGPicking(string deliveryOrderTag, string location, string partNo, string tagNo, int qty);

        bool DeleteAllFGData(string deliveryOrderTag);

        int SendDataToWebService(string referenceNo);



        int RemoveFATHTag(FGPicking fGPicking);
    }
}
