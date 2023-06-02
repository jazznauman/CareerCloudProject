using CareerCloud.Pocos;
using Library.Framework;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareerCloud.ADODataAccessLayer
{
    public  class ApplicantResumeRepository :
        DataAccessLayer.IDataRepository <ApplicantResumePoco>
    {
        private int i;
        private static readonly Settings app = new Settings();
   //     string connectionString = ConfigurationManager.ConnectionStrings["DataConnection"].ConnectionString;

        public void Add(params ApplicantResumePoco[] items)
        {
            using (SqlConnection connection = new SqlConnection(app.connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand();
                    command.Connection = connection;
                    connection.Open();
                    command.CommandText = @"INSERT INTO Applicant_Resumes( Id, Applicant, Resume, Last_Updated )
                                        VALUES( @Id, @Applicant, @Resume, @Last_Updated )";
                    foreach (ApplicantResumePoco item in items)
                    {

                        command.Parameters.AddWithValue("@Id", item.Id);
                        command.Parameters.AddWithValue("@Applicant", item.Applicant);
                        command.Parameters.AddWithValue("@Resume", item.Resume);
                        command.Parameters.AddWithValue("@Last_Updated", item.LastUpdated);


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

        public IList<ApplicantResumePoco> GetAll(params System.Linq.Expressions.Expression<Func<ApplicantResumePoco, object>>[] navigationProperties)
        {

            using (SqlConnection connection = new SqlConnection(app.connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand();


                    command.Connection = connection;
                    command.CommandType = CommandType.Text;
                    command.CommandText = @"Select * from Applicant_Resumes";
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    IList<ApplicantResumePoco> ilist = new List<ApplicantResumePoco>();

                    while (reader.Read())
                    {
                        ApplicantResumePoco poco = new ApplicantResumePoco();
                        poco.Id = reader.GetGuid(0);

                        poco.Applicant = reader.GetGuid(1);


                        poco.Resume = reader.GetString(2);
                        if (!Convert.IsDBNull(reader["Last_Updated"]))
                        {
                            poco.LastUpdated = (DateTime)reader["Last_Updated"];
                        }
                       
                    
                        ilist.Add(poco);
                        
                    }

                    return ilist;


                }
                finally { connection.Close(); }
            }
        }
        public IList<ApplicantResumePoco> GetList(System.Linq.Expressions.Expression<Func<ApplicantResumePoco, bool>> where, params System.Linq.Expressions.Expression<Func<ApplicantResumePoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public ApplicantResumePoco GetSingle(System.Linq.Expressions.Expression<Func<ApplicantResumePoco, bool>> where, params System.Linq.Expressions.Expression<Func<ApplicantResumePoco, object>>[] navigationProperties)
        {
            IQueryable<ApplicantResumePoco> pocos = GetAll().AsQueryable();

            return pocos.Where(where).FirstOrDefault();
            throw new NotImplementedException();
        }

        public void Remove(params ApplicantResumePoco[] items)
        {

            using (SqlConnection connection = new SqlConnection(app.connectionString))
            {
                SqlCommand command = new SqlCommand();
                command.Connection = connection;

                foreach (ApplicantResumePoco item in items)
                {
                    command.CommandText = @"DELETE FROM dbo.Applicant_Resumes
                                        WHERE Id = @Id";
                    command.Parameters.AddWithValue("@Id", item.Id);
                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
       //     throw new NotImplementedException();
        }

        public void Update(params ApplicantResumePoco[] items)
        {
            using (SqlConnection connection = new SqlConnection(app.connectionString))
            {
                SqlCommand command = new SqlCommand();
                command.Connection = connection;

                foreach (ApplicantResumePoco item in items)
                {
                    command.CommandText = @"UPDATE Applicant_Resumes
                                        SET Applicant = @Applicant, Resume = @Resume, Last_Updated = @Last_Updated
                                        WHERE Id = @Id";
                    command.Parameters.AddWithValue("@Id", item.Id);
                    command.Parameters.AddWithValue("@Applicant", item.Applicant);
                    command.Parameters.AddWithValue("@Resume", item.Resume);
                    command.Parameters.AddWithValue("@Last_Updated", item.LastUpdated);
                    connection.Open();
                     command.ExecuteNonQuery();
                    connection.Close();
                }
            }
            //throw new NotImplementedException();
        }
    }
}