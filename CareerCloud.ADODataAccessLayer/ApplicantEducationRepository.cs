using CareerCloud.Pocos;
using Library.Framework;
using System;
using System.Collections;
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
  


    public class ApplicantEducationRepository :
        DataAccessLayer.IDataRepository<ApplicantEducationPoco>
    {

        private int i;

        private static readonly Settings app = new Settings();
    //    string connectionString = ConfigurationManager.ConnectionStrings["DataConnection"].ConnectionString;

    //string connectionString= "Server = JAZZ2045 ; Database = JOB_PORTAL_DB; Integrated Security = true;";

        public void Add(params ApplicantEducationPoco[] items)
        {
            using (SqlConnection connection = new SqlConnection(app.connectionString))

            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand();
                    command.CommandType = CommandType.Text;
                    command.Connection = connection;
                    command.CommandText = @"INSERT INTO Applicant_Educations
(Id, Applicant, Major, Certificate_Diploma,Start_Date, Completion_date,Completion_Percent) " +
                                 "VALUES (@Id, @Applicant, @Major, @Certificate_Diploma, @Start_Date,@Completion_Date," +
                                 "@Completion_Percent )";
                    foreach (ApplicantEducationPoco item in items)
                    {


                        command.Parameters.AddWithValue("@Id", item.Id);
                        command.Parameters.AddWithValue("@Applicant", item.Applicant);
                        command.Parameters.AddWithValue("@Major", item.Major);
                        command.Parameters.AddWithValue("@Certificate_Diploma", item.CertificateDiploma);
                        command.Parameters.AddWithValue("@Start_Date", item.StartDate);
                        command.Parameters.AddWithValue("@Completion_Date", item.CompletionDate);
                        command.Parameters.AddWithValue("@Completion_Percent", item.CompletionPercent);

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

        public IList<ApplicantEducationPoco> GetAll(params Expression<Func<ApplicantEducationPoco, object>>[] navigationProperties)
        {
            List<ApplicantEducationPoco> ilist = new List<ApplicantEducationPoco>();

            using (SqlConnection connection = new SqlConnection(app.connectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand();
                    command.Connection = connection;
                    command.CommandType = CommandType.Text;
                    command.CommandText = @"Select * from Applicant_Educations";

                    SqlDataReader reader = command.ExecuteReader();



                    while (reader.Read())
                    {
                        ApplicantEducationPoco poco = new ApplicantEducationPoco();
                        poco.Id = reader.GetGuid(0);
                        poco.Applicant = reader.GetGuid(1);
                        poco.Major = reader.GetString(2);
                        poco.CertificateDiploma = reader.GetString(3);
                        poco.StartDate = reader.GetDateTime(4);
                        poco.CompletionDate = reader.GetDateTime(5);
                        poco.CompletionPercent = reader.GetByte(6);
                     
                       ilist.Add(poco);
                    }
                }
                finally { connection.Close(); }
                return ilist;
            }
        }



        public IList<ApplicantEducationPoco> GetList(Expression<Func<ApplicantEducationPoco, bool>> where, params Expression<Func<ApplicantEducationPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public ApplicantEducationPoco GetSingle(Expression<Func<ApplicantEducationPoco, bool>> where, params Expression<Func<ApplicantEducationPoco, object>>[] navigationProperties)
        {
            IQueryable<ApplicantEducationPoco> pocos = GetAll().AsQueryable();

            return pocos.Where(where).FirstOrDefault();
        }


        public void Remove(params ApplicantEducationPoco[] items)
        {
            using (SqlConnection connection = new SqlConnection(app.connectionString))
            {

                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                connection.Open();
                foreach (ApplicantEducationPoco item in items)
                {
                    command.CommandText = @"DELETE FROM Applicant_Educations
                                        WHERE Id = @Id";
                    command.Parameters.AddWithValue("@Id", item.Id);

                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }


        }

        public void Update(params ApplicantEducationPoco[] items)
        {

            using (SqlConnection connection = new SqlConnection(app.connectionString))
            {
                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                connection.Open();
                foreach (ApplicantEducationPoco item in items)
                {
                    command.CommandText = @"UPDATE Applicant_Educations
                                         SET Applicant = @Applicant, Major = @Major, 
                           Certificate_Diploma = @Certificate_Diploma, Start_Date = @Start_Date, 
                         Completion_Date = @Completion_Date, Completion_Percent = @Completion_Percent
                         WHERE Id = @Id";
                    command.Parameters.AddWithValue("@Id", item.Id);
                    command.Parameters.AddWithValue("@Applicant", item.Applicant);
                    command.Parameters.AddWithValue("@Major", item.Major);
                    command.Parameters.AddWithValue("@Certificate_Diploma", item.CertificateDiploma);
                    command.Parameters.AddWithValue("@Start_Date", item.StartDate);
                    command.Parameters.AddWithValue("@Completion_Date", item.CompletionDate);
                    command.Parameters.AddWithValue("@Completion_Percent", item.CompletionPercent);

                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
        }

    }
}
