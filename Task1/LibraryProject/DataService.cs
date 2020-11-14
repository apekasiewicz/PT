using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryProject
{
    class DataService
    {
        private IRepository repository;

        public DataService(IRepository repository)
        {
            this.repository = repository;
        }

        public List<Book> GetAllBooks()
        {
            List<Book> books = repository.GetAllBooks();
            if(books.Count == 0)
            {
                return null;
            } 
            else
            {
                return books;
            }
        }

        public Book GetBookById(int id)
        {
            return repository.GetBookById(id);
        }

    }
}
