using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind.Domain.Dto
{
    public class TotalProductByCategory
    {
        public String CategoryName { get; set; }
        public int TotalProduct { get; set; }
    }
}
