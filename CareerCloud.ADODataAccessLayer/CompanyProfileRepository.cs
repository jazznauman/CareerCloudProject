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
    public class CompanyProfileRepository
        : DataAccessLayer.IDataRepository<CompanyProfilePoco>
    {
        private int i;
        //    string connectionString = ConfigurationManager.ConnectionStrings["DataConnection"].ConnectionString;

        private static readonly Settings app = new Settings();

        public void Add(params CompanyProfilePoco[] items)
        {

            using (SqlConnection connection = new SqlConnection(app.connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand();
                    command.Connection = connection;
                    command.CommandText = @"INSERT INTO Company_Profiles(
                    [Id] ,[Registration_Date] ,[Company_Website] ,[Contact_Phone] ,[Contact_Name] ,[Company_Logo] )
              VALUES( @Id, @Registration_Date, @Company_Website,@Contact_Phone, @Contact_Name,@Company_Logo)";
                    connection.Open();
                    foreach (CompanyProfilePoco item in items)
                    {


                        command.Parameters.AddWithValue("@Id", item.Id);
                        command.Parameters.AddWithValue("@Registration_Date", item.RegistrationDate);
                        command.Parameters.AddWithValue("@Company_Website", item.CompanyWebsite);
                        command.Parameters.AddWithValue("@Contact_Phone", item.ContactPhone);
                        command.Parameters.AddWithValue("@Contact_Name", item.ContactName);
                        command.Parameters.AddWithValue("@Company_Logo", item.CompanyLogo);

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

        public IList<CompanyProfilePoco> GetAll(params Expression<Func<CompanyProfilePoco, object>>[] navigationProperties)
        {
            using (SqlConnection connection = new SqlConnection(app.connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand();

                    command.Connection = connection;
                    command.CommandType = CommandType.Text;
                    command.CommandText = @"Select * from Company_Profiles";
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    IList<CompanyProfilePoco> ilist = new List<CompanyProfilePoco>();

                    while (reader.Read())
                    {
                        CompanyProfilePoco poco = new CompanyProfilePoco();
                        poco.Id = reader.GetGuid(0);


                        poco.RegistrationDate = reader.GetDateTime(1);

                        if (!Convert.IsDBNull(reader["Company_Website"]))
                        {
                            poco.CompanyWebsite = (string)reader["Company_Website"];
                        }
                        //  poco.CompanyWebsite = reader.GetString(2);
                        poco.ContactPhone = reader.GetString(3);
                        if (!Convert.IsDBNull(reader["Contact_Name"]))
                        {
                            poco.ContactName = (string)reader["Contact_Name"];
                        }
                     //   poco.ContactName = reader.GetString(4);


                        if (!Convert.IsDBNull(reader["Company_Logo"]))
                        {
                            poco.CompanyLogo= (byte[])reader["Company_Logo"];
                        }
                     //   poco.CompanyLogo = (byte[])reader[5] ;
                            //(byte[])reader[5;

                        poco.TimeStamp = (byte[])reader[6];
                        ilist.Add(poco);
                        i++;
                    }

                    return ilist;
                }
                finally { connection.Close(); }
            }
        }

        public IList<CompanyProfilePoco> GetList(Expression<Func<CompanyProfilePoco, bool>> where, params Expression<Func<CompanyProfilePoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public CompanyProfilePoco GetSingle(Expression<Func<CompanyProfilePoco, bool>> where, params Expression<Func<CompanyProfilePoco, object>>[] navigationProperties)
        {
            IQueryable<CompanyProfilePoco> pocos = GetAll().AsQueryable();

            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params CompanyProfilePoco[] items)
        {
            using (SqlConnection connection = new SqlConnection(app.connectionString))
            {
                SqlCommand command = new SqlCommand();
                command.Connection = connection;

                foreach (CompanyProfilePoco item in items)
                {
                    command.CommandText = @"DELETE FROM Company_Profiles
                                        WHERE Id = @Id";
                    command.Parameters.AddWithValue("@Id", item.Id);
                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
            //   throw new NotImplementedException();
        }

        public void Update(params CompanyProfilePoco[] items)
        {

            using (SqlConnection connection = new SqlConnection(app.connectionString))
            {
                SqlCommand command = new SqlCommand();
                command.Connection = connection;

                foreach (CompanyProfilePoco item in items)
                {
                    command.CommandText = @"UPDATE Company_Profiles
                                        SET [Registration_Date]=@Registration_Date
      ,[Company_Website]=@Company_Website
      ,[Contact_Phone]=@Contact_Phone
      ,[Contact_Name]=@Contact_Name
      ,[Company_Logo]=@Company_Logo
                                        WHERE Id = @Id";
                    command.Parameters.AddWithValue("@Id", item.Id);
                    command.Parameters.AddWithValue("@Registration_Date", item.RegistrationDate);
                    command.Parameters.AddWithValue("@Company_Website", item.CompanyWebsite);
                    command.Parameters.AddWithValue("@Contact_Phone", item.ContactPhone);
                    command.Parameters.AddWithValue("@Contact_Name", item.ContactName);
                    command.Parameters.AddWithValue("@Company_Logo", item.CompanyLogo);
                    connection.Open();
                   command.ExecuteNonQuery();
                    connection.Close();
                }
            }
            //        throw new NotImplementedException();
           
        }
    }
}
