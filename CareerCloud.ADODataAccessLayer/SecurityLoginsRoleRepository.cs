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
    public class SecurityLoginsRoleRepository
        : DataAccessLayer.IDataRepository<SecurityLoginsRolePoco>
    {
        private int i;
        //   string connectionString = ConfigurationManager.ConnectionStrings["DataConnection"].ConnectionString;
        private static readonly Settings app = new Settings();

        public void Add(params SecurityLoginsRolePoco[] items)
        {
            using (SqlConnection connection = new SqlConnection(app.connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand();
                    command.Connection = connection;
                    connection.Open();
                    command.CommandText = @"INSERT INTO Security_Logins_Roles(
                     [Id]  ,[Login],[Role]  )
              VALUES( @Id, @Login, @Role )";
                    foreach (SecurityLoginsRolePoco item in items)
                    {


                        command.Parameters.AddWithValue("@Id", item.Id);
                        command.Parameters.AddWithValue("@Login", item.Login);
                        command.Parameters.AddWithValue("@Role", item.Role);



                        command.ExecuteNonQuery();
                    }
                }
                finally { 
                    connection.Close();
                }
            }
        }

        public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
        {
            throw new NotImplementedException();
        }

        public IList<SecurityLoginsRolePoco> GetAll(params Expression<Func<SecurityLoginsRolePoco, object>>[] navigationProperties)
        {
            using (SqlConnection connection = new SqlConnection(app.connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand();


                    command.Connection = connection;
                    command.CommandType = CommandType.Text;
                    command.CommandText = @"Select * from Security_Logins_Roles";
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    IList<SecurityLoginsRolePoco> ilist = new List<SecurityLoginsRolePoco>();

                    while (reader.Read())
                    {
                        SecurityLoginsRolePoco poco = new SecurityLoginsRolePoco();
                        poco.Id = reader.GetGuid(0);


                        poco.Login = reader.GetGuid(1);
                        poco.Role = reader.GetGuid(2);

                        poco.TimeStamp = (byte[])reader[3];
                        ilist.Add(poco);
                        
                    }

                    return ilist;
                }
                finally { connection.Close(); }
            }
        }

        public IList<SecurityLoginsRolePoco> GetList(Expression<Func<SecurityLoginsRolePoco, bool>> where, params Expression<Func<SecurityLoginsRolePoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public SecurityLoginsRolePoco GetSingle(Expression<Func<SecurityLoginsRolePoco, bool>> where, params Expression<Func<SecurityLoginsRolePoco, object>>[] navigationProperties)
        {
            IQueryable<SecurityLoginsRolePoco> pocos = GetAll().AsQueryable();

            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params SecurityLoginsRolePoco[] items)
        {
            using (SqlConnection connection = new SqlConnection(app.connectionString))
            {
                SqlCommand command = new SqlCommand();
                command.Connection = connection;

                foreach (SecurityLoginsRolePoco item in items)
                {
                    command.CommandText = @"DELETE FROM Security_Logins_Roles
                                        WHERE Id = @Id";
                    command.Parameters.AddWithValue("@Id", item.Id);
                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
            //   throw new NotImplementedException();
        }

        public void Update(params SecurityLoginsRolePoco[] items)
        {
            using (SqlConnection connection = new SqlConnection(app.connectionString))
            {
                SqlCommand command = new SqlCommand();
                command.Connection = connection;

                foreach (SecurityLoginsRolePoco item in items)
                { //Login,Role are GUIDS. Not sure about their update model
                    command.CommandText = @"UPDATE Security_Logins_Roles
                                      SET Login=@Login, Role = @Role
                                         
                                        WHERE Id = @Id";
                    command.Parameters.AddWithValue("@Id", item.Id);
                    command.Parameters.AddWithValue("@Login", item.Login);
                    command.Parameters.AddWithValue("@Role", item.Role);
                   
                    connection.Open();
                  command.ExecuteNonQuery();
                    connection.Close();
                }
            }
            //        throw new NotImplementedException();
        }
    }
}
