using DataAccess.Dao;
using DataAccess.Mapper;
using DTO;
using DTO.EspecialidadesMedicas;
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



        public void UpdateCostoCitaEspecialidad(int idEspecialidad, float nuevoPrecio)
        {
            SqlOperation operation = mapper.UpdatePrecioEspecialidadStatement(idEspecialidad,nuevoPrecio);
            sqlDao.ExecuteStoredProcedure(operation);
        }
        public List<EspecialidadMedica> GetAllEspecialidadMedicas() { 
            List<EspecialidadMedica> especialidades = new List<EspecialidadMedica>();
            SqlOperation operation = mapper.RetrieveAllStatement();
            var result = sqlDao.ExecuteStoredProcedureWithQuery(operation);
            especialidades = mapper.BuildObjects(result);
            return especialidades;
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

        public void CreateEspecialidadMedica(EspecialidadMedica especialidadMedica)
        {
            SqlOperation operation = mapper.GetCreateStatement(especialidadMedica);
            sqlDao.ExecuteStoredProcedure(operation);
        }

        public EspecialidadMedica GetEspecialidadByCitaId(int idCita)
        {
            EspecialidadMedica especialidad = new EspecialidadMedica();
            SqlOperation operation = mapper.GetEspecialidadByCitaIdStatement(idCita);
            var result = sqlDao.ExecuteStoredProcedureWithQuery(operation);
            if (result.Count > 0)
            {
                especialidad = mapper.BuildObject(result[0]);
            }
            return especialidad;
        }



    }
}
