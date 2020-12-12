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
        public void GetReadersFromDatabaseTest()
        {
            Assert.AreEqual(ReaderService.GetAllReadersNumber(), 5);
        }

        [TestMethod]
        public void AddReaderToDatabaseTest()
        {
            Assert.IsTrue(ReaderService.AddReader("Sharoon", "Steel"));
            Assert.AreEqual(ReaderService.GetAllReadersNumber(), 6);

            //delete to restore original db
            Assert.IsTrue(ReaderService.DeleteReader("Sharoon", "Steel"));
        }

        [TestMethod]
        public void AddExisistingReaderToDatabaseTest()
        {
            Assert.IsFalse(ReaderService.AddReader("Neave", "Oneal"));
            Assert.AreEqual(ReaderService.GetAllReadersNumber(), 5);
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
        public void DeleteReaderUsinsSurnameTest()
        {
            Assert.IsTrue(ReaderService.AddReader("Sharoon", "Steel"));
            Assert.AreEqual(ReaderService.GetAllReadersNumber(), 6);

            //delete to restore original db
            Assert.IsTrue(ReaderService.DeleteReader("Sharoon", "Steel"));
            Assert.AreEqual(ReaderService.GetAllReadersNumber(), 5);
        }

        
        [TestMethod]
        public void UpdateReaderNameTest()
        {
            Assert.IsTrue(ReaderService.UpdateReaderFName(55, "Ann"));
            Reader reader1 = ReaderService.GetReader("Judith", "Rojas");
            Assert.IsNull(reader1);
            Reader reader2 = ReaderService.GetReader("Ann", "Rojas");
            Assert.IsNotNull(reader2);

            //update to restore original db
            Assert.IsTrue(ReaderService.UpdateReaderFName(55, "Judith"));
        }

        [TestMethod]
        public void UpdateReaderLastNameTest()
        {
            Assert.IsTrue(ReaderService.UpdateReaderLName(55, "Perks"));
            Reader reader1 = ReaderService.GetReader("Judith", "Rojas");
            Assert.IsNull(reader1);
            Reader reader2 = ReaderService.GetReader("Judith", "Perks");
            Assert.IsNotNull(reader2);

            //update to restore original db
            Assert.IsTrue(ReaderService.UpdateReaderLName(55, "Rojas"));
        }
    }
}
