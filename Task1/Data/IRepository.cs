using System;
using System.Collections.Generic;
using System.Text;

namespace Data

//CRUD definition (create, remove, update, delete)
{
    public interface IRepository
    {
        //Products
        Dictionary<int, Book> GetAllBooks();
        int GetAllBooksNumber();
        Book GetBookById(int id);
        Book GetBookByGenre(BookGenre genre);
        void UpdateBookInfo(Book book);
        void AddBook(Book book);
        void DeleteBook(int id);

        //Readers
        List<Reader> GetAllReaders();
        int GetAllReadersNumber();
        Reader GetReaderById(int id);
        void UpdateReaderInfo(Reader reader);
        void AddReader(Reader reader);
        void DeleteReader(int id);

        //Events
        List<Event> GetAllEvents();
        int GetAllEventsNumber();
        Event GetEventById(int id);
        void UpdateEventInfo(Event e);
        void AddEvent(Event e);
        void DeleteEvent(int id);

        //States
        Dictionary<int, int> GetAllStates();
        State GetState();
        int GetBookStateById(int id);
        void UpdateBookState(int id, int newState); //decrement by one if borrowingevent, increment if returning
        void AddBookState(int id, int state); //new book in catalog, new quantity
        void DeleteBookstate(int id);   //delete book from catalog, delete its state
    }
}
