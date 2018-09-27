using System;
using System.Linq;
using System.Data;
using System.Collections.Generic;
using System.Text;
using FATHBarcode.View;
using FATHBarcode.Model;
using FATHBarcode.Config;
using CommonLibrary.IO;
using FATHBarcode.Model.Object;

namespace FATHBarcode.Presenter
{
    public class ScanDeliveryOrderTagPresenter : Base.Presenter
    {
        private readonly IScanDeliveryOrderTagView _view;
        private readonly IScanDeliveryOrderTagRepository _repository;
        private Settings.Screen _nextScreen;

        public Settings.Screen NextScreen
        {
            get { return _nextScreen; }
            set
            {
                _nextScreen = value;
                SetCaption();
            }
        }

        public ScanDeliveryOrderTagPresenter(IScanDeliveryOrderTagView view, IScanDeliveryOrderTagRepository repository) : base("")
        {
            _view = view;
            view.Presenter = this;
            _repository = repository;
        }

        private void SetCaption()
        {
            switch (NextScreen)
            {
                case Settings.Screen.FGPicking:
                    _view.SetTitle("FG Picking");
                    _view.SetDeliveryOrderTagLabel("Ref No.");
                    break;
                case Settings.Screen.Shipping:
                    _view.SetTitle("Shipping");
                    break;
                default:
                    break;
            }
        }

        internal void ProcessScanDeliveryOrderTag(string deliveryOrderTag)
        {
            bool isDeliveryOrderExists = _repository.CheckExistingDeliveryOrderTagInDatabase(deliveryOrderTag);
            if (isDeliveryOrderExists)
            {

                switch (NextScreen)
                {
                    case Settings.Screen.FGPicking:
                        _view.OpenFGPickingScreen(deliveryOrderTag);
                        SaveLog(deliveryOrderTag, "");
                        break;
                    case Settings.Screen.Shipping:
                        _view.OpenShippingScreen(deliveryOrderTag);
                        SaveLog(deliveryOrderTag, "");
                        break;
                    default:
                        break;
                }
            }
            else
            {
                _view.ShowMessage("Error", Properties.Resources.ERR_02);
                SaveLog(deliveryOrderTag, "ERR_02");
            }
        }

        private void SaveLog(string deliveryOrderTag, string error)
        {
            OperationLog.ErrorFlag errFlag = error == "" ? OperationLog.ErrorFlag.NotError : OperationLog.ErrorFlag.Error;
            var menu = NextScreen == Settings.Screen.FGPicking ? "M01" : "M02";
            var fieldName = NextScreen == Settings.Screen.FGPicking ? "RefNo" : "DOTag";
            var log = new OperationLog(
                    menu,
                    Session.GetCurrentUser().UserID,
                    OperationLog.LogEvent.Scan,
                    fieldName,
                    deliveryOrderTag,
                    errFlag,
                    error,
                    DateTime.Now);
            log.SaveLog();
        }
    }
}
