using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Library.Data;
using System.Linq;
//using System.Data.SqlClient;

namespace DataTest
{
    [TestClass]
    public class DataLayerTest
    {

        [TestMethod]
        public void AddBookToDatabase()
        {
            LibraryDataContext db = new LibraryDataContext();

            Book book1 = new Book();
            book1.title = "Game of Thrones";
            book1.author = "George R. R. Martin";
            book1.publishment_year = 2011;
            book1.genre = "Adventure";

            db.Books.InsertOnSubmit(book1);
            db.SubmitChanges();

            Book book2 = db.Books.FirstOrDefault(b => b.title.Equals("Game of Thrones"));
            Assert.IsNotNull(book2);
            Assert.AreEqual(book2.title, "Game of Thrones");
            Assert.AreEqual(book2.author, "George R. R. Martin");
            Assert.AreEqual(book2.publishment_year, 2011);
            Assert.AreEqual(book2.genre, "Adventure");
        }
        
        [TestMethod]
        public void DeleteBookFromDatabase()
        {
            LibraryDataContext db = new LibraryDataContext();

            Book book = db.Books.FirstOrDefault(b => b.title.Equals("Game of Thrones"));
            Assert.IsNotNull(book);
            db.Books.DeleteOnSubmit(book);
            db.SubmitChanges();
        }


        [TestMethod]
        public void SelectBookFromDatabase()
        {
            LibraryDataContext db = new LibraryDataContext();

            Book book = db.Books.FirstOrDefault(b => b.title.Equals("Harry Potter"));
            Assert.AreEqual(book.title, "Harry Potter");
            Assert.AreEqual(book.author, "J.K. Rowling");
            Assert.AreEqual(book.publishment_year, 1997);
            Assert.AreEqual(book.genre, "Fantasy");
        }

        [TestMethod]
        public void SelectBookWhichNotExistsFromDatabase()
        {
            LibraryDataContext db = new LibraryDataContext();

            Book book = db.Books.FirstOrDefault(b => b.title.Equals("Harry -------- Potter"));
            Assert.IsNull(book);
        }


        [TestMethod]
        [ExpectedException(typeof(System.Data.SqlClient.SqlException))]
        public void ConnectingToNonExsistingDB()
        {
            string connectionString = "Data Source=DELL-LAT5490-2;Initial Catalog=WrongCatalog;Integrated Security=True";
            using (LibraryDataContext fakeDB = new LibraryDataContext(connectionString))
            {
                Book book1 = new Book();
                book1.title = "Game of Thrones";
                book1.author = "George R. R. Martin";
                book1.publishment_year = 2011;
                book1.genre = "Adventure";

                fakeDB.Books.InsertOnSubmit(book1);
                fakeDB.SubmitChanges();
            }
        }
    }
}
