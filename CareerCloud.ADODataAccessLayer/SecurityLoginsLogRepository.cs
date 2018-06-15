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
    public class SecurityLoginsLogRepository : BaseADO, IDataRepository<SecurityLoginsLogPoco>
    {
        public void Add(params SecurityLoginsLogPoco[] items)
        {


            SqlConnection Connection = new SqlConnection(_ConnString);
            using (Connection)
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = Connection;
                int rowsEffected = 0;
                foreach (SecurityLoginsLogPoco poco in items)
                {
                    cmd.CommandText = @"INSERT INTO Security_Logins_Log
(Id, Login, Source_IP, Logon_Date, Is_Succesful) 
values (@Id, @Login, @Source_IP, @Logon_Date, @Is_Succesful)";
                    cmd.Parameters.AddWithValue("@Id", poco.Id); cmd.Parameters.AddWithValue("@Login", poco.Login); cmd.Parameters.AddWithValue("@Source_IP", poco.SourceIP); cmd.Parameters.AddWithValue("@Logon_Date", poco.LogonDate); cmd.Parameters.AddWithValue("@Is_Succesful", poco.IsSuccesful);
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

        public IList<SecurityLoginsLogPoco> GetAll(params Expression<Func<SecurityLoginsLogPoco, object>>[] navigationProperties)
        {

            SecurityLoginsLogPoco[] pocos = new SecurityLoginsLogPoco[2000];
            SqlConnection Connection = new SqlConnection(_ConnString);
            using (Connection)
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = Connection;
                cmd.CommandText = "Select * from Security_Logins_Log";
                Connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();


                int position = 0;
                while (reader.Read())
                {
                    SecurityLoginsLogPoco poco = new SecurityLoginsLogPoco();
                    poco.Id = (Guid)reader[0]; poco.Login = (Guid)reader[1]; poco.SourceIP = (string)reader[2]; poco.LogonDate = (DateTime)reader[3]; poco.IsSuccesful = (Boolean)reader[4];

                    pocos[position] = poco;
                    position++;
                }
                Connection.Close();
            }
            return pocos.Where(p => p != null).ToList();
        }

        public IList<SecurityLoginsLogPoco> GetList(Expression<Func<SecurityLoginsLogPoco, bool>> where, params Expression<Func<SecurityLoginsLogPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public SecurityLoginsLogPoco GetSingle(Expression<Func<SecurityLoginsLogPoco, bool>> where, params Expression<Func<SecurityLoginsLogPoco, object>>[] navigationProperties)
        {

            IQueryable<SecurityLoginsLogPoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();

        }

        public void Remove(params SecurityLoginsLogPoco[] items)
        {

            SqlConnection Connection = new SqlConnection(_ConnString);
            using (Connection)
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = Connection;
                foreach (SecurityLoginsLogPoco poco in items)
                {
                    cmd.CommandText = @"Delete from Security_Logins_Log where Id=@Id ";
                    cmd.Parameters.AddWithValue("@Id", poco.Id);
                    Connection.Open();
                    cmd.ExecuteNonQuery();
                    Connection.Close();
                }
            }
        }

        public void Update(params SecurityLoginsLogPoco[] items)
        {

            SqlConnection Connection = new SqlConnection(_ConnString);
            using (Connection)
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = Connection;
                foreach (SecurityLoginsLogPoco poco in items)
                {
                    cmd.CommandText = @"Update Security_Logins_Log SET 
Login=@Login, Source_IP=@Source_IP, Logon_Date=@Logon_Date, Is_Succesful=@Is_Succesful Where Id=@Id";
                    cmd.Parameters.AddWithValue("@Id", poco.Id); cmd.Parameters.AddWithValue("@Login", poco.Login); cmd.Parameters.AddWithValue("@Source_IP", poco.SourceIP); cmd.Parameters.AddWithValue("@Logon_Date", poco.LogonDate); cmd.Parameters.AddWithValue("@Is_Succesful", poco.IsSuccesful);
                    Connection.Open();
                    cmd.ExecuteNonQuery();
                    Connection.Close();
                }
            }

        }
    }
}
