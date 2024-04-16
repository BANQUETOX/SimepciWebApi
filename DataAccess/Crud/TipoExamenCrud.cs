﻿using DataAccess.Dao;
using DataAccess.Mapper;
using DTO.TiposExamenes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Crud
{
    public class TipoExamenCrud
    {
        TipoExamenMapper mapper;
        SqlDao dao;
        public TipoExamenCrud()
        {
            mapper = new TipoExamenMapper();
            dao = SqlDao.GetInstance(); 
        }


        public void Create(TipoExamen tipoExamen)
        {
            SqlOperation operation = mapper.GetCreateStatement(tipoExamen);
            dao.ExecuteStoredProcedure(operation);
        }

        public List<TipoExamen> GetTiposExamenes()
        {
            SqlOperation operation = mapper.GetRetrieveAllOperation();
            var result = dao.ExecuteStoredProcedureWithQuery(operation);
            var tiposExamenes = mapper.BuildObjects(result);
            return tiposExamenes;    
        }

        public void Update(TipoExamen tipoExamen)
        {
            SqlOperation operation = mapper.GetUpdateStatement(tipoExamen);
            dao.ExecuteStoredProcedure(operation);
        }

        public void Delete(int idTipoExamen)
        {
            SqlOperation operation = mapper.GetDeleteStatement(idTipoExamen);
            dao.ExecuteStoredProcedure(operation);
        }
    }
}
