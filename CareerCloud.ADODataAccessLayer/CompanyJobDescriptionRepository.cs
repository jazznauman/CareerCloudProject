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
    public class CompanyJobDescriptionRepository
        : DataAccessLayer.IDataRepository<CompanyJobDescriptionPoco>
    {
        private int i;
        //  string connectionString = ConfigurationManager.ConnectionStrings["DataConnection"].ConnectionString;

        private static readonly Settings app = new Settings();
        
        public void Add(params CompanyJobDescriptionPoco[] items)
        {
            using (SqlConnection connection = new SqlConnection(app.connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand();
                    command.Connection = connection;
                    command.CommandText = @"INSERT INTO Company_Jobs_Descriptions([Id]" +
                          " ,[Job] ,[Job_Name] ,[Job_Descriptions]) " +
                       "  Values(@Id, @Job, @JobName,@JobDescriptions)";
                    connection.Open();
                    foreach (CompanyJobDescriptionPoco item in items)
                    {

                        command.Parameters.AddWithValue("@Id", item.Id);
                        command.Parameters.AddWithValue("@Job", item.Job);
                        command.Parameters.AddWithValue("@JobName", item.JobName);

                        command.Parameters.AddWithValue("@JobDescriptions", item.JobDescriptions);
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

        public IList<CompanyJobDescriptionPoco> GetAll(params Expression<Func<CompanyJobDescriptionPoco, object>>[] navigationProperties)
        {

            using (SqlConnection connection = new SqlConnection(app.connectionString))
            {


                try
                {
                    SqlCommand command = new SqlCommand();


                    command.Connection = connection;
                    command.CommandType = CommandType.Text;
                    command.CommandText = @"Select * from Company_Jobs_Descriptions";
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    IList<CompanyJobDescriptionPoco> ilist = new List<CompanyJobDescriptionPoco>();

                    while (reader.Read())
                    {
                        CompanyJobDescriptionPoco poco = new CompanyJobDescriptionPoco();
                        poco.Id = reader.GetGuid(0);
                        poco.Job = reader.GetGuid(1);

                        poco.JobName = reader.GetString(2);
                        poco.JobDescriptions = reader.GetString(3);




                        poco.TimeStamp = (byte[])reader[4];
                        ilist.Add(poco);
                        
                    }

                    return ilist;
                }
                finally { connection.Close(); }
            }
        }

        public IList<CompanyJobDescriptionPoco> GetList(Expression<Func<CompanyJobDescriptionPoco, bool>> where, params Expression<Func<CompanyJobDescriptionPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public CompanyJobDescriptionPoco GetSingle(Expression<Func<CompanyJobDescriptionPoco, bool>> where, params Expression<Func<CompanyJobDescriptionPoco, object>>[] navigationProperties)
        {

            IQueryable<CompanyJobDescriptionPoco> pocos = GetAll().AsQueryable();

            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params CompanyJobDescriptionPoco[] items)
        {

            using (SqlConnection connection = new SqlConnection(app.connectionString))
            {
                SqlCommand command = new SqlCommand();
                command.Connection = connection;

                foreach (CompanyJobDescriptionPoco item in items)
                {
                    command.CommandText = @"DELETE FROM Company_Jobs_Descriptions
                                        WHERE Id = @Id";
                    command.Parameters.AddWithValue("@Id", item.Id);
                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
            //      throw new NotImplementedException();
        }

        public void Update(params CompanyJobDescriptionPoco[] items)
        {

            using (SqlConnection connection = new SqlConnection(app.connectionString))
            {
                SqlCommand command = new SqlCommand();
                command.Connection = connection;

                foreach (CompanyJobDescriptionPoco item in items)
                {
                    command.CommandText = @"UPDATE Company_Jobs_Descriptions

           SET [Job]=@Job
      ,[Job_Name]=@JobName
      ,[Job_Descriptions]=@JobDescriptions
      
                              WHERE Id = @Id";
                    command.Parameters.AddWithValue("@Id", item.Id);
                    command.Parameters.AddWithValue("@Job", item.Job);
                    command.Parameters.AddWithValue("@JobName", item.JobName);

                    command.Parameters.AddWithValue("@JobDescriptions", item.JobDescriptions);
                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
                //    throw new NotImplementedException();
            }
        }
    }
}
