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
    public class SecurityRoleRepository
        : DataAccessLayer.IDataRepository<SecurityRolePoco>
    {
      
        private static readonly Settings app = new Settings();


        public void Add(params SecurityRolePoco[] items)
        {
            using (SqlConnection connection = new SqlConnection(app.connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand();
                    command.Connection = connection;
                    command.CommandText = @"INSERT INTO Security_Roles(
                     Id,Role ,Is_Inactive )
              VALUES( @Id, @Role, @Is_Inactive )";

                    connection.Open();

                    foreach (SecurityRolePoco item in items)
                    {


                        command.Parameters.AddWithValue("@Id", item.Id);

                        command.Parameters.AddWithValue("@Role", item.Role);
                        command.Parameters.AddWithValue("@Is_Inactive", item.IsInactive);
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

        public IList<SecurityRolePoco> GetAll(params Expression<Func<SecurityRolePoco, object>>[] navigationProperties)
        {
            using (SqlConnection connection = new SqlConnection(app.connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand();


                    command.Connection = connection;
                    command.CommandType = CommandType.Text;
                    command.CommandText = @"Select * from Security_Roles";
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    IList<SecurityRolePoco> ilist = new List<SecurityRolePoco>();

                    while (reader.Read())
                    {
                        SecurityRolePoco poco = new SecurityRolePoco();
                        poco.Id = reader.GetGuid(0);

                        poco.Role = reader.GetString(1);
                        poco.IsInactive = reader.GetBoolean(2);

                        ilist.Add(poco);
                       
                    }

                    return ilist;
                }
                finally { connection.Close(); }
            }
        }

        public IList<SecurityRolePoco> GetList(Expression<Func<SecurityRolePoco, bool>> where, params Expression<Func<SecurityRolePoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public SecurityRolePoco GetSingle(Expression<Func<SecurityRolePoco, bool>> where, params Expression<Func<SecurityRolePoco, object>>[] navigationProperties)
        {
            IQueryable<SecurityRolePoco> pocos = GetAll().AsQueryable();

            return pocos.Where(where).FirstOrDefault();
        }

            public void Remove(params SecurityRolePoco[] items)
        {
            using (SqlConnection connection = new SqlConnection(app.connectionString))
            {
                SqlCommand command = new SqlCommand();
                command.Connection = connection;

                foreach (SecurityRolePoco item in items)
                {
                    command.CommandText = @"DELETE FROM Security_Roles
                                        WHERE Id = @Id";
                    command.Parameters.AddWithValue("@Id", item.Id);
                    connection.Open();
                  command.ExecuteNonQuery();
                    connection.Close();
                }
            }
            //   throw new NotImplementedException();
        }

        public void Update(params SecurityRolePoco[] items)
        {
            using (SqlConnection connection = new SqlConnection(app.connectionString))
            {
                SqlCommand command = new SqlCommand();
                command.Connection = connection;

                foreach (SecurityRolePoco item in items)
                {
                    command.CommandText = @"UPDATE Security_Roles
                                        SET [Role]=@Role  ,[Is_Inactive]=@Is_Inactive
                                        WHERE Id = @Id";
                    command.Parameters.AddWithValue("@Id", item.Id);
                    command.Parameters.AddWithValue("@Role", item.Role);
                    
                    command.Parameters.AddWithValue("@Is_Inactive", item.IsInactive);
                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
            //        throw new NotImplementedException();
        }
    }
}
