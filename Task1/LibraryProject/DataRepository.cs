using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryProject
{
	class DataRepository : IRepository
	{
		private DataContext context;
	
		public DataRepository(DataContext context)
		{
			this.context = context;
			bookKey = 1;
		}

		//Products
		public int bookKey { get; set; }

		public Dictionary<int, Book> GetAllBooks()
		{
			return context.books;
		}

		public Book GetBookById(int id)
		{
			if (context.books.ContainsKey(id))
			{
				return context.books[id];
			}
			throw new Exception("Book with such id does not exist");
		}

		public Book GetBookByGenre(BookGenre genre)
		{
			//TO DO
			throw new NotImplementedException();
		}

		public void UpdateBookInfo(Book book)
		{
			if (context.books.ContainsKey(book.Id))
			{
				context.books[book.Id].Title = book.Title;
				context.books[book.Id].Author = book.Author;
				context.books[book.Id].PublishmentYear = book.PublishmentYear;
				context.books[book.Id].Genre = book.Genre;
			}
			throw new Exception("Such book with does not exist in the library");
		}

		public void AddBook(Book book)
		{
			context.books.Add(bookKey, book);
			bookKey++;
		}

		public void DeleteBook(int id)
		{
			if (context.books.ContainsKey(id))
			{
				context.books.Remove(id);
			}
			throw new Exception("Book with such id does not exist");
		}


		//Readers
		public List<Reader> GetAllReaders()
		{
			return context.readers;
		}

		public Reader GetReaderById(int id)
		{
			for (int i = 0; i < context.readers.Count; i++)
			{
				if (context.readers[i].Id == id)
				{
					return context.readers[i];
				}
			}
			throw new Exception("Reader with such id does not exist");
		}

		public void UpdateReaderInfo(Reader reader)
		{
			for (int i = 0; i < context.readers.Count; i++)
			{
				if (context.readers[i].Id == reader.Id)
				{
					context.readers[i].FirstName = reader.FirstName;
					context.readers[i].LastName = reader.LastName;
					context.readers[i].Id = reader.Id;
				}
			}
			throw new Exception("Such reader does not exist in the library");
		}

		public void AddReader(Reader reader)
		{
			context.readers.Add(reader);
		}

		public void DeleteReader(int id)
		{
			for (int i = 0; i < context.readers.Count; i++)
			{
				if (context.readers[i].Id == id)
				{
					context.readers.Remove(context.readers[i]);
				}
			}
			throw new Exception("Reader with such id does not exist");
		}


		//Events
		public List<BorrowingEvent> GetAllBorrowingEvents()
		{
			return context.events;
		}

		public BorrowingEvent GetBorrowingEventById(int id)
		{
			for (int i = 0; i < context.events.Count; i++)
			{
				if (context.events[i].Id == id)
				{
					return context.events[i];
				}
			}
			throw new Exception("BorrowingEvent with such id does not exist");
		}


		public void AddBorrowingEvent(BorrowingEvent e)
		{
			context.events.Add(e);
		}

		public void DeleteBorrowingEvent(int id)
		{
			for (int i = 0; i < context.events.Count; i++)
			{
				if (context.events[i].Id == id)
				{
					context.events.Remove(context.events[i]);
				}
			}
			throw new Exception("Event with such id does not exist");
		}

		public void UpdateEventInfo(BorrowingEvent e)
		{
			for (int i = 0; i < context.events.Count; i++)
			{
				if (context.events[i].Id == e.Id)
				{
					context.events[i].Borrow_date = e.Borrow_date;
					context.events[i].Due_date = e.Borrow_date;
					context.events[i].Reader = e.Reader;
					context.events[i].State = e.State;
				}

			}
		}
	}
}
