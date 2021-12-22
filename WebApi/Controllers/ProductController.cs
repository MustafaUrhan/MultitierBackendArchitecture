using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _prodcutService;

        public ProductController(IProductService prodcutService)
        {
            _prodcutService = prodcutService;
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add(Product product)
        {
            var result = await _prodcutService.Add(product);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPost("update")]
        public async Task<IActionResult> Update(Product product)
        {
            var result = await _prodcutService.Update(product);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPost("delete")]
        public async Task<IActionResult> Delete([FromBody] int productId)
        {
            var result = await _prodcutService.Delete(productId);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPost("getbyid")]
        public async Task<IActionResult> GetById(int productId)
        {
            var result = await _prodcutService.GetById(productId);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPost("getbycategoryid")]
        public async Task<IActionResult> GetByCategoryId(int categoryId)
        {
            var result = await _prodcutService.GetByCategoryId(categoryId);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPost("getall")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _prodcutService.GetAll();
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
    }
}
