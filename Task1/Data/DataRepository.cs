using System;
using System.Collections.Generic;
using System.Linq;
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

        public int GetAllBooksNumber()
        {
            return context.books.allBooks.Count;
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
            foreach (var book in context.books.allBooks.ToArray())
            {
                if (context.books.allBooks[book.Key].Genre == genre)
                {
                    return context.books.allBooks[book.Key];
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
            else
            {
                throw new InvalidOperationException("Such book does not exist in the library!");
            }
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
            else
            {
                throw new InvalidOperationException("Such book does not exist in the library!");
            }
        }


        //Readers
        public List<Reader> GetAllReaders()
        {
            return context.readers;
        }

        public int GetAllReadersNumber()
        {
            return context.readers.Count;
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
                    context.readers[i].AmountOfBooksBorrowed = reader.AmountOfBooksBorrowed;
                    return;
                }
            }
            throw new InvalidOperationException("Such reader ID does not exist in the library!");
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
                    if (context.readers[i].AmountOfBooksBorrowed == 0)
                    {
                        context.readers.Remove(context.readers[i]);
                        return;
                    }
                    else
                    {
                        throw new InvalidOperationException("This reader has books borrowed!");
                    }

                }
            }
            throw new InvalidOperationException("No such reader in the library");
        }


        //Events
        public List<Event> GetAllEvents()
        {
            return context.events;
        }

        public int GetAllEventsNumber()
        {
            return context.events.Count;
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
                    return;
                }
            }
            throw new InvalidOperationException("Event with such ID does not exist!");
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
                    return;
                }
            }
            throw new InvalidOperationException("Cannot update - event with such ID does not exist!");
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

        public int GetAmountOfAvailableCopiesById(int id)
        {
            if (context.libraryState.AvailableBooks.ContainsKey(id))
            {
                return context.libraryState.AvailableBooks[id];
            }
            return 0;
        }


        public void UpdateBookState(int id, int newState)
        {
            if (context.libraryState.AvailableBooks.ContainsKey(id))
            {
                if(newState >= 0)
                {
                    context.libraryState.AvailableBooks[id] = newState;
                }
                else
                {
                    throw new InvalidOperationException("You can not set the stock amount to negative number!");
                }
                
            }
            else
            {
                throw new Exception("There is no such book in the library stock");
            }
        }

        public void AddBookState(int id, int state)
        {
            if (context.libraryState.AvailableBooks.ContainsKey(id))
            {
                throw new Exception("Book with such ID already exists in the library");
            }
            context.libraryState.AvailableBooks.Add(id, state);
        }

		public void DeleteBookstate(int id)
		{
            if (context.libraryState.AvailableBooks.ContainsKey(id))
            {
                context.libraryState.AvailableBooks.Remove(id);
            }
            else
            {
                throw new InvalidOperationException("Book with such ID does not exist in the stock!");
            }
                
        }
	}
}
