using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using FATHBarcode.Base;
using Newtonsoft.Json;

namespace FATHBarcode.Model.Object
{
    public class AuthenticationResponse : GenericObject
    {
        private List<string> _menuList;

        [JsonProperty("status")]
        public bool Status
        {
            get
            {
                return Convert.ToBoolean(this["Status"]);
            }
            set
            {
                this["Status"] = Convert.ToBoolean(value);
            }
        }

        [JsonProperty("menuList")]
        public List<string> MenuList
        {
            get
            {
                return _menuList;
            }
            set
            {
                _menuList = value.ToList<string>();
            }
        }
    }
}
