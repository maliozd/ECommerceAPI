using ECommerceAPI.Domain.Entities.BasketEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Dtos.Order
{
    public class ListOrderDto
    {
        public int TotalOrderCount { get; set; }
        public object Orders { get; set; }
    }
}
