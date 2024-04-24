using DTO.Citas;
using DTO.Diagnosticos;
using DTO.ExamenesMedicos;
using DTO.Facturas;
using DTO.InfosExpediente;
using DTO.Recetas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Expedientes
{
    public class ExpedienteCompleto
    {
        public List<InfoExpediente> notasMedicas { get; set; }
        public List<InfoExpediente> notasEnfermeria { get; set; }
        public List<InfoExpediente> historialMedico { get; set; }

        public List<CitaOutput> citas { get; set; }
        public List<ExamenMedicoOutput> examenesMedicos { get; set; }
        public List<RecetaOutput> recetas { get; set; }
        public List<FacturaCompleta> facturas { get; set; }
        public List<Diagnostico> diagnosticos { get; set; }
    }
}
