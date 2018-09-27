using System;
using System.Linq;
using System.Collections.Generic;
using System.Windows.Forms;

namespace FATHBarcode
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [MTAThread]
        static void Main()
        {
            var filePath = System.IO.Path.Combine(CommonLibrary.IO.Common.GetAppPath(), "App.config");
            Config.Settings.settings = CommonLibrary.IO.Common.GetConfigurationFromXML(filePath, "appSettings");
            var view = new View.Object.LoginForm();
            var repository = new Model.Object.LoginAuthentication();
            var presenter = new Presenter.LoginPresenter(view, repository);
            Application.Run(view);
        }
    }
}