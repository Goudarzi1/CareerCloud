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
    public class ApplicantJobApplicationRepository : BaseADO, IDataRepository<ApplicantJobApplicationPoco>
    {
        public void Add(params ApplicantJobApplicationPoco[] items)
        {

            SqlConnection Connection = new SqlConnection(_ConnString);
            using (Connection)
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = Connection;
                int rowsEffected = 0;
                foreach (ApplicantJobApplicationPoco poco in items)
                {
                    cmd.CommandText = @"INSERT INTO Applicant_Job_Applications
(Id, Applicant, Job, Application_Date) 
values (@Id, @Applicant, @Job, @Application_Date)";
                    cmd.Parameters.AddWithValue("@Id", poco.Id); cmd.Parameters.AddWithValue("@Applicant", poco.Applicant); cmd.Parameters.AddWithValue("@Job", poco.Job); cmd.Parameters.AddWithValue("@Application_Date", poco.ApplicationDate);
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

        public IList<ApplicantJobApplicationPoco> GetAll(params Expression<Func<ApplicantJobApplicationPoco, object>>[] navigationProperties)
        {
            ApplicantJobApplicationPoco[] pocos = new ApplicantJobApplicationPoco[1000];
            SqlConnection Connection = new SqlConnection(_ConnString);
            using (Connection)
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = Connection;
                cmd.CommandText = "Select * from Applicant_Job_Applications";
                Connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();


                int position = 0;
                while (reader.Read())
                {
                    ApplicantJobApplicationPoco poco = new ApplicantJobApplicationPoco();
                    poco.Id = (Guid)reader[0]; poco.Applicant = (Guid)reader[1]; poco.Job = (Guid)reader[2]; poco.ApplicationDate = (DateTime)reader[3]; poco.TimeStamp = (Byte[])reader[4];

                    pocos[position] = poco;
                    position++;
                }
                Connection.Close();
            }
            return pocos.Where(p => p != null).ToList();
        }

        public IList<ApplicantJobApplicationPoco> GetList(Expression<Func<ApplicantJobApplicationPoco, bool>> where, params Expression<Func<ApplicantJobApplicationPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public ApplicantJobApplicationPoco GetSingle(Expression<Func<ApplicantJobApplicationPoco, bool>> where, params Expression<Func<ApplicantJobApplicationPoco, object>>[] navigationProperties)
        {
            IQueryable<ApplicantJobApplicationPoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params ApplicantJobApplicationPoco[] items)
        {
            SqlConnection Connection = new SqlConnection(_ConnString);
            using (Connection)
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = Connection;
                foreach (ApplicantJobApplicationPoco poco in items)
                {
                    cmd.CommandText = @"Delete from Applicant_Job_Applications where Id=@Id ";
                    cmd.Parameters.AddWithValue("@Id", poco.Id);
                    Connection.Open();
                    cmd.ExecuteNonQuery();
                    Connection.Close();
                }
            }
        }

        public void Update(params ApplicantJobApplicationPoco[] items)
        {
            SqlConnection Connection = new SqlConnection(_ConnString);
            using (Connection)
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = Connection;
                foreach (ApplicantJobApplicationPoco poco in items)
                {
                    cmd.CommandText = @"Update Applicant_Job_Applications SET 
Applicant=@Applicant, Job=@Job, Application_Date=@Application_Date Where Id=@Id";
                    cmd.Parameters.AddWithValue("@Id", poco.Id); cmd.Parameters.AddWithValue("@Applicant", poco.Applicant); cmd.Parameters.AddWithValue("@Job", poco.Job); cmd.Parameters.AddWithValue("@Application_Date", poco.ApplicationDate);
                    Connection.Open();
                    cmd.ExecuteNonQuery();
                    Connection.Close();
                }
            }
        }
        }
    }

