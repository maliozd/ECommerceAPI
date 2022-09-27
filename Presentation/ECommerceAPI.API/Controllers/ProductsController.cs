using ECommerceAPI.Application.Abstraction.Storage;
using ECommerceAPI.Application.Features.Commands.Product.CreateProduct;
using ECommerceAPI.Application.Features.Commands.Product.DeleteProduct;
using ECommerceAPI.Application.Features.Commands.Product.ProductImageFile.DeleteProductImage;
using ECommerceAPI.Application.Features.Commands.Product.ProductImageFile.UploadProductImage;
using ECommerceAPI.Application.Features.Commands.UpdateProduct;
using ECommerceAPI.Application.Features.Queries.Product.GetAllProduct;
using ECommerceAPI.Application.Features.Queries.Product.GetByIdProduct;
using ECommerceAPI.Application.Features.Queries.ProductImageFile.GetByIdProductImageFile;
using ECommerceAPI.Application.Repositories;
using ECommerceAPI.Application.Repositories.ProductImageFileRepository;
using ECommerceAPI.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ECommerceAPI.API.Controllers
{//TEST
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Admin")] //status code 401 --> unauthorized
    public class ProductsController : ControllerBase
    {

        readonly IMediator _mediator;
        public ProductsController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetAllProductQueryRequest getAllProductQueryRequest)//paginator --> page / size queryden gelecek
        {
            var response = await _mediator.Send(getAllProductQueryRequest);
            return Ok(response);
        }
        [HttpGet("{Id}")]
        public async Task<IActionResult> Get([FromRoute] GetByIdProductRequest getByIdProductRequest)
        {
            var response = await _mediator.Send(getByIdProductRequest);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Post(CreateProductCommandRequest createProductCommandRequest)
        {
            var response = await _mediator.Send(createProductCommandRequest);
            //return StatusCode((int)HttpStatusCode.Created); //örnek
            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] UpdateProductCommandRequest updateProductCommandRequest) //sent updateRequest with body from client
        {
            var response = await _mediator.Send(updateProductCommandRequest);
            return Ok(response);
        }
        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete([FromRoute] DeleteByIdProductCommandRequest deleteByIdProductCommandRequest)
        {
            var response = await _mediator.Send(deleteByIdProductCommandRequest);
            return Ok(response);
        }

        [HttpPost("[action]")]  //http//../api/controller/action
        public async Task<IActionResult> Upload([FromQuery] UploadProductImageCommandRequest uploadProductImageCommandRequest)
        {
            uploadProductImageCommandRequest.Files = Request.Form.Files;
            var response = await _mediator.Send(uploadProductImageCommandRequest);
            return Ok();
        }

        [HttpGet("[action]/{Id}")] //alınacak parametre burdaki gibi routeda belirtilmiyorsa, parametre query stringten gelecektir.
        public async Task<IActionResult> GetProductImages([FromRoute]GetByIdProductImageFileQueryRequest getByIdProductImageFileQueryRequest)
        {
            var response = await _mediator.Send(getByIdProductImageFileQueryRequest);
            return Ok(response);
        }
        [HttpDelete("[action]/{Id}")] //imageId queryStringden, productId routedan gelecek.
        public async Task<IActionResult> DeleteProductImage([FromRoute] DeleteProductImageCommandRequest deleteProductImageCommandRequest, [FromQuery] int imageId)
        {
            deleteProductImageCommandRequest.ImageId = imageId;
            var response = await _mediator.Send(deleteProductImageCommandRequest);
            return Ok(response);
        }
    }
}
