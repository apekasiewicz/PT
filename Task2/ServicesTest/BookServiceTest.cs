using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Library.Data;
using Library.Services;
using System.Linq;
using System.Collections.Generic;

namespace ServicesTest
{
    [TestClass]
    public class BookServiceTest
    {
        [TestMethod]
        public void AddBookToDatabaseTest()
        {
            Assert.IsTrue(BookService.AddBook("J.K. Rowling", "Harry ------ Potter", 1997, "Fantasy", 5));
            Assert.AreEqual(BookService.GetBook("Harry ------ Potter", "J.K. Rowling").author, "J.K. Rowling");
            Assert.AreEqual(BookService.GetBook("Harry ------ Potter", "J.K. Rowling").title, "Harry ------ Potter");
            Assert.AreEqual(BookService.GetBook("Harry ------ Potter", "J.K. Rowling").publishment_year, 1997);
            Assert.AreEqual(BookService.GetBook("Harry ------ Potter", "J.K. Rowling").genre, "Fantasy");
            Assert.AreEqual(BookService.GetBook("Harry ------ Potter", "J.K. Rowling").quantity, 5);

            //delete to restore original db
            Assert.IsTrue(BookService.DeleteBook("Harry ------ Potter"));
        }


        [TestMethod]
        public void AddBookToDatabaseTheSameTitleTest()
        {
            Assert.IsFalse(BookService.AddBook("J.K. Rowling", "Harry Potter", 1997, "Fantasy", 5));
        }

        [TestMethod]
        public void AddBookToDatabaseWrongQuantityTest()
        {
            Assert.IsFalse(BookService.AddBook("J.K. Rowling", "Harry Potter", 1997, "Fantasy", -5));
        }

        [TestMethod]
        public void GetBookByTitleTest()
        {
            Book book = BookService.GetBook("Harry Potter", "J.K. Rowling");
            Assert.AreEqual(book.title, "Harry Potter");
            Assert.AreEqual(book.author, "J.K. Rowling");
            Assert.AreEqual(book.publishment_year, 1997);
            Assert.AreEqual(book.genre, "Fantasy");
        }

        [TestMethod]
        public void DeleteBookByTitleTest()
        {
            Assert.IsTrue(BookService.AddBook("J.K. Rowling", "Harry ------ Potter", 1997, "Fantasy", 5));

            //delete to restore original db
            Assert.IsTrue(BookService.DeleteBook("Harry ------ Potter"));
        }

        [TestMethod]
        public void UpdateBookQuantityTest()
        {
            Assert.IsTrue(BookService.AddBook("J.K. Rowling", "Harry ------ Potter", 1997, "Fantasy", 5));
            Book book = BookService.GetBook("Harry ------ Potter", "J.K. Rowling");

            Assert.IsTrue(BookService.UpdateBookQuantity(book.book_id, 10));
            Book book1 = BookService.GetBook("Harry ------ Potter", "J.K. Rowling");
            Assert.AreEqual(book1.quantity, 10);

            //update to restore original db
            Assert.IsTrue(BookService.DeleteBook("Harry ------ Potter"));
        }

        [TestMethod]
        public void UpdateBookTest()
        {
            Assert.IsTrue(BookService.AddBook("J.K. Rowling", "Harry ------ Potter", 1997, "Fantasy", 5));
            Book book = BookService.GetBook("Harry ------ Potter", "J.K. Rowling");
            Assert.IsTrue(BookService.UpdateBook(book.book_id, "Harry", "Rowling", 1997, "Fantasy", 5));
            Assert.IsNull(BookService.GetBook("Harry ------ Potter", "J.K. Rowling"));
            Book book1 = BookService.GetBook("Harry", "Rowling");
            Assert.IsNotNull(book1);
            Assert.AreEqual(book1.author, "Rowling");
            Assert.AreEqual(book1.title, "Harry");
            Assert.AreEqual(book1.quantity, 5);
            Assert.AreEqual(book1.publishment_year, 1997);
            Assert.AreEqual(book1.genre, "Fantasy");

            //restore original db
            Assert.IsTrue(BookService.DeleteBook("Harry"));
        }
    }
}
