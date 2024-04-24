using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Azure;
using Azure.Communication.Email;
using DataAccess.Crud;
using DTO;
using DTO.Citas;
using DTO.Facturas;
using DTO.Usuarios;

namespace AppLogic
{
    public class EmailManager
    {
        string connectionString;
        EmailClient emailClient;
        string sender = "DoNotReply@b1248de0-1af0-462b-8f90-5c62032638df.azurecomm.net";
        private const string caracteresPermitidosOtp = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        UsuarioCrud usuarioCrud;
        AdministradorCrud administradorCrud;
        CitaCrud citaCrud = new CitaCrud();
        PacienteCrud pacieteCrud = new PacienteCrud();
        ConfiguracionCrud configuracionCrud = new ConfiguracionCrud();

        public EmailManager()
        {
            connectionString = "endpoint=https://simepci-email-service.unitedstates.communication.azure.com/;accesskey=HsnnZIFJsRBmRc67ESCA8qjLmgnUiTSV6ugMk1RuV5EDudQt6ewcR5R5LfXJgAnXVn+FaN89IwQc1FaO/yrOZA==";
            emailClient = new EmailClient(connectionString);
            usuarioCrud = new UsuarioCrud();
            administradorCrud = new AdministradorCrud();


        }



        public async Task<string> SendOtp(string emailAddress) 
                                                                 
        {
            if (usuarioCrud.GetUsuarioByEmail(emailAddress) != null)
            {
                return "El correo ya ha sido registrado";
            }

            RegistroOtpManager loginOtpManager = new RegistroOtpManager();
            RegistroOtp loginOtp = new RegistroOtp();
            

            string codigoOtp = generarCodigoOTP();

            loginOtp.correoUsuario = emailAddress;
            loginOtp.codigoOtp = codigoOtp;


            EmailContent emailContent = new EmailContent("Verificacion para creacion de su cuenta en SIMEPCI"); //Subject
            emailContent.PlainText = $"Hemos recibido su solicitud, su codigo de verificacion es: {codigoOtp} por favor ingreselo en el campo correspondiente para crear su cuenta"; //Contenido del correo
            loginOtpManager.CrearOtp(loginOtp);
            
            List<EmailAddress> emailAddresses = new List<EmailAddress> { new EmailAddress(emailAddress, "Suscriptor de SIMEPCI") };
            EmailRecipients emailRecipients = new EmailRecipients(emailAddresses);
            EmailMessage emailMessage = new EmailMessage(sender, emailRecipients, emailContent);
            EmailSendOperation emailSendOperation = await emailClient.SendAsync(
                                                    WaitUntil.Completed,
                                                                emailMessage, CancellationToken.None);
            EmailSendResult statusMonitor = emailSendOperation.Value;

            Console.WriteLine($"Email Sent. Status = {emailSendOperation.Value.Status}");

            return emailSendOperation.Value.Status.ToString();
        }

        public async Task<string>SendPasswordOtp(string emailAddress)
        {
         
            RecuperarPasswordOtpManager  recuperarPasswordOtpManager = new RecuperarPasswordOtpManager();
            UsuarioPasswordOtpsManager relationManager = new UsuarioPasswordOtpsManager();

            string codigoOtp = generarCodigoOTP();
            int idPassword = recuperarPasswordOtpManager.CrearPasswordOtp(codigoOtp);
            if (!usuarioCrud.verificarCorreo(emailAddress))
            {
                return "Correo Invalido";
            }
            Usuario usuario = usuarioCrud.GetUsuarioByEmail(emailAddress);
            if (usuario == null)
            {
                return "Usuario no encontrado";
            }
            relationManager.CrearUsuarioPasswordOtps(usuario.Id,idPassword);

            EmailContent emailContent = new EmailContent("Verificacion para creacion de su cuenta en SIMEPCI"); //Subject
            emailContent.PlainText = $"Hemos recibido su solicitud de cambio de contraseña, su codigo de verificacion es: {codigoOtp} por favor ingreselo en el campo correspondiente para reestablecer su contraseña";

            List<EmailAddress> emailAddresses = new List<EmailAddress> { new EmailAddress(emailAddress, "Suscriptor de SIMEPCI") };
            EmailRecipients emailRecipients = new EmailRecipients(emailAddresses);
            EmailMessage emailMessage = new EmailMessage(sender, emailRecipients, emailContent);
            EmailSendOperation emailSendOperation = await emailClient.SendAsync(
                                                    WaitUntil.Completed,
                                                                emailMessage, CancellationToken.None);
            EmailSendResult statusMonitor = emailSendOperation.Value;

            Console.WriteLine($"Email Sent. Status = {emailSendOperation.Value.Status}");

            return emailSendOperation.Value.Status.ToString();
        }


        public async Task<string> SendSolicitudFuncionario(string correoSolicitud)
        {

            List<Administrador> administradoes = administradorCrud.GetAllAdministrador();
   

            EmailContent emailContent = new EmailContent("Solicitud para ser funcionario"); //Subject
            emailContent.PlainText = $"Un usuario con correo {correoSolicitud} a solicitado unirse como funcionario de la clinica, revisa tus solicitudes para apovar o denegar el acceso";

            foreach (var administrador in administradoes)
            {
                Usuario usuarioAdmin = usuarioCrud.RetrieveById(administrador.idUsuario);
                string emailAddress = usuarioAdmin.correo;
                List<EmailAddress> emailAddresses = new List<EmailAddress> { new EmailAddress(emailAddress, "Suscriptor de SIMEPCI") };
                EmailRecipients emailRecipients = new EmailRecipients(emailAddresses);
                EmailMessage emailMessage = new EmailMessage(sender, emailRecipients, emailContent);
                EmailSendOperation emailSendOperation = await emailClient.SendAsync(
                                                    WaitUntil.Completed,
                                                                emailMessage, CancellationToken.None);
                EmailSendResult statusMonitor = emailSendOperation.Value;

                Console.WriteLine($"Email Sent. Status = {emailSendOperation.Value.Status}");
            }

            return "Correo enviado a los administadores";
        }


