﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Abstraction.Services.User
{
   public interface  ICurrentUserService
    {
        public string UserId { get; }
    }
}
