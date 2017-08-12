using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyApp.Wrappers;

namespace MyApp
{
    /// <summary>
    /// The service that uses Entity Framework to perform operations.
    /// This service needs to be unit tested.
    /// </summary>
    public class MyService
    {
        private readonly IMyContextProvider contextProvider;

        /// <summary>
        /// Constructs a service instance, optionally with a custom IMyContextProvider instance (for testability reasons).
        /// If a context provider is not passed, it uses the local MyContextProvider implementation instead.
        /// </summary>
        public MyService(IMyContextProvider contextProvider = null)
        {
            this.contextProvider = contextProvider ?? new MyContextProvider();
        }

        // Operations that use the Entity Framework context obtained through the 
        // context provider and accessed through the wrappers:

        public void AddItems()
        {
            using (var context = contextProvider.CreateContext())
            {
                var rootItem1 = new MyRootItem { Id = 1, MyRootValue = "Test A" };
                rootItem1.MyChildItems.Add(new MyChildItem { Id = 1, MyChildValue = "Test A.1" });
                rootItem1.MyChildItems.Add(new MyChildItem { Id = 2, MyChildValue = "Test A.2" });
                context.MyRootItems.Add(rootItem1);
                var rootItem2 = new MyRootItem { Id = 2, MyRootValue = "Test B" };
                rootItem2.MyChildItems.Add(new MyChildItem { Id = 3, MyChildValue = "Test B.1" });
                context.MyRootItems.Add(rootItem2);
                context.SaveChanges();
            }
        }

        public IReadOnlyCollection<MyRootItemDto> GetItems()
        {
            using (var context = contextProvider.CreateContext())
            {
                return context.MyRootItems.ToArray().Select(r => new MyRootItemDto
                {
                    Id = r.Id,
                    MyRootValue = r.MyRootValue,
                    MyChildItems = r.MyChildItems.ToArray().Select(c => new MyChildItemDto
                    {
                        Id = c.Id,
                        MyChildValue = c.MyChildValue
                    }).ToArray()
                }).ToArray();
            }
        }

        public void UpdateAllChildItemValues(string postfix)
        {
            using (var context = contextProvider.CreateContext())
            {
                foreach (var childItem in context.MyChildItems)
                    childItem.MyChildValue += $" {postfix}";
                context.SaveChanges();
            }
        }

        public void RemoveItems()
        {
            using (var context = contextProvider.CreateContext())
            {
                foreach (var rootItem in context.MyRootItems.ToArray())
                {
                    foreach (var childItem in rootItem.MyChildItems.ToArray())
                        rootItem.MyChildItems.Remove(childItem);
                    context.MyRootItems.Remove(rootItem);
                }
                foreach (var childItem in context.MyChildItems.ToArray())
                    context.MyChildItems.Remove(childItem);
                context.SaveChanges();
            }
        }
    }
}
