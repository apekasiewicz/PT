using System;
using System.Collections.Generic;
using System.Text;
using Library.Services;
using Library.UI.Common;
using System.ComponentModel;

namespace Library.UI
{
    public class BookViewModel : ModelViewBase, IDataErrorInfo
    {
        public BookViewModel()
        {
            AddBookCommand = new CommandBase(AddBook);
            EditBookCommand = new CommandBase(EditBook);
        }

        private string title;
        public string Title
        {
            get
            {
                return this.title;
            }
            set
            {
                this.title = value;
                OnPropertyChanged("Title");
            }
        }

        private string author;
        public string Author
        {
            get
            {
                return this.author;
            }
            set
            {
                this.author = value;
                OnPropertyChanged("Author");
            }
        }

        private string year;
        public string Year
        {
            get
            {
                return this.year;
            }
            set
            {
                this.year = value;
                OnPropertyChanged("Year");
            }
        }


        private string genre;
        public string Genre
        {
            get
            {
                return this.genre;
            }
            set
            {
                this.genre = value;
                OnPropertyChanged("Genre");
            }
        }


        private string quantity;
        public string Quantity
        {
            get
            {
                return this.quantity;
            }
            set
            {
                this.quantity = value;
                OnPropertyChanged("Quantity");
            }
        }

        private string id;
        public string Id
        {
            get
            {
                return this.id;
            }
            set
            {
                this.id = value;
                OnPropertyChanged("Id");
            }
        }

        public CommandBase AddBookCommand { get; private set; }

		private void AddBook()
        {
            bool added = BookService.AddBook(Title, Author, int.Parse(Year), Genre, int.Parse(Quantity));
            if (added)
            {
                actionText = "Book added";
            }
            else
            {
                actionText = "Book already exists in the database";
            }
            MessageBoxShowDelegate(ActionText);
        }

        public CommandBase EditBookCommand { get; private set; }

        private void EditBook()
        {
            bool edited = BookService.UpdateBook(int.Parse(Id), Title, Author, int.Parse(Year),
                Genre, int.Parse(Quantity));
            if (edited)
            {
                actionText = "Book edited";
            }
            else
            {
                actionText = "Such book already exists in the database";
            }
            MessageBoxShowDelegate(ActionText);
        }


        public string Error
        {
            get { return null; }
        }

        string IDataErrorInfo.this[string columnName]
        {
            get
            {
                if (columnName == "Title")
                {
                    if (this.title == null)
                        return "Please enter book's title";
                    else if (title.Trim() == string.Empty)
                        return "Title is required";
                    else if (this.title.Length > 50)
                        return "Title must not exceed 50 characters";
                    else if (isNumeric(this.title))
                        return "Title cannot contain numeric characters";
                }
                else if (columnName == "Author")
                {
                    if (this.author == null)
                        return "Please enter book's author";
                    else if (this.author.Trim() == string.Empty)
                        return "Author is required";
                    else if (this.author.Length > 50)
                        return "Author must not exceed 50 characters";
                    else if (isNumeric(this.author))
                        return "Author cannot contain numeric characters";
                }
                else if (columnName == "Year")
                {
                    int pYear;
                    if (this.year == null)
                        return "Please enter book's year of publishment";
                    else if (this.year.Trim() == string.Empty)
                        return "Year of publishment is required";
                    else if (!int.TryParse(this.Year, out pYear))
                        return "Year must be integer";
                    else if (pYear > 2020)
                        return "Year must be smaller than 2021";
                }
                else if (columnName == "Genre")
                {
                    if (this.genre == null)
                        return "Please enter book's genre";
                    else if (this.genre.Trim() == string.Empty)
                        return "Genre is required";
                    else if (this.genre.Length > 50)
                        return "Genre must not exceed 50 characters";
                    else if (isNumeric(this.genre))
                        return "Genre cannot contain numeric characters";
                }
                else if (columnName == "Quantity")
                {
                    int pQuantity;
                    if (this.quantity == null)
                        return "Please enter book's quantity";
                    else if (this.quantity.Trim() == string.Empty)
                        return "Quantity is required";
                    else if (!int.TryParse(this.Quantity, out pQuantity))
                        return "Quantity must be non-negative integer";
                    else if (this.quantity.Length > 4)
                        return "Quantity must not exceed 4 characters";
                }
                else if (columnName == "Id")
                {
                    int entered_id, max_id = BookService.GetMaxId();

                    if (!int.TryParse(this.id, out entered_id))
                    {
                        return "ID must be an integer";
                    }
                    else
                    {
                        if (entered_id < 1)
                            return "ID must be at least 1";
                        else if (entered_id > max_id)
                            return "ID must not excced " + max_id.ToString();
                    }
                }
                return null;
            }
        }

        private bool isNumeric(string s)
        {
            foreach (char c in s)
            {
                if ((c >= '0' && c <= '9'))
                {
                    return true;
                }
            }
            return false;
        }


        private string actionText;
        public string ActionText
        {
            get
            {
                return this.actionText;
            }
            set
            {
                this.actionText = value;
                OnPropertyChanged("ActionText");
            }
        }

        public CommandBase DisplayPopUpCommand { get; private set; }

        public Action<string> MessageBoxShowDelegate { get; set; } = x => throw new ArgumentOutOfRangeException($"The delegate {nameof(MessageBoxShowDelegate)} must be assigned by the view layer");
    }

}

