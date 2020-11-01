using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryProject
{
	class Book
	{
		private int id;
		private string title;
		private string author;
		private int publishmentYear;

		public Book(int bookId, string bookTitle, string bookAuthor, int bookYear)
		{
			id = bookId;
			title = bookTitle;
			author = bookAuthor;
			publishmentYear = bookYear;
		}

		public int Id
		{
			get { return id; }
			set { id = value; }

		}

		public string Title
		{
			get { return title; }
			set { title = value; }

		}

		public string Author
		{
			get { return author; }
			set { author = value; }

		}

		public int PublishmentYear
		{
			get { return publishmentYear; }
			set { publishmentYear = value; }
		}

	}
}
