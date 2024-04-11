using DataAccess.Mapper.Interfaces;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Mapper
{
    public class TipoExamenMapper : IObjectMapper
    {
        public BaseClass BuildObject(Dictionary<string, object> row)
        {
            TipoExamen tipoExamen = new TipoExamen();
            tipoExamen.Id = int.Parse(row["Id"].ToString());
            tipoExamen.nombre = row["Nombre"].ToString();
            return tipoExamen;
        }

        public List<BaseClass> BuildObjects(List<Dictionary<string, object>> rowList)
        {
            List<BaseClass> results = new List<BaseClass>();

            foreach (var row in rowList)
            {
                var user = BuildObject(row);
                results.Add(user);
            }

            return results;
        }
    }
}
