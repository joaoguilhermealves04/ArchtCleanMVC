using CleanArchMvc.Application.Dto;
using CleanArchMvc.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CleanArcheMvc.API.Controllers
{
    [Route("API/[Controller]")]
    [ApiController]
    [Authorize]
    public class ProductsController : ControllerBase
    {
        private readonly IProductServices _productServices;

        public ProductsController(IProductServices productServices)
        {
            _productServices = productServices;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetProduct()
        {
            var product = await _productServices.GetProducts();
            if (product == null)
            {
                return NotFound("Products not found");
            }
            return Ok(product);
        }

        [HttpGet("{id:int}", Name = "GetProduct")]
        public async Task<ActionResult<ProductDto>> Get(int id)
        {
            var products = await _productServices.GetById(id);
            if (products == null)
            {
                return NotFound("Products not found");
            }
            return Ok(products);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] ProductDto product)
        {
            if (product == null)
            {
                return BadRequest("Invalid Data");
            }

            await _productServices.Add(product);
            return new CreatedAtRouteResult("GetProduct", new { id = product.Id },
                product);
        }

        [HttpPut]
        public async Task<ActionResult>Put (int id , [FromBody] ProductDto product)
        {
            if(id != product.Id)
            {
                return BadRequest();
            }

            if (product == null)
            {
                return BadRequest();
            }

            await _productServices.Update(product);
            return Ok(product);
        }

        [HttpDelete]
        public async Task<ActionResult> Delete(int id)
        {
            var products = await _productServices.GetById(id);
            if(products == null)
            {
                return NotFound("Category Not Found");
            }
            await _productServices.Delete(id);
            return Ok(products);
        }
    }
}
