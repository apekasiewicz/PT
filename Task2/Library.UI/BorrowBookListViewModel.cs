using Library.Data;
using Library.Services;
using Library.UI.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

            BorrowBookCommand = new CommandBase(BorrowBook);
            ReturnBookCommand = new CommandBase(ReturnBook);

            
            RefreshBooks();
            RefreshReaders();
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
        public CommandBase BorrowBookCommand { get; private set; }

        private void BorrowBook()
        {
            bool borrowed = EventService.BorrowBookForReader(CurrentBook, CurrentReader);

            if (borrowed)
            {
                actionText = "Book " + CurrentBook.title + " was borrowed.";
            }
            else
            {
                actionText = "Book cannot be borrowed. No copies in the library.";
            }
            MessageBoxShowDelegate(ActionText);
        }

        public CommandBase ReturnBookCommand { get; private set; }

        private void ReturnBook()
        {
            bool returned = EventService.ReturnBookByReader(CurrentBook, CurrentReader);
            if (returned)
            {
                actionText = "Book " + CurrentBook.title + " was returned to the library.";
            }
            else
            {
                actionText = "Reader " + CurrentReader.reader_f_name + " " +
                    CurrentReader.reader_l_name + " does not have this title among the books borrowed from the library";
            }
            MessageBoxShowDelegate(ActionText);
        }



        // pop up window

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
