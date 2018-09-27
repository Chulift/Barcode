using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using FATHBarcode.Config;
using System.Data.SqlServerCe;
using Newtonsoft.Json;
using FATHBarcode.JavaWebService;
using System.Collections;
namespace FATHBarcode.Model.Object
{
    public class ShippingRepository : IShippingRepository 
    {
        private DBConnect dbConnect;
        public ShippingRepository()
        {
            dbConnect = new DBConnect();
        }

        #region IShippingRepository Members

        public Shipping GetShipping(string deliveryOrderTag)
        {
            Hashtable request = new Hashtable();
            request.Add("doCode", deliveryOrderTag);
            var data = JsonConvert.SerializeObject(request);

            var serviceReference = new JavaWebService.ServicesBLService();
            string userId = Session.GetCurrentUser().UserID;
            var response = serviceReference.getShippingDetail(data, userId);
            var shipping = JsonConvert.DeserializeObject<Shipping>(response);
            return shipping;
        }

        public List<Shipping> GetShippingItems(string deliveryOrderTag)
        {
            var script =
                @"SELECT [DO Part No.], [Customer Qty.], [Customer Part No.] FROM Shipping
                WHERE ([Delivery Order Tag] = @DELIVERYORDERTAG)";
            List<Shipping> collections = new List<Shipping>();
            using (var conn = dbConnect.GetConnection())
            {
                conn.Open();
                using (var cmd = new SqlCeCommand(script, conn))
                {
                    cmd.Parameters.Add("DELIVERYORDERTAG", deliveryOrderTag);
                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        var shipping = new Shipping();
                        shipping["DO Part No."] = reader["DO Part No."];
                        shipping["Customer Part No."] = reader["Customer Part No."];
                        shipping["Customer Qty."] = reader["Customer Qty."];
                        collections.Add(shipping);
                    }
                }
            } return collections;
        }

