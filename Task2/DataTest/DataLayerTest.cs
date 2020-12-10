using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Data.Linq;
using Library.Data;
using System.Data.SqlClient;


namespace DataTest
{
    [TestClass]
    public class DataLayerTest
    {
        static private string GetConnectionString()
        {
            // To avoid storing the connection string in your code,
            // you can retrieve it from a configuration file.
            return "Data Source=DELL-LAT5490-2;Initial Catalog=library;Integrated Security=True";
        }

        private static void OpenSqlConnection()
        {

        }



        [TestInitialize]
        public void Initialize()
        {

        }



        [TestMethod]
        public void AddBookToDatabase()
        {

            LibraryDataContext db = new LibraryDataContext();

            Book book1 = new Book();
            book1.book_id = 6;
            book1.title = "Lord of the Rings";
            book1.author = "J.R.R.Tolkien";
            book1.publishment_year = 1954;
            book1.genre = "Adeventure";

            db.Books.InsertOnSubmit(book1);
            db.SubmitChanges();

            Book book2 = db.Books.FirstOrDefault(b => b.book_id.Equals(6));
            Assert.AreEqual(book2.title, "Lord of the Rings");
            Assert.AreEqual(book2.author, "J.R.R.Tolkien");
            Assert.AreEqual(book2.publishment_year, 1954);
            Assert.AreEqual(book2.genre, "Adeventure");

            db.Books.DeleteOnSubmit(book1);
            db.SubmitChanges();


        }
    }
}
