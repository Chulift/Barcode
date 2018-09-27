using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Data;
using FATHBarcode.Config;
using System.Data.SqlServerCe;
using Newtonsoft.Json;

namespace FATHBarcode
{
    public class OperationLog
    {
        #region Enum

        public enum LogEvent : byte
        {
            ClickButton = 0,
            Scan = 1
        }

        public enum ErrorFlag : byte
        {
            NotError = 0,
            Error = 1
        }

        #endregion

        #region Private Members

        private string _menuID;
        private string _userID;
        private LogEvent _event;
        private string _fieldName;
        private string _fieldValue;
        private ErrorFlag _errorFlag;
        private string _errorMsgID;
        private DateTime _operationDateTime;

        #endregion

        public OperationLog(string menuID, string userID, LogEvent logEvent, string fieldName, string fieldValue, ErrorFlag errorFlag, string errorMsgID, DateTime operationDateTime)
        {

            _menuID = menuID;
            _userID = userID;
            _event = logEvent;
            _fieldName = fieldName;
            _fieldValue = fieldValue;
            _errorFlag = errorFlag;
            _errorMsgID = errorMsgID;
            _operationDateTime = operationDateTime;
        }

        public void SaveLog()
        {
            var db = new DBConnect();
            var script =
                @"INSERT INTO TBL_OPER_LOG([MenuID], [Operation DateTime], [UserID], [Event], [FieldName], [FieldValue], [Error Flag], [Error Msg ID])
                VALUES(@MENUID, @OPERATIONDATETIME, @USERID, @EVENT, @FIELDNAME, @FIELDVALUE, @ERRORFLAG, @ERRORMSGID)";
            using (var conn = db.GetConnection())
            {
                using (var cmd = new SqlCeCommand(script, conn))
                {
                    cmd.Parameters.Add("MENUID", _menuID);
                    cmd.Parameters.Add("OPERATIONDATETIME", SqlDbType.DateTime).Value = _operationDateTime;
                    cmd.Parameters.Add("USERID", _userID);
                    cmd.Parameters.Add("EVENT", SqlDbType.Bit).Value = _event;
                    cmd.Parameters.Add("FIELDNAME", _fieldName);
                    cmd.Parameters.Add("FIELDVALUE", _fieldValue);
                    cmd.Parameters.Add("ERRORFLAG", SqlDbType.Bit).Value = _errorFlag;
                    cmd.Parameters.Add("ERRORMSGID", _errorMsgID);
                    conn.Open();
                    try
                    {
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception)
                    {

                    }
                }
            }
        }

        #region JSON

        [JsonProperty("operationDateTime")]
        public string ScanDateTime
        {
            get { return string.Format("{0:yyyy-MM-dd HH:mm:ss}", _operationDateTime); }
        }

        [JsonProperty("menuID")]
        public string MenuID
        {
            get { return _menuID; }
            set { _menuID = value; }
        }

        [JsonProperty("userID")]
        public string UserID
        {
            get { return _userID; }
        }

        [JsonProperty("event")]
        public string Event
        {
            get
            {
                var s = "";
                switch (_event)
                {
                    case LogEvent.Scan:
                        s = "Scan";
                        break;
                    case LogEvent.ClickButton:
                        s = "Button";
                        break;
                    default:
                        s = "";
                        break;
                }
                return s;
            }
        }

        [JsonProperty("fieldName")]
        public string FieldName
        {
            get
            {
                return _fieldName;
            }
        }

        [JsonProperty("fieldValue")]
        public string FieldValue
        {
            get
            {
                return _fieldValue;
            }
        }

        [JsonProperty("errorFlag")]
        public byte ErrFlag
        {
            get
            {
                return (byte)_errorFlag;
            }
        }

        [JsonProperty("errorMessageID")]
        public string ErrorMsgID
        {
            get
            {
                return _errorMsgID;
            }
        }

        #endregion
    }
}
