using CleanArchMvc.Application.Dto;
using CleanArchMvc.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace CleanArchMvc.WebUI.Controllers
{
    [Authorize]
    public class CategoriesController : Controller
    {
        private readonly ICategoryServices _categoryServices;
        public CategoriesController(ICategoryServices categoryServices)
        {
            _categoryServices = categoryServices;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var categories = await _categoryServices.GetCategorias();
            return View(categories);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CategoryDto categoryDto)
        {
            if (ModelState.IsValid)
            {
                 await _categoryServices.Add(categoryDto);
                return RedirectToAction(nameof(Index));
            }
            return View(categoryDto);
        }

        [HttpGet()]
        public async Task<IActionResult> Edit( int? id)
        {
            if (id == null) return NotFound();

            var category = await _categoryServices.GetById(id);

            if(category == null) return NotFound();
            
            return View(category);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(CategoryDto categoryDto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _categoryServices.Update(categoryDto);
                }
                catch (Exception)
                {
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(categoryDto);
        }

        [HttpGet()]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var category = await _categoryServices.GetById(id);

            if (category == null) return NotFound();

            return View(category);
        }

        [HttpPost,ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _categoryServices.Delete(id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if(id == null) return NotFound();

            var categoryDto = await _categoryServices.GetById(id);

            if(categoryDto == null) return NotFound();

            return View(categoryDto);
        }
    }
}
