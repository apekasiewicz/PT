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

		/*
		//Test for states
		[TestMethod]
		public void AddNewBookToInventoryTest()
		{
			Book book1 = new Book(1, "A Game of Thrones", "George R.R.Martin", 1996, BookGenre.Fantasy);
			Book book2 = new Book(2, "The Notebook", "Nicholas Sparks", 1997, BookGenre.Romance);

			Assert.AreEqual(repository.GetAmountOfAvailableCopiesById(1), 0);
			Assert.AreEqual(repository.GetAmountOfAvailableCopiesById(2), 0);

			repository.AddBookState(1, 6);
			repository.AddBookState(2, 10);

			Assert.AreEqual(repository.GetAmountOfAvailableCopiesById(1), 6);
			Assert.AreEqual(repository.GetAmountOfAvailableCopiesById(2), 10);
		}

		[TestMethod]
		public void DeleteBookFromInventoryTest()
		{
			Book book1 = new Book(1, "A Game of Thrones", "George R.R.Martin", 1996, BookGenre.Fantasy);
			Book book2 = new Book(2, "The Notebook", "Nicholas Sparks", 1997, BookGenre.Romance);

			repository.AddBookState(1, 6);
			repository.AddBookState(2, 10);
			repository.DeleteBookstate(2);

			Assert.AreEqual(repository.GetAmountOfAvailableCopiesById(1), 6);
			Assert.AreEqual(repository.GetAmountOfAvailableCopiesById(2), 0);
		}

		[TestMethod]
		[ExpectedException(typeof(System.Exception))]
		public void AddExistingBookToInventoryTest()
		{
			Book book1 = new Book(1, "A Game of Thrones", "George R.R.Martin", 1996, BookGenre.Fantasy);

			repository.AddBookState(1, 6);
			repository.AddBookState(1, 10);

			Assert.AreEqual(repository.GetAmountOfAvailableCopiesById(1), 6);
		}

		[TestMethod]
		public void DeleteNonExistingBookFromInventoryTest()
		{
			Book book1 = new Book(1, "A Game of Thrones", "George R.R.Martin", 1996, BookGenre.Fantasy);

			repository.AddBookState(1, 6);
			Assert.ThrowsException<InvalidOperationException>(
				() => repository.DeleteBookstate(5));
			Assert.AreEqual(repository.GetAmountOfAvailableCopiesById(5), 0);
		}

		[TestMethod]
		public void GetAllStatesTest()
		{
			Book book1 = new Book(1, "A Game of Thrones", "George R.R.Martin", 1996, BookGenre.Fantasy);
			Book book2 = new Book(2, "A Clash of Kings", "George R.R.Martin", 1998, BookGenre.Fantasy);
			Book book3 = new Book(3, "A Storm of Swords", "George R.R.Martin", 2000, BookGenre.Fantasy);

			repository.AddBookState(1, 5);
			repository.AddBookState(2, 4);
			repository.AddBookState(3, 3);

			Dictionary<int, int> allAvailableBooks = repository.GetAllStates();
			Assert.AreEqual(allAvailableBooks.Count, 3);

			Assert.IsTrue(allAvailableBooks.ContainsKey(1));
			Assert.IsTrue(allAvailableBooks.ContainsKey(2));
			Assert.IsTrue(allAvailableBooks.ContainsKey(3));

			Assert.IsTrue(allAvailableBooks.ContainsValue(5));
			Assert.IsTrue(allAvailableBooks.ContainsValue(4));
			Assert.IsTrue(allAvailableBooks.ContainsValue(3));
		}

		[TestMethod]
		public void UpdateBookStateTest()
		{
			Book book1 = new Book(1, "A Game of Thrones", "George R.R.Martin", 1996, BookGenre.Fantasy);
			Book book2 = new Book(2, "A Clash of Kings", "George R.R.Martin", 1998, BookGenre.Fantasy);
			repository.AddBookState(1, 5);
			repository.AddBookState(2, 4);

			repository.UpdateBookState(1, 10);
			repository.UpdateBookState(2, 2);

			Assert.AreEqual(repository.GetAmountOfAvailableCopiesById(1), 10);
			Assert.AreEqual(repository.GetAmountOfAvailableCopiesById(2), 2);
		}

		[TestMethod]
		[ExpectedException(typeof(InvalidOperationException))]
		public void UpdateBookStateNegativeTest()
		{
			Book book1 = new Book(1, "A Game of Thrones", "George R.R.Martin", 1996, BookGenre.Fantasy);
			Book book2 = new Book(2, "A Clash of Kings", "George R.R.Martin", 1998, BookGenre.Fantasy);
			repository.AddBookState(1, 5);
			repository.AddBookState(2, 4);

			repository.UpdateBookState(2, -2);
		}

		[TestMethod]
		[ExpectedException(typeof(Exception))]
		public void UpdateBookStateNonExistingTest()
		{
			repository.UpdateBookState(5, 5);
			Assert.AreEqual(repository.GetAmountOfAvailableCopiesById(5), 0);
		}

		[TestMethod]
		public void GetStateTest()
		{
			Book book1 = new Book(1, "A Game of Thrones", "George R.R.Martin", 1996, BookGenre.Fantasy);
			Book book2 = new Book(2, "A Clash of Kings", "George R.R.Martin", 1998, BookGenre.Fantasy);
			Book book3 = new Book(3, "A Storm of Swords", "George R.R.Martin", 2000, BookGenre.Fantasy);

			repository.AddBookState(1, 5);
			repository.AddBookState(2, 4);
			repository.AddBookState(3, 3);

			Dictionary<int, int> allAvailableBooks = repository.GetAllStates();
			Assert.AreEqual(repository.GetState().AvailableBooks, allAvailableBooks);
		}


		//Test for events
		[TestMethod]
		public void AddEventTest()
		{
			Reader reader = new Reader("Armaan", "Moran", 12, 0);

			Assert.AreEqual(repository.GetAllEventsNumber(), 0);
			repository.AddEvent(new BorrowingEvent(1, reader, repository.GetState(), new DateTime(2020, 11, 16, 12, 0, 0)));
			repository.AddEvent(new ReturningEvent(2, reader, repository.GetState(), new DateTime(2020, 11, 16, 12, 0, 0)));
			Assert.AreEqual(repository.GetAllEventsNumber(), 2);
		}
		
		[TestMethod]
		public void DeleteEventTest()
		{
			Reader reader = new Reader("Armaan", "Moran", 12, 0);

			repository.AddEvent(new BorrowingEvent(1, reader, repository.GetState(), new DateTime(2020, 11, 16, 12, 0, 0)));
			repository.AddEvent(new ReturningEvent(2, reader, repository.GetState(), new DateTime(2020, 11, 16, 12, 0, 0)));

			repository.DeleteEvent(1);
			Assert.AreEqual(repository.GetAllEventsNumber(), 1);
		}
		
		[TestMethod]
		public void GetAllEventsTest()
		{
			Reader reader1 = new Reader("Armaan", "Moran", 12, 0);
			Reader reader2 = new Reader("Jon", "Snow", 5, 6);

			repository.AddEvent(new BorrowingEvent(1, reader1, repository.GetState(), new DateTime(2020, 11, 16, 12, 0, 0)));
			repository.AddEvent(new ReturningEvent(2, reader2, repository.GetState(), new DateTime(2020, 11, 16, 12, 0, 0)));

			List<Event> allEvents = repository.GetAllEvents();
			Assert.AreEqual(allEvents.Count, 2);

			Assert.IsTrue(allEvents.Exists(e => e.Id == 1));
			Assert.IsTrue(allEvents.Exists(e => e.Id == 2));
			Assert.IsFalse(allEvents.Exists(e => e.Id == 3));
		}

		[TestMethod]
		[ExpectedException(typeof(System.Exception))]
		public void AddEventWithTheSameIdTest()
		{
			Reader reader = new Reader("Armaan", "Moran", 12, 0);

			repository.AddEvent(new BorrowingEvent(1, reader, repository.GetState(), new DateTime(2020, 11, 16, 12, 0, 0)));
			repository.AddEvent(new ReturningEvent(1, reader, repository.GetState(), new DateTime(2020, 11, 16, 12, 0, 0)));
			
			Assert.AreEqual(repository.GetAllEventsNumber(), 1);
			Assert.AreEqual(repository.GetEventById(1).Date, "2020, 11, 16, 12, 0, 0");
		}
		
		[TestMethod]
		public void DeleteNonExistingEventTest()
		{
			Reader reader = new Reader("Armaan", "Moran", 12, 0);
			repository.AddEvent(new BorrowingEvent(1, reader, repository.GetState(), new DateTime(2020, 11, 16, 12, 0, 0)));

			Assert.ThrowsException<InvalidOperationException>(
				() => repository.DeleteEvent(5));
		
			Assert.AreEqual(repository.GetAllEventsNumber(), 1);
		}

		[TestMethod]
		public void GetEventByIdTest()
		{
			Reader reader = new Reader("Armaan", "Moran", 12, 0);
			repository.AddEvent(new BorrowingEvent(1, reader, repository.GetState(), new DateTime(2020, 11, 16, 12, 0, 0)));

			Event returnedEvent = repository.GetEventById(1);

			Assert.IsNotNull(returnedEvent);
			Assert.AreEqual(returnedEvent.Id, 1);
			Assert.AreEqual(returnedEvent.Date, new DateTime(2020, 11, 16, 12, 0, 0));
			Assert.AreEqual(returnedEvent.Reader, reader);
		}

		[TestMethod]
		[ExpectedException(typeof(System.Exception))]
		public void GetNonExistingEventByIdTest()
		{
			Reader reader = new Reader("Armaan", "Moran", 12, 0);
			repository.AddEvent(new BorrowingEvent(1, reader, repository.GetState(), new DateTime(2020, 11, 16, 12, 0, 0)));

			Event returnedEvent = repository.GetEventById(10);

			Assert.IsNull(returnedEvent);
			Assert.AreNotEqual(returnedEvent.Id, 1);
			Assert.AreNotEqual(returnedEvent.Date, new DateTime(2020, 11, 16, 12, 0, 0));
			Assert.AreNotEqual(returnedEvent.Reader, reader);
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
			Reader reader2 = new Reader("Jon", "Snow", 6, 2);

			repository.AddEvent(new BorrowingEvent(1, reader1, repository.GetState(), new DateTime(2020, 11, 16, 12, 0, 0)));
			repository.AddEvent(new ReturningEvent(5, reader1, repository.GetState(), new DateTime(2020, 11, 16, 12, 0, 0)));

			BorrowingEvent newBorrowingEvent = new BorrowingEvent(1, reader1, repository.GetState(), new DateTime(2020, 11, 26, 12, 0, 0));

			Assert.AreEqual(repository.GetEventById(1).Date, new DateTime(2020, 11, 16, 12, 0, 0));
			repository.UpdateEventInfo(newBorrowingEvent);
			Assert.AreEqual(repository.GetEventById(1).Date, new DateTime(2020, 11, 26, 12, 0, 0));
		}

		[TestMethod]
		public void UpdateInfoAboutNonExistingEventTest()
		{
			Reader reader1 = new Reader("Armaan", "Moran", 12, 0);
			Reader reader2 = new Reader("Jon", "Snow", 6, 2);

			repository.AddEvent(new BorrowingEvent(1, reader1, repository.GetState(), new DateTime(2020, 11, 16, 12, 0, 0)));
			repository.AddEvent(new ReturningEvent(5, reader1, repository.GetState(), new DateTime(2020, 11, 16, 12, 0, 0)));

			BorrowingEvent newBorrowingEvent = new BorrowingEvent(6, reader1, repository.GetState(), new DateTime(2020, 11, 26, 12, 0, 0));

			Assert.AreEqual(repository.GetEventById(1).Date, new DateTime(2020, 11, 16, 12, 0, 0));
			Assert.ThrowsException<InvalidOperationException>(
				() => repository.UpdateEventInfo(newBorrowingEvent));
			Assert.AreEqual(repository.GetEventById(1).Date, new DateTime(2020, 11, 16, 12, 0, 0));
		}
	}*/

	}
}


