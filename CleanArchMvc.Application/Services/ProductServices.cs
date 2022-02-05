using AutoMapper;
using CleanArchMvc.Application.Dto;
using CleanArchMvc.Application.Interfaces;
using CleanArchMvc.Application.Products.Commands;
using CleanArchMvc.Application.Products.Queries;
using CleanArchMvc.Domain.Entities;
using CleanArchMvc.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchMvc.Application.Services
{
    public class ProductServices : IProductServices
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        public ProductServices(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        public async Task Add(ProductDto product)
        {
            var productCreateCommand = _mapper.Map<ProductCreateCommand>(product);
            await _mediator.Send(productCreateCommand);
        }

        public async Task Delete(int? Id)
        {
            var productRemoveCommand =new ProductRemoveCommand(Id.Value);
            if (productRemoveCommand == null)
            {
                throw new Exception($"Entity could not be loaded.");
            }
            await _mediator.Send(productRemoveCommand);
        }

        public async Task<ProductDto> GetById(int? Id)
        {
            var productsByIdQurey = new GetProductByIdQuery(Id.Value);
            if (productsByIdQurey == null)
            {
                throw new Exception($"Entity could not be loaded.");
            }

            var result = await _mediator.Send(productsByIdQurey);
            return _mapper.Map<ProductDto>(result);
        }
    

        public async Task<IEnumerable<ProductDto>> GetProducts()
        {
            
            var productsQurey = new GetProductsQuery();
            if (productsQurey == null)
            {
                throw new Exception($"Entity could not be loaded.");
            }

            var result = await _mediator.Send(productsQurey);
            return _mapper.Map<IEnumerable<ProductDto>>(result);

        }

        public async Task Update(ProductDto product)
        {
            var productUpdateCommand = _mapper.Map<ProductUpdateCommand>(product);
            await _mediator.Send(productUpdateCommand);
        }
    }
}
