using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CareerCloud.BusinessLogicLayer
{
    public class SystemLanguageCodeLogic
    {
        private IDataRepository<SystemLanguageCodePoco> _repository;
        private readonly List<ValidationException> exceptions;

        public SystemLanguageCodeLogic(IDataRepository<SystemLanguageCodePoco> repository) 
        {
            _repository = repository;
            exceptions = new List<ValidationException>();
        }

        public void Verify(SystemLanguageCodePoco[] pocos)
        {
           
            foreach (var poco in pocos)
            {
                if (string.IsNullOrEmpty(poco.LanguageID))
                {
                    exceptions.Add(new ValidationException(1000, $"LanguageID cannot be empty"));
                }
                if(string.IsNullOrEmpty(poco.Name))
                {
                    exceptions.Add(new ValidationException(1001, $"Name cannot be empty"));
                }
                if(string.IsNullOrEmpty(poco.NativeName))
                {
                    exceptions.Add(new ValidationException(1002, $"NativeName cannot be empty"));
                  }
            }

            if (exceptions.Count > 0)
            {
                throw new AggregateException(exceptions);
            }
        }
        public  void Add(SystemLanguageCodePoco[] pocos)
        {
            Verify(pocos);
            Add(pocos);
        }

        public void Update(SystemLanguageCodePoco[] pocos)
        {
            Verify(pocos);
            Update(pocos);
        }
        public void Delete(SystemLanguageCodePoco[] pocos)
        {  
             foreach(var poco in pocos)
            {
                _repository.Remove(poco);
            }
        }
        public List<SystemLanguageCodePoco> GetAll()
        {
            return _repository.GetAll().ToList();
        }

        public SystemLanguageCodePoco Get(string LanguageID)
        {
            return _repository.GetSingle(s => s.LanguageID == LanguageID);
        }

        

    }
}
