using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FATHBarcode.View
{
    public interface IMenuView : Base.IView
    {
        Presenter.MenuPresenter Presenter { set; }

        void SetMenuTag(int menu, byte tag);

        void SetMenuText(int menu, string text);
        
        void SetVisible(int menu, bool visibility);

        void Menu_Click(object sender, EventArgs e);
    }
}
