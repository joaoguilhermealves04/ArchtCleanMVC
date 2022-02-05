using CleanArchMvc.Domain.Entities.EntityBase;
using CleanArchMvc.Domain.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchMvc.Domain.Entities
{
    public sealed class Product  : Entity
    {
        private Product(){ }
     
        public string Nome { get; private set; }
        public string Description { get; private set; }
        public decimal Price { get; private set; }
        public int Stock { get; private set; }
        public string Imagem { get; private set; }
        public Product(string nome, string description, decimal preice, int stock, string imagem)
        {
            ValidationDomain(nome, description, preice, stock, imagem);
        }

        public Product(int id, string nome, string description, decimal preice, int stock, string imagem)
        {
            DomainExepetionValidation.When(id < 0, "Invalid Id value");
            Id = id;
            ValidationDomain(nome, description, preice, stock, imagem);
        }

        public void Update(string nome, string description, decimal preice, int stock, string imagem ,int categoryId)
        {
            ValidationDomain(nome, description, preice, stock, imagem);
            CategoryId = categoryId;
        }

        private void ValidationDomain(string nome, string description, decimal preice, int stock, string imagem)
        {
            DomainExepetionValidation.When(string.IsNullOrEmpty(nome),
           "Invalid name.Name Is Requered");

            DomainExepetionValidation.When(nome.Length < 3,
             "Invalid name,too short,minimum 3 charecters");

            DomainExepetionValidation.When(string.IsNullOrEmpty(description),
           "Invalid name.Name Is Requered");

            DomainExepetionValidation.When(description.Length < 5,
             "Invalid description,too short,minimum 3 charecters");

            DomainExepetionValidation.When(preice < 0, "Invalid preice value");

            DomainExepetionValidation.When(stock < 0, "Invalid stock value");

            DomainExepetionValidation.When(imagem?.Length > 250,
             "Invalid name,too short,Maximum 250 charecters");

            Nome = nome;
            Description = description;
            Price = preice;
            Stock = stock;
            Imagem = imagem;
        }

        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
