using Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Logic
{
    public class DataService
    {
        private IRepository repository;

        public DataService(IRepository repository)
        {
            this.repository = repository;
        }

        // GET ALL 
        public Dictionary<int, Book> GetAllBooks()
        {
            Dictionary<int, Book> books = repository.GetAllBooks();
            if (books.Count == 0)
            {
                return null;
            }
            else
            {
                return books;
            }
        }

        public int GetAllBooksNumber()
        {
            return repository.GetAllBooksNumber();
        }

        public List<Reader> GetAllReaders()
        {
            List<Reader> readers = repository.GetAllReaders();
            if (readers.Count == 0)
            {
                return null;
            }
            else
            {
                return readers;
            }
        }

        public int GetAllReadersNumber()
        {
            return repository.GetAllReadersNumber();
        }

        public List<Event> GetAllEvents()
        {
            List<Event> events = repository.GetAllEvents();
            if (events.Count == 0)
            {
                return null;
            }
            else
            {
                return events;
            }
        }

        public int GetAllEventsNumber()
        {
            return repository.GetAllEventsNumber();
        }

        // SEARCH FOR ONE
        public Book GetBookById(int id)
        {
            return repository.GetBookById(id);
        }

        public Book GetBookByGenre(BookGenre genre)
        {
            return repository.GetBookByGenre(genre);
        }

        public Reader GetReaderById(int id)
        {
            return repository.GetReaderById(id);
        }

        public Event GetEventById(int id)
        {
            return repository.GetEventById(id);
        }


        //ADD 
        public void AddBook(Book book)
        {
            repository.AddBook(book);
        }

        public void AddReader(Reader reader)
        {
            repository.AddReader(reader);
        }

        public void AddEvent(Event e)
        {
            repository.AddEvent(e);
        }

        //EDIT
        public void EditBook(Book book)
        {
            repository.UpdateBookInfo(book);
        }

        public void EditReader(Reader reader)
        {
            repository.UpdateReaderInfo(reader);
        }

        public void EditEvent(Event e)
        {
            repository.UpdateEventInfo(e);
        }


        //REMOVE
        public void DeleteBook(int id)
        {
            repository.DeleteBook(id);
        }

        public void DeleteReader(int id)
        {
            repository.DeleteReader(id);
        }

        public void DeleteEvent(int id)
        {
            repository.DeleteEvent(id);
        }

        //STATE
        public void UpdateStock(int bookId, int newAmount)
        {
            repository.UpdateBookState(bookId, newAmount);
        }

        public Dictionary<int, int> GetAllAvailableBooks()
        {
            return repository.GetAllStates();
        }

        public int GetAmountOfAvailableCopiesById(int bookId)
        {
            return repository.GetAmountOfAvailableCopiesById(bookId);
        }

        public void AddNewBookState(int bookId, int state)
        {
            repository.AddBookState(bookId, state);
        }

        public void DeleteBookstate(int bookId)
        {
            repository.DeleteBookstate(bookId);
        }


        //Handling Actions

        public void borrowBook(int readerId, int bookId, DateTime borrowDate)
        {
            var currentBookState = repository.GetAmountOfAvailableCopiesById(bookId);
            var reader = repository.GetReaderById(readerId);

            if (currentBookState == 0)
            {
                throw new InvalidOperationException("The book is unavailable for borrowing.");
            }

            BorrowingEvent bEvent = new BorrowingEvent(readerId, reader, repository.GetState(), borrowDate);
            repository.AddEvent(bEvent);
            OnEventUpdateState(bookId, currentBookState, reader, true);
        }

        public void returnBook(int readerId, int bookId, DateTime returnDate)
        {
            var currentBookState = repository.GetAmountOfAvailableCopiesById(bookId);
            var reader = repository.GetReaderById(readerId);
            var readerBooks = reader.AmountOfBooksBorrowed;

            if (readerBooks == 0)
            {
                throw new InvalidOperationException("You can not return books when you did not borrow.");
            }

            ReturningEvent rEvent = new ReturningEvent(readerId, reader, repository.GetState(), returnDate);
            repository.AddEvent(rEvent);
            OnEventUpdateState(bookId, currentBookState, reader, false);
        }

        private void OnEventUpdateState(int bookId, int currentBookState, Reader reader, bool isBorrowing)
        {
            if (isBorrowing)
            {
                currentBookState -= 1;
                reader.AmountOfBooksBorrowed += 1;
                repository.UpdateBookState(bookId, currentBookState);
            }
            else
            {
                currentBookState += 1;
                reader.AmountOfBooksBorrowed -= 1;
                repository.UpdateBookState(bookId, currentBookState);
            }
        }

        public IEnumerable<Event> GetEventsForReader(int readerId)
        {
            var reader = repository.GetReaderById(readerId);
            List<Event> events = new List<Event>();

            foreach (Event ev in repository.GetAllEvents())
            {
                if (ev.Reader.Equals(reader))
                {
                    events.Add(ev);
                }
            }
            return events;
        }

        public IEnumerable<Event> GetEventsBetweenDates(DateTime start, DateTime end)
        {
            List<Event> events = new List<Event>();

            foreach (Event ev in repository.GetAllEvents())
            {
                if (ev.Date >= start && ev.Date <= end)
                {
                    events.Add(ev);
                }
            }
            return events;
        }
    }
}
