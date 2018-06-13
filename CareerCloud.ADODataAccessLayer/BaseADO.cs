using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;

namespace CareerCloud.ADODataAccessLayer
{
    public abstract class BaseADO
    {
        //protected readonly string _ConnString;
        protected readonly SqlConnection _Connection;

       public BaseADO()
        {
            _Connection = new SqlConnection( ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString);
            
        }
    }
}
