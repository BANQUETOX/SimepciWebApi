using DataAccess.Crud;
using DTO;
using DTO.Citas;
using DTO.CostosAdicionales;
using DTO.EspecialidadesMedicas;
using DTO.Facturas;
using DTO.Usuarios;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppLogic
{
    public class FacturaManager
    {
        FacturaCrud facturaCrud = new FacturaCrud();
        EspecialidadMedicaCrud especialidadMedicaCrud = new EspecialidadMedicaCrud();
        CostoAdicionalCrud costoAdicionalCrud = new CostoAdicionalCrud();
        UsuarioCrud usuarioCrud = new UsuarioCrud();
        PacienteCrud pacienteCrud = new PacienteCrud();
        ConfiguracionCrud configuracionCrud = new ConfiguracionCrud();
        EmailManager emailManager = new EmailManager();
        




        public string CrearFactura(FacturaInput facturaInput)
        {
            string result;
            try
            {
               
                
                float monto = 0;

                if (facturaInput.idCita != null || facturaInput.idCita != 0)
                {
                    
                    Factura facturaExistente = facturaCrud.GetFacturaByCitaId(facturaInput.idCita);
                    if (facturaExistente.idCita != facturaInput.idCita)
                    {
                        EspecialidadMedica especialidad = especialidadMedicaCrud.GetEspecialidadByCitaId(facturaInput.idCita);
                        monto += especialidad.costoCita;

                    }
                    else {
                        return "La cita ya tiene una factura asignada";
                    }

                    
                    
                }
                if (facturaInput.costosAdicionales.Count != 0) {

                    foreach (var costoAdicional in facturaInput.costosAdicionales)
                    {
                        monto += costoAdicional.precio;
                    }
                }
                if(monto > 0)
                {
                    Configuracion impuesto = configuracionCrud.GetConfiguraciones()[0];
                    Configuracion iva = configuracionCrud.GetConfiguraciones()[2];
                    float valorImpuesto = float.Parse(impuesto.valor);
                    float valorIva = float.Parse(iva.valor);
                    

                    if (impuesto != null && valorImpuesto > 0) {
                        float impuestoDecimal = valorImpuesto / 100;
                        float valorIvaDecimal = valorIva / 100;
                        monto += (monto * impuestoDecimal) + (monto * valorIvaDecimal );
                       
                    }
                }
                Console.WriteLine(monto);
                Factura factura = castFacturaInput(facturaInput, monto);
                Factura facturaCreada  = facturaCrud.Create(factura);
              
                if (facturaInput.costosAdicionales.Count != 0)
                {

                    foreach (var costoAdicional in facturaInput.costosAdicionales)
                    {
                        CostoAdicional costoAdicionalCompleto = new CostoAdicional();
                        costoAdicionalCompleto.idFactura = facturaCreada.Id;
                        costoAdicionalCompleto.precio = costoAdicional.precio;
                        costoAdicionalCompleto.nombre = costoAdicional.nombre;
                        costoAdicionalCrud.Create(costoAdicionalCompleto);
                    }
                }
                result = "Factura creada";
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return result;

        }


        public List<FacturaCompleta> GetFacturasPaciente(string correoPaciente)
        {
            Usuario usuario = usuarioCrud.GetUsuarioByEmail(correoPaciente);
            Paciente paciente = pacienteCrud.GetPacieteByUsuarioId(usuario.Id);
            int idPaciente = paciente.Id;
            List<FacturaCompleta> facturasCompletas = new List<FacturaCompleta>();
            List<Factura> facturasPaciente = facturaCrud.GetFacturasPaciente(idPaciente);
            foreach (var factura in facturasPaciente)
            {
              
                List<CostoAdicional> costosAdicionales = costoAdicionalCrud.GetCostosByFacutaId(factura.Id);
                FacturaCompleta facturaCompleta = new FacturaCompleta();
                facturaCompleta.Id = factura.Id;
                facturaCompleta.idCita = factura.idCita;
                facturaCompleta.pagada = factura.pagada;
                facturaCompleta.monto = factura.monto;
                facturaCompleta.fechaEmision = factura.fechaEmision;    
                facturaCompleta.costosAdicionales = costosAdicionales;  
                facturasCompletas.Add(facturaCompleta);
               
            }
            return facturasCompletas;

        }


        public async Task<string> UpdateFacturaPagada(int idFactura)
        {
            string result;
            try
            {

                facturaCrud.UpdateFacturaPagada(idFactura);
                Factura factura = facturaCrud.GetFacturaById(idFactura);
                Usuario usuarioPaciente = usuarioCrud.RetrieveByFacturaId(idFactura);
                usuarioCrud.ActivarUsuario(usuarioPaciente.correo);
                Console.WriteLine(await emailManager.SendConfirmacionPago(usuarioPaciente, factura));
                result = "Factura Pagada";
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
           return result;
        }

        public string UpdateFacturaSinPagar(int idFactura)
        {
            string result;
            try
            {

                facturaCrud.UpdateFacturaSinPagar(idFactura);
                result = "Ahora la factura esta pendiente de pago";
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return result;
        }

        public Factura castFacturaInput(FacturaInput facturaInput, float monto)
        {
            Factura factura = new Factura();
            factura.idCita = facturaInput.idCita;
            factura.fechaEmision = DateTime.Now;
            factura.pagada = false;
            factura.monto = monto;
            return factura;
        }


        public List<ReporteMensual> reporteGanancias()
        {
            List<ReporteMensual> reporteCompleto = new List<ReporteMensual>();
            try {
                List<Factura> facturasPagadas = facturaCrud.GetFacturasPagadas();
                var facturasAgrupadasPorMesAnio = facturasPagadas.GroupBy(factura => new { Mes = factura.fechaEmision.Month, Anio = factura.fechaEmision.Year });
                foreach (var grupo in facturasAgrupadasPorMesAnio)
                {
                    int mes = grupo.Key.Mes; // Mes del grupo
                    int anio = grupo.Key.Anio; // Año del grupo
                    ReporteMensual reporteMensual = new ReporteMensual();
                    reporteMensual.mes = $"{mes}/{anio}";
                    reporteMensual.gananciaCitas = 0;
                    reporteMensual.gananciaServicios = 0;
                    reporteMensual.gananciasTotales = 0;
                    foreach (var factura in grupo)
                    {
                        List<CostoAdicional> cotosAdicionales = costoAdicionalCrud.GetCostosByFacutaId(factura.Id);
                        float montoCostosAdicionales = 0;
                        if (cotosAdicionales.Count > 0)
                        {
                            foreach (var costoAdicional in cotosAdicionales)
                            {
                                montoCostosAdicionales += costoAdicional.precio;
                            }
                        }
                        float costoCita = 0;
                        if (factura.idCita != null || factura.idCita != 0)
                        {
                            float montoCita = especialidadMedicaCrud.GetEspecialidadByCitaId(factura.idCita).costoCita;
                            costoCita += montoCita;
                        }

                        reporteMensual.gananciaServicios += montoCostosAdicionales;
                        reporteMensual.gananciaCitas += costoCita;
                        reporteMensual.gananciasTotales += factura.monto;

                    }

                    reporteCompleto.Add(reporteMensual);
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            
            return reporteCompleto;
        }



    }
}
