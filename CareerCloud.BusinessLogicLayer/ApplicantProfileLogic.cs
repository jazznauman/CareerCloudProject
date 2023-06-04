﻿using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareerCloud.BusinessLogicLayer
{
    public class ApplicantProfileLogic: BaseLogic <ApplicantProfilePoco>
    {
        private readonly List<ValidationException> exceptions;
        public ApplicantProfileLogic(IDataRepository<ApplicantProfilePoco> repository) : base(repository)
        {
             exceptions = new List<ValidationException>();
        }
        protected override void Verify(ApplicantProfilePoco[] pocos)
        {
            
            foreach (var poco in pocos)
            {
                if(poco.CurrentSalary < 0)
                {
                    exceptions.Add(new ValidationException(111, $"Current Salary cannot be negative"));
                }
                if(poco.CurrentRate < 0)
                {
                    exceptions.Add(new ValidationException(112, $"Current Rate cannot be negative"));
                }
            }

            if (exceptions.Count > 0)
            {
                throw new AggregateException(exceptions);
            }
        }
        public override void Add(ApplicantProfilePoco[] pocos)
        {
            Verify(pocos);
            base.Add(pocos);
        }

        public override void Update(ApplicantProfilePoco[] pocos)
        {
            Verify(pocos);
            base.Update(pocos);
        }


    }
}
