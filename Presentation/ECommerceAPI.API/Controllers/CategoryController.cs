using ECommerceAPI.Application.Abstraction.Services.Category;
using ECommerceAPI.Application.Features.Queries.Category.GetAllCategories;
using ECommerceAPI.Application.Features.Queries.Category.GetSubCategories;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        readonly IMediator _mediator;
        readonly ICategoryService _categoryService;

        public CategoryController(IMediator mediator, ICategoryService categoryService)
        {
            _mediator = mediator;
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCategories([FromQuery] GetAllCategoriesQueryRequest getAllCategoriesQueryRequest)
        {
            var response = await _mediator.Send(getAllCategoriesQueryRequest);
            return Ok(response);
        }


        [HttpGet("[action]")]

        public async Task<IActionResult> GetMainCategories([FromQuery] GetParentCategoriesQueryRequest getMainCategoriesQueryRequest)
        {
            var response = await _mediator.Send(getMainCategoriesQueryRequest);

            return Ok(response);
        }
        [HttpGet("[action]/{ParentId}")]
        public async Task<IActionResult> GetSubCategories([FromRoute] GetSubCategoryByParentIdQueryRequest getSubCategoryByParentIdQueryRequest)
        {
            var response = await _mediator.Send(getSubCategoryByParentIdQueryRequest);
            //TODO client createProduct category select
            return Ok(response);
        }

        [HttpGet("{CategoryId}")]
        public async Task<IActionResult> GetById([FromRoute] GetCategoryByIdQueryRequest getCategoryByIdQueryRequest)
        {
            var category = await _categoryService.GetDetailedCategoryByIdAsync(getCategoryByIdQueryRequest.CategoryId);
            return Ok(category);
        }

    }
}
