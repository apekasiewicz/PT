using Library.Services;
using Library.UI.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Library.UI
{
    public class ReaderViewModel : ModelViewBase
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
    }
}
