using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace DataAccess.Dao
{
    public class SqlOperation
    {
        public string ProcedureName { get; set; }
        public List<SqlParameter> parameters;

        public SqlOperation()
        {
            parameters = new List<SqlParameter>();
        }

        //Métodos para agregar distintos tipos de parámetros
        public void AddVarcharParam(string parameterName, string parameterValue)
        {
            parameters.Add(new SqlParameter("@" + parameterName, parameterValue));
        }
        public void AddIntegerParam(string parameterName, int parameterValue)
        {
            parameters.Add(new SqlParameter("@" + parameterName, parameterValue));
        }
        public void AddDatetimeParam(string parameterName, DateTime parameterValue)
        {
            parameters.Add(new SqlParameter("@" + parameterName, parameterValue));
        }


        public void AddBooleanParam(string parameterName, bool parameterValue)
        {
            parameters.Add(new SqlParameter("@" + parameterName, parameterValue));
        }
        public void AddFloatParam(string parameterName, float parameterValue)
        {
            parameters.Add(new SqlParameter("@" + parameterName, parameterValue));
        }
    }
}
