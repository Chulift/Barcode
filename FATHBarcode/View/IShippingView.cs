using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FATHBarcode.View
{
    public interface IShippingView : Base.IView
    {
        Presenter.ShippingPresenter Presenter { set; }

        bool EnableCustomerTag { set; }

        string DeliveryOrderTag { set; }

        void Bind(System.Data.DataSet ds);
    }
}
