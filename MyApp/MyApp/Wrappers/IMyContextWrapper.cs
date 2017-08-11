using System;

namespace MyApp.Wrappers
{
    /// <summary>
    /// Extracted interface for MyContextWrapper for testability reasons.
    /// </summary>
    public interface IMyContextWrapper : IDisposable
    {
        // Uses IDbSetWrappers over actual database model classes.
        IDbSetWrapper<MyChildItem> MyChildItems { get; }
        IDbSetWrapper<MyRootItem> MyRootItems { get; }

        // Wrapper commit method.
        void SaveChanges();
    }
}
