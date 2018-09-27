using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using FATHBarcode.View;
using FATHBarcode.Model;
using FATHBarcode.Model.Object;
using System.Data;
using FATHBarcode.Config;
using CommonLibrary.IO;

namespace FATHBarcode.Presenter
{
    public class FGPickingPresenter : Base.Presenter
    {
        private readonly IFGPickingView _view;
        private readonly IFGPickingRepository _repository;
        private string _deliveryOrderTag;
        private GenericCode code;
        private FGPicking _fgPicking;
        private DataSet _dataSet;
        public Settings.Screen Screen { get; set; }

        public string GetFGTag()
        {
            try
            {
                return this.code == null ? "" : this.code.TagNo;
            }
            catch
            {
                return "";
            }
        }

        public FGPickingPresenter(IFGPickingView view, IFGPickingRepository repository, string deliveryOrderTag) : base("M01")
        {
            _view = view;
            view.Presenter = this;
            _repository = repository;
            _deliveryOrderTag = deliveryOrderTag;
            view.DeliveryOrderTag = deliveryOrderTag;
            ShowDetails();
        }

        public void ShowDetails()
        {
            _fgPicking = _repository.GetFGPicking(_deliveryOrderTag);
            CreateData();
            UpdateAllQty();
        }

        internal void CreateData()
        {
            DataTable pickingDataTable = new DataTable("PickingDatatable");
            // Create columns
            pickingDataTable.Columns.Add("Location", typeof(string));
            pickingDataTable.Columns.Add("Part No.", typeof(string));
            pickingDataTable.Columns.Add("Needed Qty.", typeof(int));
            pickingDataTable.Columns.Add("Qty.", typeof(int));
            pickingDataTable.Columns.Add("Completed", typeof(string));

            foreach (var item in _fgPicking.PickingSuggestedLocationList)
            {
                var dr = pickingDataTable.NewRow();
                dr["Location"] = item.Location;
                dr["Part No."] = item.PartNo;
                dr["Needed Qty."] = item.Qty;
                dr["Qty."] = 0;
                dr["Completed"] = dr["Needed Qty."] == dr["Qty."] ? "Y" : "N";
                pickingDataTable.Rows.Add(dr);
            }

            _dataSet = new DataSet();
            _dataSet.Tables.Add(pickingDataTable);
            _view.Bind(_dataSet);         
        }

        internal void UpdateAllQty()
        {
            List<FGPicking> pickeditems = _repository.GetPickedItems(_deliveryOrderTag);
            foreach (var row in _dataSet.Tables[0].Select())
            {
                var pickedQty =
                    (from items in pickeditems
                     where items.PartNo.ToString() == row["Part No."].ToString()
                     select Convert.ToInt32(items.Qty)).Sum();
                row["Qty."] = pickedQty;
                row["Completed"] = Convert.ToInt32(row["Needed Qty."]) == Convert.ToInt32(row["Qty."]) ? "Y" : "N";
            }
            foreach (var itemQuota in _fgPicking.PickingQuotaList)
            {
                var tempQty = 0;
                foreach (var item in pickeditems)
                {
                    var lotCandidate = GetLotCandidate(item.TagNo);
                    if (itemQuota.PartNo.Equals(lotCandidate.PartNo) && itemQuota.LotDate.Equals(lotCandidate.LotDate))
                    {
                        tempQty += lotCandidate.Qty;
                    }
                }
                itemQuota.CurrentQty = tempQty;
            }
        }

        #region Scan Data

