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
    public class CompanyJobDescriptionRepository : BaseADO, IDataRepository<CompanyJobDescriptionPoco>
    {
        public void Add(params CompanyJobDescriptionPoco[] items)
        {


            SqlConnection Connection = new SqlConnection(_ConnString);
            using (Connection)
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = Connection;
                int rowsEffected = 0;
                foreach (CompanyJobDescriptionPoco poco in items)
                {
                    cmd.CommandText = @"INSERT INTO Company_Jobs_Descriptions
(Id, Job, Job_Name, Job_Descriptions) 
values (@Id, @Job, @Job_Name, @Job_Descriptions)";
                    cmd.Parameters.AddWithValue("@Id", poco.Id); cmd.Parameters.AddWithValue("@Job", poco.Job); cmd.Parameters.AddWithValue("@Job_Name", poco.JobName); cmd.Parameters.AddWithValue("@Job_Descriptions", poco.JobDescriptions);
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

        public IList<CompanyJobDescriptionPoco> GetAll(params Expression<Func<CompanyJobDescriptionPoco, object>>[] navigationProperties)
        {
            CompanyJobDescriptionPoco[] pocos = new CompanyJobDescriptionPoco[1001];
            SqlConnection Connection = new SqlConnection(_ConnString);
            using (Connection)
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = Connection;
                cmd.CommandText = "Select * from Company_Jobs_Descriptions";
                Connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();


                int position = 0;
                while (reader.Read())
                {
                    CompanyJobDescriptionPoco poco = new CompanyJobDescriptionPoco();
                    poco.Id = (Guid)reader[0]; poco.Job = (Guid)reader[1]; poco.JobName = (string)reader[2]; poco.JobDescriptions = (string)reader[3]; poco.TimeStamp = (Byte[])reader[4];

                    pocos[position] = poco;
                    position++;
                }
                Connection.Close();
            }
            return pocos.Where(p => p != null).ToList();
        }

        public IList<CompanyJobDescriptionPoco> GetList(Expression<Func<CompanyJobDescriptionPoco, bool>> where, params Expression<Func<CompanyJobDescriptionPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public CompanyJobDescriptionPoco GetSingle(Expression<Func<CompanyJobDescriptionPoco, bool>> where, params Expression<Func<CompanyJobDescriptionPoco, object>>[] navigationProperties)
        {
            IQueryable<CompanyJobDescriptionPoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params CompanyJobDescriptionPoco[] items)
        {

            SqlConnection Connection = new SqlConnection(_ConnString);
            using (Connection)
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = Connection;
                foreach (CompanyJobDescriptionPoco poco in items)
                {
                    cmd.CommandText = @"Delete from Company_Jobs_Descriptions where Id=@Id ";
                    cmd.Parameters.AddWithValue("@Id", poco.Id);
                    Connection.Open();
                    cmd.ExecuteNonQuery();
                    Connection.Close();
                }
            }
        }

        public void Update(params CompanyJobDescriptionPoco[] items)
        {

            SqlConnection Connection = new SqlConnection(_ConnString);
            using (Connection)
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = Connection;
                foreach (CompanyJobDescriptionPoco poco in items)
                {
                    cmd.CommandText = @"Update Company_Jobs_Descriptions SET 
Job=@Job, Job_Name=@Job_Name, Job_Descriptions=@Job_Descriptions Where Id=@Id";
                    cmd.Parameters.AddWithValue("@Id", poco.Id); cmd.Parameters.AddWithValue("@Job", poco.Job); cmd.Parameters.AddWithValue("@Job_Name", poco.JobName); cmd.Parameters.AddWithValue("@Job_Descriptions", poco.JobDescriptions);
                    Connection.Open();
                    cmd.ExecuteNonQuery();
                    Connection.Close();
                }
            }

        }
    }
}
