using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Dtos.Configuration
{
    public class Menu
    {
        public string MenuName { get; set; }
        public ICollection<Action> Actions { get; set; } = new List<Action>();
    }
}
