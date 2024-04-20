using DataAccess.Dao;
using DataAccess.Mapper.Interfaces;
using DTO;
using DTO.Usuarios;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Mapper
{
    public class UsuarioMapper: IObjectMapper
    {
        public BaseClass BuildObject(Dictionary<string, object> row)
        {
            Usuario user = new Usuario();
            user.Id = int.Parse(row["Id"].ToString());
            user.nombre = row["Nombre"].ToString();
            user.primerApellido = row["PrimerApellido"].ToString();
            user.segundoApellido = row["SegundoApellido"].ToString();
            user.cedula = row["Cedula"].ToString();
            user.fechaNacimiento = DateTime.Parse(row["FechaNacimiento"].ToString());
            user.edad = CalcularEdad(user.fechaNacimiento); 
            user.telefono = row["Telefono"].ToString();
            user.correo = row["Correo"].ToString();
            user.direccion = row["Direccion"].ToString();
            user.fotoPerfil = row["FotoPerfil"].ToString(); 
            user.activo = bool.Parse(row["Activo"].ToString());
            user.sexo = row["Sexo"].ToString();
            user.password = row["Password"].ToString();
            return user;
        }

        public BaseClass BuildEmptyObject()
        {
            Usuario user = new Usuario();
            user.Id = 0;
            user.nombre = string.Empty;
            user.primerApellido = string.Empty;
            user.segundoApellido = string.Empty;
            user.cedula = string.Empty;
            user.fechaNacimiento = DateTime.MinValue;
            user.edad = 0;
            user.telefono = string.Empty;
            user.correo = string.Empty;
            user.direccion = string.Empty;
            user.fotoPerfil = string.Empty;
            user.activo = false;
            user.password = string.Empty;
            user.sexo= string.Empty;    
            return user;
        }

        public List<BaseClass> BuildObjects(List<Dictionary<string, object>> rowList)
        {
            List<BaseClass> results = new List<BaseClass>();

            foreach(var row in rowList)
            {
                var user = BuildObject(row);
                results.Add(user);
            }

            return results;
        }

        public SqlOperation GetCreateStatement(BaseClass dto)
        {
            SqlOperation operation = new SqlOperation();
            operation.ProcedureName = "SP_INSERT_USUARIO";

            Usuario usuario = (Usuario) dto;

            operation.AddVarcharParam("nombre", usuario.nombre);
            operation.AddVarcharParam("primerApellido", usuario.primerApellido);
            operation.AddVarcharParam("segundoApellido", usuario.segundoApellido);
            operation.AddVarcharParam("cedula", usuario.cedula);
            operation.AddDatetimeParam("fechaNacimiento", usuario.fechaNacimiento);
            operation.AddVarcharParam("telefono", usuario.telefono);
            operation.AddVarcharParam("correo",usuario.correo);
            operation.AddVarcharParam("direccion", usuario.direccion);
            operation.AddVarcharParam("fotoPerfil",usuario.fotoPerfil);
            operation.AddBooleanParam("activo", usuario.activo);
            operation.AddVarcharParam("password", usuario.password);
            operation.AddVarcharParam("sexo",usuario.sexo);


            return operation;
        }

        public SqlOperation GetDeleteStatement(BaseClass dto)
        {
            throw new NotImplementedException();
        }

        public SqlOperation GetRetrieveAllStatement()
        {
            SqlOperation operation = new SqlOperation();
            operation.ProcedureName = "SP_GET_USUARIOS";
            return operation;
        }

        public SqlOperation GetUserByEmailStatement(string correo) {
            SqlOperation operation = new SqlOperation();
            operation.ProcedureName = "SP_GET_USUARIO_EMAIL";
            operation.AddVarcharParam("correo", correo);
            return operation;
        }

        public SqlOperation GetRetrieveByIdStatement(int id)
        {
            SqlOperation operation = new SqlOperation();
            operation.ProcedureName = "SP_GET_USUARIO_ID";
            operation.AddIntegerParam("id", id);    
            return operation;
        }

        public SqlOperation GetUpdateStatement(UsuarioUpdate usuario)
        {

            SqlOperation operation = new SqlOperation();
            operation.ProcedureName = "SP_UPDATE_USUARIO";
            operation.AddIntegerParam("id",usuario.idUsuario);
            operation.AddVarcharParam("nombre", usuario.nombre);
            operation.AddVarcharParam("primerApellido", usuario.primerApellido);
            operation.AddVarcharParam("segundoApellido", usuario.segundoApellido);
            operation.AddVarcharParam("cedula", usuario.cedula);
            operation.AddDatetimeParam("fechaNacimiento", usuario.fechaNacimiento);
            operation.AddVarcharParam("telefono", usuario.telefono);
            operation.AddVarcharParam("correo", usuario.correo);
            operation.AddVarcharParam("direccion", usuario.direccion);
            operation.AddVarcharParam("fotoPerfil", usuario.fotoPerfil);
            operation.AddVarcharParam("sexo", usuario.sexo);
            return operation;

        }

        public SqlOperation Login(string correo, string password)
        {
            SqlOperation operation = new SqlOperation();
            operation.ProcedureName = "SP_LOGIN";
            operation.AddVarcharParam("correo", correo);
            operation.AddVarcharParam("password", password);
            return operation;

        }

        public SqlOperation GetUpdatePasswordStatement(string correoUsuario,string newPassword)
        {
            SqlOperation operation = new SqlOperation();
            operation.ProcedureName = "SP_UPDATE_PASSWORD";
            operation.AddVarcharParam("correoUsuario",correoUsuario);
            operation.AddVarcharParam("newPassword", newPassword);
            return operation;
        }

        public SqlOperation GetDesactivarUsuarioStatement(string correo)
        {
            SqlOperation operation = new SqlOperation();
            operation.ProcedureName = "SP_UPDATE_USUARIO_ESTADO_INACTIVO";
            operation.AddVarcharParam("correoUsuario", correo);
            return operation;
        }

        public SqlOperation GetActivarUsuarioStatement(string correo)
        {
            SqlOperation operation = new SqlOperation();
            operation.ProcedureName = "SP_UPDATE_USUARIO_ESTADO_ACTIVO";
            operation.AddVarcharParam("correoUsuario", correo);
            return operation;
        }

        public SqlOperation GetUsuarioByDoctorId(int idDoctor)
        {
            SqlOperation operation = new SqlOperation();
            operation.ProcedureName = "SP_GET_USUARIO_ID_DOCTOR";
            operation.AddIntegerParam("idDoctor", idDoctor);
            return operation;
        }

        internal int CalcularEdad(DateTime fechaNacimiento)
        {
            DateOnly fechaActual = DateOnly.FromDateTime(DateTime.Today);

            int edad = fechaActual.Year - fechaNacimiento.Year;

            if (fechaActual.DayOfYear < fechaNacimiento.DayOfYear)
            {
                edad--;
            }

            return edad;
        }
    }

}