        internal bool InsertFGTag()
        {
            bool insertResult = false; 

            var fathTag = code;
            if (!IsPartNoExists(fathTag))
            {
                _view.ShowMessage("ERR_03", Properties.Resources.ERR_03);
                errFlag = true;
                errMsg = "ERR_03";
                return insertResult;
            }
            if (_repository.IsTagNoDuplicate(fathTag))
            {
                _view.ShowMessage("ERR_05", Properties.Resources.ERR_05);
                errFlag = true;
                errMsg = "ERR_05";
                return insertResult;
            }
            var lotCandidate = GetLotCandidate(fathTag["Tag No."].ToString());
            if (lotCandidate == null)
            {
                _view.ShowMessage("ERR_06", Properties.Resources.ERR_06);
                errFlag = true;
                errMsg = "ERR_06";
                return insertResult;
            }
            
            /*
            if (pickingQuota.CurrentQty > pickingQuota.QuotaQty)
            {
                _view.ShowMessage("ERR_07", Properties.Resources.ERR_07);
                errFlag = true;
                errMsg = "ERR_07";
                return insertResult;
            }
            */
            if (!errFlag)
            {
                #region Old
                /*
                foreach (var row in _dataSet.Tables["PickingDatatable"].Select())
                {
                    if (row["Part No."].Equals(pickingQuota.PartNo))
                    {
                        if (pickingQuota.CurrentQty > Convert.ToInt32(row["Needed Qty."]))
                        {
                            _view.ShowMessage("ERR_04", Properties.Resources.ERR_04);
                            errFlag = true;
                            errMsg = "ERR_04";
                            return insertResult;
                        }
                        else
                        {
                            row["Qty."] = Convert.ToInt32(row["Qty."]) + lotCandidate.Qty;
                            row["Completed"] = Convert.ToInt32(row["Qty."]) == Convert.ToInt32(row["Needed Qty."]) ? "Y" : "N";
                            int rowaffected = _repository.InsertFGPicking(
                                _deliveryOrderTag,
                                row["Location"].ToString(),
                                lotCandidate.PartNo,
                                lotCandidate.TagNo,
                                lotCandidate.Qty);
                            if (rowaffected > 0)
                            {
                                insertResult = true;
                            }
                        }
                    }
                }*/
                #endregion
                var rows = _dataSet.Tables[0].Select();
                for (int i = 0; i < _dataSet.Tables[0].Select().Length; i++)
                {
                    //new
                    if (rows[i]["Part No."].ToString().Equals(lotCandidate.PartNo))
                    {
                        var pickingQuota = _fgPicking.PickingQuotaList.Find(q => (q.PartNo.Equals(lotCandidate.PartNo) && q.LotDate.Equals(lotCandidate.LotDate)));
                        int tempQty = pickingQuota.CurrentQty;// old qty
                        tempQty = tempQty + lotCandidate.Qty;//sum-qty
                        if (tempQty > pickingQuota.QuotaQty)
                        {
                            _view.ShowMessage("ERR_07", Properties.Resources.ERR_07);
                            errFlag = true;
                            errMsg = "ERR_07";
                        }
                        else if (tempQty > Convert.ToInt32(rows[i]["Needed Qty."]))
                        {
                            _view.ShowMessage("ERR_04", Properties.Resources.ERR_04);
                            errFlag = true;
                            errMsg = "ERR_07";
                        }
                        else
                        {
                            _fgPicking.PickingQuotaList[_fgPicking.PickingQuotaList.IndexOf(pickingQuota)].CurrentQty  = tempQty;
                            rows[i]["Qty."] = Convert.ToInt32(rows[i]["Qty."]) + lotCandidate.Qty;
                            rows[i]["Completed"] = Convert.ToInt32(rows[i]["Qty."]) == Convert.ToInt32(rows[i]["Needed Qty."]) ? "Y" : "N";
                            int rowAffected = _repository.InsertFGPicking(
                                _deliveryOrderTag,
                                rows[i]["Location"].ToString(),
                                lotCandidate.PartNo,
                                lotCandidate.TagNo,
                                lotCandidate.Qty);
                            if (rowAffected > 0)
                            {
                                insertResult = true;
                                _view.FocusCell(i, 3);
                            }
                        }
                    }
                    //

                    /*
                    if (rows[i]["Part No."].Equals(pickingQuota.PartNo))
                    {
                        if (pickingQuota.CurrentQty > Convert.ToInt32(rows[i]["Needed Qty."]))
                        {
                            _view.ShowMessage("ERR_04", Properties.Resources.ERR_04);
                            errFlag = true;
                            errMsg = "ERR_04";
                            return insertResult;
                        }
                        else
                        {
                            rows[i]["Qty."] = Convert.ToInt32(rows[i]["Qty."]) + lotCandidate.Qty;
                            rows[i]["Completed"] = Convert.ToInt32(rows[i]["Qty."]) == Convert.ToInt32(rows[i]["Needed Qty."]) ? "Y" : "N";
                            int rowAffected = _repository.InsertFGPicking(
                                _deliveryOrderTag,
                                rows[i]["Location"].ToString(),
                            lotCandidate.PartNo,
                            lotCandidate.TagNo,
                            lotCandidate.Qty);
                            if (rowAffected > 0)
                            {
                                insertResult = true;
                                _view.FocusCell(i, 3);// 3 = Qty.
                            }
                        }
                    }
                    */
                }
            }
            return insertResult;
        }

