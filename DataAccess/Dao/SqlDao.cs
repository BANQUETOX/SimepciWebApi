using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace DataAccess.Dao
{
    public class SqlDao
    {
        private string connectionString = "Server=tcp:simepci.database.windows.net,1433;Initial Catalog=SIMEPCI-DB1;Persist Security Info=False;User ID=softsolutionadmin;Password= SIMEPCI@2024 ;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
        //Singleton instance
        private static SqlDao instance = new SqlDao();
        //Singleton Access Point
        public static SqlDao GetInstance()
        {
            if (instance == null)
                instance = new SqlDao();
            return instance;
        }

        /*
         * C --> void
         * R --> result
         * U --> void
         * D --> void
         */

        public void ExecuteStoredProcedure(SqlOperation operation)
        {

            SqlConnection connection = new SqlConnection(connectionString);

            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = operation.ProcedureName;

            foreach (SqlParameter param in operation.parameters)
            {
                command.Parameters.Add(param);
            }
            try
            {
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Dictionary<string, object>> ExecuteStoredProcedureWithQuery(SqlOperation operation)
        {

            List<Dictionary<string, object>> lstResults = new List<Dictionary<string, object>>();
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = operation.ProcedureName;

            foreach (SqlParameter param in operation.parameters)
            {
                command.Parameters.Add(param);
            }
            try
            {
                connection.Open();
                //Ejecutar el script(stored procedure) y leer los datos de vuelta
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Dictionary<string, object> rowDicc = new Dictionary<string, object>();
                        //Recorre el reader en cada fila y obtiene los campos de forma que se almacene["nombre","valor"]
                        for (var fieldCount = 0; fieldCount < reader.FieldCount; fieldCount++)
                        {
                            rowDicc.Add(reader.GetName(fieldCount), reader.GetValue(fieldCount));
                        }
                        lstResults.Add(rowDicc);
                    }
                }
                connection.Close();
                return lstResults;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
