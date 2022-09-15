using ECommerceAPI.Application.Abstraction.Storage;
using ECommerceAPI.Application.Features.Commands.Product.CreateProduct;
using ECommerceAPI.Application.Features.Commands.Product.DeleteProduct;
using ECommerceAPI.Application.Features.Commands.Product.ProductImageFile.DeleteProductImage;
using ECommerceAPI.Application.Features.Commands.Product.ProductImageFile.UploadProductImage;
using ECommerceAPI.Application.Features.Commands.UpdateProduct;
using ECommerceAPI.Application.Features.Queries.Product.GetAllProduct;
using ECommerceAPI.Application.Features.Queries.Product.GetByIdProduct;
using ECommerceAPI.Application.Repositories;
using ECommerceAPI.Application.Repositories.ProductImageFileRepository;
using ECommerceAPI.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ECommerceAPI.API.Controllers
{//TEST
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductWriteRepository _productWriteRepository;
        private readonly IProductReadRepository _productReadRepository;
        IWebHostEnvironment _webHostEnvironment;
        readonly IFileWriteRepository _fileWriteRepository;
        readonly IStorageService _storageService;
        readonly IProductImageFileWriteRepository _productImageFileWriteRepository;
        readonly IConfiguration configuration;

        readonly IMediator _mediator;
        public ProductsController(IProductWriteRepository productWriteRepository, IProductReadRepository productReadRepository, IWebHostEnvironment webHostEnvironment, IFileWriteRepository fileWriteRepository, IStorageService storageService, IProductImageFileWriteRepository productImageFileWriteRepository, IConfiguration config, IMediator mediator)
        {
            configuration = config;
            _productReadRepository = productReadRepository;
            _productWriteRepository = productWriteRepository;
            _webHostEnvironment = webHostEnvironment;
            _fileWriteRepository = fileWriteRepository;
            _storageService = storageService;
            _productImageFileWriteRepository = productImageFileWriteRepository;
            _mediator = mediator;
        }
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetAllProductQueryRequest getAllProductQueryRequest)//paginator --> page / size queryden gelecek
        {
            var response = await _mediator.Send(getAllProductQueryRequest);
            return Ok(response);
        } 
        [HttpGet("{Id}")]
        public async Task<IActionResult> Get([FromRoute]GetByIdProductRequest getByIdProductRequest)
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
        public async Task<IActionResult> Put([FromBody]UpdateProductCommandRequest request) //sent updateRequest with body from client
        {
            var response = await _mediator.Send(request);
            return Ok(response);
        }
        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete([FromRoute]DeleteByIdProductCommandRequest deleteByIdProductCommandRequest)
        {
            var response = await _mediator.Send(deleteByIdProductCommandRequest);
            return Ok(response);
        } 

        [HttpPost("[action]")]  //http//../api/controller/action
        public async Task<IActionResult> Upload([FromQuery]UploadProductImageCommandRequest uploadProductImageCommandRequest)
        {
            uploadProductImageCommandRequest.Files = Request.Form.Files;
            var response = await _mediator.Send(uploadProductImageCommandRequest);
            return Ok();
        }

        [HttpGet("[action]/{id}")] //alınacak parametre burdaki gibi routeda belirtilmiyorsa, parametre query stringten gelecektir.
        public async Task<IActionResult> GetProductImages(int id)
        {
            Product? product = await _productReadRepository.Table.Include(p => p.ProductImageFiles).FirstOrDefaultAsync(p => p.Id == id);
            return Ok(product.ProductImageFiles.Select(p => new
            {
                Path = $"{configuration["BaseStorageUrl"]}/{p.Path}",
                p.FileName,
                p.Id
            }));
        }
        [HttpDelete("[action]/{Id}")] //imageId queryStringden, productId routedan gelecek.
        public async Task<IActionResult> DeleteProductImage([FromRoute]DeleteProductImageCommandRequest deleteProductImageCommandRequest,[FromQuery] int imageId)
        {
            deleteProductImageCommandRequest.ImageId = imageId;
            var response = await _mediator.Send(deleteProductImageCommandRequest);
            return Ok(response);
        }      
    }
}
