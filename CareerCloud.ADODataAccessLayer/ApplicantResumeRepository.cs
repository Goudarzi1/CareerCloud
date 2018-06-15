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
    public class ApplicantResumeRepository : BaseADO, IDataRepository<ApplicantResumePoco>
    {
        public void Add(params ApplicantResumePoco[] items)
        {

            SqlConnection Connection = new SqlConnection(_ConnString);
            using (Connection)
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = Connection;
                int rowsEffected = 0;
                foreach (ApplicantResumePoco poco in items)
                {
                    cmd.CommandText = @"INSERT INTO Applicant_Resumes
(Id, Applicant, Resume, Last_Updated) 
values (@Id, @Applicant, @Resume, @Last_Updated)";
                    cmd.Parameters.AddWithValue("@Id", poco.Id); cmd.Parameters.AddWithValue("@Applicant", poco.Applicant); cmd.Parameters.AddWithValue("@Resume", poco.Resume); cmd.Parameters.AddWithValue("@Last_Updated", poco.LastUpdated);
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

        public IList<ApplicantResumePoco> GetAll(params Expression<Func<ApplicantResumePoco, object>>[] navigationProperties)
        {

            ApplicantResumePoco[] pocos = new ApplicantResumePoco[1000];
            SqlConnection Connection = new SqlConnection(_ConnString);
            using (Connection)
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = Connection;
                cmd.CommandText = "Select * from Applicant_Resumes";
                Connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();


                int position = 0;
                while (reader.Read())
                {
                    ApplicantResumePoco poco = new ApplicantResumePoco();
                    var lastUpdate = reader[3] == DBNull.Value ? default(DateTime?) : Convert.ToDateTime(reader[3]);
                    poco.Id = (Guid)reader[0]; poco.Applicant = (Guid)reader[1]; poco.Resume = (string)reader[2]; poco.LastUpdated = lastUpdate;

                    pocos[position] = poco;
                    position++;
                }
                Connection.Close();
            }
            return pocos.Where(p => p != null).ToList();
        }

        public IList<ApplicantResumePoco> GetList(Expression<Func<ApplicantResumePoco, bool>> where, params Expression<Func<ApplicantResumePoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public ApplicantResumePoco GetSingle(Expression<Func<ApplicantResumePoco, bool>> where, params Expression<Func<ApplicantResumePoco, object>>[] navigationProperties)
        {

            IQueryable<ApplicantResumePoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params ApplicantResumePoco[] items)
        {


            SqlConnection Connection = new SqlConnection(_ConnString);
            using (Connection)
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = Connection;
                foreach (ApplicantResumePoco poco in items)
                {
                    cmd.CommandText = @"Delete from Applicant_Resumes where Id=@Id ";
                    cmd.Parameters.AddWithValue("@Id", poco.Id);
                    Connection.Open();
                    cmd.ExecuteNonQuery();
                    Connection.Close();
                }
            }
        }

        public void Update(params ApplicantResumePoco[] items)
        {

            SqlConnection Connection = new SqlConnection(_ConnString);
            using (Connection)
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = Connection;
                foreach (ApplicantResumePoco poco in items)
                {
                    cmd.CommandText = @"Update Applicant_Resumes SET 
Applicant=@Applicant, Resume=@Resume, Last_Updated=@Last_Updated Where Id=@Id";
                    cmd.Parameters.AddWithValue("@Id", poco.Id); cmd.Parameters.AddWithValue("@Applicant", poco.Applicant); cmd.Parameters.AddWithValue("@Resume", poco.Resume); cmd.Parameters.AddWithValue("@Last_Updated", poco.LastUpdated);
                    Connection.Open();
                    cmd.ExecuteNonQuery();
                    Connection.Close();
                }
            }
        }
    }
}
