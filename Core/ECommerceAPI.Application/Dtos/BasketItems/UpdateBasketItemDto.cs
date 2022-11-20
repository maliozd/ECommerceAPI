using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Dtos.BasketItems
{
    public class UpdateBasketItemDto
    {
        public string BasketItemId { get; set; }
        public int Quantity { get; set; }
    }
}
