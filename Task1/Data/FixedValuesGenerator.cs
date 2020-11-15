using System;
using System.Collections.Generic;
using System.Text;

namespace Data
{
    public class FixedValuesGenerator : IDataGenerator
    {
        public void GenarateData(DataContext data)
        {
            //generate books
            Book book1 = new Book(1, "Lord of the Rings", "J.R.R.Tolkien", 1954, BookGenre.Adventure);
            Book book2 = new Book(2, "Harry Potter", " J.K. Rowling", 1997, BookGenre.Fantasy);
            Book book3 = new Book(3, "The Alchemist", "Paulo Coelho", 1988, BookGenre.Drama);
            Book book4 = new Book(4, "The Da Vinci Code", "Dan Brown", 2006, BookGenre.Criminal);
            Book book5 = new Book(5, "The Notebook", "Nicholas Sparks", 1997, BookGenre.Romance);

            data.books.allBooks.Add(1, book1); 
            data.books.allBooks.Add(2, book2);
            data.books.allBooks.Add(3, book3);
            data.books.allBooks.Add(4, book4);
            data.books.allBooks.Add(5, book5);


            //generate readers
            Reader reader1 = new Reader("Neave", "Oneal", 102030);
            Reader reader2 = new Reader("Charlize", "Padilla", 102031);
            Reader reader3 = new Reader("Judith", "Rojas", 102032);
            Reader reader4 = new Reader("Maliha", "Petty", 102033);
            Reader reader5 = new Reader("Fionn", "Mcclure", 102034);

            data.readers.Add(reader1);
            data.readers.Add(reader2);
            data.readers.Add(reader3);
            data.readers.Add(reader4);
            data.readers.Add(reader5);


            //generate state
            data.libraryState.AllBooks = data.books;
           
            for (int i = 0; i < data.books.allBooks.Count; i++)
            {
                data.libraryState.AvailableBooks.Add(data.books.allBooks[i].Id, 10);
            }
        }
    }
}
