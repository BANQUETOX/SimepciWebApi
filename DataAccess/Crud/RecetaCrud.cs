using DataAccess.Dao;
using DataAccess.Mapper;
using DTO.Recetas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Crud
{
    public class RecetaCrud
    {
        RecetaMapper mapper;   
        SqlDao sqlDao;

        public RecetaCrud() { 
            mapper = new RecetaMapper();
            sqlDao = SqlDao.GetInstance();
        }
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

        public void Update(Receta receta)
        {
            SqlOperation operation = mapper.GetUpdateRecetaStatement(receta);
            sqlDao.ExecuteStoredProcedure(operation);
        }

        public void Delete(int idReceta) { 
            SqlOperation operation = mapper.GetDeleteStatement(idReceta);   
            sqlDao.ExecuteStoredProcedure(operation);
        }

        public Receta GetRecetaById(int idReceta)
        {
            Receta receta = new Receta();
            SqlOperation operation = mapper.GetRertieveByIdStatement(idReceta);
            var result = sqlDao.ExecuteStoredProcedureWithQuery(operation);
            if (result.Count > 0)
            {
                receta = mapper.BuildObject(result[0]);
            }
            return receta;
        }
    }
}
