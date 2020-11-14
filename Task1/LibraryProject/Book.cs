using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryProject
{
	class Book
	{
		public Book(int bookId, string bookTitle, string bookAuthor, int bookYear, BookGenre genre)
		{
			Id = bookId;
			Title = bookTitle;
			Author = bookAuthor;
			PublishmentYear = bookYear;
			Genre = genre;
		}

		public int Id { get; set; }

		public string Title { get; set; }

		public string Author { get; set; }

		public int PublishmentYear { get; set; }

		public BookGenre Genre { get; set; }

		public override bool Equals(object obj)
		{
			if ((obj == null) || !(obj is Book))
			{
				return false;
			}
			
			Book book = (Book) obj;
			return Id == book.Id && Title == book.Title && Author == book.Author &&
				PublishmentYear == book.PublishmentYear && Genre == book.Genre;
		}

		public override int GetHashCode()
		{
			return Id.GetHashCode() ^ Title.GetHashCode() ^ Author.GetHashCode() 
				^ PublishmentYear.GetHashCode() ^ Genre.GetHashCode();
		}
	}
}
