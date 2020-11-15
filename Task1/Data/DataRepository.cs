using System;
using System.Collections.Generic;
using System.Text;

namespace Data
{
    public class DataRepository : IRepository
    {
        private DataContext context;

        public DataRepository(DataContext context)
        {
            this.context = context;
        }

        //All Books
        public Dictionary<int, Book> GetAllBooks()
        {
            return context.books.allBooks;
        }

        public Book GetBookById(int id)
        {
            if (context.books.allBooks.ContainsKey(id))
            {
                return context.books.allBooks[id];
            }
            throw new Exception("Book with such ID does not exist in the library");
        }

        public Book GetBookByGenre(BookGenre genre)
        {
            for (int i = 0; i < context.books.allBooks.Count; i++)
            {
                if (context.books.allBooks[i].Genre == genre)
                {
                    return context.books.allBooks[i];
                }
            }
            throw new Exception("There are no books of this genre in the library.");
        }

        public void UpdateBookInfo(Book book)
        {
            if (context.books.allBooks.ContainsKey(book.Id))
            {
                context.books.allBooks[book.Id].Title = book.Title;
                context.books.allBooks[book.Id].Author = book.Author;
                context.books.allBooks[book.Id].PublishmentYear = book.PublishmentYear;
                context.books.allBooks[book.Id].Genre = book.Genre;
            }
            throw new Exception("Such book does not exist in the library");
        }

        public void AddBook(Book book)
        {
            if (context.books.allBooks.ContainsKey(book.Id))
            {
                throw new Exception("Book with such ID already exists in the library");
            }
            context.books.allBooks.Add(book.Id, book);
        }

        public void DeleteBook(int id)
        {
            if (context.books.allBooks.ContainsKey(id))
            {
                context.books.allBooks.Remove(id);
            }
            throw new Exception("Book with such ID does not exist");
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
            throw new Exception("Reader with such ID does not exist");
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
            for (int i = 0; i < context.readers.Count; i++)
            {
                if (context.readers[i].Id == reader.Id)
                {
                    throw new Exception("Reader with such ID already exists in the library");
                }
            }
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
            throw new Exception("Reader with such ID does not exist");
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
            throw new Exception("Event with such ID does not exist");
        }

        public void AddEvent(Event e)
        {
            for (int i = 0; i < context.events.Count; i++)
            {
                if (context.events[i].Id == e.Id)
                {
                    throw new Exception("Event with such ID already exists");
                }
            }
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
            throw new Exception("Event with such ID does not exist");
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
            throw new Exception("Such event does not exist in the library");
        }

        //States
        public Dictionary<int, int> GetAllStates()
        {
            return context.libraryState.AvailableBooks;
        }

        public State GetState()
        {
            return context.libraryState;
        }

        public int GetBookStateById(int id)
        {
            if (context.libraryState.AvailableBooks.ContainsKey(id))
            {
                return context.libraryState.AvailableBooks[id];
            }
            throw new Exception("Book with such ID does not exist in the library");
        }

        public void UpdateBookState(int id, int newState)
        {
            if (context.libraryState.AvailableBooks.ContainsKey(id))
            {                
                context.libraryState.AvailableBooks[id] += newState;
            }
            throw new Exception("Such book does not exist in the library");
        }

        public void AddBookState(int id, int state)
        {
            context.libraryState.AvailableBooks.Add(id, state);
        }

		public void DeleteBookstate(int id)
		{
            context.libraryState.AvailableBooks.Remove(id);
        }
	}
}
