using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using FATHBarcode.View;
using FATHBarcode.Model;
using FATHBarcode.Model.Object;
using System.Data;
using FATHBarcode.Config;

namespace FATHBarcode.Presenter
{
    public class ShippingPresenter : Base.Presenter
    {
        private readonly IShippingView _view;
        private readonly IShippingRepository _repository;
        private string _deliveryOrderTag;
        private GenericCode code;
        private DataSet _dataSet;
        private Shipping _shipping;
        public Settings.Screen Screen { get; set; }

        public ShippingPresenter(IShippingView view, IShippingRepository repository, string deliveryOrderTag) : base("M02")
        {
            _view = view;
            view.Presenter = this;
            _repository = repository;
            _deliveryOrderTag = deliveryOrderTag;
            view.DeliveryOrderTag = deliveryOrderTag;
            code = new GenericCode();
            code["Delivery Order Tag"] = deliveryOrderTag;
            ShowDetails();
        }

        private void ShowDetails()
        {
            _shipping = _repository.GetShipping(_deliveryOrderTag);
            if (_shipping.CustomerCheckingPoints == 2)
            {
                _view.EnableCustomerTag = false;
            }
            CreateData();
            UpdateAllQty();
        }

        internal void CreateData()
        {
            DataTable shippingDataTable = new DataTable("ShippingDatatable");
            // Create Columns
            shippingDataTable.Columns.Add("DO Part No.", typeof(string));
            shippingDataTable.Columns.Add("DO Qty.", typeof(int));
            shippingDataTable.Columns.Add("Customer Part No.", typeof(string));
            shippingDataTable.Columns.Add("Customer Qty.", typeof(int));
            shippingDataTable.Columns.Add("FATH Part No.", typeof(string));
            shippingDataTable.Columns.Add("FATH Qty.", typeof(int));
            shippingDataTable.Columns.Add("Pass", typeof(string));
            
            var query =
                from p in _shipping.PickedItemList
                group p by p.PartNo into pg
                select pg;

            foreach (var group in query)
            {
                int qty = 0;
                foreach (var item in group)
                {
                    qty += item.Qty;
                }
                
                var dr = shippingDataTable.NewRow();
                dr["DO Part No."] = group.Key.ToString();
                dr[1] = qty;
                dr["Customer Part No."] = "";
                dr[3] = 0;
                dr["FATH Part No."] = "";
                dr[5] = 0;
                dr["Pass"] = "N";
                shippingDataTable.Rows.Add(dr);
            }

            _dataSet = new DataSet();
            _dataSet.Tables.Add(shippingDataTable);
            _view.Bind(_dataSet);
        }

        internal void UpdateAllQty()
        {
            List<Shipping> shippingitems = _repository.GetShippingItems(_deliveryOrderTag);
            List<Shipping> fathItems = _repository.GetFATHItems(_deliveryOrderTag);

            foreach (var row in _dataSet.Tables[0].Select())
            {
                row["FATH Part No."] = "";
                var shippings =
                    from item in shippingitems
                    where item["DO Part No."].ToString() == row["DO Part No."].ToString()
                    select item;
                foreach (var item in shippings)
                {
                    row["Customer Part No."] = item["Customer Part No."];
                    row["Customer Qty."] = item["Customer Qty."];

                }
                int fathQty = 0;
                var faths =
                    from items in fathItems
                    where items["FATH Part No."].ToString() == row["DO Part No."].ToString()
                    select items;
                foreach (var item in faths)
                {
                    row["FATH Part No."] = item["FATH Part No."];
                    fathQty += item.FATHQty;
                }
                row["FATH Qty."] = fathQty;

                if (_shipping.CustomerCheckingPoints == 2)
                {
                    row["Pass"] = Convert.ToInt32(row["FATH Qty."]) == Convert.ToInt32(row["DO Qty."]) ? "Y" : "N";
                }
                else
                {
                    row["Pass"] = ((Convert.ToInt32(row["FATH Qty."]) == Convert.ToInt32(row["DO Qty."])) &&
                        (Convert.ToInt32(row["Customer Qty."]) == Convert.ToInt32(row["DO Qty."]))) ? "Y" : "N";
                }
            }
        }

        private void SumUpCustomerQty(GenericCode code)
        {
            if (code["Customer Part No."] != null && code["Customer Qty."] != null)
            {
                foreach (var row in _dataSet.Tables[0].Select())
                {
                    if (row["DO Part No."].Equals(code["Customer Part No."].ToString()))
                    {
                        int custQty = Convert.ToInt32(row["Customer Qty."]);
                        custQty += Convert.ToInt32(code["Customer Qty."]);
                        if (custQty > Convert.ToInt32(row["DO Qty."]))
                        {
                            _view.ShowMessage("ERR_04", Properties.Resources.ERR_04);
                            errFlag = true;
                            errMsg = "ERR_04";
                            return;
                        }
                        else
                        {
                            code["Customer Qty."] = custQty;
                            int rowAffected = _repository.UpdateCustomerQty(code);
                            if (rowAffected > 0)
                            {
                                // update row
                                row["Customer Qty."] = custQty;
                                row["Pass"] = ((Convert.ToInt32(row["FATH Qty."]) == Convert.ToInt32(row["DO Qty."])) &&
                                    (Convert.ToInt32(row["Customer Qty."]) == Convert.ToInt32(row["DO Qty."]))) ? "Y" : "N";
                            }
                        }
                    }
                }
            }
        }

        private bool IsPartNoExists(string data)
        {
            bool exists = false;
            foreach (var row in _dataSet.Tables[0].Select())
            {
                if (row["DO Part No."].Equals(data))
                {
                    exists = true;
                    break;
                }
            }
            return exists;
        }

