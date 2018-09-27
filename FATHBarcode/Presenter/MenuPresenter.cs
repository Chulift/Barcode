using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using FATHBarcode.View;
using FATHBarcode.Model;
using System.Net;
using System.Net.Sockets;
using FATHBarcode.Model.Object;
using FATHBarcode.Config;

namespace FATHBarcode.Presenter
{
    public class MenuPresenter
    {
        private readonly IMenuView _view;
        private readonly IMenuRepository _repository;
        private readonly AuthenticationResponse _auth;
        public AuthenticationResponse Auth { get { return _auth; } }

        public MenuPresenter(IMenuView view, IMenuRepository repository, AuthenticationResponse auth)
        {
            _view = view;
            view.Presenter = this;
            _repository = repository;
            _auth = auth;
            SetUpMenu(_auth);
        }

        private void SetUpMenu(AuthenticationResponse auth)
        {
            /*
            if (!Settings.CheckConnection())
            {
                // Show all menus
                _view.ShowMessage("Internet", "No connection.");
                _view.FirstMenuTag = (byte)Settings.Screen.FGPicking;
                _view.SecondMenuTag = (byte)Settings.Screen.Shipping;
                _view.SetVisible(1, true);
                _view.SetVisible(2, true);
            }
            */
            for (int i = 0; i < auth.MenuList.Count; i++)
            {
                switch (auth.MenuList[i])
                {
                    case "FG Picking":
                        _view.SetMenuTag(i, (byte)Settings.Screen.FGPicking);
                        break;
                    case "Shipping":
                        _view.SetMenuTag(i, (byte)Settings.Screen.Shipping);
                        break;
                    default:
                        break;
                }
                _view.SetMenuText(i, auth.MenuList[i]);
                _view.SetVisible(i, true);
            }
        }
    }
}
