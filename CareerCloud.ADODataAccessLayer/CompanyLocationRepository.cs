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
    public class CompanyLocationRepository : BaseADO, IDataRepository<CompanyLocationPoco>
    {
        public void Add(params CompanyLocationPoco[] items)
        {

            SqlConnection Connection = new SqlConnection(_ConnString);
            using (Connection)
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = Connection;
                int rowsEffected = 0;
                foreach (CompanyLocationPoco poco in items)
                {
                    cmd.CommandText = @"INSERT INTO Company_Locations
(Id, Company, Country_Code, State_Province_Code, Street_Address, City_Town, Zip_Postal_Code) 
values (@Id, @Company, @Country_Code, @State_Province_Code, @Street_Address, @City_Town, @Zip_Postal_Code)";
                    cmd.Parameters.AddWithValue("@Id", poco.Id); cmd.Parameters.AddWithValue("@Company", poco.Company); cmd.Parameters.AddWithValue("@Country_Code", poco.CountryCode); cmd.Parameters.AddWithValue("@State_Province_Code", poco.Province); cmd.Parameters.AddWithValue("@Street_Address", poco.Street); cmd.Parameters.AddWithValue("@City_Town", poco.City); cmd.Parameters.AddWithValue("@Zip_Postal_Code", poco.PostalCode);
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

        public IList<CompanyLocationPoco> GetAll(params Expression<Func<CompanyLocationPoco, object>>[] navigationProperties)
        {


            CompanyLocationPoco[] pocos = new CompanyLocationPoco[1001];
            SqlConnection Connection = new SqlConnection(_ConnString);
            using (Connection)
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = Connection;
                cmd.CommandText = "Select * from Company_Locations";
                Connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();


                int position = 0;
                while (reader.Read())
                {
                    CompanyLocationPoco poco = new CompanyLocationPoco();

                    poco.Id = (Guid)reader[0]; poco.Company = (Guid)reader[1]; poco.CountryCode = (string)reader[2]; poco.Province = (string)reader[3]; poco.Street = (string)reader[4]; poco.City = (string)reader[5].ToString(); poco.PostalCode = (string)reader[6].ToString(); poco.TimeStamp = (Byte[])reader[7];

                    pocos[position] = poco;
                    position++;
                }
                Connection.Close();
            }
            return pocos.Where(p => p != null).ToList();
        }

        public IList<CompanyLocationPoco> GetList(Expression<Func<CompanyLocationPoco, bool>> where, params Expression<Func<CompanyLocationPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public CompanyLocationPoco GetSingle(Expression<Func<CompanyLocationPoco, bool>> where, params Expression<Func<CompanyLocationPoco, object>>[] navigationProperties)
        {

            IQueryable<CompanyLocationPoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params CompanyLocationPoco[] items)
        {

            SqlConnection Connection = new SqlConnection(_ConnString);
            using (Connection)
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = Connection;
                foreach (CompanyLocationPoco poco in items)
                {
                    cmd.CommandText = @"Delete from Company_Locations where Id=@Id ";
                    cmd.Parameters.AddWithValue("@Id", poco.Id);
                    Connection.Open();
                    cmd.ExecuteNonQuery();
                    Connection.Close();
                }
            }
        }

        public void Update(params CompanyLocationPoco[] items)
        {

            SqlConnection Connection = new SqlConnection(_ConnString);
            using (Connection)
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = Connection;
                foreach (CompanyLocationPoco poco in items)
                {
                    cmd.CommandText = @"Update Company_Locations SET 
Company=@Company, Country_Code=@Country_Code, State_Province_Code=@State_Province_Code, Street_Address=@Street_Address, City_Town=@City_Town, Zip_Postal_Code=@Zip_Postal_Code Where Id=@Id";
                    cmd.Parameters.AddWithValue("@Id", poco.Id); cmd.Parameters.AddWithValue("@Company", poco.Company); cmd.Parameters.AddWithValue("@Country_Code", poco.CountryCode); cmd.Parameters.AddWithValue("@State_Province_Code", poco.Province); cmd.Parameters.AddWithValue("@Street_Address", poco.Street); cmd.Parameters.AddWithValue("@City_Town", poco.City); cmd.Parameters.AddWithValue("@Zip_Postal_Code", poco.PostalCode);
                    Connection.Open();
                    cmd.ExecuteNonQuery();
                    Connection.Close();
                }
            }
        }
    }
}
