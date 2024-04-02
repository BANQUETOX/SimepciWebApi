using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Azure;
using Azure.Communication.Email;
using DataAccess.Crud;
using DTO;

namespace AppLogic
{
    public class EmailManager
    {
        string connectionString;
        EmailClient emailClient;
        string sender = "DoNotReply@b1248de0-1af0-462b-8f90-5c62032638df.azurecomm.net";
        private const string caracteresPermitidosOtp = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

        public EmailManager()
        {
            connectionString = "endpoint=https://simepci-email-service.unitedstates.communication.azure.com/;accesskey=HsnnZIFJsRBmRc67ESCA8qjLmgnUiTSV6ugMk1RuV5EDudQt6ewcR5R5LfXJgAnXVn+FaN89IwQc1FaO/yrOZA==";
            emailClient = new EmailClient(connectionString);

        }



        public async Task<string> SendOtp(string emailAddress) 
                                                                 
        {
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
            UsuarioManager usuarioManager = new UsuarioManager();
            RecuperarPasswordOtpManager  recuperarPasswordOtpManager = new RecuperarPasswordOtpManager();
            UsuarioPasswordOtpsManager relationManager = new UsuarioPasswordOtpsManager();

            string codigoOtp = generarCodigoOTP();
            int idPassword = recuperarPasswordOtpManager.CrearPasswordOtp(codigoOtp);
            //RecuperarPasswordOtp recuperarPasswordOtp = recuperarPasswordOtpManager.GetRecuperarPasswordOtpByCode(codigoOtp);
            Usuario usuario = usuarioManager.GetUsuarioByEmail(emailAddress);
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
