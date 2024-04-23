using DTO.Citas;
using DTO.ExamenesMedicos;
using DTO.Facturas;
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
        public Expediente infoExpediente {  get; set; }
        public List<CitaOutput> citas { get; set; }
        public List<ExamenMedicoOutput> examenesMedicos { get; set; }
        public List<RecetaOutput> recetas { get; set; }
        public List<FacturaCompleta> facturas { get; set; }
    }
}
