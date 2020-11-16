using System;
using System.Collections.Generic;
using Data;
using Logic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
	[TestClass]
	public class DataServiceTest
	{
		private DataService service;
		private DataContext context;
		private IDataGenerator generator;

		[TestInitialize]
		public void Initialize()
		{
			context = new DataContext();
			service = new DataService(new DataRepository(context));
			generator = new FixedValuesGenerator();
			generator.GenarateData(context);
		}

		//Test for readers
		[TestMethod]
		public void AddReaderTest()
		{
			Assert.AreEqual(service.GetAllReadersNumber(), 5);

			service.AddReader(new Reader("Armaan", "Moran", 12, 0));

			Assert.AreEqual(service.GetAllReadersNumber(), 6);

			List<Reader> allReaders = service.GetAllReaders();
			Assert.AreEqual(allReaders.Count, 6);
			Assert.IsTrue(allReaders.Exists(r => r.Id == 12));
		}

		[TestMethod]
		[ExpectedException(typeof(System.Exception))]
		public void AddReaderReapetedIdTest()
		{
			service.AddReader(new Reader("Armaan", "Moran", 12, 0));
			service.AddReader(new Reader("Armaan", "Moran", 12, 0));
			Assert.AreEqual(service.GetAllReadersNumber(), 6);
		}

		[TestMethod]
		public void DeleteReaderTest()
		{
			Assert.AreEqual(service.GetAllReadersNumber(), 5);
			service.DeleteReader(102031);
			Assert.AreEqual(service.GetAllReadersNumber(), 4);
		}

		[TestMethod]
		public void DeleteReaderWhoHasBooksTest()
		{
			Assert.AreEqual(service.GetAllReadersNumber(), 5);
			Assert.ThrowsException<InvalidOperationException>(
				() => service.DeleteReader(102030));
			Assert.AreEqual(service.GetAllReadersNumber(), 5);
		}

		[TestMethod]
		public void DeleteNonExistingReaderTest()
		{
			Assert.ThrowsException<InvalidOperationException>(
				() => service.DeleteReader(18));
		}

		[TestMethod]
		public void GetReaderByIdTest()
		{
			Reader returnedReader = service.GetReaderById(102030);

			Assert.IsNotNull(returnedReader);
			Assert.AreEqual(returnedReader.Id, 102030);
			Assert.AreEqual(returnedReader.FirstName, "Neave");
			Assert.AreEqual(returnedReader.LastName, "Oneal");
			Assert.AreEqual(returnedReader.AmountOfBooksBorrowed, 4);
		}

		[TestMethod]
		public void GetNonExistingReaderByIdTest()
		{
			Assert.ThrowsException<Exception>(
				() => service.GetReaderById(20));
		}

		[TestMethod]
		public void GetAllReadersTest()
		{
			List<Reader> allReaders = service.GetAllReaders();
			Assert.AreEqual(allReaders.Count, 5);

			Assert.IsTrue(allReaders.Exists(r => r.Id == 102030));
			Assert.IsTrue(allReaders.Exists(r => r.Id == 102031));
			Assert.IsTrue(allReaders.Exists(r => r.Id == 102032));
			Assert.IsFalse(allReaders.Exists(r => r.Id == 102));
		}

		[TestMethod]
		public void UpdateInfoAboutReaderTest()
		{
			Reader newReaderData = new Reader("Dummy", "Lala", 102030, 1);

			Assert.AreEqual(service.GetReaderById(102030).AmountOfBooksBorrowed, 4);
			Assert.AreEqual(service.GetReaderById(102030).FirstName, "Neave");
			Assert.AreEqual(service.GetReaderById(102030).LastName, "Oneal");
			service.EditReader(newReaderData);
			Assert.AreEqual(service.GetReaderById(102030).AmountOfBooksBorrowed, 1);
			Assert.AreEqual(service.GetReaderById(102030).FirstName, "Dummy");
			Assert.AreEqual(service.GetReaderById(102030).LastName, "Lala");
		}

		[TestMethod]
		public void UpdateInfoAboutNonExistingReaderTest()
		{
			Reader newReaderData = new Reader("Armaan", "Moran", 12, 5);
			Assert.ThrowsException<InvalidOperationException>(
				() => service.EditReader(newReaderData));
		}

		// Tests for book catalog
		[TestMethod]
		public void AddBookTest()
		{
			Assert.AreEqual(service.GetAllBooksNumber(), 5);

			service.AddBook(new Book(11, "A Game of Thrones", "George R.R.Martin", 1996, BookGenre.Fantasy));
			service.AddBook(new Book(12, "A Clash of Kings", "George R.R.Martin", 1998, BookGenre.Fantasy));
			service.AddBook(new Book(13, "A Storm of Swords", "George R.R.Martin", 2000, BookGenre.Fantasy));

			Assert.AreEqual(service.GetAllBooksNumber(), 8);
		}

		[TestMethod]
		public void DeleteBookTest()
		{
			Assert.AreEqual(service.GetAllBooksNumber(), 5);
			service.DeleteBook(2);
			Assert.AreEqual(service.GetAllBooksNumber(), 4);
		}

		[TestMethod]
		public void GetAllBooksTest()
		{
			Dictionary<int, Book> allBooks = service.GetAllBooks();
			Assert.AreEqual(allBooks.Count, 5);

			Assert.IsTrue(allBooks.ContainsKey(1));
			Assert.IsTrue(allBooks.ContainsKey(2));
			Assert.IsTrue(allBooks.ContainsKey(3));
			Assert.IsTrue(allBooks.ContainsKey(4));
			Assert.IsTrue(allBooks.ContainsKey(5));

			Assert.IsTrue(allBooks.ContainsValue(allBooks[1]));
			Assert.IsTrue(allBooks.ContainsValue(allBooks[2]));
			Assert.IsTrue(allBooks.ContainsValue(allBooks[3]));
			Assert.IsTrue(allBooks.ContainsValue(allBooks[4]));
			Assert.IsTrue(allBooks.ContainsValue(allBooks[5]));
		}

		[TestMethod]
		public void AddBookWithTheSameIdTest()
		{
			Assert.ThrowsException<Exception>(
				() => service.AddBook(new Book(1, "A Game of Thrones", "George R.R.Martin", 1996, BookGenre.Fantasy)));
		}

		[TestMethod]
		public void DeleteNonExistingBookTest()
		{
			Assert.ThrowsException<InvalidOperationException>(
				() => service.DeleteBook(150));
		}

		[TestMethod]
		public void GetBookByIdTest()
		{
			Book returnedBook = service.GetBookById(1);

			Assert.IsNotNull(returnedBook);
			Assert.AreEqual(returnedBook.Id, 1);
			Assert.AreEqual(returnedBook.Title, "Lord of the Rings");
			Assert.AreEqual(returnedBook.Author, "J.R.R.Tolkien");
			Assert.AreEqual(returnedBook.PublishmentYear, 1954);
			Assert.AreEqual(returnedBook.Genre, BookGenre.Adventure);
		}

		[TestMethod]
		[ExpectedException(typeof(System.Exception))]
		public void GetNonExistingBookByIdTest()
		{
			Book returnedBook = service.GetBookById(15);

			Assert.IsNull(returnedBook);
			Assert.AreNotEqual(returnedBook.Id, 5);
			Assert.AreNotEqual(returnedBook.Title, "A Game of Thrones");
			Assert.AreNotEqual(returnedBook.Genre, BookGenre.Fantasy);
		}

		[TestMethod]
		public void UpdateInfoAboutBookTest()
		{
			Book newBookData = new Book(2, "A Clash of Kings", "George R.R.Martin", 1998, BookGenre.Fantasy);

			Assert.AreEqual(service.GetBookById(2).PublishmentYear, 1997);
			service.EditBook(newBookData);
			Assert.AreEqual(service.GetBookById(2).PublishmentYear, 1998);
		}

		[TestMethod]
		public void UpdateInfoAboutNonExistingBookTest()
		{
			Book newBookData = new Book(1231, "A Clash of Kings", "George R.R.Martin", 1998, BookGenre.Fantasy);
			Assert.ThrowsException<InvalidOperationException>(
				() => service.EditBook(newBookData));

		}

		[TestMethod]
		public void GetFirstBookByGenreTest()
		{
			Book returnedBook = service.GetBookByGenre(BookGenre.Fantasy);

			Assert.IsNotNull(returnedBook);
			Assert.AreEqual(returnedBook.Id, 2);
			Assert.AreEqual(returnedBook.Title, "Harry Potter");
			Assert.AreEqual(returnedBook.Genre, BookGenre.Fantasy);
		}


		//Test for states
		[TestMethod]
		public void AddNewBooktateToInventoryTest()
		{
			Book book1 = new Book(6, "A Game of Thrones", "George R.R.Martin", 1996, BookGenre.Fantasy);
			Book book2 = new Book(7, "The Notebook", "Nicholas Sparks", 1997, BookGenre.Romance);

			Assert.AreEqual(service.GetAmountOfAvailableCopiesById(6), 0);
			Assert.AreEqual(service.GetAmountOfAvailableCopiesById(7), 0);

			service.AddNewBookState(6, 5);
			service.AddNewBookState(7, 8);

			Assert.AreEqual(service.GetAmountOfAvailableCopiesById(6), 5);
			Assert.AreEqual(service.GetAmountOfAvailableCopiesById(7), 8);
		}


		[TestMethod]
		public void DeleteBookStateFromInventoryTest()
		{
			Assert.AreEqual(service.GetAmountOfAvailableCopiesById(2), 10);
			service.DeleteBookstate(2);
			Assert.AreEqual(service.GetAmountOfAvailableCopiesById(2), 0);
		}

		[TestMethod]
		public void AddExistingStateBookToInventoryTest()
		{
			Book book1 = new Book(1, "A Game of Thrones", "George R.R.Martin", 1996, BookGenre.Fantasy);

			Assert.ThrowsException<Exception>(() => service.AddNewBookState(1, 6));
			Assert.AreEqual(service.GetAmountOfAvailableCopiesById(1), 10);
			Assert.AreEqual(service.GetBookById(1).Title, "Lord of the Rings");
		}

		[TestMethod]
		public void DeleteNonExistingBookFromInventoryTest()
		{
			Assert.ThrowsException<InvalidOperationException>(() => service.DeleteBookstate(70));
		}

		[TestMethod]
		public void GetAllStatesTest()
		{
			Dictionary<int, int> allAvailableBooks = service.GetAllAvailableBooks();
			Assert.AreEqual(allAvailableBooks.Count, 5);

			Assert.IsTrue(allAvailableBooks.ContainsKey(1));
			Assert.IsTrue(allAvailableBooks.ContainsKey(2));
			Assert.IsTrue(allAvailableBooks.ContainsKey(3));
			Assert.IsTrue(allAvailableBooks.ContainsKey(4));
			Assert.IsTrue(allAvailableBooks.ContainsKey(5));

			Assert.IsTrue(allAvailableBooks.ContainsValue(10));
		}

		[TestMethod]
		public void UpdateBookStateTest()
		{
			service.UpdateStock(1, 6);
			service.UpdateStock(5, 20);

			Assert.AreEqual(service.GetAmountOfAvailableCopiesById(1), 6);
			Assert.AreEqual(service.GetAmountOfAvailableCopiesById(5), 20);
			Assert.AreEqual(service.GetAmountOfAvailableCopiesById(2), 10);
		}

		[TestMethod]
		public void UpdateBookStateNegativeTest()
		{
			Assert.ThrowsException<InvalidOperationException>(() => service.UpdateStock(2, -2));
			Assert.AreEqual(service.GetAmountOfAvailableCopiesById(2), 10);
		}

		[TestMethod]
		public void UpdateBookStateNonExistingTest()
		{
			Assert.ThrowsException<Exception>(() => service.UpdateStock(10, 10));
			Assert.AreEqual(service.GetAmountOfAvailableCopiesById(10), 0);
		}

		[TestMethod]
		public void GetStateTest()
		{
			Dictionary<int, int> allAvailableBooks = service.GetAllAvailableBooks();
			Assert.AreEqual(service.GetAllAvailableBooks(), allAvailableBooks);
		}


		//Test for events		
		[TestMethod]
		public void GetAllEventsTest()
		{
			List<Event> allEvents = service.GetAllEvents();
			Assert.AreEqual(allEvents.Count, 5);

			Assert.IsTrue(allEvents.Exists(e => e.Id == 1));
			Assert.IsTrue(allEvents.Exists(e => e.Id == 2));
			Assert.IsTrue(allEvents.Exists(e => e.Id == 3));
			Assert.IsTrue(allEvents.Exists(e => e.Id == 4));
			Assert.IsTrue(allEvents.Exists(e => e.Id == 5));
		}

		[TestMethod]
		public void GetEventByIdTest()
		{
			Event returnedEvent = service.GetEventById(1);

			Assert.IsNotNull(returnedEvent);
			Assert.AreEqual(returnedEvent.Id, 1);
			Assert.AreEqual(returnedEvent.Date, new DateTime(2020, 11, 16, 12, 0, 0));
			Assert.AreEqual(returnedEvent.Reader, service.GetReaderById(102030));
			Assert.AreEqual(returnedEvent.State, service.GetStateLibrary());
		}


		[TestMethod]
		public void GetNonExistingEventByIdTest()
		{
			Assert.ThrowsException<Exception>(() => service.GetEventById(10));

		}

		[TestMethod]
		public void GetNonExistingEventExcpetionTest()
		{
			var ex = Assert.ThrowsException<System.Exception>(() => service.GetEventById(20));
			Assert.AreSame(ex.Message, "Event with such ID does not exist");
		}

		[TestMethod]
		public void UpdateInfoAboutEventTest()
		{
			BorrowingEvent newBorrowingEvent = new BorrowingEvent(1, service.GetReaderById(102031), service.GetStateLibrary(), new DateTime(2020, 11, 26, 15, 0, 0));

			Assert.AreEqual(service.GetEventById(1).Date, new DateTime(2020, 11, 16, 12, 0, 0));
			Assert.AreEqual(service.GetEventById(1).Reader, service.GetReaderById(102030));
			service.EditEvent(newBorrowingEvent);
			Assert.AreEqual(service.GetEventById(1).Date, new DateTime(2020, 11, 26, 15, 0, 0));
			Assert.AreEqual(service.GetEventById(1).Reader, service.GetReaderById(102031));
		}

		[TestMethod]
		public void UpdateInfoAboutNonExistingEventTest()
		{
			BorrowingEvent newBorrowingEvent = new BorrowingEvent(6, service.GetReaderById(102032), service.GetStateLibrary(), new DateTime(2020, 11, 26, 12, 0, 0));
			Assert.ThrowsException<InvalidOperationException>(() => service.EditEvent(newBorrowingEvent));
		}


		//Tests for borrowing and returning events
		[TestMethod]
		public void BorrowBookTest()
		{
			Assert.AreEqual(service.GetReaderById(102030).AmountOfBooksBorrowed, 4);
			Assert.AreEqual(service.GetAllEventsNumber(), 5);
			Assert.AreEqual(service.GetAmountOfAvailableCopiesById(1), 10);

			service.borrowBook(102030, 6, 1, DateTime.Now);

			Assert.AreEqual(service.GetReaderById(102030).AmountOfBooksBorrowed, 5);
			Assert.AreEqual(service.GetAllEventsNumber(), 6);
			Assert.AreEqual(service.GetAmountOfAvailableCopiesById(1), 9);
		}

		[TestMethod]
		public void BorrowBookNoCopiesInInventoryTest()
		{
			service.borrowBook(102030, 6, 1, DateTime.Now);
			service.borrowBook(102031, 7, 1, DateTime.Now);
			service.borrowBook(102032, 8, 1, DateTime.Now);
			service.borrowBook(102033, 9, 1, DateTime.Now);
			service.borrowBook(102034, 10, 1, DateTime.Now);
			service.borrowBook(102030, 11, 1, DateTime.Now);
			service.borrowBook(102031, 12, 1, DateTime.Now);
			service.borrowBook(102032, 13, 1, DateTime.Now);
			service.borrowBook(102033, 14, 1, DateTime.Now);
			service.borrowBook(102034, 15, 1, DateTime.Now);
			Assert.AreEqual(service.GetAllEventsNumber(), 15);
			Assert.AreEqual(service.GetAmountOfAvailableCopiesById(1), 0);

			var ex = Assert.ThrowsException<InvalidOperationException>(() => service.borrowBook(102030, 11, 1, DateTime.Now));
			Assert.AreSame(ex.Message, "The book is unavailable for borrowing.");
		}

		[TestMethod]
		public void BorrowBookWrongReaderIdTest()
		{
			var ex = Assert.ThrowsException<System.Exception>(() => service.borrowBook(102035, 6, 1, DateTime.Now));
			Assert.AreSame(ex.Message, "Reader with such ID does not exist");
		}

		[TestMethod]
		public void BorrowBookWrongEventIdTest()
		{
			var ex = Assert.ThrowsException<System.Exception>(() => service.borrowBook(102031, 2, 1, DateTime.Now));
			Assert.AreSame(ex.Message, "Event with such ID already exists");
		}

		[TestMethod]
		public void ReturnBookTest()
		{
			service.borrowBook(102031, 6, 1, DateTime.Now);
			Assert.AreEqual(service.GetReaderById(102031).AmountOfBooksBorrowed, 1);
			Assert.AreEqual(service.GetAmountOfAvailableCopiesById(1), 9);

			service.returnBook(102031, 7, 1, DateTime.Now);
			Assert.AreEqual(service.GetReaderById(102031).AmountOfBooksBorrowed, 0);
			Assert.AreEqual(service.GetAmountOfAvailableCopiesById(1), 10);
		}
		
		[TestMethod]
		public void ReturnBookNoCopiesTest()
		{
			var ex = Assert.ThrowsException<InvalidOperationException>(() => service.returnBook(102031, 6, 1, DateTime.Now));
			Assert.AreSame(ex.Message, "You can not return books when you did not borrow.");
		}
		
		[TestMethod]
		public void ReturnBookWrongReaderIdTest()
		{
			var ex = Assert.ThrowsException<System.Exception>(() => service.returnBook(102035, 6, 1, DateTime.Now));
			Assert.AreSame(ex.Message, "Reader with such ID does not exist");
		}

		[TestMethod]
		public void ReturnBookWrongEventIdTest()
		{
			var ex = Assert.ThrowsException<System.Exception>(() => service.returnBook(102030, 2, 1, DateTime.Now));
			Assert.AreSame(ex.Message, "Event with such ID already exists");
		}
	}
}


