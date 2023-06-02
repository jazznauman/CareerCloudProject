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
    public class CompanyJobSkillRepository
        : DataAccessLayer.IDataRepository<CompanyJobSkillPoco>
    {
        private int i;
        //   string connectionString = ConfigurationManager.ConnectionStrings["DataConnection"].ConnectionString;
        private static readonly Settings app = new Settings();

        public void Add(params CompanyJobSkillPoco[] items)
        {
            using (SqlConnection connection = new SqlConnection(app.connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand();
                    command.Connection = connection;
                    command.CommandText = @"INSERT INTO Company_Job_Skills(
                     [Id], [Job] ,[Skill] ,[Skill_Level] ,[Importance]  )
              VALUES( @Id, @Job, @Skill,@Skill_Level, @Importance )";
                    connection.Open();
                    foreach (CompanyJobSkillPoco item in items)
                    {


                        command.Parameters.AddWithValue("@Id", item.Id);
                        command.Parameters.AddWithValue("@Job", item.Job);
                        command.Parameters.AddWithValue("@Skill", item.Skill);
                        command.Parameters.AddWithValue("@Skill_Level", item.SkillLevel);
                        command.Parameters.AddWithValue("@Importance", item.Importance);


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

        public IList<CompanyJobSkillPoco> GetAll(params Expression<Func<CompanyJobSkillPoco, object>>[] navigationProperties)
        {
            using (SqlConnection connection = new SqlConnection(app.connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand();


                    command.Connection = connection;
                    command.CommandType = CommandType.Text;
                    command.CommandText = @"Select * from Company_Job_Skills";
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    IList<CompanyJobSkillPoco> ilist = new List<CompanyJobSkillPoco>();

                    while (reader.Read())
                    {
                        CompanyJobSkillPoco poco = new CompanyJobSkillPoco();
                        poco.Id = reader.GetGuid(0);

                        poco.Job = reader.GetGuid(1);


                        poco.Skill = reader.GetString(2);
                        poco.SkillLevel = reader.GetString(3);
                        poco.Importance = reader.GetInt32(4);
                        poco.TimeStamp = (byte[])reader[5];
                        ilist.Add(poco);
                        
                    }

                    return ilist;
                }
                finally { connection.Close(); }
            }
        }

        public IList<CompanyJobSkillPoco> GetList(Expression<Func<CompanyJobSkillPoco, bool>> where, params Expression<Func<CompanyJobSkillPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public CompanyJobSkillPoco GetSingle(Expression<Func<CompanyJobSkillPoco, bool>> where, params Expression<Func<CompanyJobSkillPoco, object>>[] navigationProperties)
        {
            IQueryable<CompanyJobSkillPoco> pocos = GetAll().AsQueryable();

            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params CompanyJobSkillPoco[] items)
        {
            using (SqlConnection connection = new SqlConnection(app.connectionString))
            {
                SqlCommand command = new SqlCommand();
                command.Connection = connection;

                foreach (CompanyJobSkillPoco item in items)
                {
                    command.CommandText = @"DELETE FROM Company_Job_Skills
                                        WHERE Id = @Id";
                    command.Parameters.AddWithValue("@Id", item.Id);
                    connection.Open();
                  command.ExecuteNonQuery();
                    connection.Close();
                }
            }
            //   throw new NotImplementedException();
        }

        public void Update(params CompanyJobSkillPoco[] items)
        {
            using (SqlConnection connection = new SqlConnection(app.connectionString))
            {
                SqlCommand command = new SqlCommand();
                command.Connection = connection;

                foreach (CompanyJobSkillPoco item in items)
                {
                    command.CommandText = @"UPDATE Company_Job_Skills
                                        SET Job=@Job, Skill=@Skill, Skill_Level = @Skill_Level,
                                          Importance=@Importance
                                        WHERE Id = @Id";
                    command.Parameters.AddWithValue("@Id", item.Id);
                    command.Parameters.AddWithValue("@Job", item.Job);
                    command.Parameters.AddWithValue("@Skill", item.Skill);
                    command.Parameters.AddWithValue("@Skill_Level", item.SkillLevel);
                    command.Parameters.AddWithValue("@Importance", item.Importance);
                    connection.Open();
                  command.ExecuteNonQuery();
                    connection.Close();
                }
            }
            //        throw new NotImplementedException();
        }
    }
}
