using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Dtos.Product
{
    public class CreateProductDto
    {
        public string Name { get; set; }
        public int Stock { get; set; }
        public int Price { get; set; }
    }
}
