using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryProject
{
    class ReturningEvent : Event
    {
        public ReturningEvent(int id, Reader reader, State state, DateTime returnDate) : base(id, reader, state, returnDate)
        {
        }
    }
}
