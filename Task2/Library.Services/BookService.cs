using Library.Data;
using System.Collections.Generic;
using System.Linq;

namespace Library.Services
{
    public class BookService
    {
        public IEnumerable<Book> GetAllBooks()
        {
            using (var context = new LibraryDataContext())  //inside using it gets closed automatically
            {
                var result = context.Books.ToList();
                return result;
            }
        }

        static public Book GetBook(string title, string author)
        {
            using (var context = new LibraryDataContext())
            {
                foreach (Book b in context.Books.ToList())
                {
                    if (b.title.Equals(title) && b.author.Equals(author))
                    {
                        return b;
                    }
                }
                return null; 
            }
        }

        public Book GetBookById(int id)
        {
            using (var context = new LibraryDataContext())
            {
                foreach (Book b in context.Books.ToList())
                {
                    if(b.book_id.Equals(id))
                    {
                        return b;
                    }
                }
                return null;
            }
        }

        static public IEnumerable<Book> GetBooksByGenre(string g)
        {
            using (var context = new LibraryDataContext())
            {
                List<Book> result = new List<Book>();
                foreach (Book b in context.Books)
                {
                    if (b.genre.Equals(g))
                    {
                        result.Add(b);
                    }
                }
                return result;
            }
        }

        static public bool AddBook(string a, string t, int year, string g, int q)
        {
            using (var context = new LibraryDataContext())
            {
                if (GetBook(t, a) == null && q >= 0) //if the book doesn't already exist in the db and quantity is not negative
                {
                    Book newBook = new Book
                    {
                        author = a,
                        title = t,
                        publishment_year = year,
                        genre = g,
                        quantity = q
                    };
                    context.Books.InsertOnSubmit(newBook);  //add to collection
                    context.SubmitChanges();    //add to db
                    return true;
                }
                return false;
            }
        }

        static public bool UpdateBookQuantity(int _id, int q)
        {
            using (var context = new LibraryDataContext())
            {
                if (q >= 0)
                {
                    Book book = context.Books.SingleOrDefault(i => i.book_id == _id);
                    book.quantity = q;
                    context.SubmitChanges();
                    return true;
                }
                return false;
            }
        }

        static public int GetMaxId()
        {
            using (var context = new LibraryDataContext())
            {
                if (context.Books.Count() == 0)
                    return 0;
                else
                    return context.Books.OrderByDescending(b => b.book_id).First().book_id;
            }
        }

        static public bool UpdateBook(int id, string title, string author,
            int year, string genre, int quantity)
        {
            using (var context = new LibraryDataContext())
            {
                Book book = context.Books.SingleOrDefault(b => b.book_id == id);
                if (GetBook(title, author) == null)
                {
                    book.title = title;
                    book.author = author;
                    book.publishment_year = year;
                    book.genre = genre;
                    book.quantity = quantity;
                    context.SubmitChanges();
                    return true;
                }
                return false;
            }
        }

        static public bool DeleteBook(string title)
        {
            using (var context = new LibraryDataContext())
            {
                Book book = context.Books.FirstOrDefault(i => i.title == title);
                EventService.DeleteEventsForBook(book.book_id);
                context.Books.DeleteOnSubmit(book);
                context.SubmitChanges();
                return true;
            }
        }
    }
}
