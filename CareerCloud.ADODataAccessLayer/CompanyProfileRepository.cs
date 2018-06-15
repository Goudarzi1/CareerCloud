using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CareerCloud.ADODataAccessLayer
{
    public class CompanyProfileRepository : BaseADO, IDataRepository<CompanyProfilePoco>
    {
        public void Add(params CompanyProfilePoco[] items)
        {
            SqlConnection Connection = new SqlConnection(_ConnString);
            using (Connection)
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = Connection;
                int rowsEffected = 0;
                foreach (CompanyProfilePoco poco in items)
                {
                    cmd.CommandText = @"INSERT INTO Company_Profiles
(Id, Registration_Date, Company_Website, Contact_Phone, Contact_Name, Company_Logo) 
values (@Id, @Registration_Date, @Company_Website, @Contact_Phone, @Contact_Name, @Company_Logo)";
                    cmd.Parameters.AddWithValue("@Id", poco.Id); cmd.Parameters.AddWithValue("@Registration_Date", poco.RegistrationDate); cmd.Parameters.AddWithValue("@Company_Website", poco.CompanyWebsite); cmd.Parameters.AddWithValue("@Contact_Phone", poco.ContactPhone); cmd.Parameters.AddWithValue("@Contact_Name", poco.ContactName); cmd.Parameters.AddWithValue("@Company_Logo", poco.CompanyLogo);
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

        public IList<CompanyProfilePoco> GetAll(params Expression<Func<CompanyProfilePoco, object>>[] navigationProperties)
        {
            CompanyProfilePoco[] pocos = new CompanyProfilePoco[1001];
            SqlConnection Connection = new SqlConnection(_ConnString);
            using (Connection)
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = Connection;
                cmd.CommandText = "Select * from Company_Profiles";
                Connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();


                int position = 0;
                while (reader.Read())
                {
                    CompanyProfilePoco poco = new CompanyProfilePoco();

                
                    poco.Id = (Guid)reader[0]; poco.RegistrationDate = (DateTime)reader[1]; poco.CompanyWebsite = (string)reader[2].ToString(); poco.ContactPhone = (string)reader[3]; poco.ContactName = (string)reader[4].ToString(); poco.CompanyLogo = (byte[])null; poco.TimeStamp = (Byte[])reader[6];

                    pocos[position] = poco;
                    position++;
                }
                Connection.Close();
            }
            return pocos.Where(p => p != null).ToList();
        }

        public IList<CompanyProfilePoco> GetList(Expression<Func<CompanyProfilePoco, bool>> where, params Expression<Func<CompanyProfilePoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public CompanyProfilePoco GetSingle(Expression<Func<CompanyProfilePoco, bool>> where, params Expression<Func<CompanyProfilePoco, object>>[] navigationProperties)
        {

            IQueryable<CompanyProfilePoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params CompanyProfilePoco[] items)
        {


            SqlConnection Connection = new SqlConnection(_ConnString);
            using (Connection)
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = Connection;
                foreach (CompanyProfilePoco poco in items)
                {
                    cmd.CommandText = @"Delete from Company_Profiles where Id=@Id ";
                    cmd.Parameters.AddWithValue("@Id", poco.Id);
                    Connection.Open();
                    cmd.ExecuteNonQuery();
                    Connection.Close();
                }
            }
        }

        public void Update(params CompanyProfilePoco[] items)
        {

            SqlConnection Connection = new SqlConnection(_ConnString);
            using (Connection)
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = Connection;
                foreach (CompanyProfilePoco poco in items)
                {
                    cmd.CommandText = @"Update Company_Profiles SET 
Registration_Date=@Registration_Date, Company_Website=@Company_Website, Contact_Phone=@Contact_Phone, Contact_Name=@Contact_Name, Company_Logo=@Company_Logo Where Id=@Id";
                    cmd.Parameters.AddWithValue("@Id", poco.Id); cmd.Parameters.AddWithValue("@Registration_Date", poco.RegistrationDate); cmd.Parameters.AddWithValue("@Company_Website", poco.CompanyWebsite); cmd.Parameters.AddWithValue("@Contact_Phone", poco.ContactPhone); cmd.Parameters.AddWithValue("@Contact_Name", poco.ContactName); cmd.Parameters.AddWithValue("@Company_Logo", poco.CompanyLogo);
                    Connection.Open();
                    cmd.ExecuteNonQuery();
                    Connection.Close();
                }
            }
        }
    }
}
