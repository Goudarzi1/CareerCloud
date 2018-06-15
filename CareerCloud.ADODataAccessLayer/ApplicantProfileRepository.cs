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
    public class ApplicantProfileRepository : BaseADO, IDataRepository<ApplicantProfilePoco>
    {
        public void Add(params ApplicantProfilePoco[] items)
        {
            SqlConnection Connection = new SqlConnection(_ConnString);
            using (Connection)
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = Connection;
                int rowsEffected = 0;
                foreach (ApplicantProfilePoco poco in items)
                {
                    cmd.CommandText = @"INSERT INTO Applicant_Profiles
(Id, Login, Current_Salary, Current_Rate, Currency, Country_Code, State_Province_Code, Street_Address, City_Town, Zip_Postal_Code) 
values (@Id, @Login, @Current_Salary, @Current_Rate, @Currency, @Country_Code, @State_Province_Code, @Street_Address, @City_Town, @Zip_Postal_Code)";
                    cmd.Parameters.AddWithValue("@Id", poco.Id); cmd.Parameters.AddWithValue("@Login", poco.Login); cmd.Parameters.AddWithValue("@Current_Salary", poco.CurrentSalary); cmd.Parameters.AddWithValue("@Current_Rate", poco.CurrentRate); cmd.Parameters.AddWithValue("@Currency", poco.Currency); cmd.Parameters.AddWithValue("@Country_Code", poco.Country); cmd.Parameters.AddWithValue("@State_Province_Code", poco.Province); cmd.Parameters.AddWithValue("@Street_Address", poco.Street); cmd.Parameters.AddWithValue("@City_Town", poco.City); cmd.Parameters.AddWithValue("@Zip_Postal_Code", poco.PostalCode);
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

        public IList<ApplicantProfilePoco> GetAll(params Expression<Func<ApplicantProfilePoco, object>>[] navigationProperties)
        {
            ApplicantProfilePoco[] pocos = new ApplicantProfilePoco[1000];
            SqlConnection Connection = new SqlConnection(_ConnString);
            using (Connection)
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = Connection;
                cmd.CommandText = "Select * from Applicant_Profiles";
                Connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();


                int position = 0;
                while (reader.Read())
                {
                    ApplicantProfilePoco poco = new ApplicantProfilePoco();
                    poco.Id = (Guid)reader[0]; poco.Login = (Guid)reader[1]; poco.CurrentSalary = (decimal?)reader[2]; poco.CurrentRate = (decimal?)reader[3]; poco.Currency = (string)reader[4]; poco.Country = (string)reader[5]; poco.Province = (string)reader[6]; poco.Street = (string)reader[7]; poco.City = (string)reader[8]; poco.PostalCode = (string)reader[9]; poco.TimeStamp = (Byte[])reader[10];

                    pocos[position] = poco;
                    position++;
                }
                Connection.Close();
            }
            return pocos.Where(p => p != null).ToList();
        }

        public IList<ApplicantProfilePoco> GetList(Expression<Func<ApplicantProfilePoco, bool>> where, params Expression<Func<ApplicantProfilePoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public ApplicantProfilePoco GetSingle(Expression<Func<ApplicantProfilePoco, bool>> where, params Expression<Func<ApplicantProfilePoco, object>>[] navigationProperties)
        {
            IQueryable<ApplicantProfilePoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();

        }

        public void Remove(params ApplicantProfilePoco[] items)
        {

            SqlConnection Connection = new SqlConnection(_ConnString);
            using (Connection)
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = Connection;
                foreach (ApplicantProfilePoco poco in items)
                {
                    cmd.CommandText = @"Delete from Applicant_Profiles where Id=@Id ";
                    cmd.Parameters.AddWithValue("@Id", poco.Id);
                    Connection.Open();
                    cmd.ExecuteNonQuery();
                    Connection.Close();
                }
            }
        }

        public void Update(params ApplicantProfilePoco[] items)
        {
            SqlConnection Connection = new SqlConnection(_ConnString);
            using (Connection)
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = Connection;
                foreach (ApplicantProfilePoco poco in items)
                {
                    cmd.CommandText = @"Update Applicant_Profiles SET 
Login=@Login, Current_Salary=@Current_Salary, Current_Rate=@Current_Rate, Currency=@Currency, Country_Code=@Country_Code, State_Province_Code=@State_Province_Code, Street_Address=@Street_Address, City_Town=@City_Town, Zip_Postal_Code=@Zip_Postal_Code Where Id=@Id";
                    cmd.Parameters.AddWithValue("@Id", poco.Id); cmd.Parameters.AddWithValue("@Login", poco.Login); cmd.Parameters.AddWithValue("@Current_Salary", poco.CurrentSalary); cmd.Parameters.AddWithValue("@Current_Rate", poco.CurrentRate); cmd.Parameters.AddWithValue("@Currency", poco.Currency); cmd.Parameters.AddWithValue("@Country_Code", poco.Country); cmd.Parameters.AddWithValue("@State_Province_Code", poco.Province); cmd.Parameters.AddWithValue("@Street_Address", poco.Street); cmd.Parameters.AddWithValue("@City_Town", poco.City); cmd.Parameters.AddWithValue("@Zip_Postal_Code", poco.PostalCode);
                    Connection.Open();
                    cmd.ExecuteNonQuery();
                    Connection.Close();
                }
            }
        }
    }
}
