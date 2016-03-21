
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace MovieTime.Business
{

    //Generic Class BookingList implements IEnumerable interface
    class BookingList<T> : IEnumerable
    {
        public List<T> ListBook { get; set; }
        public BookingList()
        {
            ListBook = new List<T>();
        }
        public void Add(T obj)
        {
            ListBook.Add(obj); // adds the object to arraylist
        }
        public IEnumerator GetEnumerator()
        {
            foreach (T someBook in ListBook)  // itreate through arraylist 
                yield return someBook;
        }
    }
}
