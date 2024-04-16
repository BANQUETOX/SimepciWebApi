using DTO.Usuarios;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Dao;
using DTO.EspecialidadesMedicas;

namespace DataAccess.Mapper
{
    public class EspecialidadMedicaMapper
    {
        public EspecialidadMedica BuildObject(Dictionary<string, object> row)
        {
            EspecialidadMedica especialidad = new EspecialidadMedica();
            especialidad.Id = int.Parse(row["Id"].ToString());
            especialidad.nombre = row["Nombre"].ToString();
            return especialidad;
        }

        public List<BaseClass> BuildObjects(List<Dictionary<string, object>> rowList)
        {
            List<BaseClass> results = new List<BaseClass>();

            foreach (var row in rowList)
            {
                var especialidad = BuildObject(row);
                results.Add(especialidad);
            }

            return results;
        }



        public SqlOperation RetrieveByIdStatement(int idEspecialidad)
        {
            SqlOperation operation = new SqlOperation();
            operation.ProcedureName = "SP_GET_ESPECIALIDAD_MEDICA_ID";
            operation.AddIntegerParam("idEspecialidad", idEspecialidad);
            return operation;
        }

        public SqlOperation GetCreateStatement(EspecialidadMedica especialidadMedica) {
            SqlOperation operation = new SqlOperation();
            operation.ProcedureName = "SP_INSERT_ESPECIALIDAD_MEDICA";
            operation.AddFloatParam("costoCita",especialidadMedica.costoCita);
            operation.AddVarcharParam("nombre", especialidadMedica.nombre);
            return operation;
        }
    }
}
