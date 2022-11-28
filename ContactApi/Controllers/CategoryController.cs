using ContactApi.Controllers.Base;
using Logic.Query.Category.GetAll;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.Providers;

namespace ContactApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : BaseApiController
    {
        private readonly IMediator _mediator;

        public CategoryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            var query = new GetAllCategoriesQuery();

            var result = await _mediator.Send(query);

            return BuildResponse(result);
        }

        [HttpGet("getCustomCategoryId")]
        public IActionResult GetCustomCategoryId()
        {
            return Ok(new
            {
                Id = CategoryIdProvider.CustomCategoryId
            });
        }
    }
}
