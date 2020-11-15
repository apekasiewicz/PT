using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;

namespace Data
{
    public class DataContext
    {
        public List<Reader> readers = new List<Reader>();
        public BookCatalog books = new BookCatalog();
        public List<Event> events = new List<Event>();
        public State libraryState;

        public DataContext(State libraryState)
        {
            this.libraryState = libraryState;
        }
    }
}
