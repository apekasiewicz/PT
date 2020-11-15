using Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
	[TestClass]
	public class RandomValGenTest
	{
		private IRepository repository;
		private IDataGenerator generator;
		private DataContext context;

		[TestInitialize]
		public void Initialize()
		{ 
			context = new DataContext();
			repository = new DataRepository(context);
			generator = new RandomValuesGenerator();
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
			Assert.AreEqual(repository.GetAllBooksNumber(), 8);
			Assert.AreEqual(repository.GetAllReadersNumber(), 8);
			Assert.AreEqual(repository.GetAllEventsNumber(), 0);
		}
	}
}
