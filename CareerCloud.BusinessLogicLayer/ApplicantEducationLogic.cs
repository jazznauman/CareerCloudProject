﻿using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CareerCloud.BusinessLogicLayer
{
    public class ApplicantEducationLogic : BaseLogic<ApplicantEducationPoco>
    {
        private readonly List<ValidationException> exceptions;
        public ApplicantEducationLogic(IDataRepository<ApplicantEducationPoco> repository) : base(repository)
        {
            exceptions = new List<ValidationException>();
        }

        public override void Add(ApplicantEducationPoco[] pocos)
        {
            Verify(pocos);
            base.Add(pocos);
        }

        public override void Update(ApplicantEducationPoco[] pocos)
        {
            Verify(pocos);
            base.Update(pocos);
        }

        protected override void Verify(ApplicantEducationPoco[] pocos)
        {
           
            

            foreach (ApplicantEducationPoco poco in pocos)
            {
                if (string.IsNullOrEmpty(poco.Major))
                {
                    exceptions.Add(new ValidationException(107, $"Major for ApplicantEducation {poco.Major} cannot be null"));
                }
                else if (poco.Major.Length < 3)
                {
                    exceptions.Add(new ValidationException(107, $"Major for ApplicantEducation {poco.Major} cannot be less than 3 characters."));
                }
               

                if (poco.StartDate > DateTime.Today)
                {
                    exceptions.Add(new ValidationException(108, $"StartDate for ApplicantEducation {poco.StartDate} cannot be greater than today"));
                }
                
              if( poco.CompletionDate < poco.StartDate)
                {
                    exceptions.Add(new ValidationException(109, $"CompletionDate for ApplicantEducation {poco.CompletionDate} cannot be earlier than start date"));
                }
                            

               
            }

            if (exceptions.Count > 0)
            {
                throw new AggregateException(exceptions);
            }
        }

    }

}
