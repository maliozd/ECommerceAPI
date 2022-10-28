using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Features.Commands.Product.ProductImageFile.ChangeShowcaseImage
{
    public class ChangeShowcaseImageCommandRequest : IRequest<ChangeShowcaseImageCommandResponse>
    {
        public int ImageId { get; set; }
        public int ProductId { get; set; }
    }
}
