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
    
        public class ApplicantProfileRepository :
       DataAccessLayer.IDataRepository<ApplicantProfilePoco>
    {
        private int i;
        private static readonly Settings app = new Settings();

        //    string connectionString = ConfigurationManager.ConnectionStrings["DataConnection"].ConnectionString;


        public void Add(params ApplicantProfilePoco[] items)
        {
            using (SqlConnection connection = new SqlConnection(app.connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand();
                    command.Connection = connection;
                    connection.Open();

                    command.CommandText = @"INSERT INTO Applicant_Profiles( Id, Login, Current_Salary, Current_Rate, Currency, Country_Code, State_Province_Code, Street_Address, City_Town, Zip_Postal_Code )
VALUES( @Id, @Login, @Current_Salary, @Current_Rate, @Currency, @Country_Code, @State_Province_Code, @Street_Address, @City_Town, @Zip_Postal_Code )";

                    foreach (ApplicantProfilePoco item in items)
                    {

                        command.Parameters.AddWithValue("@Id", item.Id);
                        command.Parameters.AddWithValue("@Login", item.Login);
                        command.Parameters.AddWithValue("@Current_Salary", item.CurrentSalary);
                        command.Parameters.AddWithValue("@Current_Rate", item.CurrentRate);
                        command.Parameters.AddWithValue("@Currency", item.Currency);
                        command.Parameters.AddWithValue("@Country_Code", item.Country);
                        command.Parameters.AddWithValue("@State_Province_Code", item.Province);
                        command.Parameters.AddWithValue("@Street_Address", item.Street);
                        command.Parameters.AddWithValue("@City_Town", item.City);
                        command.Parameters.AddWithValue("@Zip_Postal_Code", item.PostalCode);


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

        public IList<ApplicantProfilePoco> GetAll(params Expression<Func<ApplicantProfilePoco, object>>[] navigationProperties)
        {
            using (SqlConnection connection = new SqlConnection(app.connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand();


                    command.Connection = connection;
                    command.CommandType = CommandType.Text;
                    command.CommandText = @"Select * from Applicant_Profiles";
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    IList<ApplicantProfilePoco> ilist = new List<ApplicantProfilePoco>();

                    while (reader.Read())
                    {
                        ApplicantProfilePoco poco = new ApplicantProfilePoco();
                        poco.Id = reader.GetGuid(0);
                        poco.Login = reader.GetGuid(1);
                        poco.CurrentSalary = reader.GetDecimal(2);
                        poco.CurrentRate = reader.GetDecimal(3);
                        poco.Currency = reader.GetString(4);
                        poco.Country = reader.GetString(5);
                        poco.Province = reader.GetString(6);
                        poco.Street = reader.GetString(7);
                        poco.City = reader.GetString(8);
                        poco.PostalCode = reader.GetString(9);
                        poco.TimeStamp = (byte[])reader[10];
                        ilist.Add(poco);
                        
                    }

                    return ilist;
                }


                finally { connection.Close(); }
            }
            }

        public IList<ApplicantProfilePoco> GetList(Expression<Func<ApplicantProfilePoco, bool>> where, params Expression<Func<ApplicantProfilePoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public ApplicantProfilePoco GetSingle(Expression<Func<ApplicantProfilePoco, bool>> where, params Expression<Func<ApplicantProfilePoco, object>>[] navigationProperties)
        {
            IQueryable<ApplicantProfilePoco> pocos = GetAll().AsQueryable();

            return pocos.Where(where).FirstOrDefault();
            throw new NotImplementedException();
        }

        public void Remove(params ApplicantProfilePoco[] items)
        {
            using (SqlConnection connection = new SqlConnection(app.connectionString))
            {
                SqlCommand command = new SqlCommand();
                command.Connection = connection;

                foreach (ApplicantProfilePoco item in items)
                {
                    command.CommandText = @"DELETE FROM Applicant_Profiles
                                        WHERE Id = @Id";
                    command.Parameters.AddWithValue("@Id", item.Id);
                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
          //  throw new NotImplementedException();
        }

        public void Update(params ApplicantProfilePoco[] items)
        {
            using (SqlConnection connection = new SqlConnection(app.connectionString))
            {
                SqlCommand command = new SqlCommand();
                command.Connection = connection;

                foreach (ApplicantProfilePoco item in items)
                {
                    command.CommandText = @"UPDATE Applicant_Profiles
                                        SET Login = @Login, Current_Salary = @Current_Salary, Current_Rate = @Current_Rate, Currency = @Currency, Country_Code = @Country_Code, State_Province_Code = @State_Province_Code, Street_Address = @Street_Address, City_Town= @City_Town, Zip_Postal_Code = @Zip_Postal_Code
                                        WHERE Id = @Id";
                    command.Parameters.AddWithValue("@Id", item.Id);
                    command.Parameters.AddWithValue("@Login", item.Login);
                    command.Parameters.AddWithValue("@Current_Salary", item.CurrentSalary);
                    command.Parameters.AddWithValue("@Current_Rate", item.CurrentRate);
                    command.Parameters.AddWithValue("@Currency", item.Currency);
                    command.Parameters.AddWithValue("@Country_Code", item.Country);
                    command.Parameters.AddWithValue("@State_Province_Code", item.Province);
                    command.Parameters.AddWithValue("@Street_Address", item.Street);
                    command.Parameters.AddWithValue("@City_Town", item.City);
                    command.Parameters.AddWithValue("@Zip_Postal_Code", item.PostalCode);
                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
        //    throw new NotImplementedException();
        }
    }
}
