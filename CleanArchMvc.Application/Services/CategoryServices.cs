using AutoMapper;
using CleanArchMvc.Application.Dto;
using CleanArchMvc.Application.Interfaces;
using CleanArchMvc.Domain.Entities;
using CleanArchMvc.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchMvc.Application.Services
{
    public class CategoryServices : ICategoryServices
    {
        private ICategoryRepository _categoryRepository;
        private IMapper _mapper;

        public CategoryServices(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task Add(CategoryDto categoryDto)
        {
            var categoreEntity = _mapper.Map<Category>(categoryDto);
            await _categoryRepository.Craete(categoreEntity);
        }

        public async Task Delete(int? Id)
        {
            var CategoreEntity = _categoryRepository.GetById(Id).Result;
            await _categoryRepository.Remove(CategoreEntity);
        }

        public async Task<CategoryDto> GetById(int? Id)
        {
            var CategoreEntity = await _categoryRepository.GetById(Id);
            return _mapper.Map<CategoryDto>(CategoreEntity);
        }

        public async Task<IEnumerable<CategoryDto>> GetCategorias()
        {
            var categoriesEntity = await _categoryRepository.GetCategorys();
            return _mapper.Map<IEnumerable<CategoryDto>>(categoriesEntity);
        }

        public async Task Update(CategoryDto categoryDto)
        {
            var categoreEntity = _mapper.Map<Category>(categoryDto);
            await _categoryRepository.Update(categoreEntity);
        }
    }
}
