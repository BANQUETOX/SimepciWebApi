using DataAccess.Crud;
using DTO;
using DTO.Usuarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AppLogic
{
    public class PacienteManager
    {
        PacienteCrud pacienteCrud = new PacienteCrud();
       

        public void CrearPaciente(int idUsuario)
        {
            pacienteCrud.Create(idUsuario);
        }

        public void EliminarPaciente (int idUsuario)
        {
            pacienteCrud.Delete(idUsuario);
        }

        public List<Paciente> GetAllPacientes()
        {
            List<Paciente> list = pacienteCrud.RetrieveAll<Paciente>();
            return list;
        }

    }
}
