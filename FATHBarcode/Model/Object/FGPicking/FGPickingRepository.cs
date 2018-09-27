using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlServerCe;
using System.Data;
using FATHBarcode.Config;
using FATHBarcode.Model.Object;
using FATHBarcode.JavaWebService;
using System.IO;
using System.Collections;
using Newtonsoft.Json;

namespace FATHBarcode.Model.Object
{
    public class FGPickingRepository : IFGPickingRepository
    {
        private DBConnect dbConnect;

        public FGPickingRepository()
        {
            dbConnect = new DBConnect();
        }

        #region IFGPickingRepository Members

        public FGPicking GetFGPicking(string deliveryOrderTag)
        {
            Hashtable request = new Hashtable();
            request.Add("referenceNo", deliveryOrderTag);
            var data = JsonConvert.SerializeObject(request);

            var serviceReference = new JavaWebService.ServicesBLService();
            string userId = Session.GetCurrentUser().UserID;
            var response = serviceReference.getPickingDetail(data, userId);
            var fgPicking = JsonConvert.DeserializeObject<FGPicking>(response);
            return fgPicking;
        }

        public List<FGPicking> GetPickedItems(string deliveryOrderTag)
        {
            List<FGPicking> collections = new List<FATHBarcode.Model.Object.FGPicking>();
            var script =
                @"SELECT [Part No.], [Tag No.], [Qty.] FROM FGPicking WHERE [Delivery Order Tag] = @DELIVERYORDERTAG";
            using (var conn = dbConnect.GetConnection())
            {
                conn.Open();
                using (var cmd = new SqlCeCommand(script, conn))
                {
                    cmd.Parameters.Add("DELIVERYORDERTAG", deliveryOrderTag);
                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        var fgPicking = new FGPicking();
                        fgPicking["Part No."] = reader["Part No."];
                        fgPicking["Tag No."] = reader["Tag No."];
                        fgPicking["Qty."] = reader["Qty."];
                        collections.Add(fgPicking);
                    }
                }
            }
            return collections;
        }

