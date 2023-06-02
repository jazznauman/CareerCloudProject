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
    public class CompanyJobEducationRepository
        : DataAccessLayer.IDataRepository<CompanyJobEducationPoco>
    {
        private int i;
        //    string connectionString = ConfigurationManager.ConnectionStrings["DataConnection"].ConnectionString;
        private static readonly Settings app = new Settings();

        public void Add(params CompanyJobEducationPoco[] items)
        {
            using (SqlConnection connection = new SqlConnection(app.connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand();
                    command.Connection = connection;
                    command.CommandText = @"INSERT INTO Company_Job_Educations(
                    Id ,Job ,Major,Importance)
              VALUES( @Id, @Job, @Major, @Importance )";
                    connection.Open();
                    foreach (CompanyJobEducationPoco item in items)
                    {

                        command.Parameters.AddWithValue("@Id", item.Id);
                        command.Parameters.AddWithValue("@Job", item.Job);
                        command.Parameters.AddWithValue("@Major", item.Major);
                        command.Parameters.AddWithValue("@Importance", item.Importance);


                        command.ExecuteNonQuery();
                    }
                }
                finally
                {
                    connection.Close();
                }
                }
            }

       

        public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
        {
            throw new NotImplementedException();
        }

        public IList<CompanyJobEducationPoco> GetAll(params Expression<Func<CompanyJobEducationPoco, object>>[] navigationProperties)
        {
            using (SqlConnection connection = new SqlConnection(app.connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand();


                    command.Connection = connection;
                    command.CommandType = CommandType.Text;
                    command.CommandText = @"Select * from Company_Job_Educations";
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    IList<CompanyJobEducationPoco> ilist = new List<CompanyJobEducationPoco>();

                    while (reader.Read())
                    {
                        CompanyJobEducationPoco poco = new CompanyJobEducationPoco();
                        poco.Id = reader.GetGuid(0);

                        poco.Job = reader.GetGuid(1);


                        poco.Major = reader.GetString(2);
                        poco.Importance = reader.GetInt16(3);
                        poco.TimeStamp = (byte[])reader[4];
                        ilist.Add(poco);
                        
                    }

                    return ilist;
                }
                finally { connection.Close(); }
                
            }
        }

        public IList<CompanyJobEducationPoco> GetList(Expression<Func<CompanyJobEducationPoco, bool>> where, params Expression<Func<CompanyJobEducationPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public CompanyJobEducationPoco GetSingle(Expression<Func<CompanyJobEducationPoco, bool>> where, params Expression<Func<CompanyJobEducationPoco, object>>[] navigationProperties)
        {
            IQueryable<CompanyJobEducationPoco> pocos = GetAll().AsQueryable();

            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params CompanyJobEducationPoco[] items)
        {

            using (SqlConnection connection = new SqlConnection(app.connectionString))
            {
                SqlCommand command = new SqlCommand();
                command.Connection = connection;

                foreach (CompanyJobEducationPoco item in items)
                {
                    command.CommandText = @"DELETE FROM Company_Job_Educations
                                        WHERE Id = @Id";
                    command.Parameters.AddWithValue("@Id", item.Id);
                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
         //   throw new NotImplementedException();
        }

        public void Update(params CompanyJobEducationPoco[] items)
        {
            using (SqlConnection connection = new SqlConnection(app.connectionString))
            {
                SqlCommand command = new SqlCommand();
                command.Connection = connection;

                foreach (CompanyJobEducationPoco item in items)
                {
                    command.CommandText = @"UPDATE Company_Job_Educations
                                        SET Job=@Job, Major= @Major, Importance = @Importance
                                        WHERE Id = @Id";
                    command.Parameters.AddWithValue("@Id", item.Id);
                    command.Parameters.AddWithValue("@Job", item.Job);
                    command.Parameters.AddWithValue("@Major", item.Major);
                    command.Parameters.AddWithValue("@Importance", item.Importance);
                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
    //        throw new NotImplementedException();
        }
    }
}
