using CleanArchMvc.Application.Dto;
using CleanArchMvc.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CleanArchMvc.WebUI.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductServices _productServices;
        private readonly ICategoryServices _categoryServices;
        private readonly IWebHostEnvironment _hostEnvironment;
        public ProductsController(IProductServices productServices, ICategoryServices categoryServices, IWebHostEnvironment hostEnvironment)
        {
            _productServices = productServices;
            _categoryServices = categoryServices;
            _hostEnvironment = hostEnvironment;
        }
        public async Task<IActionResult> Index()
        {
            var products = await _productServices.GetProducts();
            return View(products);
        }

        [HttpGet()]
        public async Task<IActionResult> Create()
        {
            var categoriasDto = await _categoryServices.GetCategorias();

            List<SelectListItem> categoriasItm = new();

            foreach (var categoria in categoriasDto)
            {
                categoriasItm.Add(new SelectListItem(categoria.Nome, categoria.Id.ToString()));
            }

            ViewBag.Category = categoriasItm;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductDto productDto)
        {
            if (ModelState.IsValid)
            {
                await _productServices.Add(productDto);
                return RedirectToAction(nameof(Index));
            }
            return View(productDto);
        }

        [HttpGet()]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var productdto = await _productServices.GetById(id);

            if (productdto == null) return NotFound();

            var categories = await _categoryServices.GetCategorias();

            List<SelectListItem> categoriasItm = new();

            foreach (var categoria in categories)
            {
                categoriasItm.Add(new SelectListItem(categoria.Nome, categoria.Id.ToString()));
            }

            ViewBag.Category = categoriasItm;

            return View(productdto);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ProductDto productDto)
        {
            if (ModelState.IsValid)
            {
                await _productServices.Update(productDto);
                return RedirectToAction(nameof(Index));
            }
            return View(productDto);
        }

        [HttpGet()]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var productdto = await _productServices.GetById(id);
            if (productdto == null) return NotFound();

            return View(productdto);
        }

        [Authorize(Roles ="Admin")]
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _productServices.Delete(id);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var productdto = await _productServices.GetById(id);
            if (productdto == null) return NotFound();

            var wwroot = _hostEnvironment.WebRootPath;
            var imagem = Path.Combine(wwroot, "Images\\" + productdto.Imagem);
            var exists = System.IO.File.Exists(imagem);
            ViewBag.ImagemExist = exists;

            return View(productdto);
        }
    }
}
