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
    public class SystemLanguageCodeRepository
        : DataAccessLayer.IDataRepository<SystemLanguageCodePoco>
    {
        private int i;
        //     string connectionString = ConfigurationManager.ConnectionStrings["DataConnection"].ConnectionString;
        private static readonly Settings app = new Settings();

        public void Add(params SystemLanguageCodePoco[] items)
        {
            using (SqlConnection connection = new SqlConnection(app.connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand();
                    command.Connection = connection;
                    command.CommandText = @"INSERT INTO System_Language_Codes(
                    [LanguageID] ,[Name] ,[Native_Name]  )
              VALUES( @LanguageID, @Name,@Native_Name )";

                    connection.Open();
                    foreach (SystemLanguageCodePoco item in items)
                    {
                        command.Parameters.AddWithValue("@LanguageID", item.LanguageID);
                        command.Parameters.AddWithValue("@Name", item.Name);
                        command.Parameters.AddWithValue("@Native_Name", item.NativeName);


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

        public IList<SystemLanguageCodePoco> GetAll(params Expression<Func<SystemLanguageCodePoco, object>>[] navigationProperties)
        {
            using (SqlConnection connection = new SqlConnection(app.connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand();


                    command.Connection = connection;
                    command.CommandType = CommandType.Text;
                    command.CommandText = @"Select * from System_Language_Codes";
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    IList<SystemLanguageCodePoco> ilist = new List<SystemLanguageCodePoco>();

                    while (reader.Read())
                    {
                        SystemLanguageCodePoco poco = new SystemLanguageCodePoco();



                        poco.LanguageID = reader.GetString(0);
                        poco.Name = reader.GetString(1);
                        poco.NativeName = reader.GetString(2);


                        ilist.Add(poco);
                        i++;
                    }

                    return ilist;
                }
                finally { connection.Close(); }
            }
        }

        public IList<SystemLanguageCodePoco> GetList(Expression<Func<SystemLanguageCodePoco, bool>> where, params Expression<Func<SystemLanguageCodePoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public SystemLanguageCodePoco GetSingle(Expression<Func<SystemLanguageCodePoco, bool>> where, params Expression<Func<SystemLanguageCodePoco, object>>[] navigationProperties)
        {
            IQueryable<SystemLanguageCodePoco> pocos = GetAll().AsQueryable();

            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params SystemLanguageCodePoco[] items)
        {
            using (SqlConnection connection = new SqlConnection(app.connectionString))
            {
                SqlCommand command = new SqlCommand();
                command.Connection = connection;

                foreach (SystemLanguageCodePoco item in items)
                {
                    command.CommandText = @"DELETE FROM System_Language_Codes
                                        WHERE LanguageID = @LanguageID";
                    command.Parameters.AddWithValue("@LanguageID", item.LanguageID);
                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
            //   throw new NotImplementedException();
        }

        public void Update(params SystemLanguageCodePoco[] items)
        {
            using (SqlConnection connection = new SqlConnection(app.connectionString))
            {
                SqlCommand command = new SqlCommand();
                command.Connection = connection;

                foreach (SystemLanguageCodePoco item in items)
                {
                    command.CommandText = @"UPDATE System_Language_Codes
                                        SET Name=@Name, Native_Name=@NativeName
                                        WHERE LanguageID = @LanguageID";
                    command.Parameters.AddWithValue("@LanguageID", item.LanguageID);
                    command.Parameters.AddWithValue("@Name", item.Name);
                    command.Parameters.AddWithValue("@NativeName", item.NativeName);

                    connection.Open();
                   command.ExecuteNonQuery();
                    connection.Close();
                }
            }
            //        throw new NotImplementedException();
        }
    }
}
