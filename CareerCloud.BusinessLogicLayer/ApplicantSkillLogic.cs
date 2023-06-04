using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareerCloud.BusinessLogicLayer
{
    public class ApplicantSkillLogic : BaseLogic<ApplicantSkillPoco>
    {
        private readonly List<ValidationException> exceptions;
        public ApplicantSkillLogic(IDataRepository<ApplicantSkillPoco> repository) : base(repository)
        {
            exceptions = new List<ValidationException>();
        }

        protected override void Verify(ApplicantSkillPoco[] pocos)
        {
             
            foreach (var poco in pocos)
            {
              if(poco.StartMonth > 12)
                {
                    exceptions.Add(new ValidationException(101, $"Start Month cannot be greater than 12"));
                }
                if (poco.EndMonth > 12)
                {
                    exceptions.Add(new ValidationException(102, $"End Month cannot be greater than 12"));
                }
                if (poco.StartYear < 1900)
                {
                    exceptions.Add(new ValidationException(103, $"Start Year cannot be less than 1900"));
                }
                if (poco.EndYear < poco.StartYear)
                {
                    exceptions.Add(new ValidationException(104, $"End Year cannot be less than StartYear"));
                }
                if (exceptions.Count > 0)
                {
                    throw new AggregateException(exceptions);
                }
            }
        }

        public override void Add(ApplicantSkillPoco[] pocos)
        {
            Verify(pocos);
            base.Add(pocos);
        }

        public override void Update(ApplicantSkillPoco[] pocos)
        {
            Verify(pocos);
            base.Update(pocos);
        }
    }
}
