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
    public class ApplicantWorkHistoryRepository : BaseADO, IDataRepository<ApplicantWorkHistoryPoco>
    {
        public void Add(params ApplicantWorkHistoryPoco[] items)
        {

            SqlConnection Connection = new SqlConnection(_ConnString);
            using (Connection)
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = Connection;
                int rowsEffected = 0;
                foreach (ApplicantWorkHistoryPoco poco in items)
                {
                    cmd.CommandText = @"INSERT INTO Applicant_Work_History
(Id, Applicant, Company_Name, Country_Code, Location, Job_Title, Job_Description, Start_Month, Start_Year, End_Month, End_Year) 
values (@Id, @Applicant, @Company_Name, @Country_Code, @Location, @Job_Title, @Job_Description, @Start_Month, @Start_Year, @End_Month, @End_Year)";
                    cmd.Parameters.AddWithValue("@Id", poco.Id); cmd.Parameters.AddWithValue("@Applicant", poco.Applicant); cmd.Parameters.AddWithValue("@Company_Name", poco.CompanyName); cmd.Parameters.AddWithValue("@Country_Code", poco.CountryCode); cmd.Parameters.AddWithValue("@Location", poco.Location); cmd.Parameters.AddWithValue("@Job_Title", poco.JobTitle); cmd.Parameters.AddWithValue("@Job_Description", poco.JobDescription); cmd.Parameters.AddWithValue("@Start_Month", poco.StartMonth); cmd.Parameters.AddWithValue("@Start_Year", poco.StartYear); cmd.Parameters.AddWithValue("@End_Month", poco.EndMonth); cmd.Parameters.AddWithValue("@End_Year", poco.EndYear);
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

        public IList<ApplicantWorkHistoryPoco> GetAll(params Expression<Func<ApplicantWorkHistoryPoco, object>>[] navigationProperties)
        {

            ApplicantWorkHistoryPoco[] pocos = new ApplicantWorkHistoryPoco[1000];
            SqlConnection Connection = new SqlConnection(_ConnString);
            using (Connection)
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = Connection;
                cmd.CommandText = "Select * from Applicant_Work_History";
                Connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();


                int position = 0;
                while (reader.Read())
                {
                    ApplicantWorkHistoryPoco poco = new ApplicantWorkHistoryPoco();
                    poco.Id = (Guid)reader[0]; poco.Applicant = (Guid)reader[1]; poco.CompanyName = (string)reader[2]; poco.CountryCode = (string)reader[3]; poco.Location = (string)reader[4]; poco.JobTitle = (string)reader[5]; poco.JobDescription = (string)reader[6]; poco.StartMonth = (short)reader[7]; poco.StartYear = (int)reader[8]; poco.EndMonth = (short)reader[9]; poco.EndYear = (int)reader[10]; poco.TimeStamp = (Byte[])reader[11];

                    pocos[position] = poco;
                    position++;
                }
                Connection.Close();
            }
            return pocos.Where(p => p != null).ToList();
        }

        public IList<ApplicantWorkHistoryPoco> GetList(Expression<Func<ApplicantWorkHistoryPoco, bool>> where, params Expression<Func<ApplicantWorkHistoryPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public ApplicantWorkHistoryPoco GetSingle(Expression<Func<ApplicantWorkHistoryPoco, bool>> where, params Expression<Func<ApplicantWorkHistoryPoco, object>>[] navigationProperties)
        {
            
            IQueryable<ApplicantWorkHistoryPoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params ApplicantWorkHistoryPoco[] items)
        {

            SqlConnection Connection = new SqlConnection(_ConnString);
            using (Connection)
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = Connection;
                foreach (ApplicantWorkHistoryPoco poco in items)
                {
                    cmd.CommandText = @"Delete from Applicant_Work_History where Id=@Id ";
                    cmd.Parameters.AddWithValue("@Id", poco.Id);
                    Connection.Open();
                    cmd.ExecuteNonQuery();
                    Connection.Close();
                }
            }
        }

        public void Update(params ApplicantWorkHistoryPoco[] items)
        {


            SqlConnection Connection = new SqlConnection(_ConnString);
            using (Connection)
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = Connection;
                foreach (ApplicantWorkHistoryPoco poco in items)
                {
                    cmd.CommandText = @"Update Applicant_Work_History SET 
Applicant=@Applicant, Company_Name=@Company_Name, Country_Code=@Country_Code, Location=@Location, Job_Title=@Job_Title, Job_Description=@Job_Description, Start_Month=@Start_Month, Start_Year=@Start_Year, End_Month=@End_Month, End_Year=@End_Year Where Id=@Id";
                    cmd.Parameters.AddWithValue("@Id", poco.Id); cmd.Parameters.AddWithValue("@Applicant", poco.Applicant); cmd.Parameters.AddWithValue("@Company_Name", poco.CompanyName); cmd.Parameters.AddWithValue("@Country_Code", poco.CountryCode); cmd.Parameters.AddWithValue("@Location", poco.Location); cmd.Parameters.AddWithValue("@Job_Title", poco.JobTitle); cmd.Parameters.AddWithValue("@Job_Description", poco.JobDescription); cmd.Parameters.AddWithValue("@Start_Month", poco.StartMonth); cmd.Parameters.AddWithValue("@Start_Year", poco.StartYear); cmd.Parameters.AddWithValue("@End_Month", poco.EndMonth); cmd.Parameters.AddWithValue("@End_Year", poco.EndYear);
                    Connection.Open();
                    cmd.ExecuteNonQuery();
                    Connection.Close();
                }
            }
        }
    }
}
