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
        private EventService eventService = new EventService();

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

        private Reader currentReader;
        public Reader CurrentReader 
        { 
            get
            {
                return this.currentReader;
            }

            set
            {
                this.currentReader = value;
                OnPropertyChanged("CurrentReader");
                this.RefreshEvents();
            }
        }

        private IEnumerable<Event> events;
        public IEnumerable<Event> Events
        {
            get
            {
                return this.events;
            } 
            set
            {
                this.events = value;
                OnPropertyChanged("Events");
            }
        }
        private void RefreshEvents()
        {
            
            Task.Run(() => this.Events = eventService.GetEventsForReaderByName(CurrentReader.reader_f_name, CurrentReader.reader_l_name));
         
        }
    }
}
