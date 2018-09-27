using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FATHBarcode.View
{
    public interface ILoginView : Base.IView
    {
        Presenter.LoginPresenter Presenter { set; }

        void GoToMenuScreen(FATHBarcode.Model.Object.AuthenticationResponse auth);
    }
}
