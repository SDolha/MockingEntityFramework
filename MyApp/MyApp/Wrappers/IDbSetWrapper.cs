using System.Collections.Generic;

namespace MyApp.Wrappers
{
    /// <summary>
    /// Extracted interface for DbSetWrapper for testability reasons.
    /// </summary>
    public interface IDbSetWrapper<T> : IEnumerable<T> where T : class
    {
        void Add(T item);
        void Remove(T item);
    }
}
