﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EidelundBookStore.Models.DataLayer.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private BookstoreContext _context { get; set; }
        public DbSet<T> dbSet { get; set; }
        public Repository(BookstoreContext context)
        {
            _context = context;
            dbSet = _context.Set<T>();
        }

        //get number of retrieved entities - use private backing field bc when filtering,
        // count might be less then dbset.Count()
        private int? count;
        public int Count => count ?? dbSet.Count();

        public virtual void Delete(T entity) => dbSet.Remove(entity);

        public virtual T Get(QueryOptions<T> options)
        {
            IQueryable<T> query = BuildQuery(options);

            return query.FirstOrDefault();
        }

        public virtual T Get(int id) => dbSet.Find(id);

        public virtual T Get(string id) => dbSet.Find(id);

        public virtual void Insert(T entity) => dbSet.Add(entity);

        public virtual IEnumerable<T> List(QueryOptions<T> options)
        {
            IQueryable<T> query = BuildQuery(options);

            return query.ToList();
        }

        public virtual void Save() => _context.SaveChanges();

        public virtual void Update(T entity) => dbSet.Update(entity);

        //private helper method to build query expression
        private IQueryable<T> BuildQuery(QueryOptions<T> options)
        {
            IQueryable<T> query = dbSet;
            foreach(string include in options.GetIncludes())
            {
                query = query.Include(include);
            }

            if (options.HasWhere)
            {
                foreach(var clause in options.WhereClauses)
                {
                    query = query.Where(clause);
                }
                count = query.Count(); //filtered count
            }

            if (options.HasOrderBy)
            {
                if (options.OrderByDirection == "asc")
                    query = query.OrderBy(options.OrderBy);
                else
                    query = query.OrderByDescending(options.OrderBy);
            }

            if (options.HasPaging)
                query = query.PageBy(options.PageNumber, options.PageSize);

            return query;
        }
    }
}
