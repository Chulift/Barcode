using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Microsoft.WindowsCE.Forms;
using Bt;
using FATHBarcode.Base;
using CommonLibrary.IO;

namespace FATHBarcode
{
    public class MsgWindow : MessageWindow
    {
        private IView _view;

        public MsgWindow(IView view)
        {
            this._view = view;
        }

        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case (int)LibDef.WM_BT_SCAN:
                    if (m.WParam.ToInt32() == Convert.ToInt32(Bt.LibDef.BTMSG_WPARAM.WP_SCN_SUCCESS))
                    {
                        var readText = ScanData();
                        _view.OnScanData(readText);
                    }
                    break;
                default:
                    break;
            }
            base.WndProc(ref m);
        }

        private string ScanData()
        {
            var log = new StringBuilder();
            int ret = 0;
            byte[] codedataGet;
            string strCodedata = "";
            Int32 codeLen = 0;
            ushort symbolGet = 0;
            try
            {
                //-----------------------------------------------------------
                // Reading (batch)
                log.AppendLine("Scanning...");
                //-----------------------------------------------------------
                codeLen = Bt.ScanLib.Control.btScanGetStringSize();
                //-----------
                log.AppendLine(string.Format("len:{0}", codeLen));
                //-----------
                if (codeLen <= 0)
                {
                    throw new Exception();
                }
                codedataGet = new byte[codeLen];
                ret = Bt.ScanLib.Control.btScanGetString(codedataGet, ref symbolGet);
                //-------------
                log.AppendLine(string.Format("ret:{0}", ret));
                //-------------
                if (ret != Bt.LibDef.BT_OK)
                {
                    throw new Exception();
                }
                strCodedata = System.Text.Encoding.GetEncoding(932).GetString(codedataGet, 0, codeLen);
                //--------------
                log.AppendLine(string.Format("Data : {0}", strCodedata));
                //--------------
            }
            catch (Exception e)
            {
                log.AppendLine(e.Message);
                //_view.ShowMessage("","Invalid barcode, try again");
            }
            finally
            {
                var logger = Logger.GetLogger();
                logger.WriteLog(log.ToString(), "syslog.txt");
                
            }
            return strCodedata;
        }
    }
}
