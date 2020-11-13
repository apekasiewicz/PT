using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryProject
{
	class Reader
	{
        public Reader(string userFirstName, string userLastName, int userId)
		{
			FirstName = userFirstName;
			LastName = userLastName;
			Id = userId;

		}

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int Id { get; set; }
    }
}
