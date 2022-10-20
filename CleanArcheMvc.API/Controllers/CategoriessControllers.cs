using CleanArchMvc.Application.Dto;
using CleanArchMvc.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CleanArcheMvc.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CategoriessController : ControllerBase
    {
        private readonly ICategoryServices _categoryServices;
        public CategoriessController(ICategoryServices categoryServices)
        {
            _categoryServices = categoryServices;
        }

        [HttpGet]
        public async Task<ActionResult <IEnumerable<CategoryDto>>>GetCategories()
        {
            var categories = await _categoryServices.GetCategorias();
            if(categories == null)
            {
                return NotFound("Categories not Found");
            }

            return Ok(categories);
        }

        [HttpGet("{id:int}", Name = "GetCategory")]
        public async Task<ActionResult<CategoryDto>>Get(int id)
        {
            var categories = await _categoryServices.GetById(id);
            if(categories == null)
            {
                return NotFound("Categoroies not Found");
            }

            return Ok(categories);
        }

        [HttpPost]
        public async Task<ActionResult>Post([FromBody]CategoryDto categoryDto)
        {
            if(categoryDto == null)
            {
                return BadRequest("Invalid Data");
            }
            await _categoryServices.Add(categoryDto);

            return new CreatedAtRouteResult("GetCategory",new {id=categoryDto.Id},
                categoryDto);

        }

        [HttpPut]
        public async Task<ActionResult> Put(int id,[FromBody]CategoryDto category)
        {
            if(id != category.Id)
            {
                return BadRequest();
            }
            
            if(category == null)
            {
                return BadRequest();
            }

            await _categoryServices.Update(category);

            return Ok(category);
            
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<CategoryDto>> Delete(int id)
        {
            var category = await _categoryServices.GetById(id); 

            if (category == null)
            {
                return NotFound("Category Not Found");
            }
            await _categoryServices.Delete(id);
            return Ok(category);
        }
    }
}
