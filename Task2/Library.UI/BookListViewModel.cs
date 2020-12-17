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

        public BookListViewModel()
        {
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
                //RefreshBooks();
            }
        }

        private void RefreshBooks()
        {
            Task.Run(() => this.Books = bookService.GetAllBooks());
        }
    }
}
