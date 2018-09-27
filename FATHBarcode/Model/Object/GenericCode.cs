using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace FATHBarcode.Model.Object
{
    public class GenericCode : Base.GenericObject
    {
        public string Data
        {
            get { return this["Data"].ToString(); }
            set
            {
                this["Data"] = value;
            }
        }

        public string TagNo
        {
            get { return this["Tag No."].ToString(); }
            set { this["Tag No."] = value; }
        }

        public string PartNo
        {
            get { return this["Part No."].ToString(); }
            set { this["Part No,"] = value; }
        }

        public int Qty
        {
            get { return Convert.ToInt32(this["Qty."]); }
            set { this["Qty."] = value; }
        }

        public GenericCode()
        {
            Data = "";
        }

        public static GenericCode ConvertDataToGenericCode(string data)
        {
            var fathTag = new GenericCode();
            var sp = (char)10;
            if (data.Contains(sp))
            {
                var tag = data.Split(sp).ToArray<string>();
                fathTag["Part No."] = tag[0];
                fathTag["Part Name"] = tag[1];
                fathTag["Qty."] = Convert.ToInt32((((tag[2].Split(':').ToArray())[1]).Trim()).Split(' ')[0]);
                fathTag["Model"] = ((tag[3].Split(':').ToArray())[1]).Trim();
                fathTag["Customer"] = ((tag[4].Split(':').ToArray())[1]).Trim();
                fathTag["Tag No."] = ((tag[5].Split(':').ToArray())[1]).Trim();
                fathTag["Production Date"] = ((tag[6].Split(':').ToArray())[1]).Trim();
            }
            return fathTag;
        }
    }
}
