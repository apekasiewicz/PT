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

        private ReaderService readerService = new ReaderService();
        private BookService bookService = new BookService();

       public BorrowBookListViewModel()
       {
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


        public System.Windows.Input.ICommand BorrowBookCommand
        {
            get;
            private set;
        }


        public void BorrowBook(int readerId, int bookId)
        {
            
        }
    }

}
