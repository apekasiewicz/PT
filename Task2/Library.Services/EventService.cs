using System;
using System.Collections.Generic;
using System.Text;
using Library.Data;
using System.Linq;

namespace Library.Services
{
	class EventService
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

        // ReaderService can be maybe used? - code repetition
        public IEnumerable<Event> GetEventsForReaderByName(string fName, string lName)
        {
            using (var context = new LibraryDataContext())
            {
                Reader reader = new Reader();
                foreach (Reader r in context.Readers.ToList())
                {
                    if (r.reader_f_name.Equals(fName) && r.reader_l_name.Equals(lName))
                    {
                        reader = r;
                    }
                }

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

        // BookService can be maybe used? - code repetition
        public IEnumerable<Event> GetEventsForBookByTitle(string title)
        {
            using (var context = new LibraryDataContext())
            {
                Book book = new Book();
                foreach (Book b in context.Books.ToList())
                {
                    if (b.title.Equals(title))
                    {
                        book = b;
                    }
                }

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
                if (context.Readers.FirstOrDefault(r => r.reader_id.Equals(readerId)) != null &&
                        context.Books.FirstOrDefault(b => b.book_id.Equals(bookId)) != null )
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
