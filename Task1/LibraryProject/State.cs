using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryProject
{
	class State
	{
		private Book book;
		private DateTime borrowDate;
		private DateTime dueDate;

		public State(Book sBook, DateTime bDate, DateTime dDate)
		{
			book = sBook;
			borrowDate = bDate;
			dueDate = dDate;
		}

		public DateTime BorrowDate
		{
			get { return borrowDate; }
			set { borrowDate = value; }
		}

		public DateTime DueDate
		{
			get { return dueDate; }
			set { dueDate = value; }
		}
	}
}
