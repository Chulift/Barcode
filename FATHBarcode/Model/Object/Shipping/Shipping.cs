using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace FATHBarcode.Model.Object
{
    public class Shipping : Base.GenericObject
    {

        #region Public Members

        [JsonProperty("santen_flag")]
        public int CustomerCheckingPoints { get; set; }

        [JsonProperty("Result")]
        public string Result { get; set; }

        [JsonProperty("ErrMsg")]
        public string ErrMsg { get; set; }

        [JsonProperty("doCode")]
        public string DeliveryOrderTag { get { return this["Delivery Order Tag"].ToString(); } set { this["Delivery Order Tag"] = value; } }

        [JsonProperty("pickedItemList")]
        public List<PickedItem> PickedItemList { get; set; }

        public string DOPartNo
        {
            get { return this["DO Part No."].ToString(); }
            set { this["DO Part No."] = value; }
        }

        public string CustomerPartNo
        {
            get { return this["Customer Part No."].ToString(); }
            set { this["Customer Part No."] = value; }
        }

        public int CustomerQty
        {
            get { return Convert.ToInt32(this["Customer Qty."]); }
            set { this["Customer Qty."] = value; }
        }

        public string FATHPartNo
        {
            get { return this["FATH Part No."].ToString(); }
            set { this["FATH Part No."] = value; }
        }

        public int FATHQty
        {
            get { return Convert.ToInt32(this["FATH Qty."]); }
            set { this["FATH Qty."] = value; }
        }

        #endregion

        #region Inner Classes

        public class PickedItem
        {
            [JsonProperty("Part No.")]
            public string PartNo { get; set; }

            [JsonProperty("Qty")]
            public int Qty { get; set; }

            [JsonProperty("Tag No.")]
            public string TagNo { get; set; }
        }

        #endregion

    }
}
