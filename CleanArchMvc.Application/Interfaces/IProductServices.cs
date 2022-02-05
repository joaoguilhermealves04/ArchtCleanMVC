using CleanArchMvc.Application.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchMvc.Application.Interfaces
{
    public interface IProductServices
    {
        Task<IEnumerable<ProductDto>>GetProducts();
        Task<ProductDto> GetById(int? Id);
        Task Add(ProductDto product);
        Task Update(ProductDto product);
        Task Delete(int? Id);
    }
}
