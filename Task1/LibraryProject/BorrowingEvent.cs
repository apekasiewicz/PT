using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryProject
{
	class BorrowingEvent
	{
		private Reader reader;
        private State state;

        public BorrowingEvent(Reader eReader, State eState, DateTime date)
		{
			reader = eReader;
			state = eState;
			Borrow_date = date;
		}

        public DateTime Borrow_date { get; set; }
    }
}
