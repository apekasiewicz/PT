using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;

namespace LibraryProject
{
    class Library
    {
        public List<Reader> readers = new List<Reader>();
        public Dictionary<int, Book> books = new Dictionary<int, Book>();
        public List<State> states = new List<State>();
        public List<BorrowingEvent> events = new List<BorrowingEvent>();
    }
}
