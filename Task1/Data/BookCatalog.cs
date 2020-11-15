using System;
using System.Collections.Generic;
using System.Text;

namespace Data
{
    public class BookCatalog
    {
        public Dictionary<int, Book> allBooks { get; set; } = new Dictionary<int, Book>();
    }
}
