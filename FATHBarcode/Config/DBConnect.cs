using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlServerCe;

namespace FATHBarcode.Config
{
    public class DBConnect
    {
        private string _connectionString;

        public DBConnect()
        {
            //Data Source=D:\Projects\SPE1\FATH\fath_barcode\branch2018\FATHBarcode\AppDatabase.sdf
            var dbPath = System.IO.Path.Combine(CommonLibrary.IO.Common.GetAppPath(), Config.Settings.settings["DBName"]);
            var dbPassword = Config.Settings.settings["DBPassword"];
            _connectionString = string.Format("Data Source='{0}'; Password='{1}'", dbPath, dbPassword);
        }

        public SqlCeConnection GetConnection()
        {
            return new SqlCeConnection(_connectionString);
        }
    }
}
