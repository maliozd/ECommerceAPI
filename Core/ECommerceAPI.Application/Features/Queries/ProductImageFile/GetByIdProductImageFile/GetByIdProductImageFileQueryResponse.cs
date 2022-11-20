using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Features.Queries.ProductImageFile.GetByIdProductImageFile
{
    public class GetByIdProductImageFileQueryResponse
    {
        public string? Id { get; set; }
        public string? FileName { get; set; }
        public string? Path { get; set; }        
        public bool Showcase { get; set; }
    }
}
