using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using Newtonsoft.Json;

namespace FATHBarcode.Model.Object
{
    public class LoginAuthentication : ILoginRepository
    {
        #region ILoginRepository Members

        public AuthenticationResponse GetExistingUserIDInDatabase(string userId, string password)
        {
            Hashtable request = new Hashtable();
            request.Add("userId", userId);
            request.Add("password", password);
            var data = JsonConvert.SerializeObject(request);

            var serviceReference = new JavaWebService.ServicesBLService();            
            var response = serviceReference.getLogin(data,userId);
            var auth = JsonConvert.DeserializeObject<AuthenticationResponse>(response);
            return auth;
        }

        #endregion
    }
}
