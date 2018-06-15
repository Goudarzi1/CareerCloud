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
    public class SecurityLoginRepository : BaseADO, IDataRepository<SecurityLoginPoco>
    {
        public void Add(params SecurityLoginPoco[] items)
        {


            SqlConnection Connection = new SqlConnection(_ConnString);
            using (Connection)
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = Connection;
                int rowsEffected = 0;
                foreach (SecurityLoginPoco poco in items)
                {
                    cmd.CommandText = @"INSERT INTO Security_Logins
(Id, Login, Password, Created_Date, Password_Update_Date, Agreement_Accepted_Date, Is_Locked, Is_Inactive, Email_Address, Phone_Number, Full_Name, Force_Change_Password, Prefferred_Language) 
values (@Id, @Login, @Password, @Created_Date, @Password_Update_Date, @Agreement_Accepted_Date, @Is_Locked, @Is_Inactive, @Email_Address, @Phone_Number, @Full_Name, @Force_Change_Password, @Prefferred_Language)";
                    cmd.Parameters.AddWithValue("@Id", poco.Id); cmd.Parameters.AddWithValue("@Login", poco.Login); cmd.Parameters.AddWithValue("@Password", poco.Password); cmd.Parameters.AddWithValue("@Created_Date", poco.Created); cmd.Parameters.AddWithValue("@Password_Update_Date", poco.PasswordUpdate); cmd.Parameters.AddWithValue("@Agreement_Accepted_Date", poco.AgreementAccepted); cmd.Parameters.AddWithValue("@Is_Locked", poco.IsLocked); cmd.Parameters.AddWithValue("@Is_Inactive", poco.IsInactive); cmd.Parameters.AddWithValue("@Email_Address", poco.EmailAddress); cmd.Parameters.AddWithValue("@Phone_Number", poco.PhoneNumber); cmd.Parameters.AddWithValue("@Full_Name", poco.FullName); cmd.Parameters.AddWithValue("@Force_Change_Password", poco.ForceChangePassword); cmd.Parameters.AddWithValue("@Prefferred_Language", poco.PrefferredLanguage);
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

        public IList<SecurityLoginPoco> GetAll(params Expression<Func<SecurityLoginPoco, object>>[] navigationProperties)
        {

            SecurityLoginPoco[] pocos = new SecurityLoginPoco[1001];
            SqlConnection Connection = new SqlConnection(_ConnString);
            using (Connection)
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = Connection;
                cmd.CommandText = "Select * from Security_Logins";
                Connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();


                int position = 0;
                while (reader.Read())
                {
                    SecurityLoginPoco poco = new SecurityLoginPoco();
                    poco.Id = (Guid)reader[0]; poco.Login = (string)reader[1]; poco.Password = (string)reader[2]; poco.Created = (DateTime)reader[3];
                    var PswUpdate = reader[4] == DBNull.Value ? default(DateTime?) : Convert.ToDateTime(reader[4]);
                    poco.PasswordUpdate = PswUpdate;
                    var AgAccDate = reader[5] == DBNull.Value ? default(DateTime?) : Convert.ToDateTime(reader[5]);
                    poco.AgreementAccepted = AgAccDate; poco.IsLocked = (Boolean)reader[6]; poco.IsInactive = (Boolean)reader[7]; poco.EmailAddress = (string)reader[8];

                    poco.PhoneNumber = (string)reader[9].ToString(); poco.FullName = (string)reader[10]; poco.ForceChangePassword = (Boolean)reader[11]; poco.PrefferredLanguage = (string)reader[12].ToString(); poco.TimeStamp = (Byte[])reader[13];

                    pocos[position] = poco;
                    position++;
                }
                Connection.Close();
            }
            return pocos.Where(p => p != null).ToList();
        }

        public IList<SecurityLoginPoco> GetList(Expression<Func<SecurityLoginPoco, bool>> where, params Expression<Func<SecurityLoginPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public SecurityLoginPoco GetSingle(Expression<Func<SecurityLoginPoco, bool>> where, params Expression<Func<SecurityLoginPoco, object>>[] navigationProperties)
        {

            IQueryable<SecurityLoginPoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();

        }

        public void Remove(params SecurityLoginPoco[] items)
        {

            SqlConnection Connection = new SqlConnection(_ConnString);
            using (Connection)
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = Connection;
                foreach (SecurityLoginPoco poco in items)
                {
                    cmd.CommandText = @"Delete from Security_Logins where Id=@Id ";
                    cmd.Parameters.AddWithValue("@Id", poco.Id);
                    Connection.Open();
                    cmd.ExecuteNonQuery();
                    Connection.Close();
                }
            }
        }

        public void Update(params SecurityLoginPoco[] items)
        {


            SqlConnection Connection = new SqlConnection(_ConnString);
            using (Connection)
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = Connection;
                foreach (SecurityLoginPoco poco in items)
                {
                    cmd.CommandText = @"Update Security_Logins SET 
Login=@Login, Password=@Password, Created_Date=@Created_Date, Password_Update_Date=@Password_Update_Date, Agreement_Accepted_Date=@Agreement_Accepted_Date, Is_Locked=@Is_Locked, Is_Inactive=@Is_Inactive, Email_Address=@Email_Address, Phone_Number=@Phone_Number, Full_Name=@Full_Name, Force_Change_Password=@Force_Change_Password, Prefferred_Language=@Prefferred_Language Where Id=@Id";
                    cmd.Parameters.AddWithValue("@Id", poco.Id); cmd.Parameters.AddWithValue("@Login", poco.Login); cmd.Parameters.AddWithValue("@Password", poco.Password); cmd.Parameters.AddWithValue("@Created_Date", poco.Created); cmd.Parameters.AddWithValue("@Password_Update_Date", poco.PasswordUpdate); cmd.Parameters.AddWithValue("@Agreement_Accepted_Date", poco.AgreementAccepted); cmd.Parameters.AddWithValue("@Is_Locked", poco.IsLocked); cmd.Parameters.AddWithValue("@Is_Inactive", poco.IsInactive); cmd.Parameters.AddWithValue("@Email_Address", poco.EmailAddress); cmd.Parameters.AddWithValue("@Phone_Number", poco.PhoneNumber); cmd.Parameters.AddWithValue("@Full_Name", poco.FullName); cmd.Parameters.AddWithValue("@Force_Change_Password", poco.ForceChangePassword); cmd.Parameters.AddWithValue("@Prefferred_Language", poco.PrefferredLanguage);
                    Connection.Open();
                    cmd.ExecuteNonQuery();
                    Connection.Close();
                }
            }

        }
    }
}
