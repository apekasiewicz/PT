using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.Presentation.Model;
using Library.Presentation.ViewModel.MVVM;
using Library.Services;

namespace Library.Presentation.ViewModel
{
    public class MainViewModel : ModelViewBase
    {
        private ReaderService readerService = new ReaderService();

        public MainViewModel()
        {
            this.RefreshReaders();
        }

        private void RefreshReaders()
        {
            //Task.Run(() => readerService.GetReaders());
        }

        private IEnumerable<Reader> readers;
        public IEnumerable<Reader> Readers
        {
            get => this.readers;

            set
            {
                this.readers = value;
                OnPropertyChanged("Readers");
            }
            
        }
    }
}
