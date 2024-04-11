using DataAccess.Dao;
using DataAccess.Mapper;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Crud
{
    public class RecetaCrud
    {
        RecetaMapper mapper = new RecetaMapper();   
        SqlDao sqlDao = new SqlDao();
        public void Create(Receta receta)
        {
           SqlOperation operation = mapper.Create(receta);
            sqlDao.ExecuteStoredProcedure(operation);
        }
        public List<Receta> GetRecetasPaciente(int idPaciente) 
        {
            List<Receta> recetas = new List<Receta>();
            SqlOperation operation = mapper.GetRecetasByPaciente(idPaciente);
            var result = sqlDao.ExecuteStoredProcedureWithQuery(operation);
            recetas = mapper.BuildObjects(result);
            return recetas;

        }
    }
}
