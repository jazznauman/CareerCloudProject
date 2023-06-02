using CareerCloud.Pocos;
using Library.Framework;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace CareerCloud.ADODataAccessLayer
{
    public class SecurityLoginRepository
        : DataAccessLayer.IDataRepository<SecurityLoginPoco>
    {
        
        
        private static readonly Settings app = new Settings();


        public void Add(params SecurityLoginPoco[] items)
        {
            using (SqlConnection connection = new SqlConnection(app.connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand();
                 command.Connection = connection;
                    command.CommandType = System.Data.CommandType.Text;

                    command.CommandText = @"INSERT INTO Security_Logins(  Id ,Login ,Password ,Created_Date, Password_Update_Date," +
                  " Agreement_Accepted_Date ,Is_Locked ,Is_Inactive ,Email_Address" +
                  " ,Phone_Number ,Full_Name ,Force_Change_Password ,Prefferred_Language )" +

                  " VALUES( @Id, @Login, @Password,@CreatedDate,@Password_Update_Date, " +
"@Agreement_Accepted_Date, @Is_Locked,@Is_Inactive,@Email_Address,@Phone_Number,@Full_Name," +
"@Force_Change_Password,@Prefferred_Language)";
                    connection.Open();
                   
                   
                    foreach (SecurityLoginPoco item in items)
                    {
                        command.Parameters.AddWithValue("@Id", item.Id);
                        command.Parameters.AddWithValue("@Login", item.Login);
                        command.Parameters.AddWithValue("@Password", item.Password);
                        command.Parameters.AddWithValue("@CreatedDate", item.Created);

                          command.Parameters.AddWithValue("@Password_Update_Date", item.PasswordUpdate);
                        
                     

                        command.Parameters.AddWithValue("@Agreement_Accepted_Date", item.AgreementAccepted);
                     

                        command.Parameters.AddWithValue("@Is_Locked", item.IsLocked);
                        command.Parameters.AddWithValue("@Is_Inactive", item.IsInactive);
                        command.Parameters.AddWithValue("@Email_Address", item.EmailAddress);
                        command.Parameters.AddWithValue("@Phone_Number", item.PhoneNumber);

                        command.Parameters.AddWithValue("@Full_Name", item.FullName);
                        command.Parameters.AddWithValue("@Force_Change_Password", item.ForceChangePassword);
                        command.Parameters.AddWithValue("@Prefferred_Language", item.PrefferredLanguage);
               //         command.Parameters.AddWithValue("@Time_Stamp", item.TimeStamp);

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

        public IList<SecurityLoginPoco> GetAll(params Expression<Func<SecurityLoginPoco, object>>[] navigationProperties)
        {
            using (SqlConnection connection = new SqlConnection(app.connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand();


                    command.Connection = connection;
                    command.CommandType = CommandType.Text;
                    command.CommandText = @"Select * from Security_Logins";
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    IList<SecurityLoginPoco> ilist = new List<SecurityLoginPoco>();

                    while (reader.Read())
                    {
                        SecurityLoginPoco poco = new SecurityLoginPoco();
                        poco.Id = reader.GetGuid(0);


                        poco.Login = reader.GetString(1);
                        poco.Password = reader.GetString(2);
                        poco.Created = reader.GetDateTime(3);
                        if (!Convert.IsDBNull(reader["Password_Update_Date"]))
                        {
                            poco.PasswordUpdate = (DateTime)reader["Password_Update_Date"];
                        }

                        if (!Convert.IsDBNull(reader["Agreement_Accepted_Date"]))
                        {
                            poco.AgreementAccepted = (DateTime)reader["Agreement_Accepted_Date"];
                        }
                         
                        poco.IsLocked = reader.GetBoolean(6);
                        poco.IsInactive = reader.GetBoolean(7);
                        poco.EmailAddress = reader.GetString(8);

                        if (!Convert.IsDBNull(reader["Phone_Number"]))
                        {
                            poco.PhoneNumber = (string)reader["Phone_Number"];
                        }
                      
                        
                        
                        
                        poco.FullName = reader.GetString(10);
                        poco.ForceChangePassword = reader.GetBoolean(11);
                        if (!Convert.IsDBNull(reader["Prefferred_Language"]))
                        {
                            poco.PrefferredLanguage = (string)reader["Prefferred_Language"];
                        }

                     

                        poco.TimeStamp = (byte[])reader[13];
                        ilist.Add(poco);
                       
                    }

                    return ilist;
                }
                finally { connection.Close(); }
            }
        }

        public IList<SecurityLoginPoco> GetList(Expression<Func<SecurityLoginPoco, bool>> where, params Expression<Func<SecurityLoginPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public SecurityLoginPoco GetSingle(Expression<Func<SecurityLoginPoco, bool>> where, params Expression<Func<SecurityLoginPoco, object>>[] navigationProperties)
        {
            IQueryable<SecurityLoginPoco> pocos = GetAll().AsQueryable();

            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params SecurityLoginPoco[] items)
        {
            using (SqlConnection connection = new SqlConnection(app.connectionString))
            {
                SqlCommand command = new SqlCommand();
                command.Connection = connection;

                foreach (SecurityLoginPoco item in items)
                {
                    command.CommandText = @"DELETE FROM [Security_Logins]
                                        WHERE Id = @Id";
                    command.Parameters.AddWithValue("@Id", item.Id);
                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
            //   throw new NotImplementedException();
        }

        public void Update(params SecurityLoginPoco[] items)
        {
            using (SqlConnection connection = new SqlConnection(app.connectionString))
            {
                SqlCommand command = new SqlCommand();
                command.Connection = connection;

                foreach (SecurityLoginPoco item in items)
                {
                    command.CommandText = @"UPDATE [Security_Logins]

                                        SET[Login]=@Login
      ,[Password]=@Password
      ,[Created_Date]=@Created_Date
      ,Password_Update_Date=@Password_Update_Date
      ,Agreement_Accepted_Date=@Agreement_Accepted_Date
      ,[Is_Locked]=@Is_Locked
      ,[Is_Inactive]=@Is_Inactive
      ,[Email_Address]=@Email_Address
      ,[Phone_Number]=@Phone_Number
      ,[Full_Name]=@Full_Name
      ,[Force_Change_Password]=@Force_Change_Password
      ,[Prefferred_Language]=@Prefferred_Language

                                        WHERE Id = @Id";
                    command.Parameters.AddWithValue("@Id", item.Id);
                    command.Parameters.AddWithValue("@Login", item.Login);
                    command.Parameters.AddWithValue("@Password", item.Password);
                    command.Parameters.AddWithValue("@Created_Date", item.Created);
                    command.Parameters.AddWithValue("@Password_Update_Date",item.PasswordUpdate);
                    command.Parameters.AddWithValue("@Agreement_Accepted_Date", item.AgreementAccepted);
                    command.Parameters.AddWithValue("@Is_Locked", item.IsLocked);
                    command.Parameters.AddWithValue("@Is_Inactive", item.IsInactive);
                    command.Parameters.AddWithValue("@Email_Address", item.EmailAddress);
                    command.Parameters.AddWithValue("@Phone_Number", item.PhoneNumber);
                    command.Parameters.AddWithValue("@Full_Name", item.FullName);
                    command.Parameters.AddWithValue("@Force_Change_Password", item.ForceChangePassword);
                    command.Parameters.AddWithValue("@Prefferred_Language",item.PrefferredLanguage);
                 //   command.Parameters.AddWithValue("@Time_Stamp", item.TimeStamp);
                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
            //        throw new NotImplementedException();
        }
    }
}
