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
    public class CompanyDescriptionRepository : BaseADO, IDataRepository<CompanyDescriptionPoco>
    {
        public void Add(params CompanyDescriptionPoco[] items)
        {
            SqlConnection Connection = new SqlConnection(_ConnString);
            using (Connection)
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = Connection;
                int rowsEffected = 0;
                foreach (CompanyDescriptionPoco poco in items)
                {
                    cmd.CommandText = @"INSERT INTO Company_Descriptions
(Id, Company, LanguageID, Company_Name, Company_Description) 
values (@Id, @Company, @LanguageID, @Company_Name, @Company_Description)";
                    cmd.Parameters.AddWithValue("@Id", poco.Id); cmd.Parameters.AddWithValue("@Company", poco.Company); cmd.Parameters.AddWithValue("@LanguageID", poco.LanguageId); cmd.Parameters.AddWithValue("@Company_Name", poco.CompanyName); cmd.Parameters.AddWithValue("@Company_Description", poco.CompanyDescription);
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

        public IList<CompanyDescriptionPoco> GetAll(params Expression<Func<CompanyDescriptionPoco, object>>[] navigationProperties)
        {

            CompanyDescriptionPoco[] pocos = new CompanyDescriptionPoco[1000];
            SqlConnection Connection = new SqlConnection(_ConnString);
            using (Connection)
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = Connection;
                cmd.CommandText = "Select * from Company_Descriptions";
                Connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();


                int position = 0;
                while (reader.Read())
                {
                    CompanyDescriptionPoco poco = new CompanyDescriptionPoco();
                    poco.Id = (Guid)reader[0]; poco.Company = (Guid)reader[1]; poco.LanguageId = (string)reader[2]; poco.CompanyName = (string)reader[3]; poco.CompanyDescription = (string)reader[4]; poco.TimeStamp = (Byte[])reader[5];

                    pocos[position] = poco;
                    position++;
                }
                Connection.Close();
            }
            return pocos.Where(p => p != null).ToList();
        }

        public IList<CompanyDescriptionPoco> GetList(Expression<Func<CompanyDescriptionPoco, bool>> where, params Expression<Func<CompanyDescriptionPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public CompanyDescriptionPoco GetSingle(Expression<Func<CompanyDescriptionPoco, bool>> where, params Expression<Func<CompanyDescriptionPoco, object>>[] navigationProperties)
        {
            IQueryable<CompanyDescriptionPoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params CompanyDescriptionPoco[] items)
        {

            SqlConnection Connection = new SqlConnection(_ConnString);
            using (Connection)
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = Connection;
                foreach (CompanyDescriptionPoco poco in items)
                {
                    cmd.CommandText = @"Delete from Company_Descriptions where Id=@Id ";
                    cmd.Parameters.AddWithValue("@Id", poco.Id);
                    Connection.Open();
                    cmd.ExecuteNonQuery();
                    Connection.Close();
                }
            }
        }

        public void Update(params CompanyDescriptionPoco[] items)
        {

            SqlConnection Connection = new SqlConnection(_ConnString);
            using (Connection)
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = Connection;
                foreach (CompanyDescriptionPoco poco in items)
                {
                    cmd.CommandText = @"Update Company_Descriptions SET 
Company=@Company, LanguageID=@LanguageID, Company_Name=@Company_Name, Company_Description=@Company_Description Where Id=@Id";
                    cmd.Parameters.AddWithValue("@Id", poco.Id); cmd.Parameters.AddWithValue("@Company", poco.Company); cmd.Parameters.AddWithValue("@LanguageID", poco.LanguageId); cmd.Parameters.AddWithValue("@Company_Name", poco.CompanyName); cmd.Parameters.AddWithValue("@Company_Description", poco.CompanyDescription);
                    Connection.Open();
                    cmd.ExecuteNonQuery();
                    Connection.Close();
                }
            }
        }
    }
}
