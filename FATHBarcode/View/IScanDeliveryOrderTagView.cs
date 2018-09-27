using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FATHBarcode.View
{
    public interface IScanDeliveryOrderTagView : Base.IView
    {
        Presenter.ScanDeliveryOrderTagPresenter Presenter { set; }

        void SetTitle(string title);

        void SetDeliveryOrderTagLabel(string newLabel);

        void OpenFGPickingScreen(string deliveryOrderTag);

        void OpenShippingScreen(string deliveryOrderTag);
    }
}
