using Library.Data;
using Library.Services;
using Library.UI.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Library.UI
{
    public class BorrowBookListViewModel : ModelViewBase
    {

        private ReaderService readerService;
        private BookService bookService;

       public BorrowBookListViewModel()
       {
            readerService = new ReaderService();
            bookService = new BookService();

            BorrowBook = new CommandBase(o => { BorrowBookForReader(); }, o => true);
            ReturnBook = new CommandBase(o => { ReturnBookByReader(); }, o => true);

            RefreshReaders();
            RefreshBooks();
        }

        private void RefreshReaders()
        {
            Task.Run(() => this.Readers = readerService.GetReaders());
        }

        private void RefreshBooks()
        {
            Task.Run(() => this.Books = bookService.GetAllBooks());
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

        private IEnumerable<Book> books;
        public IEnumerable<Book> Books
        {
            get => books;

            set
            {
                books = value;
                OnPropertyChanged("Books");
            }
        }

        //selected book
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
                OnPropertyChanged("Book");
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
            }
        }

        /*ICommand*/
        public CommandBase BorrowBook { get; private set; }

        private void BorrowBookForReader()
        {
            EventService.AddEvent(DateTime.Today, true, CurrentBook.book_id, CurrentReader.reader_id);
        }

        public CommandBase ReturnBook { get; private set; }

        private void ReturnBookByReader()
        {
            EventService.DeleteEvent(CurrentReader.reader_id, CurrentBook.book_id);
        }
    }

}
