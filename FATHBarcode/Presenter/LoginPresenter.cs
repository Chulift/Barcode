using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using FATHBarcode.View;
using FATHBarcode.Model;
using FATHBarcode.Model.Object;
using System.Security.Cryptography;

namespace FATHBarcode.Presenter
{
    public class LoginPresenter
    {
        private readonly ILoginView _view;
        private readonly ILoginRepository _repository;

        public LoginPresenter(ILoginView view, ILoginRepository repository)
        {
            _view = view;
            view.Presenter = this;
            _repository = repository;
        }

        public void ProcessScanUserId(string userId, string password)
        {
            AuthenticationResponse auth = null;
            try
            {
                //var encryptedPassword = EncryptPassword(password);
                auth = _repository.GetExistingUserIDInDatabase(userId, password);
                bool result = (auth == null) ? false : auth.Status;
                if (result)
                {
                    Session.Login(userId, password);
                    _view.GoToMenuScreen(auth);
                }
                else
                {
                    _view.ShowMessage("ERR_01", Properties.Resources.ERR_01);
                }

            }
            catch (System.Net.WebException)
            {
            }
            catch (Exception e)
            {
                _view.ShowMessage("Application Error!", e.Message);
            }
        }

        private string EncryptPassword(string password)
        {
            using (var md5Hash = MD5.Create())
            {
                return GetMd5Hash(md5Hash, password);
            }
        }

        private string GetMd5Hash(MD5 md5Hash, string input)
        {
            // Convert data to byte array
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));
            var sb = new StringBuilder();
            // Convert each bytes to hexadecimal
            for (int i = 0; i < data.Length; i++)
            {
                sb.Append(data[i].ToString("x2"));
            }
            return sb.ToString();
        }
    }
}
