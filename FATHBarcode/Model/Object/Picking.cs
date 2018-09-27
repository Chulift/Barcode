using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace FATHBarcode.Model.Object
{
    public class Picking
    {
        [JsonProperty("partNo")]
        public string PartNo { get; set; }

        [JsonProperty("tagNo")]
        public string TagNo { get; set; }

        [JsonProperty("scanDateTime")]
        public string ScanDateTime { get; set; }
    }
}
