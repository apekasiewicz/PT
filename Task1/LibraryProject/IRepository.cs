using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryProject

    //CRUD definition (create, remove, update, delete)
{
    interface IRepository
    {
        //Products
        List<Book> GetAllBooks();
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
        List<BorrowingEvent> GetAllBorrowingEvents();
        BorrowingEvent GetBorrowingEventById(int id);
        void UpdateEventInfo(BorrowingEvent e);
        void AddBorrowingEvent(BorrowingEvent e);
        void DeleteBorrowingEvent(int id);

        //States?
    }
}
