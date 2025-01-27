﻿using DataAccess.Dao;
using DataAccess.Mapper.Interfaces;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Mapper
{
    public class RolMapper : IObjectMapper
    {
        public BaseClass BuildObject(Dictionary<string, object> row)
        {
            Rol rol = new Rol();
            rol.Id = int.Parse(row["Id"].ToString());
            rol.nombre = row["Nombre"].ToString();
            return rol;
        }

        public List<BaseClass> BuildObjects(List<Dictionary<string, object>> rowList)
        {
            List<BaseClass> results = new List<BaseClass>();

            foreach (var row in rowList)
            {
                var rol = BuildObject(row);
                results.Add(rol);
            }

            return results;
        }

        public SqlOperation GetCreateStatement(BaseClass dto)
        {
            throw new NotImplementedException();
        }

        public SqlOperation GetDeleteStatement(BaseClass dto)
        {
            throw new NotImplementedException();
        }

        public SqlOperation GetRetrieveAllStatement()
        {
            SqlOperation operation = new SqlOperation();
            operation.ProcedureName = "SP_GET_ROLES";
            return operation;
        }

        public SqlOperation GetRetrieveByIdStatement(int id)
        {
            throw new NotImplementedException();
        }

        public SqlOperation GetUpdateStatement(BaseClass dto)
        {
            throw new NotImplementedException();
        }

        public SqlOperation GetAsignarRolStatement(int idUsuario, int idRol)
        {
            SqlOperation operation = new SqlOperation();
            operation.ProcedureName = "SP_INSERT_USUARIO_ROL";
            operation.AddIntegerParam("idUsuario", idUsuario);
            operation.AddIntegerParam("idRol", idRol);
            return operation;
        }

        public SqlOperation GetRemoverRolStatement(int idUsuario, int idRol)
        {
            SqlOperation operation = new SqlOperation();
            operation.ProcedureName = "SP_DELETE_ROL_USUARIO";
            operation.AddIntegerParam("idRol", idRol);
            operation.AddIntegerParam("idUsuario", idUsuario);
            return operation;
        }

        public SqlOperation GetRolesUsuarioStatement(string correo)
        {
            SqlOperation operation = new SqlOperation();
            operation.ProcedureName = "SP_GET_USUARIO_ROLES";
            operation.AddVarcharParam("correoUsuario", correo);
            return operation;
        }

    }
}
