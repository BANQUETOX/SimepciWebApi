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
    public class ConfiguracionCrud
    {
        ConfiguracionMapper mapper;
        SqlDao sqlDao;

        public ConfiguracionCrud()
        {
            mapper = new ConfiguracionMapper();
            sqlDao = SqlDao.GetInstance();
        }

        public List<Configuracion> GetConfiguraciones()
        {
            List<Configuracion> configuraciones = new List<Configuracion>();
            SqlOperation operation = mapper.GetConfiguraciones();
            var result = sqlDao.ExecuteStoredProcedureWithQuery(operation);
            configuraciones = mapper.BuildObjects(result);
            return configuraciones;

        }

        public void UpdateImpuesto (string nuevoValor)
        {
            SqlOperation operation = mapper.GetUpdateImpuestoStatement(nuevoValor);
            sqlDao.ExecuteStoredProcedure(operation);
        }

        public void UpdateRecordatorio(string nuevoValor)
        {
            SqlOperation operation = mapper.GetUpdateRecordatorioStatement(nuevoValor);
            sqlDao.ExecuteStoredProcedure(operation);
        }
    }
}
