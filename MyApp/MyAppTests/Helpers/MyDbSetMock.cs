using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyApp.Wrappers;

namespace MyAppTests.Helpers
{
    /// <summary>
    /// Mock implementation for IDbSet using an internal list of objects instead of an actual DbSet.
    /// </summary>
    class DbSetMock<T> : IDbSet<T> where T : class
    {
        private readonly List<T> internalList;

        public DbSetMock(IEnumerable<T> items = null)
        {
            internalList = items != null ? new List<T>(items) : new List<T>();
        }

        public IEnumerator<T> GetEnumerator() => internalList.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => internalList.GetEnumerator();
        public void Add(T item) => internalList.Add(item);
        public void Remove(T item) => internalList.Remove(item);
    }
}
