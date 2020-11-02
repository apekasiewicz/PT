using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryProject
{
	class BorrowingEvent
	{
		private Reader reader;
		private State state;
		private DateTime borrow_date;

		public BorrowingEvent(Reader eReader, State eState, DateTime date)
		{
			reader = eReader;
			state = eState;
			borrow_date = date;
		}

		public DateTime Borrow_date
		{
			get { return borrow_date; }
			set { borrow_date = value; }
		}
	}
}
