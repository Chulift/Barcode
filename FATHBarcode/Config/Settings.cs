using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Net;

namespace FATHBarcode.Config
{
    public static class Settings
    {
        public enum Screen : byte
        {
            FGPicking = 1,
            Shipping = 2
        }

        public static System.Collections.Specialized.NameValueCollection settings;

        public static bool CheckConnection()
        {
            bool connected = false;
            HttpWebRequest httpWebRequest;
            HttpWebResponse httpWebResponse;
            try
            {
                httpWebRequest = (HttpWebRequest)WebRequest.Create(settings["WSAddress"]);
                httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                if (httpWebResponse.StatusCode == HttpStatusCode.OK)
                {
                    connected = true;
                }
            }
            catch (WebException)
            {
                connected = false;
            }
            catch (Exception)
            {
                connected = false;
            }
            finally
            {
                httpWebRequest = null;
                httpWebResponse = null;
            }
            return connected;
        }
    }
}
