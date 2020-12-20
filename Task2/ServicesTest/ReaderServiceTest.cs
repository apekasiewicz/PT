using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Library.Data;
using Library.Services;
using System.Linq;
using System.Collections.Generic;

namespace ServicesTest
{
	[TestClass]
	public class ReaderServiceTest
	{
        [TestMethod]
        public void AddReaderToDatabaseTest()
        {
            Assert.IsTrue(ReaderService.AddReader("Sharoon", "Steel"));

            //delete to restore original db
            Assert.IsTrue(ReaderService.DeleteReader("Sharoon", "Steel"));
        }

        [TestMethod]
        public void AddExisistingReaderToDatabaseTest()
        {
            Assert.IsFalse(ReaderService.AddReader("Neave", "Oneal"));
        }

        [TestMethod]
        public void GetReaderTest()
        {
            Reader reader = ReaderService.GetReader("Judith", "Rojas");
            Assert.AreEqual(reader.reader_f_name, "Judith");
            Assert.AreEqual(reader.reader_l_name, "Rojas");
            Assert.IsNotNull(reader);
        }

        [TestMethod]
        public void GetNotExistingReaderTest()
        {
            Reader reader = ReaderService.GetReader("Judith", "Petty");
            Assert.IsNull(reader);
        }

        [TestMethod]
        public void DeleteReaderUsingSurnameTest()
        {
            Assert.IsTrue(ReaderService.AddReader("Sharoon", "Steel"));

            //delete to restore original db
            Assert.IsTrue(ReaderService.DeleteReader("Sharoon", "Steel"));
        }

        [TestMethod]
        public void DeleteReaderAndHisEventsTest()
        {
            Assert.IsTrue(ReaderService.AddReader("Britney", "Spears"));
            Reader reader = ReaderService.GetReader("Britney", "Spears");

            Assert.IsTrue(BookService.AddBook("Awesome Author", "Awesome Title", 2020, "Cute", 10));
            Book book = BookService.GetBook("Awesome Title", "Awesome Author");

            EventService.BorrowBookForReader(book, reader);
            EventService.ReturnBookByReader(book, reader);

            IEnumerable<Event> events = EventService.GetEventsForReaderByName("Britney", "Spears");
            Assert.AreEqual(events.Count(), 2);

            //restore db
            Assert.IsTrue(ReaderService.DeleteReader(reader.reader_f_name, reader.reader_l_name));
            Assert.IsNull(EventService.GetEventsForReaderByName("Britney", "Spears"));
            Assert.IsTrue(BookService.DeleteBook("Awesome Title"));
        }

        [TestMethod]
        public void UpdateReaderNameTest()
        {
            Assert.IsTrue(ReaderService.AddReader("Britney", "Spears"));
            Reader reader = ReaderService.GetReader("Britney", "Spears");

            Assert.IsTrue(ReaderService.UpdateReader(reader.reader_id, "Ann", "Test"));
            Reader reader1 = ReaderService.GetReader("Britney", "Spears");
            Assert.IsNull(reader1);
            Reader reader2 = ReaderService.GetReader("Ann", "Test");
            Assert.IsNotNull(reader2);

            //restore original db
            Assert.IsTrue(ReaderService.DeleteReader("Ann", "Test"));
        }
    }
}
