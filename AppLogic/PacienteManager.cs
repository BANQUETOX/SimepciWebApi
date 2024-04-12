using Azure.Core;
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
        PacienteCrud crud = new PacienteCrud();
       

        public void CrearPaciente(int idUsuario)
        {
            crud.Create(idUsuario);
        }

        public void EliminarPaciente (int idUsuario)
        {
            crud.Delete(idUsuario);
        }

        public List<Paciente> GetAllPacientes()
        {
            List<Paciente> list = crud.RetrieveAll<Paciente>();
            return list;
        }

        public Paciente GetPacienteByUsuarioId(int idUsuario)
        {
            return crud.GetPacieteByUsuarioId(idUsuario);
        }

    }
}
