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
    public class SecurityLoginsLogRepository
        : DataAccessLayer.IDataRepository<SecurityLoginsLogPoco>
    {
        private int i;
        //    string connectionString = ConfigurationManager.ConnectionStrings["DataConnection"].ConnectionString;
        private static readonly Settings app = new Settings();


        public void Add(params SecurityLoginsLogPoco[] items)
        {
            using (SqlConnection connection = new SqlConnection(app.connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand();
                    command.Connection = connection;
                    command.CommandText = @"INSERT INTO Security_Logins_Log ( [Id] ,[Login] ,[Source_IP] ,[Logon_Date] ,[Is_Succesful]  )
              VALUES( @Id, @Login, @Source_IP,@Logon_Date, @Is_Successful )";
                    connection.Open();

                    foreach (SecurityLoginsLogPoco item in items)
                    {


                        command.Parameters.AddWithValue("@Id", item.Id);
                        command.Parameters.AddWithValue("@Login", item.Login);
                        command.Parameters.AddWithValue("@Source_IP", item.SourceIP);
                        command.Parameters.AddWithValue("@Logon_Date", item.LogonDate);
                        command.Parameters.AddWithValue("@Is_Successful", item.IsSuccesful);


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

        public IList<SecurityLoginsLogPoco> GetAll(params Expression<Func<SecurityLoginsLogPoco, object>>[] navigationProperties)
        {

            using (SqlConnection connection = new SqlConnection(app.connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand();


                    command.Connection = connection;
                    command.CommandType = CommandType.Text;
                    command.CommandText = @"Select * from Security_Logins_Log";
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    IList<SecurityLoginsLogPoco> ilist = new List<SecurityLoginsLogPoco>();

                    while (reader.Read())
                    {
                        SecurityLoginsLogPoco poco = new SecurityLoginsLogPoco();
                        poco.Id = reader.GetGuid(0);

                        poco.Login = reader.GetGuid(1);

                        poco.SourceIP = reader.GetString(2);
                        poco.LogonDate = reader.GetDateTime(3);
                        poco.IsSuccesful = reader.GetBoolean(4);

                        ilist.Add(poco);
                       
                    }

                    return ilist;
                }
                finally { connection.Close(); }
            }
        }

        public IList<SecurityLoginsLogPoco> GetList(Expression<Func<SecurityLoginsLogPoco, bool>> where, params Expression<Func<SecurityLoginsLogPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public SecurityLoginsLogPoco GetSingle(Expression<Func<SecurityLoginsLogPoco, bool>> where, params Expression<Func<SecurityLoginsLogPoco, object>>[] navigationProperties)
        {
            IQueryable<SecurityLoginsLogPoco> pocos = GetAll().AsQueryable();

            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params SecurityLoginsLogPoco[] items)
        {
            using (SqlConnection connection = new SqlConnection(app.connectionString))
            {
                SqlCommand command = new SqlCommand();
                command.Connection = connection;

                foreach (SecurityLoginsLogPoco item in items)
                {
                    command.CommandText = @"DELETE FROM Security_Logins_Log
                                        WHERE Id = @Id";
                    command.Parameters.AddWithValue("@Id", item.Id);
                    connection.Open();
                     command.ExecuteNonQuery();
                    connection.Close();
                }
            }
            //   throw new NotImplementedException();
        }

        public void Update(params SecurityLoginsLogPoco[] items)
        {

            using (SqlConnection connection = new SqlConnection(app.connectionString))
            {
                SqlCommand command = new SqlCommand();
                command.Connection = connection;

                foreach (SecurityLoginsLogPoco item in items)
                {
                    command.CommandText = @"UPDATE Security_Logins_Log

                               
    
    
                               SET Login=@Login, Source_IP = @SourceIP,Logon_Date=@LogonDate,
                                          Is_Succesful=@Is_Succesful
                                        WHERE Id = @Id";
                    command.Parameters.AddWithValue("@Id", item.Id);
                    command.Parameters.AddWithValue("@Login", item.Login);
                    command.Parameters.AddWithValue("@SourceIP", item.SourceIP);
                    command.Parameters.AddWithValue("@LogonDate", item.LogonDate);
                    command.Parameters.AddWithValue("@Is_Succesful", item.IsSuccesful);
                    connection.Open();
                  command.ExecuteNonQuery();
                    connection.Close();
                }
            }
            //        throw new NotImplementedException();
        }
    }
}
