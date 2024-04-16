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
    public class EspecialidadMedicaCrud
    {
        EspecialidadMedicaMapper mapper;
        SqlDao sqlDao;
        public EspecialidadMedicaCrud()
        {
            mapper = new EspecialidadMedicaMapper();
            sqlDao = SqlDao.GetInstance();
        }



        public EspecialidadMedica GetEspecialidadById(int idEspecialidad)
        {
            SqlOperation operation = mapper.RetrieveByIdStatement(idEspecialidad);
            var result = sqlDao.ExecuteStoredProcedureWithQuery(operation);
            EspecialidadMedica especialidad = new EspecialidadMedica();
            if (result.Count > 0) { 
                especialidad = mapper.BuildObject(result[0]);
            }
            return especialidad;
        }

    }
}
