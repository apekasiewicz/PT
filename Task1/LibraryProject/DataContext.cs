using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace LibraryProject
{
	class DataContext
	{
		public List<Reader> readers = new List<Reader>();
		public Dictionary<int, Book> books = new Dictionary<int, Book>();
		public List<State> states = new List<State>();
		public List<BorrowingEvent> zdarzenia = new List<BorrowingEvent>();
	}
}
