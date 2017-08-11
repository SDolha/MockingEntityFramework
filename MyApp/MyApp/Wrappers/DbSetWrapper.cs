using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Wrappers
{
    /// <summary>
    /// Wrapper over DbSet, extracting interface for testability reasons.
    /// </summary>
    class DbSetWrapper<T> : IDbSet<T>, IEnumerable<T> where T:class
    {
        /// <summary>
        /// Holds the original DbSet instance.
        /// </summary>
        private readonly DbSet<T> internalDbSet;

        /// <summary>
        /// Constructs a wrapper instance over an existing DbSet instance.
        /// </summary>
        public DbSetWrapper(DbSet<T> dbSet)
        {
            if (dbSet == null)
                throw new ArgumentNullException(nameof(dbSet));
            internalDbSet = dbSet;
        }

        // Wrapper methods.
        public IEnumerator<T> GetEnumerator() => ((IEnumerable<T>)internalDbSet).GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable)internalDbSet).GetEnumerator();
        public void Add(T item) => internalDbSet.Add(item);
        public void Remove(T item) => internalDbSet.Remove(item);
    }
}
