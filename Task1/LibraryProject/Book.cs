using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryProject
{
	class Book
	{
		public Book(int bookId, string bookTitle, string bookAuthor, int bookYear)
		{
			Id = bookId;
			Title = bookTitle;
			Author = bookAuthor;
			PublishmentYear = bookYear;
		}

		public int Id { get; set; }

		public string Title { get; set; }

		public string Author { get; set; }

		public int PublishmentYear { get; set; }

		public BookGenre Genre { get; set; } 
    }
}
