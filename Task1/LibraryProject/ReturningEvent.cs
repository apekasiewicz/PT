using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryProject
{
    class ReturningEvent : Event
    {
        public ReturningEvent(int id, Reader reader, State state, DateTime return_date) : base(id, reader, state, return_date)
        {
        }
    }
}
