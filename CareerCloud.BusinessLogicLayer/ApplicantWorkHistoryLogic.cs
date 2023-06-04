using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareerCloud.BusinessLogicLayer
{
    public class ApplicantWorkHistoryLogic : BaseLogic<ApplicantWorkHistoryPoco>
    {
        private readonly List<ValidationException> exceptions;
        public ApplicantWorkHistoryLogic(IDataRepository<ApplicantWorkHistoryPoco> repository) : base(repository)
        { 
            exceptions = new List<ValidationException>(); 
        }


        protected override void Verify(ApplicantWorkHistoryPoco[] pocos)
        {
            
            foreach (var poco in pocos)
            {
                if (poco.CompanyName.Length <= 2)
                {
                    exceptions.Add(new ValidationException(105, $"Company Name for Applicant Work History must be greater than 2 characters."));
                }
            }
            if (exceptions.Count > 0)
            {
                throw new AggregateException(exceptions);
            }
        }

        public override void Add(ApplicantWorkHistoryPoco[] pocos)
        {
            Verify(pocos);
            base.Add(pocos);
        }

        public override void Update(ApplicantWorkHistoryPoco[] pocos)
        {
            Verify(pocos);
            base.Update(pocos);
        }
    }
}
