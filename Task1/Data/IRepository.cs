using System;
using System.Collections.Generic;
using System.Text;

namespace Data

//CRUD definition (create, remove, update, delete)
{
    public interface IRepository
    {
        //Products
        BookCatalog GetAllBooks();
        Book GetBookById(int id);
        Book GetBookByGenre(BookGenre genre);
        void UpdateBookInfo(Book book);
        void AddBook(Book book);
        void DeleteBook(int id);

        //Readers
        List<Reader> GetAllReaders();
        Reader GetReaderById(int id);
        void UpdateReaderInfo(Reader reader);
        void AddReader(Reader reader);
        void DeleteReader(int id);

        //Events
        List<Event> GetAllEvents();
        Event GetEventById(int id);
        void UpdateEventInfo(Event e);
        void AddEvent(Event e);
        void DeleteEvent(int id);

        //States?
    }
}
