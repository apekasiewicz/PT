using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data
{
    public class RandomValuesGenerator : IDataGenerator
    {
        private const int strings_length = 12;
        private const int generations_number = 8;
        private static Random random = new Random();

        public static string GenerateRandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public BookGenre randomGenre()
        {
            Array values = Enum.GetValues(typeof(BookGenre));
            BookGenre randomGenre = (BookGenre)values.GetValue(random.Next(values.Length));
            return randomGenre;
        }

        public int randomNumber(int bottomBorder, int upperBorder)
        {
            return random.Next(bottomBorder, upperBorder);
        }

        public void GenarateData(DataContext data)
        {
            for(int i = 1; i <= generations_number; i++)
            {
                //generate readers
                Reader reader = new Reader(GenerateRandomString(strings_length), GenerateRandomString(strings_length), i, randomNumber(0, 20));
                data.readers.Add(reader);

                //generate books and add them to catalog
                Book book = new Book(i, GenerateRandomString(strings_length),
                    GenerateRandomString(strings_length), randomNumber(1900, 2020), randomGenre());
                data.books.allBooks.Add(i, book);
            }

            //generate state
            data.libraryState.AllBooks = data.books;

            for (int j = 1; j <= data.books.allBooks.Count; j++)
            {
                data.libraryState.AvailableBooks.Add(data.books.allBooks[j].Id, j);
            }
        }
    }
}
