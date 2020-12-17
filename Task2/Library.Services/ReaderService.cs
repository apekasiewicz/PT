using Library.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library.Services
{
    public class ReaderService
    {
        public IEnumerable<Reader> GetReaders()
        {
            using (var context = new LibraryDataContext())
            {
                return context.Readers.ToList();
            }
        }

        static public int GetAllReadersNumber()
        {
            using (var context = new LibraryDataContext())
            {
                return context.Readers.Count();
            }
        }

        public static Reader GetReader(string fName, string lName)
        {
            using (var context = new LibraryDataContext())
            {
                foreach (Reader r in context.Readers.ToList())
                {
                    if (r.reader_f_name.Equals(fName) && r.reader_l_name.Equals(lName))
                    {
                        return r;
                    }
                }
                return null;
            }
        }

        public Reader GetReaderById(int id)
        {
            using (var context = new LibraryDataContext())
            {
                foreach (Reader r in context.Readers.ToList())
                {
                    if (r.reader_id.Equals(id))
                    {
                        return r;
                    }
                }
                return null;
            }
        }

        static public bool AddReader(string fName, string lName)
        {
            using (var context = new LibraryDataContext())
            {
                if (GetReader(fName, lName) == null)
                {
                    Reader newReader = new Reader
                    {
                        reader_f_name = fName,
                        reader_l_name = lName
                    };
                    context.Readers.InsertOnSubmit(newReader);
                    context.SubmitChanges();
                    return true;
                }
                return false;
            }
        }

        static public bool UpdateReaderFName(int id, string fName)
        {
            using (var context = new LibraryDataContext())
            {
                Reader reader = context.Readers.SingleOrDefault(i => i.reader_id == id);
                reader.reader_f_name = fName;
                context.SubmitChanges();
                return true;
            }
        }

        static public bool UpdateReaderLName(int id, string lName)
        {
            using (var context = new LibraryDataContext())
            {
                Reader reader = context.Readers.SingleOrDefault(i => i.reader_id == id);
                reader.reader_l_name = lName;
                context.SubmitChanges();
                return true;
            }
        }

        static public bool DeleteReader(string fName, string lName)
        {
            using (var context = new LibraryDataContext())
            {
                Reader reader = context.Readers.SingleOrDefault(i => i.reader_f_name.Equals(fName) 
                        && i.reader_l_name.Equals(lName));
                context.Readers.DeleteOnSubmit(reader);
                context.SubmitChanges();
                return true;
            }
        }
    }
}
