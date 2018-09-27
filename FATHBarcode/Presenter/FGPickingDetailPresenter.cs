using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using FATHBarcode.Model;
using FATHBarcode.View;
using FATHBarcode.Model.Object;
using FATHBarcode.Config;

namespace FATHBarcode.Presenter
{
    public class FGPickingDetailPresenter : Base.Presenter
    {
        private readonly IFGPickingDetailView _view;
        private readonly IFGPickingRepository _repository;
        private FGPicking _baseFGPicking;
        private List<FGPicking> _fgPickingList;
        public bool DeleteFrag { get; set; }
        private Settings.Screen Screen { get; set; }

        public FGPickingDetailPresenter(IFGPickingDetailView view, IFGPickingRepository repository, FGPicking fgPicking, Settings.Screen page)
            : base(page == Settings.Screen.FGPicking ? "M01" : "M02")
        {
            _view = view;
            view.Presenter = this;
            _repository = repository;
            _baseFGPicking = fgPicking;
            Screen = page;
        }

        public void GetPickingDetailList()
        {
            switch (Screen)
            {
                case Settings.Screen.FGPicking:
                    _fgPickingList = _repository.GetPickingDetailList(_baseFGPicking);
                    break;
                case Settings.Screen.Shipping:
                    _fgPickingList = _repository.GetFATHList(_baseFGPicking);
                    break;
                default:
                    break;
            }
            _view.Bind(_fgPickingList);
        }

        internal void DeleteItem(int index)
        {
            DeleteFrag = true;
            int rowAffect = 0;
            try
            {
                switch (Screen)
                {
                    case Settings.Screen.FGPicking:
                        rowAffect = _repository.Remove(_fgPickingList[index]);
                        break;
                    case Settings.Screen.Shipping:
                        rowAffect = _repository.RemoveFATHTag(_fgPickingList[index]);
                        break;
                    default:
                        break;
                }
            }
            catch (Exception)
            {
            }
            
            if (rowAffect > 0)
            {
                _fgPickingList.RemoveAt(index);
            }
            SaveLog(OperationLog.LogEvent.ClickButton, "Delete", "");
        }

        private void SaveLog(OperationLog.LogEvent logEvent, string source, string value)
        {
            OperationLog ol = new OperationLog(this.menuId, Session.GetCurrentUser().UserID, logEvent, source, value,
                errFlag == true ? OperationLog.ErrorFlag.Error : OperationLog.ErrorFlag.NotError, errMsg, DateTime.Now);
            ol.SaveLog();
        }

        internal void DeleteItem(string tagNo)
        {
            DeleteFrag = true;
            int rowAffect = 0;
            FGPicking deletedItem = null;
            foreach (var item in _fgPickingList)
            {
                try
                {
                    
                    if (item.TagNo.Equals(tagNo))
                    {
                        deletedItem = item;
                        switch (Screen)
                        {
                            case Settings.Screen.FGPicking:
                                rowAffect = _repository.Remove(item);
                                break;
                            case Settings.Screen.Shipping:
                                rowAffect = _repository.RemoveFATHTag(item);
                                break;
                            default:
                                break;
                        }
                        break;
                    }
                }
                catch(Exception)
                {
                }
            }
            if (rowAffect > 0 && deletedItem != null)
            {
                _fgPickingList.Remove(deletedItem);
            }
            SaveLog(OperationLog.LogEvent.ClickButton, "Delete", "");
        }
    }
}
