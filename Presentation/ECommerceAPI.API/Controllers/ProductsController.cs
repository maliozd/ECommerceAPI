using ECommerceAPI.Application.Abstraction.Services.User;
using ECommerceAPI.Application.Constants;
using ECommerceAPI.Application.CustomAttributes;
using ECommerceAPI.Application.Enums;
using ECommerceAPI.Application.Features.Commands.Product.CreateProduct;
using ECommerceAPI.Application.Features.Commands.Product.DeleteProduct;
using ECommerceAPI.Application.Features.Commands.Product.ProductImageFile.ChangeShowcaseImage;
using ECommerceAPI.Application.Features.Commands.Product.ProductImageFile.DeleteProductImage;
using ECommerceAPI.Application.Features.Commands.Product.ProductImageFile.UploadProductImage;
using ECommerceAPI.Application.Features.Commands.UpdateProduct;
using ECommerceAPI.Application.Features.Queries.Product.GetAllProduct;
using ECommerceAPI.Application.Features.Queries.Product.GetByIdProduct;
using ECommerceAPI.Application.Features.Queries.ProductImageFile.GetByIdProductImageFile;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceAPI.API.Controllers
{//TEST
    [Route("api/[controller]")]
    [ApiController]

    public class ProductsController : ControllerBase
    {
        readonly IMediator _mediator;
        readonly IHttpContextAccessor httpContextAccessor;
        readonly ICurrentUserService _currentUserService;
        public ProductsController(IMediator mediator, IHttpContextAccessor httpContextAccessor, ICurrentUserService currentUserService)
        {
            _mediator = mediator;
            this.httpContextAccessor = httpContextAccessor;
            _currentUserService = currentUserService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProducts([FromQuery] GetAllProductQueryRequest getAllProductQueryRequest)//paginator --> page / size queryden gelecek
        {
            var response = await _mediator.Send(getAllProductQueryRequest);
            return Ok(response);
        }


        [HttpGet("{Id}")]
        public async Task<IActionResult> GetProductById([FromRoute] GetByIdProductRequest getByIdProductRequest)
        {
            var response = await _mediator.Send(getByIdProductRequest);
            return Ok(response);
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = "Admin")]
        [AuthorizeDefinition(Menu = AuthorizeDefinitonConstants.Products, ActionType = ActionType.Post, Definiton = "CreateProduct")]
        public async Task<IActionResult> CreateProduct(CreateProductCommandRequest createProductCommandRequest)
        {

            var response = await _mediator.Send(createProductCommandRequest);
            //return StatusCode((int)HttpStatusCode.Created); //örnek
            return Ok(response);
        }

        [HttpPut]
        //[Authorize(AuthenticationSchemes = "Admin")]
        [AuthorizeDefinition(Menu = AuthorizeDefinitonConstants.Products, ActionType = ActionType.Update, Definiton = "UpdateProduct")]
        public async Task<IActionResult> UpdateProduct([FromBody] UpdateProductCommandRequest updateProductCommandRequest) //sent updateRequest with body from client
        {
            var response = await _mediator.Send(updateProductCommandRequest);
            return Ok(response);
        }
        [HttpDelete("{Id}")]
        [Authorize(AuthenticationSchemes = "Admin")]
        [AuthorizeDefinition(Menu = AuthorizeDefinitonConstants.Products, ActionType = ActionType.Delete, Definiton = "DeleteProduct")]
        public async Task<IActionResult> DeleteProduct([FromRoute] DeleteByIdProductCommandRequest deleteByIdProductCommandRequest)
        {
            var response = await _mediator.Send(deleteByIdProductCommandRequest);
            return Ok(response);
        }

        [HttpPost("[action]")]  //http//../api/controller/action
        [Authorize(AuthenticationSchemes = "Admin")]
        [AuthorizeDefinition(Menu = AuthorizeDefinitonConstants.Products, ActionType = ActionType.Post, Definiton = "UploadProductImage")]
        public async Task<IActionResult> Upload([FromQuery] UploadProductImageCommandRequest uploadProductImageCommandRequest)
        {
            uploadProductImageCommandRequest.Files = Request.Form.Files;
            var response = await _mediator.Send(uploadProductImageCommandRequest);
            return Ok();
        }

        [HttpGet("[action]/{Id}")] //alınacak parametre burdaki gibi routeda belirtilmiyorsa, parametre query stringten gelecektir.
        [Authorize(AuthenticationSchemes = "Admin")]
        [AuthorizeDefinition(Menu = AuthorizeDefinitonConstants.Products, ActionType = ActionType.Get, Definiton = "GetProductImages")]
        public async Task<IActionResult> GetProductImages([FromRoute] GetByIdProductImageFileQueryRequest getByIdProductImageFileQueryRequest)
        {
            var response = await _mediator.Send(getByIdProductImageFileQueryRequest);
            return Ok(response);
        }
        [HttpDelete("[action]/{Id}")] //imageId queryStringden, productId routedan gelecek.
        [Authorize(AuthenticationSchemes = "Admin")]
        [AuthorizeDefinition(Menu = AuthorizeDefinitonConstants.Products, ActionType = ActionType.Delete, Definiton = "DeleteProductImage")]
        public async Task<IActionResult> DeleteProductImage([FromRoute] DeleteProductImageCommandRequest deleteProductImageCommandRequest, [FromQuery] string imageId)
        {
            deleteProductImageCommandRequest.ImageId = imageId;
            var response = await _mediator.Send(deleteProductImageCommandRequest);
            return Ok(response);
        }
        [HttpPut("[action]")]
        [Authorize(AuthenticationSchemes = "Admin")]
        [AuthorizeDefinition(Menu = AuthorizeDefinitonConstants.Products, ActionType = ActionType.Update, Definiton = "ChangeProductImageShowcase")]

        public async Task<IActionResult> ChangeProductImageShowcase([FromBody] ChangeShowcaseImageCommandRequest changeShowcaseImageCommandRequest)
        {
            var response = await _mediator.Send(changeShowcaseImageCommandRequest);
            return Ok(response);
        }

    }
}
