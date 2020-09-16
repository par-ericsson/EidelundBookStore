using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EidelundBookStore.Models.DomainModels
{
    public class BookAuthor
    {
        //composite key
        public int BookId { get; set; }
        public int AuthorId { get; set; }

        //navigation properties
        public Author Author { get; set; }
        public Book Book { get; set; }
    }
}
