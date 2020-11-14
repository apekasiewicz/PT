using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryProject
{
    abstract class Event
    {
        private Reader reader;
        private State state;
        private DateTime date;
        private int id;

        protected Event(int id, Reader reader, State state, DateTime date)
        {
            this.id = id;
            this.reader = reader;
            this.state = state;
            this.date = date;
        }

        public Reader Reader { get => reader; set => reader = value; }
        public State State { get => state; set => state = value; }

        public DateTime Date { get => date; set => date = value; }

        public int Id { get => id; set => id = value; }
    }
}
