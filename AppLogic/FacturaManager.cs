using DataAccess.Crud;
using DTO;
using DTO.CostosAdicionales;
using DTO.EspecialidadesMedicas;
using DTO.Facturas;
using DTO.Usuarios;
using System;
using System.Collections.Generic;
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
        CitaCrud citaCrud = new CitaCrud();




        public string CrearFactura(FacturaInput facturaInput)
        {
            string result;
            try
            {
               
                
                float monto = 0;

                if (facturaInput.idCita != null || facturaInput.idCita != 0)
                {
                    EspecialidadMedica especialidad = especialidadMedicaCrud.GetEspecialidadByCitaId(facturaInput.idCita);
                    monto += especialidad.costoCita;
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
                    float valorImpuesto = float.Parse(impuesto.valor);

                    if (impuesto != null && valorImpuesto > 0) {
                        float impuestoDecimal = valorImpuesto / 100;
                        Console.WriteLine(impuestoDecimal);
                        monto += (monto / impuestoDecimal);
                       
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


        public string UpdateFacturaPagada(int idFactura)
        {
            string result;
            try
            {

                facturaCrud.UpdateFacturaPagada(idFactura);
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

       

    }
}
