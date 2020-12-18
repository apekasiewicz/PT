using Library.Services;
using Library.UI.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Library.UI
{
    public class ReaderViewModel : ModelViewBase, IDataErrorInfo
    {
        public ReaderViewModel()
        {
            AddReaderCommand = new CommandBase(AddReader);
        }

        private string fName;
        public string FName
        {
            get
            {
                return this.fName;
            }
            set
            {
                this.fName = value;
                OnPropertyChanged("FName");
            }
        }

        private string lName;
        public string LName
        {
            get
            {
                return this.lName;
            }
            set
            {
                this.lName = value;
                OnPropertyChanged("LName");
            }
        }

        public ICommand AddReaderCommand { get; private set; }

        private void AddReader()
        {
            ReaderService.AddReader(FName, LName);
        }

        public string Error
        {
            get { return null;  }
        }

        string IDataErrorInfo.this[string columnName]
        {
            get
            {
                if (columnName == "FName")
                {
                    if (this.fName == null)
                        return "Please enter reader's first name";
                    else if (fName.Trim() == string.Empty)
                        return "First name is required";
                    else if (this.fName.Length > 50)
                        return "First name must not exceed 50 characters";
                    /*else if (Regex.IsMatch(this.fName, @"^\d+$")) s.All(c => (c >= 48 && c <= 57))
                        return "First name cannot contain numeric characters";*/
                    else if (isNumeric(this.fName))
                        return "First name cannot contain numeric characters";
                }
                return null;
            }
        }

        private bool isNumeric(string s)
        {
            foreach (char c in s)
            {
                if ((c >= '0' && c <= '9'))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
