using CleanArchMvc.Domain.Entities;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CleanArchtMvc.DomainTest
{
    public class ProductTest1
    {
        [Fact]
        public void CreateProduct_WithValidParameters_ResultObjectValidState()
        {
            Action action = () => new Product(1, "Product Nome", "Product Description", 9.99m, 99,
                "Imagem Produto");
            action.Should()
                .NotThrow<CleanArchMvc.Domain.Validation.DomainExepetionValidation>();

        }

        [Fact]
        public void CreateProduct_NValidParameters_ResultObjectValidId()
        {
            Action action = () => new Product(-1, "Product Nome", "Product Description", 9.99m, 99,
                "Imagem Produto");
            action.Should()
                .Throw<CleanArchMvc.Domain.Validation.DomainExepetionValidation>()
                .WithMessage("Invalid Id value");

        }

        [Fact]
        public void CreateProduct_NValidParameters_ResultObjectValidNome()
        {
            Action action = () => new Product(1, "e", "Product Description", 9.99m, 99,
                "Imagem Produto");
            action.Should()
                .Throw<CleanArchMvc.Domain.Validation.DomainExepetionValidation>()
                .WithMessage("Invalid name,too short,minimum 3 charecters");

        }

        [Fact]
        public void CreateProduct_NValidParameters_ResultObjectValidImagem()
        {
            Action action = () => new Product(1, "Produto Nome", "Product Description", 9.99m, 99,
                "Imagem Produto LOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOO GOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOO" +
                "0000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000");
            action.Should()
                .Throw<CleanArchMvc.Domain.Validation.DomainExepetionValidation>()
                .WithMessage("Invalid name,too short,Maximum 250 charecters");

        }

        [Fact]
        public void CreateProduct_NullValidParameters_ResultObjectValidImagem()
        {
            Action action = () => new Product(1, "Produto Nome", "Product Description", 9.99m, 99,
                null);
            action.Should()
                .NotThrow<NullReferenceException>();
                

        }

        [Fact]
        public void CreateProduct_NullValidParameters_ResultObjectValidNullImagem()
        {
            Action action = () => new Product(1, "Produto Nome", "Product Description", 9.99m, 99,
                null);
            action.Should()
                .NotThrow<CleanArchMvc.Domain.Validation.DomainExepetionValidation>();


        }

        [Theory]
        [InlineData(-5)]
        public void stockInvalid (int velue)
        {
            Action action = () => new Product(1, "Produto Nome", "Product Description", 9.99m,velue,
                null);
            action.Should()
                .Throw<CleanArchMvc.Domain.Validation.DomainExepetionValidation>()
                .WithMessage("Invalid stock value");


        }

    }
}
