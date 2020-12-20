using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Library.Data;
using Library.Services;
using System.Linq;
using System.Collections.Generic;

namespace ServicesTest
{
    [TestClass]
    public class EventServiceTest
    {
        [TestMethod]
        public void AddEventForNonExistingReaderToDatabaseTest()
        {
            Assert.IsFalse(EventService.AddEvent(DateTime.Now, true, 40, 154));
        }

        [TestMethod]
        public void AddEventToDatabaseTest()
        {
            Assert.IsTrue(ReaderService.AddReader("Britney", "Spears"));
            Reader reader = ReaderService.GetReader("Britney", "Spears");

            Assert.IsTrue(BookService.AddBook("Awesome Author", "Awesome Title", 2020, "Cute", 10));
            Book book = BookService.GetBook("Awesome Title", "Awesome Author");

            Assert.IsTrue(EventService.AddEvent(DateTime.Now, true, book.book_id, reader.reader_id));

            //delete to restore original db
            Assert.IsTrue(EventService.DeleteEvent(reader.reader_id, book.book_id));
            Assert.IsTrue(ReaderService.DeleteReader(reader.reader_f_name, reader.reader_l_name));
            Assert.IsTrue(BookService.DeleteBook(book.title));
        }

        [TestMethod]
        public void GetEventsForReaderByNameTest()
        {
            Assert.IsTrue(ReaderService.AddReader("Britney", "Spears"));
            Reader reader = ReaderService.GetReader("Britney", "Spears");

            Assert.IsTrue(BookService.AddBook("Awesome Author", "Awesome Title", 2020, "Cute", 10));
            Book book = BookService.GetBook("Awesome Title", "Awesome Author");

            EventService.BorrowBookForReader(book, reader);
            EventService.ReturnBookByReader(book, reader);

            IEnumerable<Event> events = EventService.GetEventsForReaderByName("Britney", "Spears");
            Assert.AreEqual(events.Count(), 2);
            Assert.AreEqual(events.ElementAt(0).is_borrowing_event, true);
            Assert.AreEqual(events.ElementAt(1).is_borrowing_event, false);

            //restore db
            Assert.IsTrue(ReaderService.DeleteReader(reader.reader_f_name, reader.reader_l_name));
            Assert.IsTrue(BookService.DeleteBook(book.title));
        }

        [TestMethod]
        public void GetBorrowingEventsTest()
        {
            IEnumerable<Event> events = EventService.GetBorrowingEvents();
            Assert.IsNotNull(events);
        }

        [TestMethod]
        public void GetReturningEventsTest()
        {
            IEnumerable<Event> events = EventService.GetReturningEvents();
            Assert.IsNotNull(events);
        }

        [TestMethod]
        public void BorrowBookEventTest()
        {
            Book book = BookService.GetBook("Harry Potter", "J.K. Rowling");
            Reader reader = ReaderService.GetReader("Judith", "Rojas");
            Assert.AreEqual(book.quantity, 3);

            EventService.BorrowBookForReader(book, reader);
            Assert.AreEqual(book.quantity, 2);
        }


        [TestMethod]
        public void BorrowBookQuantityEqualTo0Test()
        {
            BookService.AddBook("a", "t", 1999, "g", 0);
            Book book = BookService.GetBook("t", "a");
            Reader reader = ReaderService.GetReader("Judith", "Rojas");
            Assert.AreEqual(book.quantity, 0);
            
            EventService.BorrowBookForReader(book, reader);
            Assert.AreEqual(book.quantity, 0);
        }

        [TestMethod]
        public void BorrowBookNonExisitingBookTest()
        {
            Book book = BookService.GetBook("Harry Potter part 2", "J.K. Rowling");
            Reader reader = ReaderService.GetReader("Judith", "Rojas");

            EventService.BorrowBookForReader(book, reader);
        }

        [TestMethod]
        public void ReturnBookEventForWrongReaderTest()
        {
            Book book = BookService.GetBook("Harry Potter", "J.K. Rowling");
            Reader reader = ReaderService.GetReader("Charilize", "Padilla");

            EventService.ReturnBookByReader(book, reader);
        }

        [TestMethod]
        public void ReturnBookEventTest()
        {
            Book book = BookService.GetBook("Harry Potter", "J.K. Rowling");
            Reader reader = ReaderService.GetReader("Judith", "Rojas");
            Assert.AreEqual(book.quantity, 2);

            EventService.ReturnBookByReader(book, reader);
            Assert.AreEqual(book.quantity, 3);
        }

    }
}
