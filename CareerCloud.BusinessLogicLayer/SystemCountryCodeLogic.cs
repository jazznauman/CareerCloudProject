using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareerCloud.BusinessLogicLayer
{
    public class SystemCountryCodeLogic 

    { 
        private IDataRepository<SystemCountryCodePoco> _repository;
        private readonly List<ValidationException> exceptions;
        public SystemCountryCodeLogic(IDataRepository<SystemCountryCodePoco> repository)
        {
               _repository = repository;
            exceptions = new List<ValidationException>();
        }

        public  void Verify(SystemCountryCodePoco[] pocos)
        {
            
            foreach (var poco in pocos)
            {
                if (string.IsNullOrEmpty(poco.Code))
                {
                    exceptions.Add(new ValidationException(900, $"Code cannot be empty"));
                }
               if(string.IsNullOrEmpty(poco.Name))
                {
                    exceptions.Add(new ValidationException(901, $"Name cannot be empty"));
                }
            }

            if (exceptions.Count > 0)
            {
                throw new AggregateException(exceptions);
            }
        }
        public  void Add(SystemCountryCodePoco[] pocos)
        {
            Verify(pocos);
            Add(pocos);
        }

        public void Update(SystemCountryCodePoco[] pocos)
        {
            Verify(pocos);
            Update(pocos);
        }

        public void Delete(SystemCountryCodePoco[] pocos)
        {
            foreach(var poco in pocos)
            {
                _repository.Remove(poco);
            }
           
        }
        public List<SystemCountryCodePoco> GetAll()
        {
           return _repository.GetAll().ToList();
        }
        public SystemCountryCodePoco Get(string Code)
        {
           return _repository.GetSingle(s=>s.Code == Code);
        }
    }
}

    
