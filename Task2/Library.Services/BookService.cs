using Library.Data;
using System.Collections.Generic;
using System.Linq;

namespace Library.Services
{
    public class BookService
    {
        private LibraryDataContext context = new LibraryDataContext();

        public List<Book> GetAllBooks()
        {
            return new List<Book>(context.Books);
        }

        /*Dictionary<int, Book> GetAllBooks();
        int GetAllBooksNumber();
        Book GetBookById(int id);
        Book GetBookByGenre(BookGenre genre);
        void UpdateBookInfo(Book book);
        void AddBook(Book book);
        void DeleteBook(int id);*/
    }
}