        public async Task<string> SendRecordatorio(Usuario usuario)
        {
            string result = "";
            try
            {
                string emailAddress = usuario.correo;
                Paciente paciente = pacieteCrud.GetPacieteByUsuarioId(usuario.Id);
                Configuracion configuracion = configuracionCrud.GetConfiguraciones()[1];
                int diasNotificacion = int.Parse(configuracion.valor.ToString());
                TimeSpan diasFecha = TimeSpan.FromDays(diasNotificacion);
                List<Cita> citasPaciente = citaCrud.GetCitasPaciente(paciente.Id);
                DateTime hoy = DateTime.Now;
                foreach (var cita in citasPaciente)
                {
                    if (cita.horaInicio >= hoy)
                    {
                        TimeSpan diferencia = cita.horaInicio  - hoy.Date ;
                        if (diferencia <= diasFecha)
                        {
                            EmailContent emailContent = new EmailContent("Recordatorio de su cita programada"); //Subject
                            emailContent.PlainText = $"Recordar que tiene una cita el dia {cita.horaInicio}"; //Contenido del correo
                            List<EmailAddress> emailAddresses = new List<EmailAddress> { new EmailAddress(emailAddress, "Suscriptor de SIMEPCI") };
                            EmailRecipients emailRecipients = new EmailRecipients(emailAddresses);
                            EmailMessage emailMessage = new EmailMessage(sender, emailRecipients, emailContent);
                            EmailSendOperation emailSendOperation = await emailClient.SendAsync(
                                                                    WaitUntil.Completed,
                                                                                emailMessage, CancellationToken.None);
                            EmailSendResult statusMonitor = emailSendOperation.Value;

                            Console.WriteLine($"Email Sent. Status = {emailSendOperation.Value.Status}");
                            result = "Recordatorios enviados";

                        }
                    }
                    else
                    {
                        result = "No hay recordatorios por enviar";
                    }

                }


            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return result;
            
        }

        public async Task<string> SendConfirmacionPago(Usuario usuarioPaciente, Factura factura)
        {
            string result;
            try
            {
                string emailAddress = usuarioPaciente.correo;
                EmailContent emailContent = new EmailContent("Confirmacion de pago"); //Subject
                emailContent.PlainText = $"Gracias por confiar en nuestros servicios, se ha confirmado el pago realizado por el monto de ₡{factura.monto}"; //Contenido del correo
                
                List<EmailAddress> emailAddresses = new List<EmailAddress> { new EmailAddress(emailAddress, "Suscriptor de SIMEPCI") };
                EmailRecipients emailRecipients = new EmailRecipients(emailAddresses);
                EmailMessage emailMessage = new EmailMessage(sender, emailRecipients, emailContent);
                EmailSendOperation emailSendOperation = await emailClient.SendAsync(
                                                        WaitUntil.Completed,
                                                                    emailMessage, CancellationToken.None);
                EmailSendResult statusMonitor = emailSendOperation.Value;

                Console.WriteLine($"Email Sent. Status = {emailSendOperation.Value.Status}");
                result = "Confirmacion de pago enviada";
            }
            catch (Exception e)
            {
                result=e.Message;   
            }
            return result;

        }

        public async void SendConfirmacionCita(Cita cita)
        {

            Usuario usuarioPaciente = usuarioCrud.RetrieveByPacienteId(cita.idPaciente);
            string emailAddress = usuarioPaciente.correo;
            DateTime horaInicioCr = cita.horaInicio.AddHours(-6);
            EmailContent emailContent = new EmailContent("Confirmacion de Cita"); //Subject
            emailContent.PlainText = $"Su cita para la fecha {horaInicioCr} se ha confirmado"; //Contenido del correo

            List<EmailAddress> emailAddresses = new List<EmailAddress> { new EmailAddress(emailAddress, "Suscriptor de SIMEPCI") };
            EmailRecipients emailRecipients = new EmailRecipients(emailAddresses);
            EmailMessage emailMessage = new EmailMessage(sender, emailRecipients, emailContent);
            EmailSendOperation emailSendOperation = await emailClient.SendAsync(
                                                    WaitUntil.Completed,
                                                                emailMessage, CancellationToken.None);
            EmailSendResult statusMonitor = emailSendOperation.Value;

            Console.WriteLine($"Email Sent. Status = {emailSendOperation.Value.Status}");
            
            

        }
        internal static string generarCodigoOTP()
        {
            int longitud = 8;
            StringBuilder sb = new StringBuilder();
            Random random = new Random();

            for (int i = 0; i < longitud; i++)
            {
                
                int indice = random.Next(caracteresPermitidosOtp.Length);
                sb.Append(caracteresPermitidosOtp[indice]);
            }

            return sb.ToString();
        } 
    }
}
