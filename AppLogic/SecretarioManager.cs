using DataAccess.Crud;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppLogic
{
    public class SecretarioManager
    {
        SecretarioCrud secretarioCrud = new SecretarioCrud();


        public void CrearSecretario(int idUsuario)
        {
            secretarioCrud.Create(idUsuario);
        }

        public void EliminarSecretario(int idUsuario)
        {
            secretarioCrud.Delete(idUsuario);
        }

        public Secretario GetSecretarioByUsuarioId(int idUsuario)
        {
            return secretarioCrud.GetSecretarioByUsuarioId(idUsuario);
        }
    }
}
