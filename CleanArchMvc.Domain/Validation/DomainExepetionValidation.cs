using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchMvc.Domain.Validation
{
    public class DomainExepetionValidation : Exception
    {
        public DomainExepetionValidation(string erro) : base(erro)
        { }

        public static void When(bool hasError, string erro)
        {
            if (hasError)
                throw new DomainExepetionValidation(erro);
        }
    }
}
