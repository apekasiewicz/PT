using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryProject
{
	class BorrowingEvent
	{
        public BorrowingEvent(Reader eReader, State eState, int eId, DateTime bDate, DateTime dDate)
		{
			Reader = eReader;
			State = eState;
			Id = eId;
			Borrow_date = bDate;
			Due_date = dDate;
		}

        public DateTime Borrow_date { get; set; }

        public Reader Reader { get; set; }

        public State State { get; set; }

		public int Id { get; set; }
		public DateTime Due_date { get; set; }
    }
}
