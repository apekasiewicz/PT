using System;
using System.Collections.Generic;
using System.Text;

namespace Data
{
    public class Reader
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

        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is Reader))
            {
                return false;
            }

            Reader reader = (Reader)obj;
            return Id == reader.Id && FirstName == reader.FirstName && LastName == reader.LastName;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode() ^ FirstName.GetHashCode() ^ LastName.GetHashCode();
        }
    }
}
