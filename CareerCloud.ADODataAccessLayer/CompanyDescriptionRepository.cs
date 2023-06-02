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
    public class CompanyDescriptionRepository
        : DataAccessLayer.IDataRepository<CompanyDescriptionPoco>
    {
        private int i;
        //    string connectionString = ConfigurationManager.ConnectionStrings["DataConnection"].ConnectionString;
        private static readonly Settings app = new Settings();

        public void Add(params CompanyDescriptionPoco[] items)
        {

            using (SqlConnection connection = new SqlConnection(app.connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand();
                    command.Connection = connection;
                    command.CommandText = @"INSERT INTO Company_Descriptions( [Id],[Company] ,[LanguageID] ,[Company_Name] ,[Company_Description] )
                    VALUES(  @Id, @Company,@LanguageID,@Company_Name, @Company_Description )";
                    connection.Open();

                    foreach (CompanyDescriptionPoco item in items)
                    {

                        command.Parameters.AddWithValue("@Id", item.Id);
                        command.Parameters.AddWithValue("@Company", item.Company);
                        command.Parameters.AddWithValue("@LanguageID", item.LanguageId);
                        command.Parameters.AddWithValue("@Company_Name", item.CompanyName);

                        command.Parameters.AddWithValue("@Company_Description", item.CompanyDescription);

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

        public IList<CompanyDescriptionPoco> GetAll(params Expression<Func<CompanyDescriptionPoco, object>>[] navigationProperties)
        {

            using (SqlConnection connection = new SqlConnection(app.connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand();


                    command.Connection = connection;
                    command.CommandType = CommandType.Text;
                    command.CommandText = @"Select * from Company_Descriptions";
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    IList<CompanyDescriptionPoco> ilist = new List<CompanyDescriptionPoco>();

                    while (reader.Read())
                    {
                        CompanyDescriptionPoco poco = new CompanyDescriptionPoco();

                        poco.Id = reader.GetGuid(0);
                        poco.Company = reader.GetGuid(1);
                        poco.LanguageId = reader.GetString(2);
                        poco.CompanyName = reader.GetString(3);
                        poco.CompanyDescription = reader.GetString(4);



                        poco.TimeStamp = (byte[])reader[5];
                        ilist.Add(poco);
                        
                    }

                    return ilist;
                }


                finally { connection.Close(); }
            }
        }
        public IList<CompanyDescriptionPoco> GetList(Expression<Func<CompanyDescriptionPoco, bool>> where, params Expression<Func<CompanyDescriptionPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public CompanyDescriptionPoco GetSingle(Expression<Func<CompanyDescriptionPoco, bool>> where, params Expression<Func<CompanyDescriptionPoco, object>>[] navigationProperties)
        {
            IQueryable<CompanyDescriptionPoco> pocos = GetAll().AsQueryable();

            return pocos.Where(where).FirstOrDefault();
            throw new NotImplementedException();
        }

        public void Remove(params CompanyDescriptionPoco[] items)
        {
            using (SqlConnection connection = new SqlConnection(app.connectionString))
            {
                SqlCommand command = new SqlCommand();
                command.Connection = connection;

                foreach (CompanyDescriptionPoco item in items)
                {
                    command.CommandText = @"DELETE FROM Company_Descriptions
                                        WHERE Id = @Id";
                    command.Parameters.AddWithValue("@Id", item.Id);
                    connection.Open();
                     command.ExecuteNonQuery();
                    connection.Close();
                }
            }
        }

        public void Update(params CompanyDescriptionPoco[] items)
        {

            using (SqlConnection connection = new SqlConnection(app.connectionString))
            {
                SqlCommand command = new SqlCommand();
                command.Connection = connection;

                foreach (CompanyDescriptionPoco item in items)
                {
                    command.CommandText = @"UPDATE Company_Descriptions

                                     SET Company= @Company
                                      ,LanguageID= @LanguageID
                                      ,Company_Name=@CompanyName
                               ,Company_Description= @CompanyDescription
                                     WHERE Id = @Id";

                    command.Parameters.AddWithValue("@Id", item.Id);
                    command.Parameters.AddWithValue("@Company", item.Company);
                    command.Parameters.AddWithValue("@LanguageID", item.LanguageId);
                    command.Parameters.AddWithValue("@CompanyName", item.CompanyName);
                    command.Parameters.AddWithValue("@CompanyDescription", item.CompanyDescription);

                    connection.Open();
                     command.ExecuteNonQuery();
                    connection.Close();
                }
            //    throw new NotImplementedException();
            }
        }
    }
}
