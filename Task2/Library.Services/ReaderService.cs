using Library.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library.Services
{
    class ReaderService
    {
        public IEnumerable<Reader> GetReaders()
        {
            using (var context = new LibraryDataContext())
            {
                return context.Readers.ToList();
            }
        }

        public int GetAllReadersNumber()
        {
            using (var context = new LibraryDataContext())
            {
                return context.Readers.Count();
            }
        }

        public Reader GetReader(string fName, string lName)
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

        /*public bool AddReader(string fName, string lName)
        {
            using (var context = new LibraryDataContext())
            {
                if (GetReader())
            }
        }*/
    }

    /*
        int GetAllReadersNumber();
        Reader GetReaderById(int id);
        void UpdateReaderInfo(Reader reader);
        void AddReader(Reader reader);
        void DeleteReader(int id);*/
}
