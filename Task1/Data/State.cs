using System;
using System.Collections.Generic;
using System.Text;

namespace Data
{
    public class State
    {
        public State(BookCatalog catalog, Dictionary<int, int> availableBooks)
        {
            AllBooks = catalog;
            AvailableBooks = availableBooks;
        }

        public BookCatalog AllBooks { get; set; }

        public Dictionary<int, int> AvailableBooks { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is State))
            {
                return false;
            }

            State s = (State)obj;
            return AllBooks == s.AllBooks && AvailableBooks == s.AvailableBooks;
        }

        public override int GetHashCode()
        {
            return AllBooks.GetHashCode() ^ AvailableBooks.GetHashCode();
        }
    }
}
