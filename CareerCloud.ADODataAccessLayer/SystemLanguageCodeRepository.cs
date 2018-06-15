using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareerCloud.ADODataAccessLayer
{
    public class SystemLanguageCodeRepository : BaseADO, IDataRepository<SystemLanguageCodePoco>
    {
        public void Add(params SystemLanguageCodePoco[] items)
        {


            SqlConnection Connection = new SqlConnection(_ConnString);
            using (Connection)
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = Connection;
                int rowsEffected = 0;
                foreach (SystemLanguageCodePoco poco in items)
                {
                    cmd.CommandText = @"INSERT INTO System_Language_Codes
(LanguageID, Name, Native_Name) 
values (@LanguageID, @Name, @Native_Name)";
                    cmd.Parameters.AddWithValue("@LanguageID", poco.LanguageID); cmd.Parameters.AddWithValue("@Name", poco.Name); cmd.Parameters.AddWithValue("@Native_Name", poco.NativeName);
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

        public IList<SystemLanguageCodePoco> GetAll(params System.Linq.Expressions.Expression<Func<SystemLanguageCodePoco, object>>[] navigationProperties)
        {

            SystemLanguageCodePoco[] pocos = new SystemLanguageCodePoco[1001];
            SqlConnection Connection = new SqlConnection(_ConnString);
            using (Connection)
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = Connection;
                cmd.CommandText = "Select * from System_Language_Codes";
                Connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();


                int position = 0;
                while (reader.Read())
                {
                    SystemLanguageCodePoco poco = new SystemLanguageCodePoco();
                    poco.LanguageID = (string)reader[0]; poco.Name = (string)reader[1]; poco.NativeName = (string)reader[2];

                    pocos[position] = poco;
                    position++;
                }
                Connection.Close();
            }
            return pocos.Where(p => p != null).ToList();
        }

        public IList<SystemLanguageCodePoco> GetList(System.Linq.Expressions.Expression<Func<SystemLanguageCodePoco, bool>> where, params System.Linq.Expressions.Expression<Func<SystemLanguageCodePoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public SystemLanguageCodePoco GetSingle(System.Linq.Expressions.Expression<Func<SystemLanguageCodePoco, bool>> where, params System.Linq.Expressions.Expression<Func<SystemLanguageCodePoco, object>>[] navigationProperties)
        {

            IQueryable<SystemLanguageCodePoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();

        }

        public void Remove(params SystemLanguageCodePoco[] items)
        {
            SqlConnection Connection = new SqlConnection(_ConnString);
            using (Connection)
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = Connection;
                foreach (SystemLanguageCodePoco poco in items)
                {
                    cmd.CommandText = @"Delete from System_Language_Codes where LanguageID=@LanguageID ";
                    cmd.Parameters.AddWithValue("@LanguageID", poco.LanguageID);
                    Connection.Open();
                    cmd.ExecuteNonQuery();
                    Connection.Close();
                }
            }
        }

        public void Update(params SystemLanguageCodePoco[] items)
        {

            SqlConnection Connection = new SqlConnection(_ConnString);
            using (Connection)
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = Connection;
                foreach (SystemLanguageCodePoco poco in items)
                {
                    cmd.CommandText = @"Update System_Language_Codes SET 
LanguageID=@LanguageID, Name=@Name, Native_Name=@Native_Name Where LanguageID=@LanguageID";
                    cmd.Parameters.AddWithValue("@LanguageID", poco.LanguageID); cmd.Parameters.AddWithValue("@Name", poco.Name); cmd.Parameters.AddWithValue("@Native_Name", poco.NativeName);
                    Connection.Open();
                    cmd.ExecuteNonQuery();
                    Connection.Close();
                }
            }

        }
    }
}
