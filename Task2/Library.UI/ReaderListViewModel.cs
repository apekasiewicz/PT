using Library.Data;
using Library.Services;
using Library.UI.Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.UI
{
    public class ReaderListViewModel : ModelViewBase
    {
        private ReaderService readerService = new ReaderService();

        public ReaderListViewModel()
        {
            RefreshReaders();
        }

        private void RefreshReaders()
        {
            Task.Run(() => this.Readers = readerService.GetReaders());
        }

        private IEnumerable<Reader> readers;
        public IEnumerable<Reader> Readers
        {
            get => readers;

            set
            {
                readers = value;
                OnPropertyChanged("Readers");
            }
        }
    }
}
