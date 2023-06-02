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
    public class CompanyLocationRepository
        : DataAccessLayer.IDataRepository<CompanyLocationPoco>
    {
        private int i;
        //      string connectionString = ConfigurationManager.ConnectionStrings["DataConnection"].ConnectionString;
        private static readonly Settings app = new Settings();

        public void Add(params CompanyLocationPoco[] items)
        {
            using (SqlConnection connection = new SqlConnection(app.connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand();
                    command.Connection = connection;
                    command.CommandType = CommandType.Text;
                    connection.Open();
                    command.CommandText = @"INSERT INTO Company_Locations(

                    [Id],[Company],[Country_Code],[State_Province_Code],[Street_Address],[City_Town],[Zip_Postal_Code]  )
              VALUES( @Id, @Company, @Country_Code,@State_Province_Code, @Street_Address,@City_Town,@Zip_Postal_code )";
                    foreach (CompanyLocationPoco item in items)
                    {


                        command.Parameters.AddWithValue("@Id", item.Id);
                        command.Parameters.AddWithValue("@Company", item.Company);
                        command.Parameters.AddWithValue("@Country_Code", item.CountryCode);
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

        public IList<CompanyLocationPoco> GetAll(params Expression<Func<CompanyLocationPoco, object>>[] navigationProperties)
        {
            using (SqlConnection connection = new SqlConnection(app.connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand();


                    command.Connection = connection;
                    command.CommandType = CommandType.Text;
                    command.CommandText = @"Select * from Company_Locations";
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    IList<CompanyLocationPoco> ilist = new List<CompanyLocationPoco>();

                    while (reader.Read())
                    {
                        CompanyLocationPoco poco = new CompanyLocationPoco();

                        poco.Id = reader.GetGuid(0);


                        poco.Company = reader.GetGuid(1);
                        poco.CountryCode = reader.GetString(2);

                        poco.Province = reader.GetString(3);
                        poco.Street = reader.GetString(4);

                        if (!Convert.IsDBNull(reader["City_Town"]))
                        {
                            poco.City = (string)reader["City_Town"];
                        }


                        if (!Convert.IsDBNull(reader["Zip_Postal_Code"]))
                        {
                            poco.PostalCode = (string)reader["Zip_Postal_Code"];
                        }
                      
                        
                        
                        poco.TimeStamp = (byte[])reader[7];
                        ilist.Add(poco);
                        
                    }

                    return ilist;
                }
                finally
                {
                    connection.Close();
                }
            }
        }
        public IList<CompanyLocationPoco> GetList(Expression<Func<CompanyLocationPoco, bool>> where, params Expression<Func<CompanyLocationPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public CompanyLocationPoco GetSingle(Expression<Func<CompanyLocationPoco, bool>> where, params Expression<Func<CompanyLocationPoco, object>>[] navigationProperties)
        {
            IQueryable<CompanyLocationPoco> pocos = GetAll().AsQueryable();

            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params CompanyLocationPoco[] items)
        {
            using (SqlConnection connection = new SqlConnection(app.connectionString))
            {
                SqlCommand command = new SqlCommand();
                command.Connection = connection;

                foreach (CompanyLocationPoco item in items)
                {
                    command.CommandText = @"DELETE FROM Company_Locations
                                        WHERE Id = @Id";
                    command.Parameters.AddWithValue("@Id", item.Id);
                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
            //   throw new NotImplementedException();
        }

        public void Update(params CompanyLocationPoco[] items)
        {
            using (SqlConnection connection = new SqlConnection(app.connectionString))
            {
                SqlCommand command = new SqlCommand();
                command.Connection = connection;

                foreach (CompanyLocationPoco item in items)
                {
                    command.CommandText = @"UPDATE Company_Locations
                                        SET Company=@Company, Country_Code=@CountryCode,
                  [State_Province_Code]=@StateProvinceCode
		              ,[Street_Address]=@StreetAddress
		                  ,[City_Town]=@CityTown
		                 ,[Zip_Postal_Code]=@ZipPostalCode
                                         
                                        WHERE Id = @Id";
                    command.Parameters.AddWithValue("@Id", item.Id);
                    command.Parameters.AddWithValue("@Company", item.Company);
                    command.Parameters.AddWithValue("@CountryCode", item.CountryCode);
                    command.Parameters.AddWithValue("@StateProvinceCode", item.Province);
                    command.Parameters.AddWithValue("@StreetAddress", item.Street);
                    command.Parameters.AddWithValue("@CityTown", item.City);
                    command.Parameters.AddWithValue("@ZipPostalCode", item.PostalCode);
                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
            //        throw new NotImplementedException();
        }
    }
}