        internal void ProcessScanCustomerTag(string data)
        {
            code["Customer Part No."] = data;
            if (!IsPartNoExists(code["Customer Part No."].ToString()))
            {
                _view.ShowMessage("ERR_03", Properties.Resources.ERR_03);
                errFlag = true;
                errMsg = "ERR_03";
                SaveLog(OperationLog.LogEvent.Scan, "CustPartNo.", data);
                return;
            }

            // sum-up qty
            SumUpCustomerQty(code);

            if (_repository.UpdateCustomerPathNo(code) > 0)
            {
                foreach (var row in _dataSet.Tables[0].Select())
                {
                    if (row["DO Part No."].Equals(code["Customer Part No."].ToString()))
                    {
                        row["Customer Part No."] = code["Customer Part No."].ToString();
                        break;
                    }
                }
            };
            SaveLog(OperationLog.LogEvent.Scan, "CustPartNo.", data);
        }

        internal void ProcessScanCustomerQty(string data)
        {
            code["Customer Qty."] = data;
            SumUpCustomerQty(code);
            SaveLog(OperationLog.LogEvent.Scan, "CustQty", data); 
        }

        internal void ProcessScanFATHTag(string data)
        {
            GenericCode tempCode = null;
            try
            {
                tempCode = GenericCode.ConvertDataToGenericCode(data);
                code["Delivery Order Tag"] = _deliveryOrderTag;
                code["FATH Part No."] = tempCode["Part No."];
                code["FATH Tag No."] = tempCode["Tag No."];
                code["FATH Qty."] = tempCode["Qty."];
                if (_repository.IsTagNoDuplicate(code))
                {
                    _view.ShowMessage("ERR_05", Properties.Resources.ERR_05);
                    errFlag = true;
                    errMsg = "ERR_05";
                }
                if (!IsPartNoExists(code["FATH Part No."].ToString()) && !errFlag)
                {
                    _view.ShowMessage("ERR_03", Properties.Resources.ERR_03);
                    errFlag = true;
                    errMsg = "ERR_03";
                }
            }
            catch (Exception)
            {
                errFlag = true;
                errMsg = "";
            }
            
            if (!errFlag)
            {
                #region Update Row

                foreach (var row in _dataSet.Tables[0].Select())
                {
                    if (row["DO Part No."].Equals(code["FATH Part No."].ToString()))
                    {
                        // sum-up  qty
                        int fathQty = 0;
                        fathQty = Convert.ToInt32(row["FATH Qty."]) + Convert.ToInt32(code["FATH Qty."]);
                        if (fathQty > Convert.ToInt32(row["DO Qty."]))
                        {
                            _view.ShowMessage("ERR_04", Properties.Resources.ERR_04);
                            errFlag = true;
                            errMsg = "ERR_04";
                            break;
                        }
                        else
                        {
                            int rowAffected = 0;
                            //update to db
                            try
                            {
                                rowAffected = _repository.UpdateFATHTag(code);
                            }
                            catch
                            {
                            }
                            if (rowAffected > 0)
                            {
                                row["FATH Part No."] = code["FATH Part No."];
                                row["FATH Qty."] = fathQty;
                                if (_shipping.CustomerCheckingPoints == 2)
                                {
                                    row["Pass"] = Convert.ToInt32(row["FATH Qty."]) == Convert.ToInt32(row["DO Qty."]) ? "Y" : "N";
                                }
                                else
                                {
                                    row["Pass"] = ((Convert.ToInt32(row["FATH Qty."]) == Convert.ToInt32(row["DO Qty."])) &&
                                        (Convert.ToInt32(row["Customer Qty."]) == Convert.ToInt32(row["DO Qty."]))) ? "Y" : "N";
                                }
                            }
                        }
                    }
                }

            #endregion
            }
            var value = "";
            if (tempCode == null)
                value = "";
            else 
                value = string.Format("{0}/{1}", tempCode["Part No."].ToString(), tempCode["Qty."].ToString());
            SaveLog(OperationLog.LogEvent.Scan, "FATHTag", value);
        }

        private void SaveLog(OperationLog.LogEvent logEvent, string source, string value)
        {
            OperationLog ol = new OperationLog(menuId, Session.GetCurrentUser().UserID, logEvent, source, value,
                errFlag == true? OperationLog.ErrorFlag.Error:OperationLog.ErrorFlag.NotError, errMsg, DateTime.Now);
            errFlag = false;
            errMsg = "";
            ol.SaveLog();
        }

        internal bool ValidateSubmit()
        {
            bool result = true;
            foreach (var row in _dataSet.Tables[0].Select())
            {
                if (row["Pass"].ToString().Equals("N"))
                {
                    result = false;
                    _view.ShowMessage("ERR_09", Properties.Resources.ERR_09);
                    break;
                }
            }
            return result;
        }

        internal int Submit()
        {
            try
            {
                SaveLog(OperationLog.LogEvent.ClickButton, "Submit", "");
                var result = _repository.SendDataToWebService(_deliveryOrderTag);
                if (result > 0)
                {
                    
                    ClearAllData();
                    _repository.ClearOperationLog(_deliveryOrderTag, this.menuId);
                    return 1;
                }
            }
            catch (Exception e)
            {
                _view.ShowMessage("Error", e.Message);
            }
            return 0;

        }

        internal int ClearAllData()
        {
            try
            {
                SaveLog(OperationLog.LogEvent.ClickButton, "Clear", "");
                var result = _repository.ClearAllData(_deliveryOrderTag);
                return result;
            }
            catch (Exception e)
            {
                _view.ShowMessage("Error", e.Message);
            }
            return 0;
        }
    }
}