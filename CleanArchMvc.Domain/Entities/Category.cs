using CleanArchMvc.Domain.Entities.EntityBase;
using CleanArchMvc.Domain.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchMvc.Domain.Entities
{
    public sealed class Category :Entity
    {
        private Category(){}
     
        public string Nome { get; private set; }

        public Category(string nome)
        {
            ValidationDomain(nome);
        }


        public Category(int id, string nome)
        {
            DomainExepetionValidation.When(id < 0, "Invalid Id value");
            Id = id;
            ValidationDomain(nome);
        }

        public ICollection<Product> Products { get; set; }

        public void Update(string nome)
        {
            ValidationDomain(nome);

        }

        private void ValidationDomain(string nome)
        {
            DomainExepetionValidation.When(string.IsNullOrEmpty(nome),
                "Invalid name.Name Is Requered");

            DomainExepetionValidation.When(nome.Length < 3,
                "Invalid name,too short,minimum 3 charecters");

            Nome = nome;
        }
    }
}
