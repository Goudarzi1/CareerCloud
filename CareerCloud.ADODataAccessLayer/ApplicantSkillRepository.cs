using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CareerCloud.ADODataAccessLayer
{
    public class ApplicantSkillRepository : BaseADO, IDataRepository<ApplicantSkillPoco>
    {
        public void Add(params ApplicantSkillPoco[] items)
        {
            SqlConnection Connection = new SqlConnection(_ConnString);
            using (Connection)
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = Connection;
                int rowsEffected = 0;
                foreach (ApplicantSkillPoco poco in items)
                {
                    cmd.CommandText = @"INSERT INTO Applicant_Skills
(Id, Applicant, Skill, Skill_Level, Start_Month, Start_Year, End_Month, End_Year) 
values (@Id, @Applicant, @Skill, @Skill_Level, @Start_Month, @Start_Year, @End_Month, @End_Year)";
                    cmd.Parameters.AddWithValue("@Id", poco.Id); cmd.Parameters.AddWithValue("@Applicant", poco.Applicant); cmd.Parameters.AddWithValue("@Skill", poco.Skill); cmd.Parameters.AddWithValue("@Skill_Level", poco.SkillLevel); cmd.Parameters.AddWithValue("@Start_Month", poco.StartMonth); cmd.Parameters.AddWithValue("@Start_Year", poco.StartYear); cmd.Parameters.AddWithValue("@End_Month", poco.EndMonth); cmd.Parameters.AddWithValue("@End_Year", poco.EndYear);
                    Connection.Open();
                    rowsEffected += cmd.ExecuteNonQuery();
                    Connection.Close();
                }
            }
        }

        public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
        {
            throw new NotImplementedException();
        }

        public IList<ApplicantSkillPoco> GetAll(params Expression<Func<ApplicantSkillPoco, object>>[] navigationProperties)
        {
            ApplicantSkillPoco[] pocos = new ApplicantSkillPoco[1000];
            SqlConnection Connection = new SqlConnection(_ConnString);
            using (Connection)
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = Connection;
                cmd.CommandText = "Select * from Applicant_Skills";
                Connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();


                int position = 0;
                while (reader.Read())
                {
                    ApplicantSkillPoco poco = new ApplicantSkillPoco();
                    poco.Id = (Guid)reader[0]; poco.Applicant = (Guid)reader[1]; poco.Skill = (string)reader[2]; poco.SkillLevel = (string)reader[3]; poco.StartMonth = (byte)reader[4]; poco.StartYear = (int)reader[5]; poco.EndMonth = (byte)reader[6]; poco.EndYear = (int)reader[7]; poco.TimeStamp = (Byte[])reader[8];

                    pocos[position] = poco;
                    position++;
                }
                Connection.Close();
            }
            return pocos.Where(p => p != null).ToList();
        }

        public IList<ApplicantSkillPoco> GetList(Expression<Func<ApplicantSkillPoco, bool>> where, params Expression<Func<ApplicantSkillPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public ApplicantSkillPoco GetSingle(Expression<Func<ApplicantSkillPoco, bool>> where, params Expression<Func<ApplicantSkillPoco, object>>[] navigationProperties)
        {

            IQueryable<ApplicantSkillPoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params ApplicantSkillPoco[] items)
        {
            SqlConnection Connection = new SqlConnection(_ConnString);
            using (Connection)
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = Connection;
                foreach (ApplicantSkillPoco poco in items)
                {
                    cmd.CommandText = @"Delete from Applicant_Skills where Id=@Id ";
                    cmd.Parameters.AddWithValue("@Id", poco.Id);
                    Connection.Open();
                    cmd.ExecuteNonQuery();
                    Connection.Close();
                }
            }
        }

        public void Update(params ApplicantSkillPoco[] items)
        {

            SqlConnection Connection = new SqlConnection(_ConnString);
            using (Connection)
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = Connection;
                foreach (ApplicantSkillPoco poco in items)
                {
                    cmd.CommandText = @"Update Applicant_Skills SET 
Applicant=@Applicant, Skill=@Skill, Skill_Level=@Skill_Level, Start_Month=@Start_Month, Start_Year=@Start_Year, End_Month=@End_Month, End_Year=@End_Year Where Id=@Id";
                    cmd.Parameters.AddWithValue("@Id", poco.Id); cmd.Parameters.AddWithValue("@Applicant", poco.Applicant); cmd.Parameters.AddWithValue("@Skill", poco.Skill); cmd.Parameters.AddWithValue("@Skill_Level", poco.SkillLevel); cmd.Parameters.AddWithValue("@Start_Month", poco.StartMonth); cmd.Parameters.AddWithValue("@Start_Year", poco.StartYear); cmd.Parameters.AddWithValue("@End_Month", poco.EndMonth); cmd.Parameters.AddWithValue("@End_Year", poco.EndYear);
                    Connection.Open();
                    cmd.ExecuteNonQuery();
                    Connection.Close();
                }
            }

        }
    }
}
