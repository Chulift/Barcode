using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using FATHBarcode.Base;

namespace FATHBarcode.Model.Object
{
    public class FGPicking : Base.GenericObject
    {

        #region Public Members

        [JsonProperty("doCode")]
        public string DeliveryOrderTag { get; set; }
        
        [JsonProperty("locationList")]
        public List<PickingSuggestedLocation> PickingSuggestedLocationList { get; set; }

        [JsonProperty("candidateList")]
        public List<PickingLotCandidate> PickingLotCandidateList { get; set; }

        [JsonProperty("quotaList")]
        public List<PickingQuota> PickingQuotaList { get; set; }

        public int Qty
        {
            get
            {
                return Convert.ToInt32(this["Qty."]);
            }
        }

        public string PartNo
        {
            get
            {
                return this["Part No."].ToString();
            }
        }

        public string TagNo
        {
            get
            {
                return this["Tag No."].ToString();
            }
        }

        #endregion

        #region Inner Classes

        public class PickingLotCandidate
        {
            public PickingLotCandidate(string partNo, string tagNo, string lotDate, int qty)
            {
                this.PartNo = partNo;
                this.TagNo = tagNo;
                this.LotDate = lotDate;
                this.Qty = qty;
            }

            [JsonProperty("Part No.")]
            public string PartNo { get; set; }

            [JsonProperty("Tag No.")]
            public string TagNo { get; set; }

            [JsonProperty("Lot Date")]
            public string LotDate { get; set; }

            [JsonProperty("Qty")]
            public int Qty { get; set; }
        }

        public class PickingQuota
        {
            public PickingQuota(string partNo, string lotDate, int QuataQty)
            {
                this.PartNo = partNo;
                this.LotDate = lotDate;
                this.QuotaQty = QuataQty;
            }

            [JsonProperty("Part No.")]
            public string PartNo { get; set; }

            [JsonProperty("Lot Date")]
            public string LotDate { get; set; }

            [JsonProperty("Quota Qty")]
            public int QuotaQty { get; set; }

            public int CurrentQty
            {
                get;
                set;
            }
        }

        public class PickingSuggestedLocation
        {
            public PickingSuggestedLocation(string location, string partNo, int qty)
            {
                this.Location = location;
                this.PartNo = partNo;
                this.Qty = qty;
            }

            [JsonProperty("Location")]
            public string Location { get; set; }

            [JsonProperty("Part No.")]
            public string PartNo { get; set; }

            [JsonProperty("Qty")]
            public int Qty { get; set; }
        }

        #endregion

    }
}
