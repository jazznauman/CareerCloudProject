using CareerCloud.Pocos;
using Library.Framework;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CareerCloud.ADODataAccessLayer
{
    public class SystemCountryCodeRepository
        : DataAccessLayer.IDataRepository<SystemCountryCodePoco>
    {
        private int i;
        // string connectionString = ConfigurationManager.ConnectionStrings["DataConnection"].ConnectionString;
        private static readonly Settings app = new Settings();

        public void Add(params SystemCountryCodePoco[] items)
        {
            using (SqlConnection connection = new SqlConnection(app.connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand();
                    command.Connection = connection;
                    command.CommandText = @"INSERT INTO System_Country_Codes(
                     Code, Name  )
              VALUES( @Code, @Name )";
                    connection.Open();
                    foreach (SystemCountryCodePoco item in items)
                    {


                        command.Parameters.AddWithValue("@Code", item.Code);
                        command.Parameters.AddWithValue("@Name", item.Name);


                        command.ExecuteNonQuery();
                    }
                }
                finally { connection.Close(); }
                }
            }
        
        public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
        {
            throw new NotImplementedException();
        }

        public IList<SystemCountryCodePoco> GetAll(params Expression<Func<SystemCountryCodePoco, object>>[] navigationProperties)
        {
            using (SqlConnection connection = new SqlConnection(app.connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand();


                    command.Connection = connection;
                    command.CommandType = CommandType.Text;
                    command.CommandText = @"Select * from System_Country_Codes";
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    IList<SystemCountryCodePoco> ilist = new List<SystemCountryCodePoco>();

                    while (reader.Read())
                    {
                        SystemCountryCodePoco poco = new SystemCountryCodePoco();
                        poco.Code = reader.GetString(0);
                        poco.Name = reader.GetString(1);


                        ilist.Add(poco);
                        
                    }

                    return ilist;
                }
                finally { connection.Close(); }
            }
        }
        public IList<SystemCountryCodePoco> GetList(Expression<Func<SystemCountryCodePoco, bool>> where, params Expression<Func<SystemCountryCodePoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public SystemCountryCodePoco GetSingle(Expression<Func<SystemCountryCodePoco, bool>> where, params Expression<Func<SystemCountryCodePoco, object>>[] navigationProperties)
        {
            IQueryable<SystemCountryCodePoco> pocos = GetAll().AsQueryable();

            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params SystemCountryCodePoco[] items)
        {
            using (SqlConnection connection = new SqlConnection(app.connectionString))
            {
                SqlCommand command = new SqlCommand();
                command.Connection = connection;

                foreach (SystemCountryCodePoco item in items)
                {
                    command.CommandText = @"DELETE FROM System_Country_Codes
                                        WHERE Code = @Code";
                    command.Parameters.AddWithValue("@Code", item.Code);
                    connection.Open();
                   command.ExecuteNonQuery();
                    connection.Close();
                }
            }
            //   throw new NotImplementedException();
        }

        public void Update(params SystemCountryCodePoco[] items)
        {
            using (SqlConnection connection = new SqlConnection(app.connectionString))
            {
                SqlCommand command = new SqlCommand();
                command.Connection = connection;

                foreach (SystemCountryCodePoco item in items)
                {
                    command.CommandText = @"UPDATE System_Country_Codes
                                        SET Name=@Name
                                        WHERE Code = @Code";
                    command.Parameters.AddWithValue("@Code", item.Code);
                    command.Parameters.AddWithValue("@Name", item.Name);
                    
                    connection.Open();
                  command.ExecuteNonQuery();
                    connection.Close();
                }
            }
            //        throw new NotImplementedException();
        }
    }
}
