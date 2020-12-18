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
            //actionText = "Reader Added";
            AddReaderCommand = new CommandBase(AddReader);
            EditReaderCommand = new CommandBase(EditReader);
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

        public CommandBase AddReaderCommand { get; private set; }

        private void AddReader()
        {
            bool added = ReaderService.AddReader(FName, LName);
            if (added)
            {
                actionText = "Reader Added";
            } else
            {
                actionText = "Reader already exists in the database";
            }
            MessageBoxShowDelegate(ActionText);
        }

        private string id;
        public string Id
        {
            get
            {
                return this.id;
            }
            set
            {
                this.id = value;
                OnPropertyChanged("Id");
            }
        }

        public CommandBase EditReaderCommand { get; private set; }

        private void EditReader()
        {
            bool edited = ReaderService.UpdateReader(int.Parse(Id), FName, LName);
            if (edited)
            {
                actionText = "Reader edited";
            }
            else
            {
                actionText = "Such reader already exists in the database";
            }
            MessageBoxShowDelegate(ActionText);
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
                    else if (isNumeric(this.fName))
                        return "First name cannot contain numeric characters";
                }
                else if (columnName == "LName")
                {
                    if (this.lName == null)
                        return "Please enter reader's last name";
                    else if (this.lName.Trim() == string.Empty)
                        return "Last name is required";
                    else if (this.lName.Length > 50)
                        return "Last name must not exceed 50 characters";
                    else if (isNumeric(this.lName))
                        return "Last name cannot contain numeric characters";
                } 
                else if (columnName == "Id")
                {
                    int entered_id, max_id = ReaderService.GetMaxId();

                    if (!int.TryParse(this.id, out entered_id))
                    {
                        return "ID must be an integer";
                    }
                     else
                    {
                        if (entered_id < 1)
                            return "ID must be at least 1";
                        else if (entered_id > max_id)
                            return "ID must not excced " + max_id.ToString();
                    }

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

        private string actionText;
        public string ActionText
        {
            get
            {
                return this.actionText;
            }
            set
            {
                this.actionText = value;
                OnPropertyChanged("ActionText");
            }
        }

        public CommandBase DisplayPopUpCommand { get; private set; }

        public Action<string> MessageBoxShowDelegate { get; set; } = x => throw new ArgumentOutOfRangeException($"The delegate {nameof(MessageBoxShowDelegate)} must be assigned by the view layer");
    }
}
