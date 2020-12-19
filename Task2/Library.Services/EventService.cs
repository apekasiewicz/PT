using System;
using System.Collections.Generic;
using Library.Data;
using System.Linq;

namespace Library.Services
{
    public class EventService
	{
        static public IEnumerable<Event> GetEvents()
        {
            using (var context = new LibraryDataContext())
            {
                return context.Events.ToList();
            }
        }

        static public int GetAllEventsNumber()
        {
            using (var context = new LibraryDataContext())
            {
                return context.Events.Count();
            }
        }

        static public IEnumerable<Event> GetEventsForReaderById(int readerId)
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

        static public IEnumerable<Event> GetEventsForBookById(int bookId)
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

        static public IEnumerable<Event> GetBorrowingEvents()
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

        static public IEnumerable<Event> GetReturningEvents()
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

      
        static public IEnumerable<Event> GetEventsByDate(DateTime date)
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

        
        static public bool AddEvent(DateTime time, bool isBorrowingEvent, int bookId, int readerId)
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

        static public bool DeleteEvent(int readerId, int bookId)
        {
            using (var context = new LibraryDataContext())
            {
                Event ev = context.Events.FirstOrDefault(e => e.reader == readerId && e.book == bookId);
                if (ev != null)
                {
                    context.Events.DeleteOnSubmit(ev);
                    context.SubmitChanges();
                    return true;
                }
                return false;
            }
        }


        static public void DeleteEventsForReader(int readerId)
        {
            using (var context = new LibraryDataContext())
            {
                IEnumerable<Event> events = context.Events.Where(e => e.reader == readerId);
                foreach (Event e in events)
                {
                    context.Events.DeleteOnSubmit(e);
                    context.SubmitChanges();
                }
            }
        }

        static public void DeleteEventsForBook(int bookId)
        {
            using (var context = new LibraryDataContext())
            {
                IEnumerable<Event> events = context.Events.Where(e => e.book == bookId);
                foreach (Event e in events)
                {
                    context.Events.DeleteOnSubmit(e);
                    context.SubmitChanges();
                }
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

        static public bool BorrowBookForReader(Book book, Reader reader)
        {
            using (var context = new LibraryDataContext())
            {
                if (book != null & reader != null)
                {
                    if (book.quantity > 0)
                    {
                        AddEvent(DateTime.Today, true, book.book_id, reader.reader_id);
                        book.quantity -= 1;
                        BookService.UpdateBookQuantity(book.book_id, (int)book.quantity);
                        return true;
                    }
                }
            }
            return false;
        }

        static public bool ReturnBookByReader(Book book, Reader reader)
        {
            using (var context = new LibraryDataContext())
            {
                if (CheckEventForBookAndReader(reader.reader_id, book.book_id))
                {
                    AddEvent(DateTime.Today, false, book.book_id, reader.reader_id);
                    book.quantity += 1;
                    BookService.UpdateBookQuantity(book.book_id, (int)book.quantity);
                    return true;
                }
                return false;
            }
        }

        static public bool CheckEventForBookAndReader(int readerId, int bookId)
        {
            using (var context = new LibraryDataContext())
            {
                Book book = context.Books.SingleOrDefault(b => b.book_id.Equals(bookId));
                Reader reader = context.Readers.SingleOrDefault(r => r.reader_id.Equals(readerId));

                List<Event> bEvents = new List<Event>();
                List<Event> rEvents = new List<Event>();
                foreach (Event e in context.Events.ToList())
                {
                    if (e.book == book.book_id && e.reader == readerId && e.is_borrowing_event)
                    {
                        bEvents.Add(e);
                    }
                    else if (e.book == book.book_id && e.reader == readerId && !e.is_borrowing_event)
                    {
                        rEvents.Add(e);
                    }
                }
                return bEvents.Count() > rEvents.Count();
            }
        }


    }
}
