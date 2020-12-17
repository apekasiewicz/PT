using Library.Data;
using Library.Services;
using Library.UI.Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Library.UI
{
    public class ReaderListViewModel : ModelViewBase
    {
        private ReaderService readerService;
        private EventService eventService;
        private BookService bookService;

        public ReaderListViewModel()
        {
            readerService = new ReaderService();
            eventService = new EventService();
            bookService = new BookService();
            AddReaderCommand = new CommandBase(ShowAddNewReader);
            EditReaderCommand = new CommandBase(ShowEditReader);
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

        /*Master detail - displays events for selected customer*/
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

        /*Display book for selected event */
        private Event currentEvent;
        public Event CurrentEvent
        {
            get
            {
                return this.currentEvent;
            }
            set
            {
                this.currentEvent = value;
                OnPropertyChanged("CurrentEvent");
                this.RefreshBook();
            }
        }

        private Book book;
        public Book Book
        {
            get
            {
                return this.book;
            }
            set
            {
                this.book = value;
                OnPropertyChanged("Book");
            }
        }

        private void RefreshBook()
        {
            if (this.currentEvent != null)  //prevents the program crash when reader is changed having selected a book
                Task.Run(() => this.Book = bookService.GetBookById(CurrentEvent.book));
            else
                this.Book = null;
        }

        /*ICommand */
        public ICommand AddReaderCommand { get; private set; }

        public ICommand EditReaderCommand { get; private set; }

        public CommandBase DeleteReaderCommand { get; private set; }

        public Lazy<IWindow> AddWindow { get; set; }

        public Lazy<IWindow> EditWindow { get; set; }

        private void ShowAddNewReader()
        {
            IWindow newWindow = AddWindow.Value;
            newWindow.Show();
        }

        private void ShowEditReader()
        {
            IWindow newWindow = EditWindow.Value;
            newWindow.Show();
        }
    }
}
