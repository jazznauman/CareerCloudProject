using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using Library.Framework;

namespace CareerCloud.ADODataAccessLayer
{
    public class CompanyJobRepository
        : DataAccessLayer.IDataRepository<CompanyJobPoco>
    {
        private int i;
        //    string connectionString = ConfigurationManager.ConnectionStrings["DataConnection"].ConnectionString;

        private static readonly Settings app = new Settings();
        public void Add(params CompanyJobPoco[] items)
        {

            using (SqlConnection connection = new SqlConnection(app.connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand();
                    connection.Open();
                    command.Connection = connection;
                    command.CommandType = CommandType.Text;
                    command.CommandText = @"INSERT INTO Company_Jobs
       ([Id],[Company],[Profile_Created],[Is_Inactive],[Is_Company_Hidden])
VALUES (@Id, @Company, @Profile_Created, @Is_Inactive,@Is_Company_Hidden)";

                    foreach (CompanyJobPoco item in items)
                    {


                        command.Parameters.AddWithValue("@Id", item.Id);
                        command.Parameters.AddWithValue("@Company", item.Company);
                        command.Parameters.AddWithValue("@Profile_Created", item.ProfileCreated);
                        command.Parameters.AddWithValue("@Is_Inactive", item.IsInactive);

                        command.Parameters.AddWithValue("@Is_Company_Hidden", item.IsCompanyHidden);

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

        public IList<CompanyJobPoco> GetAll(params Expression<Func<CompanyJobPoco, object>>[] navigationProperties)
        {
            using (SqlConnection connection = new SqlConnection(app.connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand();

                    command.Connection = connection;
                    command.CommandType = CommandType.Text;
                    command.CommandText = @"Select * from Company_Jobs";
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    IList<CompanyJobPoco> ilist = new List<CompanyJobPoco>();

                    while (reader.Read())
                    {
                        CompanyJobPoco poco = new CompanyJobPoco();
                        poco.Id = reader.GetGuid(0);
                        poco.Company = reader.GetGuid(1);
                        poco.ProfileCreated = reader.GetDateTime(2);
                        poco.IsInactive = reader.GetBoolean(3);
                        poco.IsCompanyHidden = reader.GetBoolean(4);


                        poco.TimeStamp = (byte[])reader[5];
                        ilist.Add(poco);
                        
                    }

                    return ilist;

                }
                finally { connection.Close(); }
                // throw new NotImplementedException();
            }
        }

        public IList<CompanyJobPoco> GetList(Expression<Func<CompanyJobPoco, bool>> where, params Expression<Func<CompanyJobPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public CompanyJobPoco GetSingle(Expression<Func<CompanyJobPoco, bool>> where, params Expression<Func<CompanyJobPoco, object>>[] navigationProperties)
        {

            IQueryable<CompanyJobPoco> pocos = GetAll().AsQueryable();

            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params CompanyJobPoco[] items)
        {
            using (SqlConnection connection = new SqlConnection(app.connectionString))
            {
                SqlCommand command = new SqlCommand();
                command.Connection = connection;

                foreach (CompanyJobPoco item in items)
                {
                    command.CommandText = @"DELETE FROM Company_Jobs
                                        WHERE Id = @Id";
                    command.Parameters.AddWithValue("@Id", item.Id);
                    connection.Open();
                  command.ExecuteNonQuery();
                    connection.Close();
                }
            }


        }

        public void Update(params CompanyJobPoco[] items)
        {

            using (SqlConnection connection = new SqlConnection(app.connectionString))
            {
                SqlCommand command = new SqlCommand();
                command.Connection = connection;

                foreach (CompanyJobPoco item in items)
                {
                    command.CommandText = @"UPDATE Company_Jobs
                                         SET Company=@Company,Profile_Created=@Profile_Created, 
                           [Is_Inactive]=@Is_Inactive, Is_Company_Hidden=@Is_Company_Hidden

                         
                         WHERE Id = @Id";
                    command.Parameters.AddWithValue("@Id", item.Id);
                    command.Parameters.AddWithValue("@Company", item.Company);
                    command.Parameters.AddWithValue("@Profile_Created", item.ProfileCreated);
                    command.Parameters.AddWithValue("@Is_Inactive", item.IsInactive);
                    command.Parameters.AddWithValue("@Is_Company_Hidden", item.IsCompanyHidden);
                    
                    connection.Open();
                  command.ExecuteNonQuery();
                    connection.Close();
                }
            }
        }
    }
}
