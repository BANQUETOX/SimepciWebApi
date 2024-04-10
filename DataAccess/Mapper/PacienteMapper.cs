﻿using DataAccess.Mapper.Interfaces;
using DTO;
using System;
using DataAccess.Dao;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Mapper
{
    public class PacienteMapper : IObjectMapper
    {
        public BaseClass BuildObject(Dictionary<string, object> row)
        {
            Paciente paciente = new Paciente();
            paciente.Id = int.Parse(row["Id"].ToString());
            paciente.IdUsuario = int.Parse(row["IdUsuario"].ToString());
            return paciente;
        }

        public List<BaseClass> BuildObjects(List<Dictionary<string, object>> rowList)
        {
            throw new NotImplementedException();
        }

        public SqlOperation GetCreateStatement(int idUsuario)
        {
            SqlOperation operation = new SqlOperation();
            operation.ProcedureName = "SP_INSERT_PACIENTE";
            operation.AddIntegerParam("idUsuario", idUsuario);
            return operation;

        }
        public SqlOperation GetDeleteStatement(int idUsuario)
        {
            SqlOperation operation = new SqlOperation();
            operation.ProcedureName = "SP_DELETE_PACIENTE";
            operation.AddIntegerParam("idUsuario", idUsuario);
            return operation;

        }
    }
}
