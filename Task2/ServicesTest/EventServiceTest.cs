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
        /*
        [TestMethod]
        public void GetEventsFromDatabaseTest()
        {
            Assert.AreEqual(EventService.GetAllEventsNumber(), 5);
        }

        [TestMethod]
        public void AddEventToDatabaseTest()
        {
            Assert.IsTrue(EventService.AddEvent(DateTime.Now, true, 40, 54));
            Assert.AreEqual(EventService.GetAllEventsNumber(), 6);

            //delete to restore original db
            Assert.IsTrue(EventService.DeleteEvent(EventService.GetEvents().ElementAt(5).event_id));
        }

        [TestMethod]
        public void AddEventForNonExistingReaderToDatabaseTest()
        {
            Assert.IsFalse(EventService.AddEvent(DateTime.Now, true, 40, 154));
            Assert.AreEqual(EventService.GetAllEventsNumber(), 5);
        }

        [TestMethod]
        public void GetEventsForReaderByIdTest()
        {
            IEnumerable<Event> events = EventService.GetEventsForReaderById(EventService.GetEvents().ElementAt(0).reader);
            Assert.AreEqual(events.Count(), 1);
            Assert.AreEqual(events.ElementAt(0).is_borrowing_event, true);
        }

        [TestMethod]
        public void GetEventForReaderByIdTest()
        {
            IEnumerable<Event> events = EventService.GetEventsForReaderById(EventService.GetEvents().ElementAt(0).reader);
            Assert.AreEqual(events.Count(), 1);
            Assert.AreEqual(events.ElementAt(0).is_borrowing_event, true);
        }

        [TestMethod]
        public void GetBorrowingEventsTest()
        {
            IEnumerable<Event> events = EventService.GetBorrowingEvents();
            Assert.AreEqual(events.Count(), 3);
        }

        [TestMethod]
        public void GetReturningEventsTest()
        {
            IEnumerable<Event> events = EventService.GetReturningEvents();
            Assert.AreEqual(events.Count(), 2);
        }

        [TestMethod]
        public void GetEventsByDateTest()
        {
            IEnumerable<Event> events = EventService.GetEventsByDate(DateTime.Now);
            Assert.AreEqual(events.Count(), 0);
        }
        */

        [TestMethod]
        public void BorrowBookEventTest()
        {
            Book book = BookService.GetBook("Harry Potter", "J.K. Rowling");
            Reader reader = ReaderService.GetReader("Judith", "Rojas");
            Assert.AreEqual(book.quantity, 3);

            EventService.BorrowBookForReader(book, reader);
            Assert.AreEqual(book.quantity, 2);
            Assert.AreEqual(EventService.GetAllEventsNumber(), 6);
        }


        [TestMethod]
        public void BorrowBookQuantityEqualTo0Test()
        {
            Book book = BookService.GetBook("Pan Tadeusz", "Adam Mickiewicz");
            Reader reader = ReaderService.GetReader("Judith", "Rojas");
            Assert.AreEqual(book.quantity, 0);
            
            EventService.BorrowBookForReader(book, reader);
            Assert.AreEqual(book.quantity, 0);
            Assert.AreEqual(EventService.GetAllEventsNumber(), 6);
        }

        [TestMethod]
        public void BorrowBookNonExisitingBookTest()
        {
            Book book = BookService.GetBook("Harry Potter part 2", "J.K. Rowling");
            Reader reader = ReaderService.GetReader("Judith", "Rojas");

            EventService.BorrowBookForReader(book, reader);
            Assert.AreEqual(EventService.GetAllEventsNumber(), 6);
        }

        [TestMethod]
        public void ReturnBookEventForWrongReaderTest()
        {
            Book book = BookService.GetBook("Harry Potter", "J.K. Rowling");
            Reader reader = ReaderService.GetReader("Charilize", "Padilla");

            EventService.ReturnBookByReader(book, reader);
            Assert.AreEqual(EventService.GetAllEventsNumber(), 6);
        }

        [TestMethod]
        public void ReturnBookEventTest()
        {
            Book book = BookService.GetBook("Harry Potter", "J.K. Rowling");
            Reader reader = ReaderService.GetReader("Judith", "Rojas");
            Assert.AreEqual(book.quantity, 2);

            EventService.ReturnBookByReader(book, reader);
            Assert.AreEqual(book.quantity, 3);
            Assert.AreEqual(EventService.GetAllEventsNumber(), 7);
        }

     





    }
}
