using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FATHBarcode.Model
{
    public interface ILoginRepository
    {
        FATHBarcode.Model.Object.AuthenticationResponse GetExistingUserIDInDatabase(string userId, string password);
    }
}
