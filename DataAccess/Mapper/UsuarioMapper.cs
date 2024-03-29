using DataAccess.Dao;
using DataAccess.Mapper.Interfaces;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Mapper
{
    public class UsuarioMapper: IObjectMapper, ICrudStatements
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
            user.password = row["Password"].ToString();
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

        public SqlOperation GetRetrieveByIdStatement(int id)
        {
            throw new NotImplementedException();
        }

        public SqlOperation GetUpdateStatement(BaseClass dto)
        {
            throw new NotImplementedException();
        }

        public SqlOperation GetUpdatePasswordStatement(string correoUsuario,string newPassword)
        {
            SqlOperation operation = new SqlOperation();
            operation.ProcedureName = "SP_UPDATE_PASSWORD";
            operation.AddVarcharParam("correoUsuario",correoUsuario);
            operation.AddVarcharParam("newPassword", newPassword);
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


