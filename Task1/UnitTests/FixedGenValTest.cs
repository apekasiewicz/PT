using Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
	[TestClass]
	public class FixedGenValTest
	{
		private IRepository repository;
		private IDataGenerator generator;
		private DataContext context;

		[TestInitialize]
		public void Initialize()
		{
			context = new DataContext();
			repository = new DataRepository(context);
			generator = new FixedValuesGenerator();
			generator.GenarateData(context);
		}

		[TestMethod]
		public void TestCheckGenerationNotNull()
		{
			Assert.IsNotNull(repository.GetAllBooks());
			Assert.IsNotNull(repository.GetAllReaders());
			Assert.IsNotNull(repository.GetAllEvents());
			Assert.IsNotNull(repository.GetAllStates());
		}

		[TestMethod]
		public void TestCheckGenerationCount()
		{
			Assert.AreEqual(repository.GetAllBooksNumber(), 5);
			Assert.AreEqual(repository.GetAllReadersNumber(), 5);
			Assert.AreEqual(repository.GetAllEventsNumber(), 5);
		}

		[TestMethod]
		[ExpectedException(typeof(System.Exception))]
		public void TestCheckReaderGenerationEqual()
		{
			Reader reader1 = new Reader("Judith", "Rojas", 102032, 6);
			Assert.IsNotNull(repository.GetReaderById(reader1.Id));
			Assert.AreEqual(repository.GetReaderById(reader1.Id), reader1);

			Reader reader2 = new Reader("Neave", "Oneal", 102030, 4);
			Assert.IsNotNull(repository.GetReaderById(reader2.Id));
			Assert.AreEqual(repository.GetReaderById(reader2.Id), reader2);
			reader2 = repository.GetReaderById(0);
		}

		[TestMethod]
		[ExpectedException(typeof(System.Exception))]
		public void TestCheckRBookGenerationEqual()
		{
			Book book1 = new Book(1, "Lord of the Rings", "J.R.R.Tolkien", 1954, BookGenre.Adventure);
			Assert.IsNotNull(repository.GetBookById(book1.Id));
			Assert.AreEqual(repository.GetReaderById(book1.Id), book1);
			book1 = repository.GetBookById(150);
		}
	}
}
