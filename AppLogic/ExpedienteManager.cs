using DataAccess.Crud;
using DTO.Expedientes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppLogic
{
    public class ExpedienteManager
    {
        ExpedienteCrud crud = new ExpedienteCrud();

        public string CreateExpediente(ExpedienteInput expedienteInput)
        {
            Expediente expediente = new Expediente();
            expediente.idPaciente = expedienteInput.idPaciente;
            expediente.notasEnfermeria = expedienteInput.notasEnfermeria;
            expediente.notasMedicas = expedienteInput.notasMedicas;
            expediente.historialMedico = expedienteInput.historialMedico;
            crud.Create(expediente);
            return "Expediente Creado";
        }
    }
}
