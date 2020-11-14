using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryProject
{
    class ReturningEvent : Event
    {
        public ReturningEvent(Reader reader, State state, DateTime return_date) : base(reader, state, return_date)
        {
        }
    }
}
