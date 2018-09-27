using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using System.ComponentModel;

namespace FATHBarcode.Base
{
    public class GenericObject : INotifyPropertyChanged
    {
        List<string> fields = new List<string>();
        Dictionary<string, object> currentObject;

        public object this[string field]
        {
            get
            {
                return fields.Contains(field) ? currentObject[field] : null;
            }
            set
            {
                if (!fields.Contains(field)) fields.Add(field);
                currentObject[field] = value;
                OnPropertyChanged(field);
            }
        }

        public GenericObject()
        {
            Init();
        }

        public void Init()
        {
            currentObject = new Dictionary<string, object>();
        }

        public List<string> Field
        {
            get { return fields; }
        }

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
