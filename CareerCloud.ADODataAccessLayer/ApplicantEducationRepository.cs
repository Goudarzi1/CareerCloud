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
    public class ApplicantEducationRepository : BaseADO, IDataRepository<ApplicantEducationPoco>
    {
        public void Add(params ApplicantEducationPoco[] items)
        {
            //throw new NotImplementedException();
            using (_Connection)
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = _Connection;
                int rowsEffected = 0;
                foreach (ApplicantEducationPoco poco in items)
                {
                    cmd.CommandText = @"INSERT INTO Applicant_Educations
                           (Id, Applicant, Major, Certificate_Diploma, Start_Date, Completion_Date, Completion_Percent)
                           valyes (@Id, @Applicant, @Major, @Certificate_Diploma, @Start_Date, @Completion_Date, @Completion_Percent)";

                    cmd.Parameters.AddWithValue("@Id", poco.Id);
                    cmd.Parameters.AddWithValue("@Applicant", poco.Applicant);
                    cmd.Parameters.AddWithValue("@Major", poco.Major);
                    cmd.Parameters.AddWithValue("@Certificate_Diploma", poco.CertificateDiploma);
                    cmd.Parameters.AddWithValue("@Start_Date", poco.StartDate);
                    cmd.Parameters.AddWithValue("@Completion_Date", poco.CompletionDate);
                    cmd.Parameters.AddWithValue("@Completion_Percent", poco.CompletionPercent);

                    _Connection.Open();
                    rowsEffected += cmd.ExecuteNonQuery();
                    _Connection.Close();
                }
            }
        }

        public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
        {
            throw new NotImplementedException();
        }

        public IList<ApplicantEducationPoco> GetAll(params Expression<Func<ApplicantEducationPoco, object>>[] navigationProperties)
        {
            //throw new NotImplementedException();
            ApplicantEducationPoco[] pocos = new ApplicantEducationPoco[1000];
            using (_Connection)
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = _Connection;
                cmd.CommandText = "Select * from Applicant_Educations";
                _Connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();


                int position = 0;
                while (reader.Read())
                {
                    ApplicantEducationPoco poco = new ApplicantEducationPoco();
                    poco.Id = reader.GetGuid(0);
                    poco.Applicant = reader.GetGuid(1);
                    poco.Major = reader.GetString(2);
                    poco.CertificateDiploma = reader.GetString(3);
                    poco.StartDate = (DateTime?)reader[4];
                    poco.CompletionDate = (DateTime?)reader[5];
                    poco.CompletionPercent = (byte?)reader[6];
                    poco.TimeStamp =(byte[])reader[7];

                    pocos[position] = poco;
                    position++;
                }
                _Connection.Close();
            }
            return pocos.Where(p => p != null).ToList();
            
        }

        public IList<ApplicantEducationPoco> GetList(Expression<Func<ApplicantEducationPoco, bool>> where, params Expression<Func<ApplicantEducationPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public ApplicantEducationPoco GetSingle(Expression<Func<ApplicantEducationPoco, bool>> where, params Expression<Func<ApplicantEducationPoco, object>>[] navigationProperties)
        {
            // throw new NotImplementedException();
            IQueryable<ApplicantEducationPoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params ApplicantEducationPoco[] items)
        {
            //throw new NotImplementedException();
            using (_Connection)
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = _Connection;
                foreach (ApplicantEducationPoco poco in items)
                {
                    cmd.CommandText = @"Delete from Applicant_Educations Where Id=@Id ";
                    cmd.Parameters.AddWithValue("@Id", poco.Id);
                    _Connection.Open();
                    cmd.ExecuteNonQuery();
                    _Connection.Close();
                }
            }
        }

        public void Update(params ApplicantEducationPoco[] items)
        {
            //throw new NotImplementedException();
            using (_Connection)
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = _Connection;
                foreach (ApplicantEducationPoco poco in items)
                {
                    cmd.CommandText = @"Update Applicant_Educations 
                                        SET Applicant=@Applicant,
                                            Major=@Major, 
                                            Certificate_Diploma=@Certificate_Diploma, 
                                            Start_Date=@Start_Date, 
                                            Completion_Date=@Completion_Date,
                                            Completion_Percent=@Completion_Percent, 
                                            Time_Stamp=@Time_Stamp
                                        Where Id=@Id ";

                    cmd.Parameters.AddWithValue("@Applicant", poco.Applicant);
                    cmd.Parameters.AddWithValue("@Major", poco.Major);
                    cmd.Parameters.AddWithValue("@Certificate_Diploma", poco.CertificateDiploma);
                    cmd.Parameters.AddWithValue("@Start_Date", poco.StartDate);
                    cmd.Parameters.AddWithValue("@Completion_Date", poco.CompletionDate);
                    cmd.Parameters.AddWithValue("@Completion_Percent", poco.CompletionPercent);
                    cmd.Parameters.AddWithValue("@Id", poco.Id);

                    _Connection.Open();
                    cmd.ExecuteNonQuery();
                    _Connection.Close();
                }
            }
        }
    }
}