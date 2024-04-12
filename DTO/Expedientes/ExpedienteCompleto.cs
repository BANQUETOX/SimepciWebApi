using DTO.Citas;
using DTO.ExamenesMedicos;
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
        public List<Cita> citas { get; set; }
        public List<ExamenMedico> examenesMedicos { get; set; }
        public List<Receta> recetas { get; set; }
        public List<Factura> facturas { get; set; }
    }
}
