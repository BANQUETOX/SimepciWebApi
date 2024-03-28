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
    public class UserMapper: IObjectMapper, ICrudStatements
    {
        public BaseClass BuildObject(Dictionary<string, object> row)
        {
            User user = new User();
            user.Id = int.Parse(row["Id"].ToString());
            user.nombre = row["Nombre"].ToString();
            user.primerApellido = row["PrimerApellido"].ToString();
            user.segundoApellido = row["SegundoApellido"].ToString();
            user.cedula = int.Parse(row["Cedula"].ToString());
            user.fechaNacimieto = DateOnly.Parse(row["FechaNacimiento"].ToString());
            user.edad = 0; //Calcular edad
            user.telefono = int.Parse(row["Telefono"].ToString());
            user.correo = row["Correo"].ToString();
            user.direccion = row["Direccion"].ToString();
            user.fotoPerfil = row["FotoPerfil"].ToString(); //Mapear correctamente la imagen
            user.activo = int.Parse(row["Activo"].ToString());
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
            throw new NotImplementedException();
        }

        public SqlOperation GetDeleteStatement(BaseClass dto)
        {
            throw new NotImplementedException();
        }

        public SqlOperation GetRetrieveAllStatement()
        {
            SqlOperation operation = new SqlOperation();
            operation.ProcedureName = "SP_GET_USERS";//Falta crear este store procedure
            throw new NotImplementedException();
        }

        public SqlOperation GetRetrieveByIdStatement(int id)
        {
            throw new NotImplementedException();
        }

        public SqlOperation GetUpdateStatement(BaseClass dto)
        {
            throw new NotImplementedException();
        }
    }
}
