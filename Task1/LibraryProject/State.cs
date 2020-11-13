using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryProject
{
	class State
    {
        private Book book;

        public State(Book sBook, DateTime bDate, DateTime dDate)
		{
			book = sBook;
			BorrowDate = bDate;
			DueDate = dDate;
		}

        public DateTime BorrowDate { get; set; }

        public DateTime DueDate { get; set; }
    }
}
