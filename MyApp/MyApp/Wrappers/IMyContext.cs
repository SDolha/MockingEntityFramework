using System;

namespace MyApp.Wrappers
{
    /// <summary>
    /// Extracted interface for MyContextWrapper for testability reasons.
    /// </summary>
    public interface IMyContext : IDisposable
    {
        // Uses IDbSet over actual database model classes.
        IDbSet<MyChildItem> MyChildItems { get; }
        IDbSet<MyRootItem> MyRootItems { get; }

        // Wrapper commit method.
        void SaveChanges();
    }
}
