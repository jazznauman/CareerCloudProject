
using CareerCloud.DataAccessLayer;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CareerCloud.EntityFrameworkDataAccess
{
    public class EFGenericRepository<T> : IDataRepository<T> where T : class
    {
        private CareerCloudContext _context;


        public EFGenericRepository() 
        { 
          _context = new CareerCloudContext();

        }
      
     public  IList<T> GetAll(params Expression<Func<T, object>>[] navigationProperties)
        {

            IQueryable<T> query = _context.Set<T>().AsQueryable();
            foreach (var navigationProperty in navigationProperties)
            {
                query= query.Include<T,Object>(navigationProperty);
            }
            return query.ToList<T>();
            
        }
     public IList<T> GetList(Expression<Func<T, bool>> where, params Expression<Func<T, object>>[] navigationProperties)
        {
            
         IQueryable<T> queryList = _context.Set<T>().AsQueryable();
         foreach (var navigationProperty in navigationProperties)
         {
          queryList= queryList.Include<T, Object>(navigationProperty);
          }
         return queryList.Where(where).ToList<T>();
        }
    public  T GetSingle(Expression<Func<T, bool>> where, params Expression<Func<T, object>>[] navigationProperties)
        {
            IQueryable<T> dbQuery = _context.Set<T>();
            foreach (var navigationProperty in navigationProperties)
            {
               dbQuery= dbQuery.Include<T, Object>(navigationProperty);
            }
            return dbQuery.Where(where).SingleOrDefault();

        }

         public void Add(params T[] items)
        {
            foreach (var item in items)
            {
                _context.Entry(item).State = EntityState.Added;

            }
              _context.SaveChanges();
        }
   public  void Update(params T[] items)
        {
            foreach(var item in items)
            {
                _context.Entry(item).State = EntityState.Modified;
            }
            _context.SaveChanges();
        }
        public void Remove(params T[] items)
        {
            foreach (var item in items)
            {
                _context.Entry(item).State = EntityState.Deleted;
            }
                _context.SaveChanges();
            
        }
    public void CallStoredProc(string name, params Tuple<string, string>[] parameters) 
        { 
           
        
        }


    }
}
