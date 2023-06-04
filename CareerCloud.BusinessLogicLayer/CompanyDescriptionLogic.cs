using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareerCloud.BusinessLogicLayer
{
    public class CompanyDescriptionLogic : BaseLogic<CompanyDescriptionPoco>
    {
        private readonly List<ValidationException> exceptions;
        public CompanyDescriptionLogic(IDataRepository<CompanyDescriptionPoco> repository) : base(repository)
        { 
            exceptions = new List<ValidationException>();
        }

        protected override void Verify(CompanyDescriptionPoco[] pocos)
        {
            
            foreach (var poco in pocos)
            {
                if (poco.CompanyDescription?.Length <= 2)
                {
                    exceptions.Add( new ValidationException(107, $"Company Description must be greater than 2 characters."));
                }

                if(poco.CompanyName?.Length<= 2)
                {
                    exceptions.Add(new ValidationException(106, $"Company Name must be greater than 2 characters."));
                }
            }
            if (exceptions.Count > 0)
            {
                throw new AggregateException(exceptions);
            }
        }

        public override void Add(CompanyDescriptionPoco[] pocos)
        {
            Verify(pocos);
            base.Add(pocos);
        }

        public override void Update(CompanyDescriptionPoco[] pocos)
        {
            Verify(pocos);
            base.Update(pocos);
        }
    }
}