        public List<Shipping> GetFATHItems(string deliveryOrderTag)
        {
            var script =
                @"SELECT [Part no.] AS [FATH Part No.], [Qty.] AS [FATH Qty.] FROM FATHTag
                WHERE ([Delivery Order Tag] = @DELIVERYORDERTAG)";
            List<Shipping> collections = new List<Shipping>();
            using (var conn = dbConnect.GetConnection())
            {
                conn.Open();
                using (var cmd = new SqlCeCommand(script, conn))
                {
                    cmd.Parameters.Add("DELIVERYORDERTAG", deliveryOrderTag);
                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        var fath = new Shipping();
                        fath["FATH Part No."] = reader["FATH Part No."];
                        fath["FATH Qty."] = reader["FATH Qty."];
                        collections.Add(fath);
                    }
                }
            }
            return collections;
        }

        public bool DuplicateTagNo(GenericCode code)
        {
            var script =
                @"SELECT COUNT(*) FROM FGPicking WHERE ([Tag No.] = @DATA) AND ([Delivery Order Tag] = @DELIVERYORDERTAG)";
            using (var conn = dbConnect.GetConnection())
            {
                conn.Open();
                using (var cmd = new SqlCeCommand(script, conn))
                {
                    cmd.Parameters.Add("DELIVERYORDERTAG", code["Delivery Order Tag"]);
                    cmd.Parameters.Add("TAGNO", code["Tag No."].ToString());
                    int rowAffected = Convert.ToInt32(cmd.ExecuteScalar());
                    return rowAffected > 0;
                }
            }
        }

        public int UpdateCustomerPathNo(GenericCode code)
        {
            bool isExists = CheckCustomerPartNo(code);
            if (!isExists)
            {
                var script =
                    @"INSERT INTO Shipping([Delivery Order Tag], [DO Part No.], [Customer Part No.], [Scan DateTime])
                VALUES(@DELIVERYORDERTAG, @PARTNO, @PARTNO, @SCANDATETIME)";
                using (var conn = dbConnect.GetConnection())
                {
                    conn.Open();
                    using (var cmd = new SqlCeCommand(script, conn))
                    {
                        cmd.Parameters.Add("DELIVERYORDERTAG", code["Delivery Order Tag"]);
                        cmd.Parameters.Add("PARTNO", code["Customer Part No."]);
                        var dt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                        cmd.Parameters.Add("SCANDATETIME", dt.ToString());
                        return cmd.ExecuteNonQuery();
                    }
                }
            }
            return 0;
        }

        private bool CheckCustomerPartNo(GenericCode code)
        {
            var script =
                @"SELECT COUNT(*) FROM Shipping WHERE ([Delivery Order Tag] = @DELIVERYORDERTAG) AND ([DO Part No.] = @PARTNO) 
                AND ([Customer Part No.] = @PARTNO)";
            using (var conn = dbConnect.GetConnection())
            {
                conn.Open();
                using (var cmd = new SqlCeCommand(script, conn))
                {
                    cmd.Parameters.Add("DELIVERYORDERTAG", code["Delivery Order Tag"]);
                    cmd.Parameters.Add("PARTNO", code["Customer Part No."]);
                    return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
                }
            }
        }

        public int UpdateCustomerQty(GenericCode code)
        {
            var script =
                @"UPDATE Shipping
                SET [Customer Qty.] = @QTY, [Scan DateTime] = @SCANDATETIME
                WHERE ([Delivery Order Tag] = @DELIVERYORDERTAG) AND ([DO Part No.] = @PARTNO)";
            using (var conn = dbConnect.GetConnection())
            {
                conn.Open();
                using (var cmd = new SqlCeCommand(script, conn))
                {
                    cmd.Parameters.Add("DELIVERYORDERTAG", code["Delivery Order Tag"]);
                    cmd.Parameters.Add("PARTNO", code["Customer Part No."]);
                    var dt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    cmd.Parameters.Add("SCANDATETIME", dt.ToString());
                    cmd.Parameters.Add("QTY", code["Customer Qty."]);
                    return cmd.ExecuteNonQuery();
                }
            }
        }

        public int UpdateFATHTag(GenericCode code)
        {
            var script =
                @"INSERT INTO FATHTag ([Delivery Order Tag], [Part No.], [Qty.], [Tag No.], [Scan DateTime])
                VALUES(@DELIVERYORDERTAG, @PARTNO, @QTY, @TAGNO, @SCANDATETIME)";
            using (var conn = dbConnect.GetConnection())
            {
                conn.Open();
                using (var cmd = new SqlCeCommand(script, conn))
                {
                    cmd.Parameters.Add("DELIVERYORDERTAG", code["Delivery Order Tag"]);
                    cmd.Parameters.Add("PARTNO", code["FATH Part No."]);
                    cmd.Parameters.Add("QTY", code["FATH Qty."]);
                    cmd.Parameters.Add("TAGNO", code["FATH Tag No."]);
                    var dt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    cmd.Parameters.Add("SCANDATETIME", dt.ToString());
                    return cmd.ExecuteNonQuery();
                }
            }
        }

        public bool IsTagNoDuplicate(GenericCode code)
        {
            var script =
                @"SELECT COUNT(*) FROM FATHTag WHERE ([Delivery Order Tag] = @DELIVERYORDERTAG)
                AND ([Part No.] = @PARTNO) AND ([Tag No.] = @TAGNO)";
            using (var conn = dbConnect.GetConnection())
            {
                conn.Open();
                using (var cmd = new SqlCeCommand(script, conn))
                {
                    cmd.Parameters.Add("DELIVERYORDERTAG", code["Delivery Order Tag"]);
                    cmd.Parameters.Add("PARTNO", code["FATH Part No."]);
                    cmd.Parameters.Add("TAGNO" , code["FATH Tag No."]);
                    return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
               }
            }
        }

        public int ClearAllData(string deliveryOrderTag)
        {
            var result = 0;
            var shippingScript =
                @"DELETE FROM Shipping WHERE [Delivery Order Tag] = @DELIVERYORDERTAG";
            var fathScript =
                @"DELETE FROM FATHTag WHERE [Delivery Order Tag] = @DELIVERYORDERTAG";
            using (var conn = dbConnect.GetConnection())
            {
                conn.Open();
                using (var cmd = new SqlCeCommand(shippingScript, conn))
                {
                    cmd.Parameters.Add("DELIVERYORDERTAG", deliveryOrderTag);
                    result = cmd.ExecuteNonQuery();
                }
                using (var cmd = new SqlCeCommand(fathScript, conn))
                {
                    cmd.Parameters.Add("DELIVERYORDERTAG", deliveryOrderTag);
                    result = result + cmd.ExecuteNonQuery();
                }
                return result;
            }
        }

        public int SendDataToWebService(string doCode)
        {
            List<ShippingTransaction> shippingList = new List<ShippingTransaction>();
            List<OperationLog> logData = new List<OperationLog>();

            var script =
                @"SELECT Shipping.[Delivery Order Tag], Shipping.[DO Part No.], FATHTag.[Tag No.], FATHTag.[Scan DateTime]
                FROM Shipping INNER JOIN
                FATHTag ON Shipping.[Delivery Order Tag] = FATHTag.[Delivery Order Tag] AND Shipping.[DO Part No.] = FATHTag.[Part No.]
                WHERE Shipping.[Delivery Order Tag] = @DELIVERYORDERTAG";
            var logScript =
                @"SELECT [MenuID], [Operation DateTime], [UserID], [Event], [FieldName], [FieldValue], [Error Flag], [Error Msg ID] 
                FROM TBL_OPER_LOG WHERE [MenuID] = @MENUID";
            
            using (var conn = dbConnect.GetConnection())
            {
                conn.Open();
                using (var cmd = new SqlCeCommand(script, conn))
                {
                    cmd.Parameters.Add("DELIVERYORDERTAG", doCode);
                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        var shipping = new ShippingTransaction();
                        shipping.PartNo = reader["DO Part No."].ToString();
                        shipping.TagNo = reader["Tag No."].ToString();
                        shipping.ScanDateTime = reader["Scan DateTime"].ToString();
                        shippingList.Add(shipping);
                    }
                }
                using (var cmd = new SqlCeCommand(logScript, conn))
                {
                    cmd.Parameters.Add("MENUID", "M02");
                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        var log = new OperationLog(
                            reader["MenuID"].ToString(),
                            Session.GetCurrentUser().UserID,
                            Convert.ToBoolean(reader["Event"]) == true ? OperationLog.LogEvent.Scan : OperationLog.LogEvent.ClickButton,
                            reader["FieldName"].ToString(),
                            reader["FieldValue"].ToString(),
                            Convert.ToBoolean(reader["Error Flag"]) == true ? OperationLog.ErrorFlag.Error : OperationLog.ErrorFlag.NotError,
                            reader["Error Msg ID"].ToString(),
                            Convert.ToDateTime(reader["Operation DateTime"]));
                        logData.Add(log);
                    }
                }
            }
            
            int result = 0;
            if (shippingList.Count > 0)
            {
                Hashtable hs = new Hashtable();
                hs.Add("doCode", doCode);
                hs.Add("shippingList", shippingList);
                hs.Add("logData", logData);

                var request = JsonConvert.SerializeObject(hs);
                var serviceReference = new ServicesBLService();
                string userId = Session.GetCurrentUser().UserID;
                var response = serviceReference.insertShippingDetail(request, userId);
                if (response == "success")
                {
                    result = 1;
                }
            }
            return result;
        }

        public void ClearOperationLog(string deliveryOrderTag, string menuId)
        {
            var script =
                @"DELETE FROM TBL_OPER_LOG WHERE [MenuID] = @MENUID";
            using (var conn = dbConnect.GetConnection())
            {
                conn.Open();
                using (var cmd = new SqlCeCommand(script, conn))
                {
                    cmd.Parameters.Add("MENUID", menuId);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        #endregion
    }
}
