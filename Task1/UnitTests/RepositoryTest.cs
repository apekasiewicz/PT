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

		[TestInitialize]
		public void Initialize()
		{
			context = new DataContext();
			repository = new DataRepository(context);
		}

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
		
	}
}
