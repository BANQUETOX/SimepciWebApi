using DataAccess.Crud;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;

namespace AppLogic
{
    public class DoctorManager
    {
        DoctorCrud doctorCrud = new DoctorCrud();


        public void CrearDoctor(Doctor doctor)
        {
            doctorCrud.Create(doctor);
        }

        public void EliminarDoctor(int idUsuario)
        {
            doctorCrud.Delete(idUsuario);
        }

        public string UpdateHorarioDoctor(int idDoctor, int horario)
        {
           
            if (horario >= 4  || horario == 0) {
                return "Numero de horario invalido";
            }
            try
            {
                doctorCrud.UpdateHorarioDoctor(idDoctor,horario);
                return "Horario actualizado";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
