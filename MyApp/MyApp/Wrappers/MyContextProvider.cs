using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Wrappers
{
    /// <summary>
    /// Provides instances of disposable MyContextWrapper when needed, having interface extracted for testability reasons.
    /// </summary>
    class MyContextProvider : IMyContextProvider
    {
        /// <summary>
        /// Creates a MyDatabaseEntities (DbContext) wrapped as the returned instance.
        /// </summary>
        public IMyContextWrapper CreateContext() => new MyContextWrapper(new MyDatabaseEntities());
    }
}
