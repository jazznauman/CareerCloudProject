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
   
        public class ApplicantJobApplicationRepository :
        DataAccessLayer.IDataRepository<ApplicantJobApplicationPoco>
        {
            private int i;
        private static readonly Settings app = new Settings();

        //         string connectionString = ConfigurationManager.ConnectionStrings["DataConnection"].ConnectionString;


        public void Add(params ApplicantJobApplicationPoco[] items)
            {
                using (SqlConnection connection = new SqlConnection(app.connectionString))
                {
                    try
                    {
                        SqlCommand command = new SqlCommand();
                        command.Connection = connection;
                        command.CommandType = CommandType.Text;
                        command.CommandText = @"INSERT INTO Applicant_Job_Applications
(Id, Applicant, Job,Application_Date) " +
                                     "VALUES (@Id, @Applicant, @Job, @Application_Date )";
                        connection.Open();
                        foreach (ApplicantJobApplicationPoco item in items)
                        {


                            command.Parameters.AddWithValue("@Id", item.Id);
                            command.Parameters.AddWithValue("@Applicant", item.Applicant);
                            command.Parameters.AddWithValue("@Job", item.Job);

                            command.Parameters.AddWithValue("@Application_Date", item.ApplicationDate);


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

            public IList<ApplicantJobApplicationPoco> GetAll(params Expression<Func<ApplicantJobApplicationPoco, object>>[] navigationProperties)
            {
                using (SqlConnection connection = new SqlConnection(app.connectionString))
                {
                    try
                    {
                        SqlCommand command = new SqlCommand();



                        command.Connection = connection;
                        command.CommandType = CommandType.Text;
                        command.CommandText = @"Select * from Applicant_Job_Applications";
                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();

                        List<ApplicantJobApplicationPoco> ilist = new List<ApplicantJobApplicationPoco>();

                        while (reader.Read())
                        {
                            ApplicantJobApplicationPoco poco = new ApplicantJobApplicationPoco();
                            poco.Id = reader.GetGuid(0);
                            poco.Applicant = reader.GetGuid(1);

                            poco.Job = reader.GetGuid(2);
                            poco.ApplicationDate = reader.GetDateTime(3);
                            poco.TimeStamp = (byte[])reader[4];
                            ilist.Add(poco);
                           
                        }

                        return ilist;
                    }


                    finally { connection.Close(); }
                }
            }

            public IList<ApplicantJobApplicationPoco> GetList(Expression<Func<ApplicantJobApplicationPoco, bool>> where, params Expression<Func<ApplicantJobApplicationPoco, object>>[] navigationProperties)
            {
                throw new NotImplementedException();
            }

            public ApplicantJobApplicationPoco GetSingle(Expression<Func<ApplicantJobApplicationPoco, bool>> where, params Expression<Func<ApplicantJobApplicationPoco, object>>[] navigationProperties)
            {
                IQueryable<ApplicantJobApplicationPoco> pocos = GetAll().AsQueryable();

                return pocos.Where(where).FirstOrDefault();
                throw new NotImplementedException();
            }

            public void Remove(params ApplicantJobApplicationPoco[] items)
            {
                using (SqlConnection connection = new SqlConnection(app.connectionString))
                {
                    SqlCommand command = new SqlCommand();
                    command.Connection = connection;

                    foreach (ApplicantJobApplicationPoco item in items)
                    {
                        command.CommandText = @"DELETE FROM Applicant_Job_Applications
                                        WHERE Id = @Id";
                        command.Parameters.AddWithValue("@Id", item.Id);
                        connection.Open();
                        command.ExecuteNonQuery();
                        connection.Close();
                    }
                }
                //throw new NotImplementedException();
            }

            public void Update(params ApplicantJobApplicationPoco[] items)
            {
                using (SqlConnection connection = new SqlConnection(app.connectionString))
                {
                    SqlCommand command = new SqlCommand();
                    command.Connection = connection;

                    foreach (ApplicantJobApplicationPoco item in items)
                    {
                        command.CommandText = @"UPDATE Applicant_Job_Applications
                                        SET Applicant = @Applicant, Job = @Job, Application_Date= @Application_Date
                                        WHERE Id = @Id";
                        command.Parameters.AddWithValue("@Applicant", item.Applicant);
                        command.Parameters.AddWithValue("@Job", item.Job);
                        command.Parameters.AddWithValue("@Application_Date", item.ApplicationDate);
                        command.Parameters.AddWithValue("@Id", item.Id);
                        connection.Open();
                        command.ExecuteNonQuery();
                        connection.Close();
                    }
                }
                // throw new NotImplementedException();
            }
        }
    }
