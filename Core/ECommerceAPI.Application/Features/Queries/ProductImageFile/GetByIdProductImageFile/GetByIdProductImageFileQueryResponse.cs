using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Features.Queries.ProductImageFile.GetByIdProductImageFile
{
    public class GetByIdProductImageFileQueryResponse
    {
        public IEnumerable<ECommerceAPI.Domain.Entities.FileEntities.ProductImageFile> productImageFiles;
    }
}
