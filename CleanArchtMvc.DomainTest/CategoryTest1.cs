using CleanArchMvc.Domain.Entities;
using FluentAssertions;
using System;
using Xunit;

namespace CleanArchtMvc.DomainTest
{
    public class CategoryTest1
    {
        [Fact(DisplayName = "Create Category With Valid state")]
        public void CreateCategory_WithValidParameters_ResultObjctValidstate()
        {
            Action action = () => new Category(1, "Nome Category");
            action.Should()
                .NotThrow<CleanArchMvc.Domain.Validation.DomainExepetionValidation>();
        }

        [Fact(DisplayName = "Id Invalid")]
        public void IdInvalid()
        {
            Action action = () => new Category(-1, "Id nao Permitido");
            action.Should()
                .Throw<CleanArchMvc.Domain.Validation.DomainExepetionValidation>()
                .WithMessage("Invalid Id value");
        }

        [Fact(DisplayName = "Requerer")]
        public void NomeInvalid()
        {
            Action action = () => new Category(1, "");
            action.Should()
                .Throw<CleanArchMvc.Domain.Validation.DomainExepetionValidation>()
                .WithMessage("Invalid name.Name Is Requered");
        }

        [Fact(DisplayName = "Nome Minimun")]
        public void NameMinimumInvalid()
        {
            Action action = () => new Category(1, "Id");
            action.Should()
                .Throw<CleanArchMvc.Domain.Validation.DomainExepetionValidation>()
                .WithMessage("Invalid name,too short,minimum 3 charecters");
        }
    }
}
