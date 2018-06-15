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
    public class CompanyJobRepository : BaseADO, IDataRepository<CompanyJobPoco>
    {
        public void Add(params CompanyJobPoco[] items)
        {

            SqlConnection Connection = new SqlConnection(_ConnString);
            using (Connection)
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = Connection;
                int rowsEffected = 0;
                foreach (CompanyJobPoco poco in items)
                {
                    cmd.CommandText = @"INSERT INTO Company_Jobs
(Id, Company, Profile_Created, Is_Inactive, Is_Company_Hidden) 
values (@Id, @Company, @Profile_Created, @Is_Inactive, @Is_Company_Hidden)";
                    cmd.Parameters.AddWithValue("@Id", poco.Id); cmd.Parameters.AddWithValue("@Company", poco.Company); cmd.Parameters.AddWithValue("@Profile_Created", poco.ProfileCreated); cmd.Parameters.AddWithValue("@Is_Inactive", poco.IsInactive); cmd.Parameters.AddWithValue("@Is_Company_Hidden", poco.IsCompanyHidden);
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

        public IList<CompanyJobPoco> GetAll(params Expression<Func<CompanyJobPoco, object>>[] navigationProperties)
        {

            CompanyJobPoco[] pocos = new CompanyJobPoco[1001];
            SqlConnection Connection = new SqlConnection(_ConnString);
            using (Connection)
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = Connection;
                cmd.CommandText = "Select * from Company_Jobs";
                Connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();


                int position = 0;
                while (reader.Read())
                {
                    CompanyJobPoco poco = new CompanyJobPoco();
                    poco.Id = (Guid)reader[0]; poco.Company = (Guid)reader[1]; poco.ProfileCreated = (DateTime)reader[2]; poco.IsInactive = (Boolean)reader[3]; poco.IsCompanyHidden = (Boolean)reader[4]; poco.TimeStamp = (Byte[])reader[5];

                    pocos[position] = poco;
                    position++;
                }
                Connection.Close();
            }
            return pocos.Where(p => p != null).ToList();
        }

        public IList<CompanyJobPoco> GetList(Expression<Func<CompanyJobPoco, bool>> where, params Expression<Func<CompanyJobPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public CompanyJobPoco GetSingle(Expression<Func<CompanyJobPoco, bool>> where, params Expression<Func<CompanyJobPoco, object>>[] navigationProperties)
        {
            IQueryable<CompanyJobPoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params CompanyJobPoco[] items)
        {

            SqlConnection Connection = new SqlConnection(_ConnString);
            using (Connection)
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = Connection;
                foreach (CompanyJobPoco poco in items)
                {
                    cmd.CommandText = @"Delete from Company_Jobs where Id=@Id ";
                    cmd.Parameters.AddWithValue("@Id", poco.Id);
                    Connection.Open();
                    cmd.ExecuteNonQuery();
                    Connection.Close();
                }
            }
        }

        public void Update(params CompanyJobPoco[] items)
        {

            SqlConnection Connection = new SqlConnection(_ConnString);
            using (Connection)
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = Connection;
                foreach (CompanyJobPoco poco in items)
                {
                    cmd.CommandText = @"Update Company_Jobs SET 
Company=@Company, Profile_Created=@Profile_Created, Is_Inactive=@Is_Inactive, Is_Company_Hidden=@Is_Company_Hidden Where Id=@Id";
                    cmd.Parameters.AddWithValue("@Id", poco.Id); cmd.Parameters.AddWithValue("@Company", poco.Company); cmd.Parameters.AddWithValue("@Profile_Created", poco.ProfileCreated); cmd.Parameters.AddWithValue("@Is_Inactive", poco.IsInactive); cmd.Parameters.AddWithValue("@Is_Company_Hidden", poco.IsCompanyHidden);
                    Connection.Open();
                    cmd.ExecuteNonQuery();
                    Connection.Close();
                }
            }

        }
    }
}
