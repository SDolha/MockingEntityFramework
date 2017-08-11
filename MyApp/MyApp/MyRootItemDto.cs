using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp
{
    class MyRootItemDto
    {
        public int Id { get; set; }
        public string MyRootValue { get; set; }
        public IReadOnlyCollection<MyChildItemDto> MyChildItems { get; set; }
    }

    class MyChildItemDto
    {
        public int Id { get; set; }
        public string MyChildValue { get; set; }
    }
}
