using DataAccess.Dao;
using DTO;
using DTO.Comentarios;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Mapper
{
    public class ComentarioMapper
    {
        public Comentario BuildObject(Dictionary<string, object> row)
        {
            Comentario comentario = new Comentario();
            comentario.Id = int.Parse(row["Id"].ToString());
            comentario.satisfaccion = int.Parse(row["Satisfaccion"].ToString());
            comentario.profecionalismo = int.Parse(row["Profecionalismo"].ToString());
            comentario.instalaciones = int.Parse(row["Instalaciones"].ToString());
            comentario.comentarios = row["Comentarios"].ToString();
            comentario.recomendaria = bool.Parse(row["Recomendacion"].ToString());
            return comentario;

        }

        public List<Comentario> BuildObjects(List<Dictionary<string, object>> rowList)
        {
            List<Comentario> results = new List<Comentario>();

            foreach (var row in rowList)
            {
                var comentario = BuildObject(row);
                results.Add(comentario);
            }

            return results;
        }


        public SqlOperation GetCreateStatement(Comentario comentario)
        {
            SqlOperation operation = new SqlOperation();
            operation.ProcedureName = "SP_INSERT_COMENTARIO";
            operation.AddIntegerParam("satisfaccion",comentario.satisfaccion);
            operation.AddIntegerParam("profecionalismo", comentario.profecionalismo);
            operation.AddIntegerParam("instalaciones",comentario.instalaciones);
            operation.AddBooleanParam("recomendaciones", comentario.recomendaria);
            operation.AddVarcharParam("comentarios", comentario.comentarios);
            return operation;

        }

        public SqlOperation GetRetrieveAllStatement()
        {
            SqlOperation operation = new SqlOperation();
            operation.ProcedureName = "SP_GET_COMENTARIOS";
            return operation;
        }
    }
}
