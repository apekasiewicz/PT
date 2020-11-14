using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryProject
{
    internal class BorrowingEvent : Event
	{
        public BorrowingEvent(Reader reader, State state, DateTime borrow_date) : base(reader, state, borrow_date)
        {
        }
    }
}
