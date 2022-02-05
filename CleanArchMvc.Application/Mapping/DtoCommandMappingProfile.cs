using AutoMapper;
using CleanArchMvc.Application.Dto;
using CleanArchMvc.Application.Products.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchMvc.Application.Mapping
{
    public class DtoCommandMappingProfile :Profile
    {
        public DtoCommandMappingProfile()
        {
            CreateMap<ProductDto,ProductCreateCommand>();
            CreateMap<ProductDto, ProductUpdateCommand>();
        }
    }
}
