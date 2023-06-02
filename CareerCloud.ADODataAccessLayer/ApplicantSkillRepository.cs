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
    public class ApplicantSkillRepository : DataAccessLayer.IDataRepository<ApplicantSkillPoco>
    {
        private int i;
        //    string connectionString = ConfigurationManager.ConnectionStrings["DataConnection"].ConnectionString;
        private static readonly Settings app = new Settings();

        public void Add(params ApplicantSkillPoco[] items)
        {
            using (SqlConnection connection = new SqlConnection(app.connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand();
                    command.Connection = connection;
                    command.CommandText = @"INSERT INTO Applicant_Skills( Id, Applicant,Skill,Skill_Level, Start_Month,Start_Year,End_Month,End_Year )
VALUES(  @Id, @Applicant,@Skill,@Skill_Level, @Start_Month,@Start_Year,@End_Month,@End_Year )";
                    connection.Open();
                    foreach (ApplicantSkillPoco item in items)
                    {

                        command.Parameters.AddWithValue("@Id", item.Id);
                        command.Parameters.AddWithValue("@Applicant", item.Applicant);
                        command.Parameters.AddWithValue("@Skill", item.Skill);
                        command.Parameters.AddWithValue("@Skill_Level", item.SkillLevel);
                        command.Parameters.AddWithValue("@Start_Month", item.StartMonth);
                        command.Parameters.AddWithValue("@Start_Year", item.StartYear);
                        command.Parameters.AddWithValue("@End_Month", item.EndMonth);
                        command.Parameters.AddWithValue("@End_Year", item.EndYear);



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

        public IList<ApplicantSkillPoco> GetAll(params System.Linq.Expressions.Expression<Func<ApplicantSkillPoco, object>>[] navigationProperties)
        {
            using (SqlConnection connection = new SqlConnection(app.connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand();


                    command.Connection = connection;
                    command.CommandType = CommandType.Text;
                    command.CommandText = @"Select * from Applicant_Skills";
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    IList<ApplicantSkillPoco> ilist = new List<ApplicantSkillPoco>();

                    while (reader.Read())
                    {
                        ApplicantSkillPoco poco = new ApplicantSkillPoco();
                        poco.Id = reader.GetGuid(0);
                        poco.Applicant = reader.GetGuid(1);

                        poco.Skill = reader.GetString(2);
                        poco.SkillLevel = reader.GetString(3);
                        poco.StartMonth = reader.GetByte(4);
                        poco.StartYear = reader.GetInt32(5);
                        poco.EndMonth = reader.GetByte(6);
                        poco.EndYear = reader.GetInt32(7);


                        poco.TimeStamp = (byte[])reader[8];
                        ilist.Add(poco);
                        
                    }

                    return ilist;


                }
                finally { connection.Close(); }
            }
        }

        public IList<ApplicantSkillPoco> GetList(System.Linq.Expressions.Expression<Func<ApplicantSkillPoco, bool>> where, params System.Linq.Expressions.Expression<Func<ApplicantSkillPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public ApplicantSkillPoco GetSingle(System.Linq.Expressions.Expression<Func<ApplicantSkillPoco, bool>> where, params System.Linq.Expressions.Expression<Func<ApplicantSkillPoco, object>>[] navigationProperties)
        {
            IQueryable<ApplicantSkillPoco> pocos = GetAll().AsQueryable();

            return pocos.Where(where).FirstOrDefault();
            throw new NotImplementedException();
        }

        public void Remove(params ApplicantSkillPoco[] items)
        {
            using (SqlConnection connection = new SqlConnection(app.connectionString))
            {
                SqlCommand command = new SqlCommand();
                command.Connection = connection;

                foreach (ApplicantSkillPoco item in items)
                {
                    command.CommandText = @"DELETE FROM dbo.Applicant_Skills
                                        WHERE Id = @Id";
                    command.Parameters.AddWithValue("@Id", item.Id);
                    connection.Open();
                   command.ExecuteNonQuery();
                    connection.Close();
                }
            }
            //throw new NotImplementedException();
        }

        public void Update(params ApplicantSkillPoco[] items)
        {
            using (SqlConnection connection = new SqlConnection(app.connectionString))
            {
                SqlCommand command = new SqlCommand();
                command.Connection = connection;

                foreach (ApplicantSkillPoco item in items)
                {
                    command.CommandText = @"UPDATE Applicant_Skills
           SET Applicant = @Applicant,Skill= @Skill, Skill_Level= @Skill_Level,
         Start_Month=@Start_Month,Start_Year=@Start_Year,End_Month=@End_Month,End_Year=@End_Year
      
                              WHERE Id = @Id";
                    command.Parameters.AddWithValue("@Id", item.Id);
                    command.Parameters.AddWithValue("@Applicant", item.Applicant);
                    command.Parameters.AddWithValue("@Skill", item.Skill);
                    command.Parameters.AddWithValue("@Skill_Level", item.SkillLevel);
                    command.Parameters.AddWithValue("@Start_Month", item.StartMonth);
                    command.Parameters.AddWithValue("@Start_Year", item.StartYear);
                    command.Parameters.AddWithValue("@End_Month", item.EndMonth);
                    command.Parameters.AddWithValue("@End_Year", item.EndYear);
                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
               // throw new NotImplementedException();
            }
        }
    }
}
