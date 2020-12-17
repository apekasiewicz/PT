using Library.Data;
using Library.Services;
using Library.UI.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Library.UI
{
    public class BookListViewModel : ModelViewBase 
    {
        private BookService bookService;
        private EventService eventService;
        private ReaderService readerService;

        public BookListViewModel()
        {
            this.readerService = new ReaderService();
            this.eventService = new EventService();
            this.bookService = new BookService();
            RefreshBooks();
        }

        private IEnumerable<Book> books;
        public IEnumerable<Book> Books
        {
            get
            {
                return this.books;
            }
            set
            {
                this.books = value;
                OnPropertyChanged("Books");
            }
        }

        private void RefreshBooks()
        {
            Task.Run(() => this.Books = bookService.GetAllBooks());
        }

        private Book currentBook;
        public Book CurrentBook
        {
            get
            {
                return this.currentBook;
            }
            set
            {
                this.currentBook = value;
                OnPropertyChanged("CurrentBook");
                RefreshEvents();
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
            Task.Run(() => this.Events = eventService.GetEventsForBookByTitle(CurrentBook.title));
        }

        /*Display reader for selected event */
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
                this.RefreshReader();
            }
        }

        private Reader reader;
        public Reader Reader
        {
            get
            {
                return this.reader;
            }
            set
            {
                this.reader = value;
                OnPropertyChanged("Reader");
            }
        }

        private void RefreshReader()
        {
            if (this.currentEvent != null)  //prevents the program crash when book is changed having selected an event
                Task.Run(() => this.Reader = readerService.GetReaderById(CurrentEvent.reader));
            else
                this.Reader = null;
        }
    }
}
