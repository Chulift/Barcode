using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FATHBarcode.View
{
    public interface IFGPickingDetailView
    {
        Presenter.FGPickingDetailPresenter Presenter {set;}

        void Bind(List<FATHBarcode.Model.Object.FGPicking> fgPickingList);
    }
}
