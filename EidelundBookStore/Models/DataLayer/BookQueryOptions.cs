using EidelundBookStore.Models.DomainModels;
using EidelundBookStore.Models.ExtensionMethods;
using EidelundBookStore.Models.Grid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EidelundBookStore.Models.DataLayer
{
    // Extends generic QueryOptions<Book> class to add a SortFilter() method
    // that adds the Sort and Filter code specific to the Bookstore application
    public class BookQueryOptions : QueryOptions<Book>
    {
        public void SortFilter(BooksGridBuilder builder)
        {
            //filter
            if (builder.isFilterByGenre)
            {
                Where = b => b.GenreId == builder.CurrentRoute.GenreFilter;
            }
            if (builder.isFilterByPrice)
            {
                if (builder.CurrentRoute.PriceFilter == "under7")
                {
                    Where = b => b.Price < 7;
                }
                else if (builder.CurrentRoute.PriceFilter == "7to14")
                {
                    Where = b => b.Price >= 7 && b.Price <= 14;
                }
                else
                    Where = b => b.Price > 14;
            }

            if (builder.isFilterByAuthor)
            {
                int id = builder.CurrentRoute.AuthorFilter.ToInt();
                if(id > 0)
                {
                    Where = b => b.BookAuthors.Any(ba => ba.Author.AuthorId == id);
                }
            }

            //sort
            if (builder.IsSortByGenre)
                OrderBy = b => b.Genre.Name;
            else if (builder.IsSortByPrice)
                OrderBy = b => b.Price;
            else
                OrderBy = b => b.Title;
        }
    }
}
