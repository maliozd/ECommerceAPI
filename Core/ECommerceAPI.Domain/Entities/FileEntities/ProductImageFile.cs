using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Domain.Entities
{
    public class ProductImageFile : BaseFile //--> eklenen ürün fotoğrafının entity karşılığı
    {
        public ICollection<Product> Products { get; set; }
    }
}