        internal void ProcessScanFGTag(string data)
        {
            try
            {
                // Extract part no.
                code = GenericCode.ConvertDataToGenericCode(data);
                code["Delivery Order Tag"] = _deliveryOrderTag;

                if (!string.IsNullOrEmpty(GetFGTag()))
                {
                    var result = InsertFGTag();
                }
                else
                {
                    errFlag = true;
                    errMsg = "";
                }

                var value = "";
                if (!string.IsNullOrEmpty(code["Tag No."].ToString()) && !string.IsNullOrEmpty(code["Qty."].ToString()))
                {
                    data = code["Tag No."].ToString() + "/" + code["Qty."].ToString();
                }
                SaveLog(OperationLog.LogEvent.Scan, "FGTag", value);
            }
            catch(Exception)
            {
               
            }
        }
        
        private FGPicking.PickingLotCandidate GetLotCandidate(string tagNo)
        {
            var fgLotCandidates = _fgPicking.PickingLotCandidateList;
            FGPicking.PickingLotCandidate pc = null;
            foreach (var lotcandidate in fgLotCandidates)
            {
                if (lotcandidate.TagNo.Equals(tagNo))
                {
                    pc = lotcandidate;
                    break;
                }
            }
            return pc;
        }

        private bool IsPartNoExists(GenericCode fathTag)
        {
            bool exists = false;
            foreach (var row in _dataSet.Tables[0].Select())
            {
                if (row["Part No."].Equals(fathTag["Part No."]))
                {
                    exists = true;
                    break;
                }
            }
            return exists;
        }

        private FGPicking.PickingQuota UpdatePickingQuota(FGPicking.PickingLotCandidate lotCandidate)
        {
            for (int i = 0; i < _fgPicking.PickingQuotaList.Count; i++)
            {
                if (_fgPicking.PickingQuotaList[i].PartNo.Equals(lotCandidate.PartNo) && _fgPicking.PickingQuotaList[i].LotDate.Equals(lotCandidate.LotDate))
                {
                    int tempQty = _fgPicking.PickingQuotaList[i].CurrentQty + lotCandidate.Qty;
                    if (tempQty > _fgPicking.PickingQuotaList[i].QuotaQty)
                    {
                        _view.ShowMessage("ERR_07", Properties.Resources.ERR_07);
                        errFlag = true;
                        errMsg = "ERR_07";
                    }
                    else
                    {
                        _fgPicking.PickingQuotaList[i].CurrentQty = tempQty;
                    }

                    //log
                    var logger = Logger.GetLogger();
                    var log = string.Format("PartNo:{0}\nTagNo:{1}\nQty:{2}\nLotDate:{3}\nCurrentQty:{4}\nMax:{5}\n", _fgPicking.PickingQuotaList[i].PartNo, lotCandidate.TagNo, lotCandidate.Qty, _fgPicking.PickingQuotaList[i].LotDate, _fgPicking.PickingQuotaList[i].CurrentQty, _fgPicking.PickingQuotaList[i].QuotaQty);
                    logger.WriteLog(log, "fglog.txt");
                    return _fgPicking.PickingQuotaList[i];
                }
            }
            return null;
        }

        #endregion

        internal bool ValidateSubmit()
        {
            bool result = true;
            foreach (var row in _dataSet.Tables[0].Select())
            {
                if (row["Completed"].ToString().Equals("N"))
                {
                    result = false;
                    _view.ShowMessage("ERR_08", Properties.Resources.ERR_08);
                    break;
                }
            }
            return result;
        }

        internal void Submit()
        {
            int result = _repository.SendDataToWebService(_deliveryOrderTag);
            SaveLog(OperationLog.LogEvent.ClickButton, "Submit", "");
        }

        internal bool ClearData()
        {
            SaveLog(OperationLog.LogEvent.ClickButton, "Clear", "");
            bool result = _repository.DeleteAllFGData(_deliveryOrderTag);
            return result;
        }

        private void SaveLog(OperationLog.LogEvent logEvent, string source, string value)
        {
            OperationLog ol = new OperationLog(this.menuId, Session.GetCurrentUser().UserID, logEvent, source, value,
                errFlag == true ? OperationLog.ErrorFlag.Error : OperationLog.ErrorFlag.NotError, errMsg, DateTime.Now);
            errFlag = false;
            errMsg = "";
            ol.SaveLog();
        }
    }
}
