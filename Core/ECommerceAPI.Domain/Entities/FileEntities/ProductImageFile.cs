using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Domain.Entities.FileEntities
{
    public class ProductImageFile : BaseFile //--> eklenen ürün fotoğrafının entity karşılığı
    {
        public bool Showcase { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
