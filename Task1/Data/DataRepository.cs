using System;
using System.Collections.Generic;
using System.Text;

namespace Data
{
    /*public class DataRepository : IRepository
    {
        private DataContext context;

        public DataRepository(DataContext context)
        {
            this.context = context;
            bookKey = 1;
        }

        //Products
        public int bookKey { get; set; }

        public BookCatalog GetAllBooks()
        {
            return context.allBooks;
        }

        public BookCatalog GetBookById(int id)
        {
            if (context.allBooks.ContainsKey(id))
            {
                return context.books[id];
            }
            throw new Exception("Book with such id does not exist");
        }

        public Book GetBookByGenre(BookGenre genre)
        {
            for (int i = 0; i < context.books.Count; i++)
            {
                if (context.books[i].Genre == genre)
                {
                    return context.books[i];
                }
            }
            throw new Exception("There are no books of this genre in the library.");
        }

        public void UpdateBookInfo(Book book)
        {
            if (context.books.ContainsKey(book.Id))
            {
                context.books[book.Id].Title = book.Title;
                context.books[book.Id].Author = book.Author;
                context.books[book.Id].PublishmentYear = book.PublishmentYear;
                context.books[book.Id].Genre = book.Genre;
            }
            throw new Exception("Such book with does not exist in the library");
        }

        public void AddBook(Book book)
        {
            context.books.Add(bookKey, book);
            bookKey++;
        }

        public void DeleteBook(int id)
        {
            if (context.books.ContainsKey(id))
            {
                context.books.Remove(id);
            }
            throw new Exception("Book with such id does not exist");
        }


        //Readers
        public List<Reader> GetAllReaders()
        {
            return context.readers;
        }

        public Reader GetReaderById(int id)
        {
            for (int i = 0; i < context.readers.Count; i++)
            {
                if (context.readers[i].Id == id)
                {
                    return context.readers[i];
                }
            }
            throw new Exception("Reader with such id does not exist");
        }

        public void UpdateReaderInfo(Reader reader)
        {
            for (int i = 0; i < context.readers.Count; i++)
            {
                if (context.readers[i].Id == reader.Id)
                {
                    context.readers[i].FirstName = reader.FirstName;
                    context.readers[i].LastName = reader.LastName;
                    context.readers[i].Id = reader.Id;
                }
            }
            throw new Exception("Such reader does not exist in the library");
        }

        public void AddReader(Reader reader)
        {
            context.readers.Add(reader);
        }

        public void DeleteReader(int id)
        {
            for (int i = 0; i < context.readers.Count; i++)
            {
                if (context.readers[i].Id == id)
                {
                    context.readers.Remove(context.readers[i]);
                }
            }
            throw new Exception("Reader with such id does not exist");
        }


        //Events
        public List<Event> GetAllEvents()
        {
            return context.events;
        }

        public Event GetEventById(int id)
        {
            for (int i = 0; i < context.events.Count; i++)
            {
                if (context.events[i].Id == id)
                {
                    return context.events[i];
                }
            }
            throw new Exception("Event with such id does not exist");
        }


        public void AddEvent(Event e)
        {
            context.events.Add(e);
        }

        public void DeleteEvent(int id)
        {
            for (int i = 0; i < context.events.Count; i++)
            {
                if (context.events[i].Id == id)
                {
                    context.events.Remove(context.events[i]);
                }
            }
            throw new Exception("Event with such id does not exist");
        }

        public void UpdateEventInfo(Event e)
        {
            for (int i = 0; i < context.events.Count; i++)
            {
                if (context.events[i].Id == e.Id)
                {
                    context.events[i].Date = e.Date;
                    context.events[i].Reader = e.Reader;
                    context.events[i].State = e.State;
                }

            }
        }
    }*/
}
