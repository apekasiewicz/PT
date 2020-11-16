using System;
using System.Collections.Generic;
using Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
	[TestClass]
	public class RepositoryTest
	{
		private IRepository repository;
		private DataContext context;
		private IDataGenerator generator;

		[TestInitialize]
		public void Initialize()
		{
			context = new DataContext();
			repository = new DataRepository(context);
			generator = new FixedValuesGenerator();
			generator.GenarateData(context);
		}

		//Test for readers
		[TestMethod]
		public void AddReaderTest()
		{
			Assert.AreEqual(repository.GetAllReadersNumber(), 5);

			repository.AddReader(new Reader("Armaan", "Moran", 12, 0));
			repository.AddReader(new Reader("Brandon", "Cobb", 15, 1));
			repository.AddReader(new Reader("Karter", "Moyer", 18, 2));

			Assert.AreEqual(repository.GetAllReadersNumber(), 8);
		}

		[TestMethod]
		public void DeleteReaderTest()
		{
			repository.DeleteReader(102031);
			Assert.AreEqual(repository.GetAllReadersNumber(), 4);
		}

		//Tests for readers
		[TestMethod]
		public void GetAllReadersTest()
		{
			List<Reader> allReaders = repository.GetAllReaders();
			Assert.AreEqual(allReaders.Count, 5);

			Assert.IsTrue(allReaders.Exists(r => r.Id == 102030));
			Assert.IsTrue(allReaders.Exists(r => r.Id == 102031));
			Assert.IsTrue(allReaders.Exists(r => r.Id == 102032));
			Assert.IsFalse(allReaders.Exists(r => r.Id == 1));
			Assert.IsFalse(allReaders.Exists(r => r.Id == 2));
		}


		[TestMethod]
		public void AddReaderWithTheSameIdTest()
		{
			Assert.ThrowsException<Exception>(() => repository.AddReader(new Reader("Neave", "Oneal", 102030, 4)));
			
			Assert.AreEqual(repository.GetAllReadersNumber(), 5);
			Assert.AreEqual(repository.GetReaderById(102030).FirstName, "Neave");
		}

		[TestMethod]
		public void DeleteNonExistingReaderTest()
		{
			Assert.ThrowsException<InvalidOperationException>(
				() => repository.DeleteReader(18));

			Assert.AreEqual(repository.GetAllReadersNumber(), 5);
		}

		[TestMethod]
		public void DeleteReaderWhoHasBooksTest()
		{
			Assert.ThrowsException<InvalidOperationException>(
				() => repository.DeleteReader(102030));
			Assert.AreEqual(repository.GetAllReadersNumber(), 5);
		}

		[TestMethod]
		public void GetReaderByIdTest()
		{
			Reader returnedReader = repository.GetReaderById(102034);

			Assert.IsNotNull(returnedReader);
			Assert.AreEqual(returnedReader.Id, 102034);
			Assert.AreEqual(returnedReader.FirstName, "Fionn");
			Assert.AreEqual(returnedReader.LastName, "Mcclure");
		}

		[TestMethod]
		[ExpectedException(typeof(System.Exception))]
		public void GetNonExistingReaderByIdTest()
		{
			Reader returnedReader = repository.GetReaderById(20);

			Assert.IsNull(returnedReader);
			Assert.AreNotEqual(returnedReader.Id, 15);
			Assert.AreNotEqual(returnedReader.FirstName, "Brandon");
			Assert.AreNotEqual(returnedReader.LastName, "Cobb");
		}

		[TestMethod]
		public void GetNonExistingReaderExcpetionTest()
		{
			var ex = Assert.ThrowsException<Exception>(() => repository.GetReaderById(20));
			Assert.AreSame(ex.Message, "Reader with such ID does not exist");
		}


		[TestMethod]
		public void UpdateInfoAboutReaderTest()
		{
			Reader newReaderData = new Reader("Armaan", "Moran", 102030, 5);

			Assert.AreEqual(repository.GetReaderById(102030).AmountOfBooksBorrowed, 4);
			repository.UpdateReaderInfo(newReaderData);
			Assert.AreEqual(repository.GetReaderById(102030).AmountOfBooksBorrowed, 5);
		}

		[TestMethod]
		public void UpdateInfoAboutNonExistingReaderTest()
		{
			Reader newReaderData = new Reader("Armaan", "Moran", 12, 5);
			Assert.ThrowsException<InvalidOperationException>(
				() => repository.UpdateReaderInfo(newReaderData));
		}


		// Tests for book catalog
		[TestMethod]
		public void AddBookTest()
		{
			Assert.AreEqual(repository.GetAllBooksNumber(), 5);

			repository.AddBook(new Book(10, "A Game of Thrones", "George R.R.Martin", 1996, BookGenre.Fantasy));
			repository.AddBook(new Book(12, "A Clash of Kings", "George R.R.Martin", 1998, BookGenre.Fantasy));
			repository.AddBook(new Book(13, "A Storm of Swords", "George R.R.Martin", 2000, BookGenre.Fantasy));

			Assert.AreEqual(repository.GetAllBooksNumber(), 8);
		}

		[TestMethod]
		public void DeleteBookTest()
		{
			Assert.AreEqual(repository.GetAllBooksNumber(), 5);
			repository.DeleteBook(2);
			Assert.AreEqual(repository.GetAllBooksNumber(), 4);
		}

		[TestMethod]
		public void GetAllBooksTest()
		{
			Dictionary<int, Book> allBooks = repository.GetAllBooks();
			Assert.AreEqual(allBooks.Count, 5);

			Assert.IsTrue(allBooks.ContainsKey(1));
			Assert.IsTrue(allBooks.ContainsKey(2));
			Assert.IsTrue(allBooks.ContainsKey(3));
			Assert.IsFalse(allBooks.ContainsKey(6));

			Assert.IsTrue(allBooks.ContainsValue(allBooks[1]));
			Assert.IsTrue(allBooks.ContainsValue(allBooks[2]));
			Assert.IsTrue(allBooks.ContainsValue(allBooks[3]));

			Assert.ThrowsException<KeyNotFoundException>(
				() => Assert.IsFalse(allBooks.ContainsValue(allBooks[6])));
		}


		[TestMethod]
		public void AddBookWithTheSameIdTest()
		{
			Assert.AreEqual(repository.GetAllBooksNumber(), 5);
			Assert.ThrowsException<Exception>(
				() => repository.AddBook(new Book(1, "A", "George R.R.Martin", 1996, BookGenre.Fantasy)));

			Assert.AreEqual(repository.GetAllBooksNumber(), 5);
			Assert.AreEqual(repository.GetBookById(1).Title, "Lord of the Rings");
		}

		[TestMethod]
		public void DeleteNonExistingBookTest()
		{
			Assert.ThrowsException<InvalidOperationException>(
				() => repository.DeleteBook(10));

			Assert.AreEqual(repository.GetAllBooksNumber(), 5);
		}


		[TestMethod]
		public void GetBookByIdTest()
		{
			Book returnedBook = repository.GetBookById(5);

			Assert.IsNotNull(returnedBook);
			Assert.AreEqual(returnedBook.Id, 5);
			Assert.AreEqual(returnedBook.Title, "The Notebook");
			Assert.AreEqual(returnedBook.Genre, BookGenre.Romance);
		}

		[TestMethod]
		[ExpectedException(typeof(System.Exception))]
		public void GetNonExistingBookByIdTest()
		{
			Assert.ThrowsException<InvalidOperationException>(
				() => repository.DeleteBook(15));
			Book returnedBook = repository.GetBookById(15);

			Assert.IsNull(returnedBook);
			Assert.AreNotEqual(returnedBook.Id, 5);
			Assert.AreNotEqual(returnedBook.Title, "A Game of Thrones");
			Assert.AreNotEqual(returnedBook.Genre, BookGenre.Fantasy);
		}

		[TestMethod]
		public void GetNonExistingBookExcpetionTest()
		{
			var ex = Assert.ThrowsException<System.Exception>(() => repository.GetBookById(20));
			Assert.AreSame(ex.Message, "Book with such ID does not exist in the library");
		}

		[TestMethod]
		public void UpdateInfoAboutBookTest()
		{
			Book newBookData = new Book(2, "A Clash of Kings", "George R.R.Martin", 1998, BookGenre.Fantasy);

			Assert.AreEqual(repository.GetBookById(1).PublishmentYear, 1954);
			Assert.AreEqual(repository.GetBookById(2).PublishmentYear, 1997);
			repository.UpdateBookInfo(newBookData);
			Assert.AreEqual(repository.GetBookById(1).PublishmentYear, 1954);
			Assert.AreEqual(repository.GetBookById(2).PublishmentYear, 1998);
		}

		[TestMethod]
		public void UpdateInfoAboutNonExistingBookTest()
		{
			Book newBookData = new Book(15, "A Clash of Kings", "George R.R.Martin", 1998, BookGenre.Fantasy);
			Assert.ThrowsException<InvalidOperationException>(
				() => repository.UpdateBookInfo(newBookData));

		}

		[TestMethod]
		public void GetFirstBookByGenreTest()
		{
			Book returnedBook = repository.GetBookByGenre(BookGenre.Fantasy);

			Assert.IsNotNull(returnedBook);
			Assert.AreEqual(returnedBook.Id, 2);
			Assert.AreEqual(returnedBook.Title, "Harry Potter");
			Assert.AreEqual(returnedBook.Genre, BookGenre.Fantasy);
		}


		//Test for states
		[TestMethod]
		public void AddNewBookToInventoryTest()
		{
			Book book1 = new Book(10, "A Game of Thrones", "George R.R.Martin", 1996, BookGenre.Fantasy);
			Book book2 = new Book(12, "The Notebook", "Nicholas Sparks", 1997, BookGenre.Romance);

			Assert.AreEqual(repository.GetAmountOfAvailableCopiesById(10), 0);
			Assert.AreEqual(repository.GetAmountOfAvailableCopiesById(12), 0);

			repository.AddBookState(10, 6);
			repository.AddBookState(12, 10);

			Assert.AreEqual(repository.GetAmountOfAvailableCopiesById(10), 6);
			Assert.AreEqual(repository.GetAmountOfAvailableCopiesById(12), 10);
		}

		[TestMethod]
		public void DeleteBookFromInventoryTest()
		{
			Book book1 = new Book(10, "A Game of Thrones", "George R.R.Martin", 1996, BookGenre.Fantasy);
			Book book2 = new Book(12, "The Notebook", "Nicholas Sparks", 1997, BookGenre.Romance);

			repository.AddBookState(10, 6);
			repository.AddBookState(12, 10);
			repository.DeleteBookstate(12);

			Assert.AreEqual(repository.GetAmountOfAvailableCopiesById(10), 6);
			Assert.AreEqual(repository.GetAmountOfAvailableCopiesById(12), 0);
		}

		[TestMethod]
		public void AddExistingBookToInventoryTest()
		{
			Assert.ThrowsException<Exception>(
				() => repository.AddBookState(1, 6));
			

			Assert.AreEqual(repository.GetAmountOfAvailableCopiesById(1), 10);
		}

		[TestMethod]
		public void DeleteNonExistingBookFromInventoryTest()
		{
			Assert.ThrowsException<InvalidOperationException>(
				() => repository.DeleteBookstate(15));
			Assert.AreEqual(repository.GetAmountOfAvailableCopiesById(15), 0);
		}

		[TestMethod]
		public void GetAllStatesTest()
		{
			Dictionary<int, int> allAvailableBooks = repository.GetAllStates();
			Assert.AreEqual(allAvailableBooks.Count, 5);

			Assert.IsTrue(allAvailableBooks.ContainsKey(1));
			Assert.IsTrue(allAvailableBooks.ContainsKey(2));
			Assert.IsTrue(allAvailableBooks.ContainsKey(3));

			Assert.IsTrue(allAvailableBooks.ContainsValue(10));
		}

		[TestMethod]
		public void UpdateBookStateTest()
		{
			repository.UpdateBookState(1, 10);
			repository.UpdateBookState(2, 2);

			Assert.AreEqual(repository.GetAmountOfAvailableCopiesById(1), 10);
			Assert.AreEqual(repository.GetAmountOfAvailableCopiesById(2), 2);
		}

		[TestMethod]
		public void UpdateBookStateNegativeTest()
		{
			Assert.ThrowsException<InvalidOperationException>(
				() => repository.UpdateBookState(2, -2));

			Assert.AreEqual(repository.GetAmountOfAvailableCopiesById(2), 10);
		}

		[TestMethod]
		[ExpectedException(typeof(Exception))]
		public void UpdateBookStateNonExistingTest()
		{
			repository.UpdateBookState(15, 5);
		}

		[TestMethod]
		public void GetStateTest()
		{
			Dictionary<int, int> allAvailableBooks = repository.GetAllStates();
			Assert.AreEqual(repository.GetState().AvailableBooks, allAvailableBooks);
		}


		//Test for events
		[TestMethod]
		public void AddEventTest()
		{
			Reader reader = new Reader("Armaan", "Moran", 12, 0);

			Assert.AreEqual(repository.GetAllEventsNumber(), 5);
			repository.AddEvent(new BorrowingEvent(6, reader, repository.GetState(), new DateTime(2020, 11, 16, 12, 0, 0)));
			repository.AddEvent(new ReturningEvent(7, reader, repository.GetState(), new DateTime(2020, 11, 16, 12, 0, 0)));
			Assert.AreEqual(repository.GetAllEventsNumber(), 7);
		}
		
		[TestMethod]
		public void DeleteEventTest()
		{
			Assert.AreEqual(repository.GetAllEventsNumber(), 5);
			repository.DeleteEvent(1);
			Assert.AreEqual(repository.GetAllEventsNumber(), 4);
		}
		
		[TestMethod]
		public void GetAllEventsTest()
		{
			List<Event> allEvents = repository.GetAllEvents();
			Assert.AreEqual(allEvents.Count, 5);

			Assert.IsTrue(allEvents.Exists(e => e.Id == 1));
			Assert.IsTrue(allEvents.Exists(e => e.Id == 2));
			Assert.IsFalse(allEvents.Exists(e => e.Id == 6));
		}

		[TestMethod]
		public void AddEventWithTheSameIdTest()
		{
			Reader reader = new Reader("Armaan", "Moran", 12, 0);

			Assert.ThrowsException<Exception>(
				() => repository.AddEvent(new BorrowingEvent(1, reader, repository.GetState(), new DateTime(2020, 11, 16, 12, 0, 0))));
			
			
			Assert.AreEqual(repository.GetAllEventsNumber(), 5);
			Assert.AreEqual(repository.GetEventById(1).Date, new DateTime(2020, 11, 16, 12, 0, 0));
		}
		
		[TestMethod]
		public void DeleteNonExistingEventTest()
		{
			Assert.AreEqual(repository.GetAllEventsNumber(), 5);
			Assert.ThrowsException<InvalidOperationException>(
				() => repository.DeleteEvent(6));
		
			Assert.AreEqual(repository.GetAllEventsNumber(), 5);
		}

		[TestMethod]
		public void GetEventByIdTest()
		{
			Reader reader = new Reader("Neave", "Oneal", 102030, 4);
			Event returnedEvent = repository.GetEventById(1);

			Assert.IsNotNull(returnedEvent);
			Assert.AreEqual(returnedEvent.Id, 1);
			Assert.AreEqual(returnedEvent.Date, new DateTime(2020, 11, 16, 12, 0, 0));
			Assert.AreEqual(returnedEvent.Reader, reader);
		}

		[TestMethod]
		[ExpectedException(typeof(Exception))]
		public void GetNonExistingEventByIdTest()
		{
			Event returnedEvent = repository.GetEventById(10);
		}

		[TestMethod]
		public void GetNonExistingEventExcpetionTest()
		{
			var ex = Assert.ThrowsException<System.Exception>(() => repository.GetEventById(20));
			Assert.AreSame(ex.Message, "Event with such ID does not exist");
		}

		[TestMethod]
		public void UpdateInfoAboutEventTest()
		{
			Reader reader1 = new Reader("Armaan", "Moran", 12, 0);
			BorrowingEvent newBorrowingEvent = new BorrowingEvent(1, reader1, repository.GetState(), new DateTime(2020, 11, 26, 12, 0, 0));

			Assert.AreEqual(repository.GetEventById(1).Date, new DateTime(2020, 11, 16, 12, 0, 0));
			repository.UpdateEventInfo(newBorrowingEvent);
			Assert.AreEqual(repository.GetEventById(1).Date, new DateTime(2020, 11, 26, 12, 0, 0));
		}

		[TestMethod]
		public void UpdateInfoAboutNonExistingEventTest()
		{
			Reader reader1 = new Reader("Armaan", "Moran", 12, 0);
			BorrowingEvent newBorrowingEvent = new BorrowingEvent(6, reader1, repository.GetState(), new DateTime(2020, 11, 26, 12, 0, 0));

			Assert.AreEqual(repository.GetEventById(1).Date, new DateTime(2020, 11, 16, 12, 0, 0));
			Assert.ThrowsException<InvalidOperationException>(
				() => repository.UpdateEventInfo(newBorrowingEvent));
			Assert.AreEqual(repository.GetEventById(1).Date, new DateTime(2020, 11, 16, 12, 0, 0));
		}
	}
}


