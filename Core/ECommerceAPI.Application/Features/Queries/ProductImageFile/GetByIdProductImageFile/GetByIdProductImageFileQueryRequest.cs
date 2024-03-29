﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Features.Queries.ProductImageFile.GetByIdProductImageFile
{
    public class GetByIdProductImageFileQueryRequest : IRequest<List<GetByIdProductImageFileQueryResponse>>
    {
        public string Id { get; set; }
    }
}
