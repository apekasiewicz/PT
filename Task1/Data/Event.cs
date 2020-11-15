using System;
using System.Collections.Generic;
using System.Text;

namespace Data
{
    abstract class Event
    {
        protected Event(int id, Reader reader, State state, DateTime date)
        {
            Id = id;
            Reader = reader;
            State = state;
            Date = date;
        }

        public Reader Reader { get; set; }
        public State State { get; set; }

        public DateTime Date { get; set; }

        public int Id { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is Event))
            {
                return false;
            }

            Event e = (Event)obj;
            return Id == e.Id && Date == e.Date && Reader == e.Reader && State == e.State;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode() ^ Date.GetHashCode() ^ Reader.GetHashCode() ^ State.GetHashCode();
        }
    }
}
