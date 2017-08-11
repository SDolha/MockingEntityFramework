using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Wrappers
{
    /// <summary>
    /// Wrapper over MyDatabaseEntities (DbContext), extracting interface for testability reasons.
    /// </summary>
    class MyContextWrapper : IMyContext, IDisposable
    {
        /// <summary>
        /// Holds the original MyDatabaseEntities (DbContext) instance.
        /// </summary>
        private readonly MyDatabaseEntities internalContext;

        /// <summary>
        /// Constructs a wrapper instance over an existing MyDatabaseEntities (DbContext) instance.
        /// </summary>
        public MyContextWrapper(MyDatabaseEntities context)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));
            internalContext = context;
        }

        // Fields that hold IDbSet instances.
        private IDbSet<MyRootItem> myRootItems;
        private IDbSet<MyChildItem> myChildItems;

        // Uses IDbSets (singleton instances under current context) over actual database model classes.
        public IDbSet<MyRootItem> MyRootItems => myRootItems ?? (myRootItems = new DbSetWrapper<MyRootItem>(internalContext.MyRootItems));
        public IDbSet<MyChildItem> MyChildItems => myChildItems ?? (myChildItems = new DbSetWrapper<MyChildItem>(internalContext.MyChildItems));

        // Wrapper commit method.
        public void SaveChanges() => internalContext.SaveChanges();

        // Dispose pattern providing rollback mechanism as well.
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    if (internalContext != null)
                        internalContext.Dispose();
                }
                disposed = true;
            }
        }
        private bool disposed = false;
    }
}
