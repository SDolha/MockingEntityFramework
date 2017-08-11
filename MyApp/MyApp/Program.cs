using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var service = new MyService();
            service.AddItems();
            service.UpdateAllChildItemValues("(updated)");
            foreach (var rootItem in service.GetItems())
            {
                Console.WriteLine($"Root item {rootItem.Id}: {rootItem.MyRootValue}");
                foreach (var childItem in rootItem.MyChildItems)
                    Console.WriteLine($"\tChild item {childItem.Id}: {childItem.MyChildValue}");
            }
            service.RemoveItems();
        }
    }
}
