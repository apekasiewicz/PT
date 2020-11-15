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

            data.books.allBooks.Add(1, book1); //the same for the rest
            /*data.books.Add(2, book2);
            data.books.Add(3, book3);
            data.books.Add(4, book4);
            data.books.Add(5, book5);


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


            //generate states
            State state1 = new State(book1, DateTime.Today, DateTime.Today.AddDays(10));
            State state2 = new State(book2, DateTime.Today, DateTime.Today.AddDays(7));

            data.states.Add(state1);
            data.states.Add(state2);


            //generate events
            BorrowingEvent bEvent1 = new BorrowingEvent(1, reader1, state1, DateTime.Today);
            BorrowingEvent bEvent2 = new BorrowingEvent(2, reader1, state2, DateTime.Today);
            BorrowingEvent bEvent3 = new BorrowingEvent(3, reader1, state2, DateTime.Today);

            data.events.Add(bEvent1);
            data.events.Add(bEvent2);
            data.events.Add(bEvent3);*/
        }
    }
}
