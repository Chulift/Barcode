using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FATHBarcode.View
{
    public interface IFGPickingView : Base.IView
    {
        Presenter.FGPickingPresenter Presenter { set; }

        string DeliveryOrderTag { set; }

        void Bind(System.Data.DataSet ds);

        void GoBackToScanDeliveryOrderTag();

        void FocusCell(int row, int column);
    }
}
