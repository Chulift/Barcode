using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace FATHBarcode
{
    class Session
    {
        static User _currentUser;

        public static User GetCurrentUser()
        {
            return _currentUser;
        }

        public static void Login(string userId, string password)
        {
            _currentUser = new User(userId, password);
        }

        public static void Logout()
        {
            _currentUser = null;
        }

        public class User
        {
            private string _userId;
            private string _password;
            public User(string userId, string password)
            {
                _userId = userId;
                _password = password;
            }

            public string UserID
            {
                get { return _userId; }
            }
        }
    }
}
