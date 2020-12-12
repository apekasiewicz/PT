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
        public void GetBooksFromDatabaseTest()
        {
            Assert.AreEqual(BookService.GetAllBooksNumber(), 5);
        }

        [TestMethod]
        public void AddBookToDatabaseTest()
        {
            Assert.IsTrue(BookService.AddBook("J.K. Rowling", "Harry ------ Potter", 1997, "Fantasy", 5));
            Assert.AreEqual(BookService.GetAllBooksNumber(), 6);

            //delete to restore original db
            Assert.IsTrue(BookService.DeleteBook("Harry ------ Potter"));
        }


        [TestMethod]
        public void AddBookToDatabaseTheSameTitleTest()
        {
            Assert.IsFalse(BookService.AddBook("J.K. Rowling", "Harry Potter", 1997, "Fantasy", 5));
            Assert.AreEqual(BookService.GetAllBooksNumber(), 5);
        }

        [TestMethod]
        public void AddBookToDatabaseWrongQuantityTest()
        {
            Assert.IsFalse(BookService.AddBook("J.K. Rowling", "Harry Potter", 1997, "Fantasy", -5));
            Assert.AreEqual(BookService.GetAllBooksNumber(), 5);
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
        public void GetBooksByGenreTest()
        {
            IEnumerable<Book> books = BookService.GetBooksByGenre("Adventure");
            Assert.AreEqual(books.Count(), 1);

            Assert.AreEqual(books.ElementAt(0).title, "Lord of the Rings");
            Assert.AreEqual(books.ElementAt(0).quantity, 5);
            Assert.AreEqual(books.ElementAt(0).genre, "Adventure");
        }

        [TestMethod]
        public void DeleteBookByTitleTest()
        {
            Assert.IsTrue(BookService.AddBook("J.K. Rowling", "Harry ------ Potter", 1997, "Fantasy", 5));
            Assert.AreEqual(BookService.GetAllBooksNumber(), 6);

            //delete to restore original db
            Assert.IsTrue(BookService.DeleteBook("Harry ------ Potter"));
            Assert.AreEqual(BookService.GetAllBooksNumber(), 5);
        }

        /*[TestMethod]
        public void UpdateBookTitleTest()
        {
            Assert.IsTrue(BookService.UpdateBookTitle(44, "The Notebook part 2"));
            Book book1 = BookService.GetBook("The Notebook", "Nicholas Sparks");
            Assert.IsNull(book1);
            Book book2 = BookService.GetBook("The Notebook part 2", "Nicholas Sparks");
            Assert.IsNotNull(book2);

            //update to restore original db
            Assert.IsTrue(BookService.UpdateBookTitle(44, "The Notebook"));
        }

        [TestMethod]
        public void UpdateBookAuthorTest()
        {
            Assert.IsTrue(BookService.UpdateBookAuthor(44, "Nicholas Sparks +"));
            Book book1 = BookService.GetBook("The Notebook", "Nicholas Sparks");
            Assert.IsNull(book1);
            Book book2 = BookService.GetBook("The Notebook", "Nicholas Sparks +");
            Assert.IsNotNull(book2);

            //update to restore original db
            Assert.IsTrue(BookService.UpdateBookAuthor(44, "Nicholas Sparks"));
        }


        [TestMethod]
        public void UpdateBookGenreTest()
        {
            Assert.IsTrue(BookService.UpdateBookGenre(44, "Drama"));
            Book book1 = BookService.GetBook("The Notebook", "Nicholas Sparks");
            Assert.AreEqual(book1.genre, "Drama");
            Assert.AreEqual(BookService.GetBooksByGenre("Drama").Count(), 2);

            //update to restore original db
            Assert.IsTrue(BookService.UpdateBookGenre(44, "Romance"));
        }

        [TestMethod]
        public void UpdateBookQuantityTest()
        {
            Assert.IsTrue(BookService.UpdateBookQuantity(44, 10));
            Book book1 = BookService.GetBook("The Notebook", "Nicholas Sparks");
            Assert.AreEqual(book1.quantity, 10);

            //update to restore original db
            Assert.IsTrue(BookService.UpdateBookQuantity(44, 6));
        }

        [TestMethod]
        public void UpdateBookYearTest()
        {
            Assert.IsTrue(BookService.UpdateBookYear(44, 2020));
            Book book1 = BookService.GetBook("The Notebook", "Nicholas Sparks");
            Assert.AreEqual(book1.publishment_year, 2020);

            //update to restore original db
            Assert.IsTrue(BookService.UpdateBookYear(44, 1997));
        }*/
    }
}
