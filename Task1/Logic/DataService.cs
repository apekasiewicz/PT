using Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Logic
{
    class DataService
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

        public List<Reader> getAllReaders()
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

        public List<Event> getAllEvents()
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

        //Handling Events

        public void borrowBook(int readerId, int bookId, DateTime borrowDate)
        {
            BorrowingEvent bEvent = new BorrowingEvent(readerId, repository.GetReaderById(readerId), repository.GetState(), borrowDate);
            repository.AddEvent(bEvent);
            repository.UpdateBookState(bookId, -1);
        }

        public void returnBook(int readerId, int bookId, DateTime returnDate)
        {
            ReturningEvent rEvent = new ReturningEvent(readerId, repository.GetReaderById(readerId), repository.GetState(), returnDate);
            repository.AddEvent(rEvent);
            repository.UpdateBookState(bookId, 1);
        }
    }
}
