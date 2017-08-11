using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp
{
    class MyService
    {
        public void AddItems()
        {
            using (var context = new MyDatabaseEntities())
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
            using (var context = new MyDatabaseEntities())
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

        public void RemoveItems()
        {
            using (var context = new MyDatabaseEntities())
            {
                foreach (var rootItem in context.MyRootItems.ToArray())
                {
                    foreach (var childItem in rootItem.MyChildItems.ToArray())
                        rootItem.MyChildItems.Remove(childItem);
                    context.MyRootItems.Remove(rootItem);
                }
                context.SaveChanges();
            }
        }
    }
}
