using System;
using System.Collections.Generic;
using Library.Data;
using System.Linq;

namespace Library.Services
{
    public class EventService
	{
        public IEnumerable<Event> GetEvents()
        {
            using (var context = new LibraryDataContext())
            {
                return context.Events.ToList();
            }
        }

        public int GetAllEventsNumber()
        {
            using (var context = new LibraryDataContext())
            {
                return context.Events.Count();
            }
        }

        public IEnumerable<Event> GetEventsForReaderById(int readerId)
        {
            using (var context = new LibraryDataContext())
            {
                List<Event> result = new List<Event>();
                foreach (Event e in context.Events)
                {
                    if (e.reader == readerId)
                    {
                        result.Add(e);
                    }
                }
                return result;
            }
        }

        // ReaderService can be maybe used? 
        public IEnumerable<Event> GetEventsForReaderByName(string fName, string lName)
        {
            using (var context = new LibraryDataContext())
            {
                Reader reader = ReaderService.GetReader(fName, lName);

                List<Event> result = new List<Event>();
                foreach (Event e in context.Events.ToList())
                {
                    if (e.reader == reader.reader_id)
                    {
                        result.Add(e);
                    }
                }
                return result;
            }
        }

        public IEnumerable<Event> GetEventsForBookById(int bookId)
        {
            using (var context = new LibraryDataContext())
            {
                List<Event> result = new List<Event>();
                foreach (Event e in context.Events)
                {
                    if (e.book == bookId)
                    {
                        result.Add(e);
                    }
                }
                return result;
            }
        }

        // BookService can be maybe used? 
        public IEnumerable<Event> GetEventsForBookByTitle(string title)
        {
            using (var context = new LibraryDataContext())
            {
                Book book = context.Books.SingleOrDefault(b => b.title.Equals(title));

                List<Event> result = new List<Event>();
                foreach (Event e in context.Events.ToList())
                {
                    if (e.book == book.book_id)
                    {
                        result.Add(e);
                    }
                }
                return result;
            }
        }

        public IEnumerable<Event> GetBorrowingEvents()
        {
            using (var context = new LibraryDataContext())
            {
                List<Event> result = new List<Event>();
                foreach (Event e in context.Events.ToList())
                {
                    if (e.is_borrowing_event)
                    {
                        result.Add(e);
                    }
                }
                return result;
            }
        }

        public IEnumerable<Event> GetReturningEvents()
        {
            using (var context = new LibraryDataContext())
            {
                List<Event> result = new List<Event>();
                foreach (Event e in context.Events.ToList())
                {
                    if (!e.is_borrowing_event)
                    {
                        result.Add(e);
                    }
                }
                return result;
            }
        }

      
        public IEnumerable<Event> GetEventsByDate(DateTime date)
        {
            using (var context = new LibraryDataContext())
            {
                List<Event> events = new List<Event>();
                foreach (Event ev in context.Events.ToList())
                {
                    if (ev.event_time == date)
                    {
                        events.Add(ev);
                    }
                }
                return events;
            }
        }

        
        public bool AddEvent(DateTime time, bool isBorrowingEvent, int bookId, int readerId)
        {
            using (var context = new LibraryDataContext())
            {
                if (context.Readers.SingleOrDefault(r => r.reader_id.Equals(readerId)) != null &&
                        context.Books.SingleOrDefault(b => b.book_id.Equals(bookId)) != null )
                {
                    Event newEvent = new Event
                    {
                        event_time = time,
                        is_borrowing_event = isBorrowingEvent,
                        book = bookId,
                        reader = readerId
                    };
                    context.Events.InsertOnSubmit(newEvent);
                    context.SubmitChanges();
                    return true;
                }
                return false;
            }
        }

        public bool DeleteEvent(int id)
        {
            using (var context = new LibraryDataContext())
            {
                Event ev = context.Events.SingleOrDefault(e => e.event_id == id);
                context.Events.DeleteOnSubmit(ev);
                context.SubmitChanges();
                return true;
            }
        }

        
        public bool UpdateEventTime(int id, DateTime time)
        {
            using (var context = new LibraryDataContext())
            {
                Event ev = context.Events.SingleOrDefault(e => e.event_id == id);
                ev.event_time = time;
                context.SubmitChanges();
                return true;
            }
        }

        public bool UpdateEventType(int id)
        {
            using (var context = new LibraryDataContext())
            {
                Event ev = context.Events.SingleOrDefault(e => e.event_id == id);
                ev.is_borrowing_event = !ev.is_borrowing_event;
                context.SubmitChanges();
                return true;
            }
        }
    }
}
