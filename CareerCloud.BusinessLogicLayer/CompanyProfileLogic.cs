using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CareerCloud.BusinessLogicLayer
{
    public class CompanyProfileLogic : BaseLogic<CompanyProfilePoco>
    {
        private readonly List<ValidationException> exceptions;
        public CompanyProfileLogic(IDataRepository<CompanyProfilePoco> repository) : base(repository)
        {
            exceptions = new List<ValidationException>();
        
        }

        protected override void Verify(CompanyProfilePoco[] pocos)
        {
            
            foreach (var poco in pocos)
            {
                string[] phoneParts = poco.ContactPhone?.Split('-');
                if (phoneParts?.Length != 3)
                {
                    exceptions.Add(new ValidationException(601, $"Contact phone must correspond to a valid phone number (e.g. 416-555-1234)"));
                }
                else
                {
                    if (phoneParts[0]?.Length != 3)
                    {
                        exceptions.Add(new ValidationException(601, $"Contact phone must correspond to a valid phone number (e.g. 416-555-1234)"));
                    }
                    else if (phoneParts[1]?.Length != 3)
                    {
                        exceptions.Add(new ValidationException(601, $"Contact phone must correspond to a valid phone number (e.g. 416-555-1234)"));
                    }
                    else if (phoneParts[2]?.Length != 4)
                    {
                        exceptions.Add(new ValidationException(601, $"Contact phone must correspond to a valid phone number (e.g. 416-555-1234)"));
                    }
                }

                string[] website = poco.CompanyWebsite?.Split('.');
                if (website?.Length != 3)
                {
                    exceptions.Add(new ValidationException(600, $"Valid websites must end with the following extensions – \".ca\", \".com\", \".biz\" "));
                }
                else if (website[0]?.Length != 3)
                {
                    exceptions.Add(new ValidationException(600, $"Valid websites must end with the following extensions- \".ca\", \".com\",\".biz\" "));

                }
                else if (website[1]?.Length != 3)
                {
                    exceptions.Add(new ValidationException(600, $"Valid websites must end with the following extensions – \".ca\", \".com\", \".biz\" "));
                }

                else if (website[2]?.Length > 3 || website[2]?.Length < 2)
                {
                    exceptions.Add(new ValidationException(600, $"Valid websites must end with the following extensions – \".ca\", \".com\", \".biz\""));
                }
            }




            if (exceptions.Count > 0)
            {
                throw new AggregateException(exceptions);
            }
        }

public override void Add(CompanyProfilePoco[] pocos)
{
    Verify(pocos);
    base.Add(pocos);
}

       public override void Update(CompanyProfilePoco[] pocos)
    {
    Verify(pocos);
    base.Update(pocos);
      }
  }
}