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

        public int GetAllBooksNumber()
        {
            using (var context = new LibraryDataContext())
            {
                return context.Books.Count();
            }
        }

        public Book GetBook(string title, string author)
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

        public IEnumerable<Book> GetBooksByGenre(string g)
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

        public bool AddBook(string a, string t, int year, string g, int q)
        {
            using (var context = new LibraryDataContext())
            {
                if (GetBook(t, a) == null && q >= 0) //if the book doesn't already exist in the db or quantity is not negative
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

        public bool UpdateBookAuthor(int _id, string author) //not sure if id will work with the view
        {
            using (var context = new LibraryDataContext())
            {
                Book book = context.Books.SingleOrDefault(i => i.book_id == _id);
                //if (book != null) always not null?
                //{
                    book.author = author;
                    context.SubmitChanges();
                    return true;
                //}
                //return false;
            }
        }

        public bool UpdateBookTitle(int _id, string title)
        {
            using (var context = new LibraryDataContext())
            {
                Book book = context.Books.SingleOrDefault(i => i.book_id == _id);
                book.title = title;
                context.SubmitChanges();
                return true;
            }
        }

        public bool UpdateBookYear(int _id, int year)
        {
            using (var context = new LibraryDataContext())
            {
                Book book = context.Books.SingleOrDefault(i => i.book_id == _id);
                book.publishment_year = year;
                context.SubmitChanges();
                return true;
            }
        }

        public bool UpdateBookGenre(int _id, string genre)
        {
            using (var context = new LibraryDataContext())
            {
                Book book = context.Books.SingleOrDefault(i => i.book_id == _id);
                book.genre = genre;
                context.SubmitChanges();
                return true;
            }
        }

        public bool UpdateBookQuantity(int _id, int q)
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

        public bool DeleteBook(int _id)
        {
            using (var context = new LibraryDataContext())
            {
                Book book = context.Books.SingleOrDefault(i => i.book_id == _id);
                context.Books.DeleteOnSubmit(book);
                context.SubmitChanges();
                return true;
            }
        }
    }
}
