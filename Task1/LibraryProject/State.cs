using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryProject
{
	class State
    {
        public State(Book sBook, DateTime bDate, DateTime dDate)
		{
			Book = sBook;
			BorrowDate = bDate;
			DueDate = dDate;
		}

		public Book Book { get; set; }
        public DateTime BorrowDate { get; set; }

        public DateTime DueDate { get; set; }

        public override bool Equals(object obj)
        {
            if ((obj == null) || !(obj is State))
            {
                return false;
            }

            State s = (State) obj;
            return Book == s.Book && BorrowDate == s.BorrowDate && DueDate == s.DueDate;
        }

        public override int GetHashCode()
        {
            return Book.GetHashCode() ^ BorrowDate.GetHashCode() ^ DueDate.GetHashCode();
        }
    }
}