        public List<FGPicking> GetFATHList(FGPicking baseFGPicking)
        {
            List<FGPicking> collections = new List<FATHBarcode.Model.Object.FGPicking>();
            var script =
                @"SELECT [Part No.], [Tag No.], [Qty.] FROM FATHTag WHERE [Delivery Order Tag] = @DELIVERYORDERTAG AND [Part No.] = @PARTNO";
            using (var conn = dbConnect.GetConnection())
            {
                conn.Open();
                using (var cmd = new SqlCeCommand(script, conn))
                {
                    cmd.Parameters.Add("DELIVERYORDERTAG", baseFGPicking["Delivery Order Tag"]);
                    cmd.Parameters.Add("PARTNO", baseFGPicking["Part No."]);
                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        var fgPicking = new FGPicking();
                        fgPicking["Delivery Order Tag"] = baseFGPicking["Delivery Order Tag"];
                        fgPicking["Part No."] = reader["Part No."];
                        fgPicking["Tag No."] = reader["Tag No."];
                        fgPicking["Qty."] = reader["Qty."];
                        collections.Add(fgPicking);
                    }
                }
            }
            return collections;
        }


        public List<FGPicking> GetPickingDetailList(FGPicking baseFGPicking)
        {
            List<FGPicking> collections = new List<FATHBarcode.Model.Object.FGPicking>();
            var script =
                @"SELECT [Part No.], [Location], [Tag No.], [Qty.] FROM FGPicking WHERE [Delivery Order Tag] = @DELIVERYORDERTAG AND [Part No.] = @PARTNO";
            using (var conn = dbConnect.GetConnection())
            {
                conn.Open();
                using (var cmd = new SqlCeCommand(script, conn))
                {
                    cmd.Parameters.Add("DELIVERYORDERTAG", baseFGPicking["Delivery Order Tag"]);
                    cmd.Parameters.Add("PARTNO", baseFGPicking["Part No."]);
                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        var fgPicking = new FGPicking();
                        fgPicking["Delivery Order Tag"] = baseFGPicking["Delivery Order Tag"];
                        fgPicking["Location"] = reader["Location"];
                        fgPicking["Part No."] = reader["Part No."];
                        fgPicking["Tag No."] = reader["Tag No."];
                        fgPicking["Qty."] = reader["Qty."];
                        collections.Add(fgPicking);
                    }
                }
            }
            return collections;
        }

        public bool IsTagNoDuplicate(GenericCode fathTag)
        {
            var script =
                @"SELECT COUNT(*) FROM FGPicking 
                WHERE ([Delivery Order Tag] = @DELIVERYORDERTAG) AND ([Part No.] = @PARTNO) AND ([Tag No.] = @TAGNO)";
            using (var conn = dbConnect.GetConnection())
            {
                conn.Open();
                using (var cmd = new SqlCeCommand(script, conn))
                {
                    cmd.Parameters.Add("DELIVERYORDERTAG", fathTag["Delivery Order Tag"]);
                    cmd.Parameters.Add("PARTNO", fathTag["Part No."].ToString());
                    cmd.Parameters.Add("TAGNO", fathTag["Tag No."].ToString());
                    int rowAffected = Convert.ToInt32(cmd.ExecuteScalar());
                    return rowAffected > 0;
                }
            }
        }

        public int InsertFGPicking(string deliveryOrderTag, string location, string partNo, string tagNo, int qty)
        {
            var rowaffected = 0;
            var script =
                @"INSERT INTO FGPicking([Delivery Order Tag], [Location], [Part No.], [Tag No.], [Qty.], [Scan DateTime])
                VALUES(@DELIVERYORDERTAG, @LOCATION, @PARTNO, @TAGNO, @QTY, @SCANDATETIME)";
            using (var conn = dbConnect.GetConnection())
            {
                conn.Open();
                using (var cmd = new SqlCeCommand(script, conn))
                {
                    cmd.Parameters.Add("DELIVERYORDERTAG", deliveryOrderTag);
                    cmd.Parameters.Add("LOCATION", location);
                    cmd.Parameters.Add("PARTNO", partNo);
                    cmd.Parameters.Add("TAGNO", tagNo);
                    cmd.Parameters.Add("QTY", qty);
                    var dt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    cmd.Parameters.Add("SCANDATETIME", dt.ToString());
                    rowaffected = cmd.ExecuteNonQuery();
                }
            }
            return rowaffected;
        }

        public bool DeleteAllFGData(string deliveryOrderTag)
        {
            var script =
                @"DELETE FROM FGPicking WHERE [Delivery Order Tag] = @DELIVERYORDERTAG";
            using (var conn = dbConnect.GetConnection())
            {
                conn.Open();
                using (var cmd = new SqlCeCommand(script, conn))
                {
                    cmd.Parameters.Add("DELIVERYORDERTAG", deliveryOrderTag);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        public int SendDataToWebService(string referenceNo)
        {
            List<Picking> pickngList = new List<Picking>();
            var script =
                @"SELECT [Delivery Order Tag], [Part No.], [Tag No.], [Scan DateTime] FROM FGPicking
                WHERE ([Delivery Order Tag] = @REFERENCENO)";
            using (var conn = dbConnect.GetConnection())
            {
                conn.Open();
                using (var cmd = new SqlCeCommand(script, conn))
                {
                    cmd.Parameters.Add("REFERENCENO", referenceNo);
                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        var pg = new Picking();
                        pg.PartNo = reader["Part No."].ToString();
                        pg.TagNo = reader["Tag No."].ToString();
                        pg.ScanDateTime = reader["Scan DateTime"].ToString();
                        pickngList.Add(pg);
                    }
                }
            }
            int result = 0;
            if (pickngList.Count > 0)
            {

                Hashtable hs = new Hashtable();
                hs.Add("referenceNo", referenceNo);
                hs.Add("pickingList", pickngList);

                var request = JsonConvert.SerializeObject(hs);
                var serviceReference = new ServicesBLService();
                string userId = Session.GetCurrentUser().UserID;
                var response = serviceReference.insertPickingDetail(request, userId);
                if (response.Equals("success"))
                {
                    result = 1;
                }
            }
            return result;
        }

        #endregion

        #region IFGPickingRepository.Remove Members

        public int Remove(FGPicking fGPicking)
        {
            var script =
                @"DELETE FROM FGPicking 
                WHERE ([Delivery Order Tag] = @DELIVERYORDERTAG) AND ([Part No.] = @PARTNO) AND ([Tag No.] = @TAGNO)";
            using (var conn = dbConnect.GetConnection())
            {
                conn.Open();
                using (var cmd = new SqlCeCommand(script, conn))
                {
                    cmd.Parameters.Add("DELIVERYORDERTAG", fGPicking["Delivery Order Tag"].ToString());
                    cmd.Parameters.Add("PARTNO", fGPicking.PartNo);
                    cmd.Parameters.Add("TAGNO", fGPicking.TagNo);
                    return cmd.ExecuteNonQuery();
                }
            }
        }

        public int RemoveFATHTag(FGPicking fGPicking)
        {
            var script =
                @"DELETE FROM FATHTag
                WHERE ([Delivery Order Tag] = @DELIVERYORDERTAG) AND ([Part No.] = @PARTNO) AND ([Tag No.] = @TAGNO)";
            using (var conn = dbConnect.GetConnection())
            {
                conn.Open();
                using (var cmd = new SqlCeCommand(script, conn))
                {
                    cmd.Parameters.Add("DELIVERYORDERTAG", fGPicking["Delivery Order Tag"].ToString());
                    cmd.Parameters.Add("PARTNO", fGPicking.PartNo);
                    cmd.Parameters.Add("TAGNO", fGPicking.TagNo);
                    return cmd.ExecuteNonQuery();
                }
            }
        }

        #endregion
    }
}
