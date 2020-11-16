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
		}

		//Test for readers
		[TestMethod]
		public void AddReaderTest()
		{
			Assert.AreEqual(repository.GetAllReadersNumber(), 0);

			repository.AddReader(new Reader("Armaan", "Moran", 12, 0));
			repository.AddReader(new Reader("Brandon", "Cobb", 15, 1));
			repository.AddReader(new Reader("Karter", "Moyer", 18, 2));

			Assert.AreEqual(repository.GetAllReadersNumber(), 3);
		}

		[TestMethod]
		public void DeleteReaderTest()
		{
			repository.AddReader(new Reader("Armaan", "Moran", 12, 0));
			repository.AddReader(new Reader("Brandon", "Cobb", 15, 1));

			repository.DeleteReader(12);
			Assert.AreEqual(repository.GetAllReadersNumber(), 1);
		}

		//Tests for readers
		[TestMethod]
		public void GetAllReadersTest()
		{
			repository.AddReader(new Reader("Armaan", "Moran", 12, 0));
			repository.AddReader(new Reader("Brandon", "Cobb", 15, 1));
			repository.AddReader(new Reader("Jon", "Snow", 20, 3));

			List<Reader> allReaders = repository.GetAllReaders();
			Assert.AreEqual(allReaders.Count, 3);

			Assert.IsTrue(allReaders.Exists(r => r.Id == 12));
			Assert.IsTrue(allReaders.Exists(r => r.Id == 15));
			Assert.IsTrue(allReaders.Exists(r => r.Id == 20));
			Assert.IsFalse(allReaders.Exists(r => r.Id == 5));
		}


		[TestMethod]
		[ExpectedException(typeof(System.Exception))]
		public void AddReaderWithTheSameIdTest()
		{
			repository.AddReader(new Reader("Armaan", "Moran", 12, 0));
			repository.AddReader(new Reader("Brandon", "Cobb", 12, 1));
			repository.AddReader(new Reader("Karter", "Moyer", 18, 2));

			Assert.AreEqual(repository.GetAllReadersNumber(), 2);
			Assert.AreEqual(repository.GetReaderById(12).FirstName, "Armaan");
		}

		[TestMethod]
		public void DeleteNonExistingReaderTest()
		{
			repository.AddReader(new Reader("Armaan", "Moran", 12, 0));
			repository.AddReader(new Reader("Brandon", "Cobb", 15, 1));

			repository.DeleteReader(18);
			Assert.AreEqual(repository.GetAllReadersNumber(), 2);
		}

		[TestMethod]
		public void DeleteReaderWhoHasBooksTest()
		{
			repository.AddReader(new Reader("Brandon", "Cobb", 15, 1));

			repository.DeleteReader(15);
			Assert.AreEqual(repository.GetAllReadersNumber(), 1);
		}

		[TestMethod]
		public void GetReaderByIdTest()
		{
			repository.AddReader(new Reader("Brandon", "Cobb", 15, 1));
			Reader returnedReader = repository.GetReaderById(15);

			Assert.IsNotNull(returnedReader);
			Assert.AreEqual(returnedReader.Id, 15);
			Assert.AreEqual(returnedReader.FirstName, "Brandon");
			Assert.AreEqual(returnedReader.LastName, "Cobb");
		}

		[TestMethod]
		[ExpectedException(typeof(System.Exception))]
		public void GetNonExistingReaderByIdTest()
		{
			repository.AddReader(new Reader("Brandon", "Cobb", 15, 1));
			Reader returnedReader = repository.GetReaderById(20);

			Assert.IsNull(returnedReader);
			Assert.AreNotEqual(returnedReader.Id, 15);
			Assert.AreNotEqual(returnedReader.FirstName, "Brandon");
			Assert.AreNotEqual(returnedReader.LastName, "Cobb");
		}

		[TestMethod]
		public void GetNonExistingReaderExcpetionTest()
		{
			var ex = Assert.ThrowsException<System.Exception>(() => repository.GetReaderById(20));
			Assert.AreSame(ex.Message, "Reader with such ID does not exist");
		}


		[TestMethod]
		public void UpdateInfoAboutReaderTest()
		{
			repository.AddReader(new Reader("Armaan", "Moran", 12, 0));
			repository.AddReader(new Reader("Brandon", "Cobb", 15, 1));

			Reader newReaderData = new Reader("Armaan", "Moran", 12, 5);

			Assert.AreEqual(repository.GetReaderById(12).AmountOfBooksBorrowed, 0);
			Assert.AreEqual(repository.GetReaderById(15).AmountOfBooksBorrowed, 1);
			repository.UpdateReaderInfo(newReaderData);
			Assert.AreEqual(repository.GetReaderById(12).AmountOfBooksBorrowed, 5);
			Assert.AreEqual(repository.GetReaderById(15).AmountOfBooksBorrowed, 1);
		}


		// Tests for book catalog
		[TestMethod]
		public void AddBookTest()
		{
			Assert.AreEqual(repository.GetAllBooksNumber(), 0);

			repository.AddBook(new Book(1, "A Game of Thrones", "George R.R.Martin", 1996, BookGenre.Fantasy));
			repository.AddBook(new Book(2, "A Clash of Kings", "George R.R.Martin", 1998, BookGenre.Fantasy));
			repository.AddBook(new Book(3, "A Storm of Swords", "George R.R.Martin", 2000, BookGenre.Fantasy));

			Assert.AreEqual(repository.GetAllBooksNumber(), 3);
		}

		[TestMethod]
		public void DeleteBookTest()
		{
			repository.AddBook(new Book(1, "A Game of Thrones", "George R.R.Martin", 1996, BookGenre.Fantasy));
			repository.AddBook(new Book(2, "A Clash of Kings", "George R.R.Martin", 1998, BookGenre.Fantasy));

			repository.DeleteBook(2);
			Assert.AreEqual(repository.GetAllBooksNumber(), 1);
		}

		[TestMethod]
		public void GetAllBooksTest()
		{
			Book book1 = new Book(1, "A Game of Thrones", "George R.R.Martin", 1996, BookGenre.Fantasy);
			Book book2 = new Book(2, "A Clash of Kings", "George R.R.Martin", 1998, BookGenre.Fantasy);
			Book book3 = new Book(3, "A Storm of Swords", "George R.R.Martin", 2000, BookGenre.Fantasy);

			repository.AddBook(book1);
			repository.AddBook(book2);
			repository.AddBook(book3);

			Dictionary<int, Book> allBooks = repository.GetAllBooks();
			Assert.AreEqual(allBooks.Count, 3);

			Assert.IsTrue(allBooks.ContainsKey(1));
			Assert.IsTrue(allBooks.ContainsKey(2));
			Assert.IsTrue(allBooks.ContainsKey(3));

			Assert.IsTrue(allBooks.ContainsValue(book1));
			Assert.IsTrue(allBooks.ContainsValue(book2));
			Assert.IsTrue(allBooks.ContainsValue(book3));
		}


		[TestMethod]
		[ExpectedException(typeof(System.Exception))]
		public void AddBookWithTheSameIdTest()
		{
			repository.AddBook(new Book(1, "A Game of Thrones", "George R.R.Martin", 1996, BookGenre.Fantasy));
			repository.AddBook(new Book(1, "A Clash of Kings", "George R.R.Martin", 1998, BookGenre.Fantasy));

			Assert.AreEqual(repository.GetAllBooksNumber(), 1);
			Assert.AreEqual(repository.GetBookById(1).Title, "A Game of Thrones");
		}

		[TestMethod]
		public void DeleteNonExistingBookTest()
		{
			repository.AddBook(new Book(1, "A Game of Thrones", "George R.R.Martin", 1996, BookGenre.Fantasy));
			repository.AddBook(new Book(2, "A Clash of Kings", "George R.R.Martin", 1998, BookGenre.Fantasy));

			repository.DeleteBook(3);
			Assert.AreEqual(repository.GetAllBooksNumber(), 2);
		}


		[TestMethod]
		public void GetBookByIdTest()
		{
			repository.AddBook(new Book(5, "A Game of Thrones", "George R.R.Martin", 1996, BookGenre.Fantasy));
			Book returnedBook = repository.GetBookById(5);

			Assert.IsNotNull(returnedBook);
			Assert.AreEqual(returnedBook.Id, 5);
			Assert.AreEqual(returnedBook.Title, "A Game of Thrones");
			Assert.AreEqual(returnedBook.Genre, BookGenre.Fantasy);
		}

		[TestMethod]
		[ExpectedException(typeof(System.Exception))]
		public void GetNonExistingBookByIdTest()
		{
			repository.AddBook(new Book(5, "A Game of Thrones", "George R.R.Martin", 1996, BookGenre.Fantasy));
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
			repository.AddBook(new Book(1, "A Game of Thrones", "George R.R.Martin", 1996, BookGenre.Fantasy));
			repository.AddBook(new Book(2, "A Clash of Kings", "George R.R.Martin", 1298, BookGenre.Fantasy));

			Book newBookData = new Book(2, "A Clash of Kings", "George R.R.Martin", 1998, BookGenre.Fantasy);

			Assert.AreEqual(repository.GetBookById(1).PublishmentYear, 1996);
			Assert.AreEqual(repository.GetBookById(2).PublishmentYear, 1298);
			repository.UpdateBookInfo(newBookData);
			Assert.AreEqual(repository.GetBookById(1).PublishmentYear, 1996);
			Assert.AreEqual(repository.GetBookById(2).PublishmentYear, 1998);
		}

		[TestMethod]
		public void GetFirstBookByGenreTest()
		{
			repository.AddBook(new Book(1, "The Notebook", "Nicholas Sparks", 1997, BookGenre.Romance));
			repository.AddBook(new Book(5, "A Game of Thrones", "George R.R.Martin", 1996, BookGenre.Fantasy));
			repository.AddBook(new Book(2, "A Clash of Kings", "George R.R.Martin", 1998, BookGenre.Fantasy));

			Book returnedBook = repository.GetBookByGenre(BookGenre.Fantasy);

			Assert.IsNotNull(returnedBook);
			Assert.AreEqual(returnedBook.Id, 5);
			Assert.AreEqual(returnedBook.Title, "A Game of Thrones");
			Assert.AreEqual(returnedBook.Genre, BookGenre.Fantasy);
		}


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
			repository.DeleteBookstate(5);
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

	}
}


