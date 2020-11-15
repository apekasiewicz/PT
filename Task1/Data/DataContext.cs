using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;

namespace Data
{
    public class DataContext
    {
        public List<Reader> readers = new List<Reader>();
        public Dictionary<int, Book> books = new Dictionary<int, Book>();
        public List<State> states = new List<State>();
        public List<Event> events = new List<Event>();
    }
}
