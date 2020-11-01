using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryProject
{
	class Reader
	{
		private string firstName;
		private string lastName;
		private int id;

		public Reader(string userFirstName, string userLastName, int userId)
		{
			firstName = userFirstName;
			lastName = userLastName;
			id = userId;

		}

		public string FirstName
		{
			get { return firstName; }
			set { firstName = value; }
		}

		public string LastName
		{
			get { return lastName; }
			set { lastName = value; }
		}

		public int Id
		{
			get { return id; }
			set { id = value; }
		}
	}
}
