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
          /*  if (usuarioCrud.GetUsuarioByEmail(emailAddress) != null)
            {
                return "El correo ya ha sido registrado";
            }*/

            RegistroOtpManager loginOtpManager = new RegistroOtpManager();
            RegistroOtp loginOtp = new RegistroOtp();
            

            string codigoOtp = generarCodigoOTP();

            loginOtp.correoUsuario = emailAddress;
            loginOtp.codigoOtp = codigoOtp;


            EmailContent emailContent = new EmailContent("Verificacion para creacion de su cuenta en SIMEPCI"); //Subject
            emailContent.Html = $"<!DOCTYPE HTML PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional //EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\">\r\n<html xmlns=\"http://www.w3.org/1999/xhtml\" xmlns:v=\"urn:schemas-microsoft-com:vml\" xmlns:o=\"urn:schemas-microsoft-com:office:office\">\r\n<head>\r\n<!--[if gte mso 9]>\r\n<xml>\r\n  <o:OfficeDocumentSettings>\r\n    <o:AllowPNG/>\r\n    <o:PixelsPerInch>96</o:PixelsPerInch>\r\n  </o:OfficeDocumentSettings>\r\n</xml>\r\n<![endif]-->\r\n  <meta http-equiv=\"Content-Type\" content=\"text/html; charset=UTF-8\">\r\n  <meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\">\r\n  <meta name=\"x-apple-disable-message-reformatting\">\r\n  <!--[if !mso]><!--><meta http-equiv=\"X-UA-Compatible\" content=\"IE=edge\"><!--<![endif]-->\r\n  <title></title>\r\n  \r\n    <style type=\"text/css\">\r\n      @media only screen and (min-width: 620px) {{\r\n  .u-row {{\r\n    width: 600px !important;\r\n  }}\r\n  .u-row .u-col {{\r\n    vertical-align: top;\r\n  }}\r\n\r\n  .u-row .u-col-100 {{\r\n    width: 600px !important;\r\n  }}\r\n\r\n}}\r\n\r\n@media (max-width: 620px) {{\r\n  .u-row-container {{\r\n    max-width: 100% !important;\r\n    padding-left: 0px !important;\r\n    padding-right: 0px !important;\r\n  }}\r\n  .u-row .u-col {{\r\n    min-width: 320px !important;\r\n    max-width: 100% !important;\r\n    display: block !important;\r\n  }}\r\n  .u-row {{\r\n    width: 100% !important;\r\n  }}\r\n  .u-col {{\r\n    width: 100% !important;\r\n  }}\r\n  .u-col > div {{\r\n    margin: 0 auto;\r\n  }}\r\n}}\r\nbody {{\r\n  margin: 0;\r\n  padding: 0;\r\n}}\r\n\r\ntable,\r\ntr,\r\ntd {{\r\n  vertical-align: top;\r\n  border-collapse: collapse;\r\n}}\r\n\r\np {{\r\n  margin: 0;\r\n}}\r\n\r\n.ie-container table,\r\n.mso-container table {{\r\n  table-layout: fixed;\r\n}}\r\n\r\n* {{\r\n  line-height: inherit;\r\n}}\r\n\r\na[x-apple-data-detectors='true'] {{\r\n  color: inherit !important;\r\n  text-decoration: none !important;\r\n}}\r\n\r\ntable, td {{ color: #000000; }} #u_body a {{ color: #0000ee; text-decoration: underline; }} @media (max-width: 480px) {{ #u_content_heading_1 .v-container-padding-padding {{ padding: 51px 10px 30px !important; }} #u_content_heading_1 .v-text-align {{ text-align: center !important; }} #u_content_heading_2 .v-container-padding-padding {{ padding: 20px 10px 27px !important; }} #u_content_heading_3 .v-container-padding-padding {{ padding: 51px 10px 50px !important; }} #u_content_text_1 .v-container-padding-padding {{ padding: 10px !important; }} #u_content_button_1 .v-container-padding-padding {{ padding: 10px !important; }} #u_content_button_1 .v-size-width {{ width: 80% !important; }} #u_content_text_2 .v-container-padding-padding {{ padding: 10px 10px 40px !important; }} #u_content_heading_4 .v-container-padding-padding {{ padding: 40px 10px 10px !important; }} #u_content_heading_4 .v-text-align {{ text-align: center !important; }} #u_content_text_3 .v-container-padding-padding {{ padding: 10px 40px 0px !important; }} #u_content_text_3 .v-text-align {{ text-align: center !important; }} }}\r\n    </style>\r\n  \r\n  \r\n\r\n<!--[if !mso]><!--><link href=\"https://fonts.googleapis.com/css?family=Raleway:400,700&display=swap\" rel=\"stylesheet\" type=\"text/css\"><!--<![endif]-->\r\n\r\n</head>\r\n\r\n<body class=\"clean-body u_body\" style=\"margin: 0;padding: 0;-webkit-text-size-adjust: 100%;background-color: #ecf0f1;color: #000000\">\r\n  <!--[if IE]><div class=\"ie-container\"><![endif]-->\r\n  <!--[if mso]><div class=\"mso-container\"><![endif]-->\r\n  <table id=\"u_body\" style=\"border-collapse: collapse;table-layout: fixed;border-spacing: 0;mso-table-lspace: 0pt;mso-table-rspace: 0pt;vertical-align: top;min-width: 320px;Margin: 0 auto;background-color: #ecf0f1;width:100%\" cellpadding=\"0\" cellspacing=\"0\">\r\n  <tbody>\r\n  <tr style=\"vertical-align: top\">\r\n    <td style=\"word-break: break-word;border-collapse: collapse !important;vertical-align: top\">\r\n    <!--[if (mso)|(IE)]><table width=\"100%\" cellpadding=\"0\" cellspacing=\"0\" border=\"0\"><tr><td align=\"center\" style=\"background-color: #ecf0f1;\"><![endif]-->\r\n    \r\n\r\n    <!--[if gte mso 9]>\r\n      <table cellpadding=\"0\" cellspacing=\"0\" border=\"0\" style=\"margin: 0 auto;min-width: 320px;max-width: 600px;\">\r\n        <tr>\r\n          <td background=\"https://cdn.templates.unlayer.com/assets/1712173422382-bg.png\" valign=\"top\" width=\"100%\">\r\n      <v:rect xmlns:v=\"urn:schemas-microsoft-com:vml\" fill=\"true\" stroke=\"false\" style=\"width: 600px;\">\r\n        <v:fill type=\"frame\" src=\"https://cdn.templates.unlayer.com/assets/1712173422382-bg.png\" /><v:textbox style=\"mso-fit-shape-to-text:true\" inset=\"0,0,0,0\">\r\n      <![endif]-->\r\n  \r\n<div class=\"u-row-container\" style=\"padding: 0px;background-image: url('https://cdn.templates.unlayer.com/assets/1712173422382-bg.png');background-repeat: no-repeat;background-position: center top;background-color: transparent\">\r\n  <div class=\"u-row\" style=\"margin: 0 auto;min-width: 320px;max-width: 600px;overflow-wrap: break-word;word-wrap: break-word;word-break: break-word;background-color: transparent;\">\r\n    <div style=\"border-collapse: collapse;display: table;width: 100%;height: 100%;background-color: transparent;\">\r\n      <!--[if (mso)|(IE)]><table width=\"100%\" cellpadding=\"0\" cellspacing=\"0\" border=\"0\"><tr><td style=\"padding: 0px;background-image: url('images/image-1.png');background-repeat: no-repeat;background-position: center top;background-color: transparent;\" align=\"center\"><table cellpadding=\"0\" cellspacing=\"0\" border=\"0\" style=\"width:600px;\"><tr style=\"background-color: transparent;\"><![endif]-->\r\n      \r\n<!--[if (mso)|(IE)]><td align=\"center\" width=\"600\" style=\"width: 600px;padding: 0px;border-top: 0px solid transparent;border-left: 0px solid transparent;border-right: 0px solid transparent;border-bottom: 0px solid transparent;\" valign=\"top\"><![endif]-->\r\n<div class=\"u-col u-col-100\" style=\"max-width: 320px;min-width: 600px;display: table-cell;vertical-align: top;\">\r\n  <div style=\"height: 100%;width: 100% !important;\">\r\n  <!--[if (!mso)&(!IE)]><!--><div style=\"box-sizing: border-box; height: 100%; padding: 0px;border-top: 0px solid transparent;border-left: 0px solid transparent;border-right: 0px solid transparent;border-bottom: 0px solid transparent;\"><!--<![endif]-->\r\n  \r\n\r\n<table id=\"u_content_heading_2\" style=\"font-family:'Raleway',sans-serif;\" role=\"presentation\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" border=\"0\">\r\n  <tbody>\r\n    <tr>\r\n        <img src=\"https://simepciwebb.azurewebsites.net/imgs/landing/Logo.png\" alt=\"\" style=\"max-width: 100px; display: flex; margin-bottom: -50px; margin-top: 100px; \">\r\n        \r\n  <!--[if mso]><table width=\"100%\"><tr><td><![endif]-->\r\n  <!--[if mso]></td></tr></table><![endif]-->\r\n\r\n      </td>\r\n    </tr>\r\n  </tbody>\r\n</table>\r\n\r\n<table id=\"u_content_heading_3\" style=\"font-family:'Raleway',sans-serif;\" role=\"presentation\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" border=\"0\">\r\n  <tbody>\r\n    <tr>\r\n      <td class=\"v-container-padding-padding\" style=\"overflow-wrap:break-word;word-break:break-word;padding:51px 10px 50px 43px;font-family:'Raleway',sans-serif;\" align=\"left\">\r\n        \r\n  <!--[if mso]><table width=\"100%\"><tr><td><![endif]-->\r\n    <h1 class=\"v-text-align\" style=\"margin-bottom: 100px; color: #c9c9c9; line-height: 120%; text-align: left; word-wrap: break-word; font-size: 29px; font-weight: 400;\"><span><span><span><span><span style=\"line-height: 34.8px;\"><strong><span style=\"line-height: 34.8px;\">Activacion de cuenta</span></strong></span></span></span></span></span></h1>\r\n  <!--[if mso]></td></tr></table><![endif]-->\r\n\r\n      </td>\r\n    </tr>\r\n  </tbody>\r\n</table>\r\n\r\n<table id=\"u_content_text_1\" style=\"font-family:'Raleway',sans-serif;\" role=\"presentation\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" border=\"0\">\r\n  <tbody>\r\n    <tr>\r\n      <td class=\"v-container-padding-padding\" style=\"overflow-wrap:break-word;word-break:break-word;padding:10px 43px;font-family:'Raleway',sans-serif;\" align=\"left\">\r\n        \r\n  <div class=\"v-text-align\" style=\"font-size: 14px; line-height: 160%; text-align: left; word-wrap: break-word;\">\r\n    <p style=\"line-height: 160%;\">Bienvenido al servicio web del Hospial Su salud Primero!</p>\r\n    <br>\r\n<p style=\"line-height: 160%;\">Para la finalizar de crear de su cuenta por favor ingrese el siguiente codigo en el espacio solicitado al terminar el formulario: <b>{codigoOtp}</b>   </p>\r\n<ol>\r\n</ol>\r\n\r\n  </div>\r\n\r\n      </td>\r\n    </tr>\r\n  </tbody>\r\n</table>\r\n\r\n<table id=\"u_content_button_1\" style=\"font-family:'Raleway',sans-serif;\" role=\"presentation\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" border=\"0\">\r\n  <tbody>\r\n    <tr>\r\n      <td class=\"v-container-padding-padding\" style=\"overflow-wrap:break-word;word-break:break-word;padding:10px 10px 10px 43px;font-family:'Raleway',sans-serif;\" align=\"left\">\r\n        \r\n  <!--[if mso]><style>.v-button {{background: transparent !important;}}</style><![endif]-->\r\n<div class=\"v-text-align\" align=\"left\">\r\n  <!--[if mso]><table border=\"0\" cellspacing=\"0\" cellpadding=\"0\"><tr><td align=\"center\" bgcolor=\"#117c5d\" style=\"padding:10px 20px;\" valign=\"top\"><![endif]-->\r\n   \r\n    <!--[if mso]></td></tr></table><![endif]-->\r\n</div>\r\n\r\n      </td>\r\n    </tr>\r\n  </tbody>\r\n</table>\r\n\r\n<table id=\"u_content_text_2\" style=\"font-family:'Raleway',sans-serif;\" role=\"presentation\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" border=\"0\">\r\n  <tbody>\r\n    <tr>\r\n      <td class=\"v-container-padding-padding\" style=\"overflow-wrap:break-word;word-break:break-word;padding:10px 43px 30px;font-family:'Raleway',sans-serif;\" align=\"left\">\r\n        \r\n \r\n\r\n      </td>\r\n    </tr>\r\n  </tbody>\r\n</table>\r\n\r\n  <!--[if (!mso)&(!IE)]><!--></div><!--<![endif]-->\r\n  </div>\r\n</div>\r\n<!--[if (mso)|(IE)]></td><![endif]-->\r\n      <!--[if (mso)|(IE)]></tr></table></td></tr></table><![endif]-->\r\n    </div>\r\n  </div>\r\n  </div>\r\n  \r\n    <!--[if gte mso 9]>\r\n      </v:textbox></v:rect>\r\n    </td>\r\n    </tr>\r\n    </table>\r\n    <![endif]-->\r\n    \r\n\r\n\r\n  \r\n  \r\n<div class=\"u-row-container\" style=\"padding: 0px;background-color: transparent\">\r\n  <div class=\"u-row\" style=\"margin: 0 auto;min-width: 320px;max-width: 600px;overflow-wrap: break-word;word-wrap: break-word;word-break: break-word;background-color: transparent;\">\r\n    <div style=\"border-collapse: collapse;display: table;width: 100%;height: 100%;background-color: transparent;\">\r\n      <!--[if (mso)|(IE)]><table width=\"100%\" cellpadding=\"0\" cellspacing=\"0\" border=\"0\"><tr><td style=\"padding: 0px;background-color: transparent;\" align=\"center\"><table cellpadding=\"0\" cellspacing=\"0\" border=\"0\" style=\"width:600px;\"><tr style=\"background-color: transparent;\"><![endif]-->\r\n      \r\n<!--[if (mso)|(IE)]><td align=\"center\" width=\"600\" style=\"background-color: #117c5d;width: 600px;padding: 0px;border-top: 0px solid transparent;border-left: 0px solid transparent;border-right: 0px solid transparent;border-bottom: 0px solid transparent;border-radius: 0px;-webkit-border-radius: 0px; -moz-border-radius: 0px;\" valign=\"top\"><![endif]-->\r\n<div class=\"u-col u-col-100\" style=\"max-width: 320px;min-width: 600px;display: table-cell;vertical-align: top;\">\r\n  <div style=\"background-color: #AAD9BB;height: 100%;width: 100% !important;border-radius: 0px;-webkit-border-radius: 0px; -moz-border-radius: 0px;\">\r\n  <!--[if (!mso)&(!IE)]><!--><div style=\"box-sizing: border-box; height: 100%; padding: 0px;border-top: 0px solid transparent;border-left: 0px solid transparent;border-right: 0px solid transparent;border-bottom: 0px solid transparent;border-radius: 0px;-webkit-border-radius: 0px; -moz-border-radius: 0px;\"><!--<![endif]-->\r\n  \r\n<table id=\"u_content_heading_4\" style=\"font-family:'Raleway',sans-serif;\" role=\"presentation\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" border=\"0\">\r\n  <tbody>\r\n    <tr>\r\n      <td class=\"v-container-padding-padding\" style=\"overflow-wrap:break-word;word-break:break-word;padding:40px 10px 10px 40px;font-family:'Raleway',sans-serif;\" align=\"left\">\r\n        \r\n  <!--[if mso]><table width=\"100%\"><tr><td><![endif]-->\r\n    <h1 class=\"v-text-align\" style=\"margin: 0px; color: #000000; line-height: 140%; text-align: left; word-wrap: break-word; font-size: 22px; font-weight: 400;\"><span><span><span>Gracias por preferirnos!!</span></span></span></h1>\r\n  <!--[if mso]></td></tr></table><![endif]-->\r\n\r\n      </td>\r\n    </tr>\r\n  </tbody>\r\n</table>\r\n\r\n<table id=\"u_content_text_3\" style=\"font-family:'Raleway',sans-serif;\" role=\"presentation\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" border=\"0\">\r\n  <tbody>\r\n    <tr>\r\n      <td class=\"v-container-padding-padding\" style=\"overflow-wrap:break-word;word-break:break-word;padding:10px 60px 25px 40px;font-family:'Raleway',sans-serif;\" align=\"left\">\r\n        \r\n  <div class=\"v-text-align\" style=\"font-size: 14px; color: #000000; line-height: 170%; text-align: left; word-wrap: break-word;\">\r\n    <p style=\"line-height: 170%;\"><span data-metadata=\"&lt;!--(figmeta)eyJmaWxlS2V5IjoiaUdkbDhwdlZkTlBQU2x2aTlkeXpHRyIsInBhc3RlSUQiOjMxOTY0Mjc2NCwiZGF0YVR5cGUiOiJzY2VuZSJ9Cg==(/figmeta)--&gt;\" style=\"line-height: 23.8px;\"></span>En el hospital Su salud Primero agradecemos su confianza en nuestros servicios. </p>\r\n  </div>\r\n\r\n      </td>\r\n    </tr>\r\n  </tbody>\r\n</table>\r\n\r\n<table style=\"font-family:'Raleway',sans-serif;\" role=\"presentation\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" border=\"0\">\r\n  <tbody>\r\n    <tr>\r\n      <td class=\"v-container-padding-padding\" style=\"overflow-wrap:break-word;word-break:break-word;padding:10px 10px 0px;font-family:'Raleway',sans-serif;\" align=\"left\">\r\n        \r\n  <table height=\"0px\" align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"92%\" style=\"border-collapse: collapse;table-layout: fixed;border-spacing: 0;mso-table-lspace: 0pt;mso-table-rspace: 0pt;vertical-align: top;border-top: 1px solid #BBBBBB;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%\">\r\n    <tbody>\r\n      <tr style=\"vertical-align: top\">\r\n        <td style=\"word-break: break-word;border-collapse: collapse !important;vertical-align: top;font-size: 0px;line-height: 0px;mso-line-height-rule: exactly;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%\">\r\n          <span>&#160;</span>\r\n        </td>\r\n      </tr>\r\n    </tbody>\r\n  </table>\r\n\r\n      </td>\r\n    </tr>\r\n  </tbody>\r\n</table>\r\n\r\n  <!--[if (!mso)&(!IE)]><!--></div><!--<![endif]-->\r\n  </div>\r\n</div>\r\n<!--[if (mso)|(IE)]></td><![endif]-->\r\n      <!--[if (mso)|(IE)]></tr></table></td></tr></table><![endif]-->\r\n    </div>\r\n  </div>\r\n  </div>\r\n  \r\n\r\n\r\n    <!--[if (mso)|(IE)]></td></tr></table><![endif]-->\r\n    </td>\r\n  </tr>\r\n  </tbody>\r\n  </table>\r\n  <!--[if mso]></div><![endif]-->\r\n  <!--[if IE]></div><![endif]-->\r\n</body>\r\n\r\n</html>\r\n"; //Contenido del correo
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
            emailContent.Html = $"<!DOCTYPE HTML PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional //EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\">\r\n<html xmlns=\"http://www.w3.org/1999/xhtml\" xmlns:v=\"urn:schemas-microsoft-com:vml\" xmlns:o=\"urn:schemas-microsoft-com:office:office\">\r\n<head>\r\n<!--[if gte mso 9]>\r\n<xml>\r\n  <o:OfficeDocumentSettings>\r\n    <o:AllowPNG/>\r\n    <o:PixelsPerInch>96</o:PixelsPerInch>\r\n  </o:OfficeDocumentSettings>\r\n</xml>\r\n<![endif]-->\r\n  <meta http-equiv=\"Content-Type\" content=\"text/html; charset=UTF-8\">\r\n  <meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\">\r\n  <meta name=\"x-apple-disable-message-reformatting\">\r\n  <!--[if !mso]><!--><meta http-equiv=\"X-UA-Compatible\" content=\"IE=edge\"><!--<![endif]-->\r\n  <title></title>\r\n  \r\n    <style type=\"text/css\">\r\n      @media only screen and (min-width: 620px) {{\r\n  .u-row {{\r\n    width: 600px !important;\r\n  }}\r\n  .u-row .u-col {{\r\n    vertical-align: top;\r\n  }}\r\n\r\n  .u-row .u-col-100 {{\r\n    width: 600px !important;\r\n  }}\r\n\r\n}}\r\n\r\n@media (max-width: 620px) {{\r\n  .u-row-container {{\r\n    max-width: 100% !important;\r\n    padding-left: 0px !important;\r\n    padding-right: 0px !important;\r\n  }}\r\n  .u-row .u-col {{\r\n    min-width: 320px !important;\r\n    max-width: 100% !important;\r\n    display: block !important;\r\n  }}\r\n  .u-row {{\r\n    width: 100% !important;\r\n  }}\r\n  .u-col {{\r\n    width: 100% !important;\r\n  }}\r\n  .u-col > div {{\r\n    margin: 0 auto;\r\n  }}\r\n}}\r\nbody {{\r\n  margin: 0;\r\n  padding: 0;\r\n}}\r\n\r\ntable,\r\ntr,\r\ntd {{\r\n  vertical-align: top;\r\n  border-collapse: collapse;\r\n}}\r\n\r\np {{\r\n  margin: 0;\r\n}}\r\n\r\n.ie-container table,\r\n.mso-container table {{\r\n  table-layout: fixed;\r\n}}\r\n\r\n* {{\r\n  line-height: inherit;\r\n}}\r\n\r\na[x-apple-data-detectors='true'] {{\r\n  color: inherit !important;\r\n  text-decoration: none !important;\r\n}}\r\n\r\ntable, td {{ color: #000000; }} #u_body a {{ color: #0000ee; text-decoration: underline; }} @media (max-width: 480px) {{ #u_content_heading_1 .v-container-padding-padding {{ padding: 51px 10px 30px !important; }} #u_content_heading_1 .v-text-align {{ text-align: center !important; }} #u_content_heading_2 .v-container-padding-padding {{ padding: 20px 10px 27px !important; }} #u_content_heading_3 .v-container-padding-padding {{ padding: 51px 10px 50px !important; }} #u_content_text_1 .v-container-padding-padding {{ padding: 10px !important; }} #u_content_button_1 .v-container-padding-padding {{ padding: 10px !important; }} #u_content_button_1 .v-size-width {{ width: 80% !important; }} #u_content_text_2 .v-container-padding-padding {{ padding: 10px 10px 40px !important; }} #u_content_heading_4 .v-container-padding-padding {{ padding: 40px 10px 10px !important; }} #u_content_heading_4 .v-text-align {{ text-align: center !important; }} #u_content_text_3 .v-container-padding-padding {{ padding: 10px 40px 0px !important; }} #u_content_text_3 .v-text-align {{ text-align: center !important; }} }}\r\n    </style>\r\n  \r\n  \r\n\r\n<!--[if !mso]><!--><link href=\"https://fonts.googleapis.com/css?family=Raleway:400,700&display=swap\" rel=\"stylesheet\" type=\"text/css\"><!--<![endif]-->\r\n\r\n</head>\r\n\r\n<body class=\"clean-body u_body\" style=\"margin: 0;padding: 0;-webkit-text-size-adjust: 100%;background-color: #ecf0f1;color: #000000\">\r\n  <!--[if IE]><div class=\"ie-container\"><![endif]-->\r\n  <!--[if mso]><div class=\"mso-container\"><![endif]-->\r\n  <table id=\"u_body\" style=\"border-collapse: collapse;table-layout: fixed;border-spacing: 0;mso-table-lspace: 0pt;mso-table-rspace: 0pt;vertical-align: top;min-width: 320px;Margin: 0 auto;background-color: #ecf0f1;width:100%\" cellpadding=\"0\" cellspacing=\"0\">\r\n  <tbody>\r\n  <tr style=\"vertical-align: top\">\r\n    <td style=\"word-break: break-word;border-collapse: collapse !important;vertical-align: top\">\r\n    <!--[if (mso)|(IE)]><table width=\"100%\" cellpadding=\"0\" cellspacing=\"0\" border=\"0\"><tr><td align=\"center\" style=\"background-color: #ecf0f1;\"><![endif]-->\r\n    \r\n\r\n    <!--[if gte mso 9]>\r\n      <table cellpadding=\"0\" cellspacing=\"0\" border=\"0\" style=\"margin: 0 auto;min-width: 320px;max-width: 600px;\">\r\n        <tr>\r\n          <td background=\"https://cdn.templates.unlayer.com/assets/1712173422382-bg.png\" valign=\"top\" width=\"100%\">\r\n      <v:rect xmlns:v=\"urn:schemas-microsoft-com:vml\" fill=\"true\" stroke=\"false\" style=\"width: 600px;\">\r\n        <v:fill type=\"frame\" src=\"https://cdn.templates.unlayer.com/assets/1712173422382-bg.png\" /><v:textbox style=\"mso-fit-shape-to-text:true\" inset=\"0,0,0,0\">\r\n      <![endif]-->\r\n  \r\n<div class=\"u-row-container\" style=\"padding: 0px;background-image: url('https://cdn.templates.unlayer.com/assets/1712173422382-bg.png');background-repeat: no-repeat;background-position: center top;background-color: transparent\">\r\n  <div class=\"u-row\" style=\"margin: 0 auto;min-width: 320px;max-width: 600px;overflow-wrap: break-word;word-wrap: break-word;word-break: break-word;background-color: transparent;\">\r\n    <div style=\"border-collapse: collapse;display: table;width: 100%;height: 100%;background-color: transparent;\">\r\n      <!--[if (mso)|(IE)]><table width=\"100%\" cellpadding=\"0\" cellspacing=\"0\" border=\"0\"><tr><td style=\"padding: 0px;background-image: url('images/image-1.png');background-repeat: no-repeat;background-position: center top;background-color: transparent;\" align=\"center\"><table cellpadding=\"0\" cellspacing=\"0\" border=\"0\" style=\"width:600px;\"><tr style=\"background-color: transparent;\"><![endif]-->\r\n      \r\n<!--[if (mso)|(IE)]><td align=\"center\" width=\"600\" style=\"width: 600px;padding: 0px;border-top: 0px solid transparent;border-left: 0px solid transparent;border-right: 0px solid transparent;border-bottom: 0px solid transparent;\" valign=\"top\"><![endif]-->\r\n<div class=\"u-col u-col-100\" style=\"max-width: 320px;min-width: 600px;display: table-cell;vertical-align: top;\">\r\n  <div style=\"height: 100%;width: 100% !important;\">\r\n  <!--[if (!mso)&(!IE)]><!--><div style=\"box-sizing: border-box; height: 100%; padding: 0px;border-top: 0px solid transparent;border-left: 0px solid transparent;border-right: 0px solid transparent;border-bottom: 0px solid transparent;\"><!--<![endif]-->\r\n  \r\n\r\n<table id=\"u_content_heading_2\" style=\"font-family:'Raleway',sans-serif;\" role=\"presentation\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" border=\"0\">\r\n  <tbody>\r\n    <tr>\r\n        <img src=\"https://simepciwebb.azurewebsites.net/imgs/landing/Logo.png\" alt=\"\" style=\"max-width: 100px; display: flex; margin-bottom: -50px; margin-top: 100px; \">\r\n        \r\n  <!--[if mso]><table width=\"100%\"><tr><td><![endif]-->\r\n  <!--[if mso]></td></tr></table><![endif]-->\r\n\r\n      </td>\r\n    </tr>\r\n  </tbody>\r\n</table>\r\n\r\n<table id=\"u_content_heading_3\" style=\"font-family:'Raleway',sans-serif;\" role=\"presentation\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" border=\"0\">\r\n  <tbody>\r\n    <tr>\r\n      <td class=\"v-container-padding-padding\" style=\"overflow-wrap:break-word;word-break:break-word;padding:51px 10px 50px 43px;font-family:'Raleway',sans-serif;\" align=\"left\">\r\n        \r\n  <!--[if mso]><table width=\"100%\"><tr><td><![endif]-->\r\n    <h1 class=\"v-text-align\" style=\"margin-bottom: 100px; color: #c9c9c9; line-height: 120%; text-align: left; word-wrap: break-word; font-size: 29px; font-weight: 400;\"><span><span><span><span><span style=\"line-height: 34.8px;\"><strong><span style=\"line-height: 34.8px;\">Recuperacion de contraseña</span></strong></span></span></span></span></span></h1>\r\n  <!--[if mso]></td></tr></table><![endif]-->\r\n\r\n      </td>\r\n    </tr>\r\n  </tbody>\r\n</table>\r\n\r\n<table id=\"u_content_text_1\" style=\"font-family:'Raleway',sans-serif;\" role=\"presentation\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" border=\"0\">\r\n  <tbody>\r\n    <tr>\r\n      <td class=\"v-container-padding-padding\" style=\"overflow-wrap:break-word;word-break:break-word;padding:10px 43px;font-family:'Raleway',sans-serif;\" align=\"left\">\r\n        \r\n  <div class=\"v-text-align\" style=\"font-size: 14px; line-height: 160%; text-align: left; word-wrap: break-word;\">\r\n    <br>\r\n<p style=\"line-height: 160%;\">Finalize el cambio de contraseña ingresando el siguiente codigo al finalizar el formulario: <b>{codigoOtp}</b>   </p>\r\n<ol>\r\n</ol>\r\n\r\n  </div>\r\n\r\n      </td>\r\n    </tr>\r\n  </tbody>\r\n</table>\r\n\r\n<table id=\"u_content_button_1\" style=\"font-family:'Raleway',sans-serif;\" role=\"presentation\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" border=\"0\">\r\n  <tbody>\r\n    <tr>\r\n      <td class=\"v-container-padding-padding\" style=\"overflow-wrap:break-word;word-break:break-word;padding:10px 10px 10px 43px;font-family:'Raleway',sans-serif;\" align=\"left\">\r\n        \r\n  <!--[if mso]><style>.v-button {{background: transparent !important;}}</style><![endif]-->\r\n<div class=\"v-text-align\" align=\"left\">\r\n  <!--[if mso]><table border=\"0\" cellspacing=\"0\" cellpadding=\"0\"><tr><td align=\"center\" bgcolor=\"#117c5d\" style=\"padding:10px 20px;\" valign=\"top\"><![endif]-->\r\n   \r\n    <!--[if mso]></td></tr></table><![endif]-->\r\n</div>\r\n\r\n      </td>\r\n    </tr>\r\n  </tbody>\r\n</table>\r\n\r\n<table id=\"u_content_text_2\" style=\"font-family:'Raleway',sans-serif;\" role=\"presentation\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" border=\"0\">\r\n  <tbody>\r\n    <tr>\r\n      <td class=\"v-container-padding-padding\" style=\"overflow-wrap:break-word;word-break:break-word;padding:10px 43px 30px;font-family:'Raleway',sans-serif;\" align=\"left\">\r\n        \r\n \r\n\r\n      </td>\r\n    </tr>\r\n  </tbody>\r\n</table>\r\n\r\n  <!--[if (!mso)&(!IE)]><!--></div><!--<![endif]-->\r\n  </div>\r\n</div>\r\n<!--[if (mso)|(IE)]></td><![endif]-->\r\n      <!--[if (mso)|(IE)]></tr></table></td></tr></table><![endif]-->\r\n    </div>\r\n  </div>\r\n  </div>\r\n  \r\n    <!--[if gte mso 9]>\r\n      </v:textbox></v:rect>\r\n    </td>\r\n    </tr>\r\n    </table>\r\n    <![endif]-->\r\n    \r\n\r\n\r\n  \r\n  \r\n<div class=\"u-row-container\" style=\"padding: 0px;background-color: transparent\">\r\n  <div class=\"u-row\" style=\"margin: 0 auto;min-width: 320px;max-width: 600px;overflow-wrap: break-word;word-wrap: break-word;word-break: break-word;background-color: transparent;\">\r\n    <div style=\"border-collapse: collapse;display: table;width: 100%;height: 100%;background-color: transparent;\">\r\n      <!--[if (mso)|(IE)]><table width=\"100%\" cellpadding=\"0\" cellspacing=\"0\" border=\"0\"><tr><td style=\"padding: 0px;background-color: transparent;\" align=\"center\"><table cellpadding=\"0\" cellspacing=\"0\" border=\"0\" style=\"width:600px;\"><tr style=\"background-color: transparent;\"><![endif]-->\r\n      \r\n<!--[if (mso)|(IE)]><td align=\"center\" width=\"600\" style=\"background-color: #117c5d;width: 600px;padding: 0px;border-top: 0px solid transparent;border-left: 0px solid transparent;border-right: 0px solid transparent;border-bottom: 0px solid transparent;border-radius: 0px;-webkit-border-radius: 0px; -moz-border-radius: 0px;\" valign=\"top\"><![endif]-->\r\n<div class=\"u-col u-col-100\" style=\"max-width: 320px;min-width: 600px;display: table-cell;vertical-align: top;\">\r\n  <div style=\"background-color: #AAD9BB;height: 100%;width: 100% !important;border-radius: 0px;-webkit-border-radius: 0px; -moz-border-radius: 0px;\">\r\n  <!--[if (!mso)&(!IE)]><!--><div style=\"box-sizing: border-box; height: 100%; padding: 0px;border-top: 0px solid transparent;border-left: 0px solid transparent;border-right: 0px solid transparent;border-bottom: 0px solid transparent;border-radius: 0px;-webkit-border-radius: 0px; -moz-border-radius: 0px;\"><!--<![endif]-->\r\n  \r\n<table id=\"u_content_heading_4\" style=\"font-family:'Raleway',sans-serif;\" role=\"presentation\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" border=\"0\">\r\n  <tbody>\r\n    <tr>\r\n      <td class=\"v-container-padding-padding\" style=\"overflow-wrap:break-word;word-break:break-word;padding:40px 10px 10px 40px;font-family:'Raleway',sans-serif;\" align=\"left\">\r\n        \r\n  <!--[if mso]><table width=\"100%\"><tr><td><![endif]-->\r\n    <h1 class=\"v-text-align\" style=\"margin: 0px; color: #000000; line-height: 140%; text-align: left; word-wrap: break-word; font-size: 22px; font-weight: 400;\"><span><span><span>Gracias por preferirnos!!</span></span></span></h1>\r\n  <!--[if mso]></td></tr></table><![endif]-->\r\n\r\n      </td>\r\n    </tr>\r\n  </tbody>\r\n</table>\r\n\r\n<table id=\"u_content_text_3\" style=\"font-family:'Raleway',sans-serif;\" role=\"presentation\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" border=\"0\">\r\n  <tbody>\r\n    <tr>\r\n      <td class=\"v-container-padding-padding\" style=\"overflow-wrap:break-word;word-break:break-word;padding:10px 60px 25px 40px;font-family:'Raleway',sans-serif;\" align=\"left\">\r\n        \r\n  <div class=\"v-text-align\" style=\"font-size: 14px; color: #000000; line-height: 170%; text-align: left; word-wrap: break-word;\">\r\n    <p style=\"line-height: 170%;\"><span data-metadata=\"&lt;!--(figmeta)eyJmaWxlS2V5IjoiaUdkbDhwdlZkTlBQU2x2aTlkeXpHRyIsInBhc3RlSUQiOjMxOTY0Mjc2NCwiZGF0YVR5cGUiOiJzY2VuZSJ9Cg==(/figmeta)--&gt;\" style=\"line-height: 23.8px;\"></span>En el hospital Su salud Primero agradecemos su confianza en nuestros servicios. </p>\r\n  </div>\r\n\r\n      </td>\r\n    </tr>\r\n  </tbody>\r\n</table>\r\n\r\n<table style=\"font-family:'Raleway',sans-serif;\" role=\"presentation\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" border=\"0\">\r\n  <tbody>\r\n    <tr>\r\n      <td class=\"v-container-padding-padding\" style=\"overflow-wrap:break-word;word-break:break-word;padding:10px 10px 0px;font-family:'Raleway',sans-serif;\" align=\"left\">\r\n        \r\n  <table height=\"0px\" align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"92%\" style=\"border-collapse: collapse;table-layout: fixed;border-spacing: 0;mso-table-lspace: 0pt;mso-table-rspace: 0pt;vertical-align: top;border-top: 1px solid #BBBBBB;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%\">\r\n    <tbody>\r\n      <tr style=\"vertical-align: top\">\r\n        <td style=\"word-break: break-word;border-collapse: collapse !important;vertical-align: top;font-size: 0px;line-height: 0px;mso-line-height-rule: exactly;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%\">\r\n          <span>&#160;</span>\r\n        </td>\r\n      </tr>\r\n    </tbody>\r\n  </table>\r\n\r\n      </td>\r\n    </tr>\r\n  </tbody>\r\n</table>\r\n\r\n  <!--[if (!mso)&(!IE)]><!--></div><!--<![endif]-->\r\n  </div>\r\n</div>\r\n<!--[if (mso)|(IE)]></td><![endif]-->\r\n      <!--[if (mso)|(IE)]></tr></table></td></tr></table><![endif]-->\r\n    </div>\r\n  </div>\r\n  </div>\r\n  \r\n\r\n\r\n    <!--[if (mso)|(IE)]></td></tr></table><![endif]-->\r\n    </td>\r\n  </tr>\r\n  </tbody>\r\n  </table>\r\n  <!--[if mso]></div><![endif]-->\r\n  <!--[if IE]></div><![endif]-->\r\n</body>\r\n\r\n</html>\r\n";

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
            emailContent.Html = $"<!DOCTYPE HTML PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional //EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\">\r\n<html xmlns=\"http://www.w3.org/1999/xhtml\" xmlns:v=\"urn:schemas-microsoft-com:vml\" xmlns:o=\"urn:schemas-microsoft-com:office:office\">\r\n<head>\r\n<!--[if gte mso 9]>\r\n<xml>\r\n  <o:OfficeDocumentSettings>\r\n    <o:AllowPNG/>\r\n    <o:PixelsPerInch>96</o:PixelsPerInch>\r\n  </o:OfficeDocumentSettings>\r\n</xml>\r\n<![endif]-->\r\n  <meta http-equiv=\"Content-Type\" content=\"text/html; charset=UTF-8\">\r\n  <meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\">\r\n  <meta name=\"x-apple-disable-message-reformatting\">\r\n  <!--[if !mso]><!--><meta http-equiv=\"X-UA-Compatible\" content=\"IE=edge\"><!--<![endif]-->\r\n  <title></title>\r\n  \r\n    <style type=\"text/css\">\r\n      @media only screen and (min-width: 620px) {{\r\n  .u-row {{\r\n    width: 600px !important;\r\n  }}\r\n  .u-row .u-col {{\r\n    vertical-align: top;\r\n  }}\r\n\r\n  .u-row .u-col-100 {{\r\n    width: 600px !important;\r\n  }}\r\n\r\n}}\r\n\r\n@media (max-width: 620px) {{\r\n  .u-row-container {{\r\n    max-width: 100% !important;\r\n    padding-left: 0px !important;\r\n    padding-right: 0px !important;\r\n  }}\r\n  .u-row .u-col {{\r\n    min-width: 320px !important;\r\n    max-width: 100% !important;\r\n    display: block !important;\r\n  }}\r\n  .u-row {{\r\n    width: 100% !important;\r\n  }}\r\n  .u-col {{\r\n    width: 100% !important;\r\n  }}\r\n  .u-col > div {{\r\n    margin: 0 auto;\r\n  }}\r\n}}\r\nbody {{\r\n  margin: 0;\r\n  padding: 0;\r\n}}\r\n\r\ntable,\r\ntr,\r\ntd {{\r\n  vertical-align: top;\r\n  border-collapse: collapse;\r\n}}\r\n\r\np {{\r\n  margin: 0;\r\n}}\r\n\r\n.ie-container table,\r\n.mso-container table {{\r\n  table-layout: fixed;\r\n}}\r\n\r\n* {{\r\n  line-height: inherit;\r\n}}\r\n\r\na[x-apple-data-detectors='true'] {{\r\n  color: inherit !important;\r\n  text-decoration: none !important;\r\n}}\r\n\r\ntable, td {{ color: #000000; }} #u_body a {{ color: #0000ee; text-decoration: underline; }} @media (max-width: 480px) {{ #u_content_heading_1 .v-container-padding-padding {{ padding: 51px 10px 30px !important; }} #u_content_heading_1 .v-text-align {{ text-align: center !important; }} #u_content_heading_2 .v-container-padding-padding {{ padding: 20px 10px 27px !important; }} #u_content_heading_3 .v-container-padding-padding {{ padding: 51px 10px 50px !important; }} #u_content_text_1 .v-container-padding-padding {{ padding: 10px !important; }} #u_content_button_1 .v-container-padding-padding {{ padding: 10px !important; }} #u_content_button_1 .v-size-width {{ width: 80% !important; }} #u_content_text_2 .v-container-padding-padding {{ padding: 10px 10px 40px !important; }} #u_content_heading_4 .v-container-padding-padding {{ padding: 40px 10px 10px !important; }} #u_content_heading_4 .v-text-align {{ text-align: center !important; }} #u_content_text_3 .v-container-padding-padding {{ padding: 10px 40px 0px !important; }} #u_content_text_3 .v-text-align {{ text-align: center !important; }} }}\r\n    </style>\r\n  \r\n  \r\n\r\n<!--[if !mso]><!--><link href=\"https://fonts.googleapis.com/css?family=Raleway:400,700&display=swap\" rel=\"stylesheet\" type=\"text/css\"><!--<![endif]-->\r\n\r\n</head>\r\n\r\n<body class=\"clean-body u_body\" style=\"margin: 0;padding: 0;-webkit-text-size-adjust: 100%;background-color: #ecf0f1;color: #000000\">\r\n  <!--[if IE]><div class=\"ie-container\"><![endif]-->\r\n  <!--[if mso]><div class=\"mso-container\"><![endif]-->\r\n  <table id=\"u_body\" style=\"border-collapse: collapse;table-layout: fixed;border-spacing: 0;mso-table-lspace: 0pt;mso-table-rspace: 0pt;vertical-align: top;min-width: 320px;Margin: 0 auto;background-color: #ecf0f1;width:100%\" cellpadding=\"0\" cellspacing=\"0\">\r\n  <tbody>\r\n  <tr style=\"vertical-align: top\">\r\n    <td style=\"word-break: break-word;border-collapse: collapse !important;vertical-align: top\">\r\n    <!--[if (mso)|(IE)]><table width=\"100%\" cellpadding=\"0\" cellspacing=\"0\" border=\"0\"><tr><td align=\"center\" style=\"background-color: #ecf0f1;\"><![endif]-->\r\n    \r\n\r\n    <!--[if gte mso 9]>\r\n      <table cellpadding=\"0\" cellspacing=\"0\" border=\"0\" style=\"margin: 0 auto;min-width: 320px;max-width: 600px;\">\r\n        <tr>\r\n          <td background=\"https://cdn.templates.unlayer.com/assets/1712173422382-bg.png\" valign=\"top\" width=\"100%\">\r\n      <v:rect xmlns:v=\"urn:schemas-microsoft-com:vml\" fill=\"true\" stroke=\"false\" style=\"width: 600px;\">\r\n        <v:fill type=\"frame\" src=\"https://cdn.templates.unlayer.com/assets/1712173422382-bg.png\" /><v:textbox style=\"mso-fit-shape-to-text:true\" inset=\"0,0,0,0\">\r\n      <![endif]-->\r\n  \r\n<div class=\"u-row-container\" style=\"padding: 0px;background-image: url('https://cdn.templates.unlayer.com/assets/1712173422382-bg.png');background-repeat: no-repeat;background-position: center top;background-color: transparent\">\r\n  <div class=\"u-row\" style=\"margin: 0 auto;min-width: 320px;max-width: 600px;overflow-wrap: break-word;word-wrap: break-word;word-break: break-word;background-color: transparent;\">\r\n    <div style=\"border-collapse: collapse;display: table;width: 100%;height: 100%;background-color: transparent;\">\r\n      <!--[if (mso)|(IE)]><table width=\"100%\" cellpadding=\"0\" cellspacing=\"0\" border=\"0\"><tr><td style=\"padding: 0px;background-image: url('images/image-1.png');background-repeat: no-repeat;background-position: center top;background-color: transparent;\" align=\"center\"><table cellpadding=\"0\" cellspacing=\"0\" border=\"0\" style=\"width:600px;\"><tr style=\"background-color: transparent;\"><![endif]-->\r\n      \r\n<!--[if (mso)|(IE)]><td align=\"center\" width=\"600\" style=\"width: 600px;padding: 0px;border-top: 0px solid transparent;border-left: 0px solid transparent;border-right: 0px solid transparent;border-bottom: 0px solid transparent;\" valign=\"top\"><![endif]-->\r\n<div class=\"u-col u-col-100\" style=\"max-width: 320px;min-width: 600px;display: table-cell;vertical-align: top;\">\r\n  <div style=\"height: 100%;width: 100% !important;\">\r\n  <!--[if (!mso)&(!IE)]><!--><div style=\"box-sizing: border-box; height: 100%; padding: 0px;border-top: 0px solid transparent;border-left: 0px solid transparent;border-right: 0px solid transparent;border-bottom: 0px solid transparent;\"><!--<![endif]-->\r\n  \r\n\r\n<table id=\"u_content_heading_2\" style=\"font-family:'Raleway',sans-serif;\" role=\"presentation\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" border=\"0\">\r\n  <tbody>\r\n    <tr>\r\n        <img src=\"https://simepciwebb.azurewebsites.net/imgs/landing/Logo.png\" alt=\"\" style=\"max-width: 100px; display: flex; margin-bottom: -50px; margin-top: 100px; \">\r\n        \r\n  <!--[if mso]><table width=\"100%\"><tr><td><![endif]-->\r\n  <!--[if mso]></td></tr></table><![endif]-->\r\n\r\n      </td>\r\n    </tr>\r\n  </tbody>\r\n</table>\r\n\r\n<table id=\"u_content_heading_3\" style=\"font-family:'Raleway',sans-serif;\" role=\"presentation\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" border=\"0\">\r\n  <tbody>\r\n    <tr>\r\n      <td class=\"v-container-padding-padding\" style=\"overflow-wrap:break-word;word-break:break-word;padding:51px 10px 50px 43px;font-family:'Raleway',sans-serif;\" align=\"left\">\r\n        \r\n  <!--[if mso]><table width=\"100%\"><tr><td><![endif]-->\r\n    <h1 class=\"v-text-align\" style=\"margin-bottom: 100px; color: #c9c9c9; line-height: 120%; text-align: left; word-wrap: break-word; font-size: 29px; font-weight: 400;\"><span><span><span><span><span style=\"line-height: 34.8px;\"><strong><span style=\"line-height: 34.8px;\">Nuevo Funcionario Registrado</span></strong></span></span></span></span></span></h1>\r\n  <!--[if mso]></td></tr></table><![endif]-->\r\n\r\n      </td>\r\n    </tr>\r\n  </tbody>\r\n</table>\r\n\r\n<table id=\"u_content_text_1\" style=\"font-family:'Raleway',sans-serif;\" role=\"presentation\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" border=\"0\">\r\n  <tbody>\r\n    <tr>\r\n      <td class=\"v-container-padding-padding\" style=\"overflow-wrap:break-word;word-break:break-word;padding:10px 43px;font-family:'Raleway',sans-serif;\" align=\"left\">\r\n        \r\n  <div class=\"v-text-align\" style=\"font-size: 14px; line-height: 160%; text-align: left; word-wrap: break-word;\">\r\n    <br>\r\n<p style=\"line-height: 160%;\">El usuario con correo: <b>{correoSolicitud}</b> ha solicitado ser parte de los funcionarios del hopital</p>\r\n<br>\r\n<p>Aprueba o deniega la solicitud desde tu panel de gestion de usuarios</p>\r\n<ol>\r\n</ol>\r\n\r\n  </div>\r\n\r\n      </td>\r\n    </tr>\r\n  </tbody>\r\n</table>\r\n\r\n<table id=\"u_content_button_1\" style=\"font-family:'Raleway',sans-serif;\" role=\"presentation\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" border=\"0\">\r\n  <tbody>\r\n    <tr>\r\n      <td class=\"v-container-padding-padding\" style=\"overflow-wrap:break-word;word-break:break-word;padding:10px 10px 10px 43px;font-family:'Raleway',sans-serif;\" align=\"left\">\r\n        \r\n  <!--[if mso]><style>.v-button {{background: transparent !important;}}</style><![endif]-->\r\n<div class=\"v-text-align\" align=\"left\">\r\n  <!--[if mso]><table border=\"0\" cellspacing=\"0\" cellpadding=\"0\"><tr><td align=\"center\" bgcolor=\"#117c5d\" style=\"padding:10px 20px;\" valign=\"top\"><![endif]-->\r\n   \r\n    <!--[if mso]></td></tr></table><![endif]-->\r\n</div>\r\n\r\n      </td>\r\n    </tr>\r\n  </tbody>\r\n</table>\r\n\r\n<table id=\"u_content_text_2\" style=\"font-family:'Raleway',sans-serif;\" role=\"presentation\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" border=\"0\">\r\n  <tbody>\r\n    <tr>\r\n      <td class=\"v-container-padding-padding\" style=\"overflow-wrap:break-word;word-break:break-word;padding:10px 43px 30px;font-family:'Raleway',sans-serif;\" align=\"left\">\r\n        \r\n \r\n\r\n      </td>\r\n    </tr>\r\n  </tbody>\r\n</table>\r\n\r\n  <!--[if (!mso)&(!IE)]><!--></div><!--<![endif]-->\r\n  </div>\r\n</div>\r\n<!--[if (mso)|(IE)]></td><![endif]-->\r\n      <!--[if (mso)|(IE)]></tr></table></td></tr></table><![endif]-->\r\n    </div>\r\n  </div>\r\n  </div>\r\n  \r\n    <!--[if gte mso 9]>\r\n      </v:textbox></v:rect>\r\n    </td>\r\n    </tr>\r\n    </table>\r\n    <![endif]-->\r\n    \r\n\r\n\r\n  \r\n  \r\n<div class=\"u-row-container\" style=\"padding: 0px;background-color: transparent\">\r\n  <div class=\"u-row\" style=\"margin: 0 auto;min-width: 320px;max-width: 600px;overflow-wrap: break-word;word-wrap: break-word;word-break: break-word;background-color: transparent;\">\r\n    <div style=\"border-collapse: collapse;display: table;width: 100%;height: 100%;background-color: transparent;\">\r\n      <!--[if (mso)|(IE)]><table width=\"100%\" cellpadding=\"0\" cellspacing=\"0\" border=\"0\"><tr><td style=\"padding: 0px;background-color: transparent;\" align=\"center\"><table cellpadding=\"0\" cellspacing=\"0\" border=\"0\" style=\"width:600px;\"><tr style=\"background-color: transparent;\"><![endif]-->\r\n      \r\n<!--[if (mso)|(IE)]><td align=\"center\" width=\"600\" style=\"background-color: #117c5d;width: 600px;padding: 0px;border-top: 0px solid transparent;border-left: 0px solid transparent;border-right: 0px solid transparent;border-bottom: 0px solid transparent;border-radius: 0px;-webkit-border-radius: 0px; -moz-border-radius: 0px;\" valign=\"top\"><![endif]-->\r\n<div class=\"u-col u-col-100\" style=\"max-width: 320px;min-width: 600px;display: table-cell;vertical-align: top;\">\r\n  <div style=\"background-color: #AAD9BB;height: 100%;width: 100% !important;border-radius: 0px;-webkit-border-radius: 0px; -moz-border-radius: 0px;\">\r\n  <!--[if (!mso)&(!IE)]><!--><div style=\"box-sizing: border-box; height: 100%; padding: 0px;border-top: 0px solid transparent;border-left: 0px solid transparent;border-right: 0px solid transparent;border-bottom: 0px solid transparent;border-radius: 0px;-webkit-border-radius: 0px; -moz-border-radius: 0px;\"><!--<![endif]-->\r\n  \r\n<table id=\"u_content_heading_4\" style=\"font-family:'Raleway',sans-serif;\" role=\"presentation\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" border=\"0\">\r\n  <tbody>\r\n    <tr>\r\n      <td class=\"v-container-padding-padding\" style=\"overflow-wrap:break-word;word-break:break-word;padding:40px 10px 10px 40px;font-family:'Raleway',sans-serif;\" align=\"left\">\r\n        \r\n  <!--[if mso]><table width=\"100%\"><tr><td><![endif]-->\r\n    <!-- <h1 class=\"v-text-align\" style=\"margin: 0px; color: #000000; line-height: 140%; text-align: left; word-wrap: break-word; font-size: 22px; font-weight: 400;\"><span><span><span>Gracias por preferirnos!!</span></span></span></h1> -->\r\n  <!--[if mso]></td></tr></table><![endif]-->\r\n\r\n      </td>\r\n    </tr>\r\n  </tbody>\r\n</table>\r\n\r\n<table id=\"u_content_text_3\" style=\"font-family:'Raleway',sans-serif;\" role=\"presentation\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" border=\"0\">\r\n  <tbody>\r\n    <tr>\r\n      <td class=\"v-container-padding-padding\" style=\"overflow-wrap:break-word;word-break:break-word;padding:10px 60px 25px 40px;font-family:'Raleway',sans-serif;\" align=\"left\">\r\n        \r\n  <div class=\"v-text-align\" style=\"font-size: 14px; color: #000000; line-height: 170%; text-align: left; word-wrap: break-word;\">\r\n    <!-- <p style=\"line-height: 170%;\"><span data-metadata=\"&lt;!--(figmeta)eyJmaWxlS2V5IjoiaUdkbDhwdlZkTlBQU2x2aTlkeXpHRyIsInBhc3RlSUQiOjMxOTY0Mjc2NCwiZGF0YVR5cGUiOiJzY2VuZSJ9Cg==(/figmeta)--&gt;\" style=\"line-height: 23.8px;\"></span>En el hospital Su salud Primero agradecemos su confianza en nuestros servicios. </p> -->\r\n  </div>\r\n\r\n      </td>\r\n    </tr>\r\n  </tbody>\r\n</table>\r\n\r\n<table style=\"font-family:'Raleway',sans-serif;\" role=\"presentation\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" border=\"0\">\r\n  <tbody>\r\n    <tr>\r\n      <td class=\"v-container-padding-padding\" style=\"overflow-wrap:break-word;word-break:break-word;padding:10px 10px 0px;font-family:'Raleway',sans-serif;\" align=\"left\">\r\n        \r\n  <table height=\"0px\" align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"92%\" style=\"border-collapse: collapse;table-layout: fixed;border-spacing: 0;mso-table-lspace: 0pt;mso-table-rspace: 0pt;vertical-align: top;border-top: 1px solid #BBBBBB;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%\">\r\n    <tbody>\r\n      <tr style=\"vertical-align: top\">\r\n        <td style=\"word-break: break-word;border-collapse: collapse !important;vertical-align: top;font-size: 0px;line-height: 0px;mso-line-height-rule: exactly;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%\">\r\n          <span>&#160;</span>\r\n        </td>\r\n      </tr>\r\n    </tbody>\r\n  </table>\r\n\r\n      </td>\r\n    </tr>\r\n  </tbody>\r\n</table>\r\n\r\n  <!--[if (!mso)&(!IE)]><!--></div><!--<![endif]-->\r\n  </div>\r\n</div>\r\n<!--[if (mso)|(IE)]></td><![endif]-->\r\n      <!--[if (mso)|(IE)]></tr></table></td></tr></table><![endif]-->\r\n    </div>\r\n  </div>\r\n  </div>\r\n  \r\n\r\n\r\n    <!--[if (mso)|(IE)]></td></tr></table><![endif]-->\r\n    </td>\r\n  </tr>\r\n  </tbody>\r\n  </table>\r\n  <!--[if mso]></div><![endif]-->\r\n  <!--[if IE]></div><![endif]-->\r\n</body>\r\n\r\n</html>\r\n";

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
                            emailContent.Html = $"<!DOCTYPE HTML PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional //EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\">\r\n<html xmlns=\"http://www.w3.org/1999/xhtml\" xmlns:v=\"urn:schemas-microsoft-com:vml\" xmlns:o=\"urn:schemas-microsoft-com:office:office\">\r\n<head>\r\n  <meta http-equiv=\"Content-Type\" content=\"text/html; charset=UTF-8\">\r\n  <meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\">\r\n  <meta name=\"x-apple-disable-message-reformatting\">\r\n  <meta http-equiv=\"X-UA-Compatible\" content=\"IE=edge\">\r\n  <title></title>\r\n  \r\n    <style type=\"text/css\">\r\n      @media only screen and (min-width: 620px) {{\r\n  .u-row {{\r\n    width: 600px !important;\r\n  }}\r\n  .u-row .u-col {{\r\n    vertical-align: top;\r\n  }}\r\n\r\n  .u-row .u-col-100 {{\r\n    width: 600px !important;\r\n  }}\r\n\r\n}}\r\n\r\n@media (max-width: 620px) {{\r\n  .u-row-container {{\r\n    max-width: 100% !important;\r\n    padding-left: 0px !important;\r\n    padding-right: 0px !important;\r\n  }}\r\n  .u-row .u-col {{\r\n    min-width: 320px !important;\r\n    max-width: 100% !important;\r\n    display: block !important;\r\n  }}\r\n  .u-row {{\r\n    width: 100% !important;\r\n  }}\r\n  .u-col {{\r\n    width: 100% !important;\r\n  }}\r\n  .u-col > div {{\r\n    margin: 0 auto;\r\n  }}\r\n}}\r\nbody {{\r\n  margin: 0;\r\n  padding: 0;\r\n}}\r\n\r\ntable,\r\ntr,\r\ntd {{\r\n  vertical-align: top;\r\n  border-collapse: collapse;\r\n}}\r\n\r\np {{\r\n  margin: 0;\r\n}}\r\n\r\n.ie-container table,\r\n.mso-container table {{\r\n  table-layout: fixed;\r\n}}\r\n\r\n* {{\r\n  line-height: inherit;\r\n}}\r\n\r\na[x-apple-data-detectors='true'] {{\r\n  color: inherit !important;\r\n  text-decoration: none !important;\r\n}}\r\n\r\ntable, td {{ color: #000000; }} #u_body a {{ color: #0000ee; text-decoration: underline; }} @media (max-width: 480px) {{ #u_content_heading_1 .v-container-padding-padding {{ padding: 51px 10px 30px !important; }} #u_content_heading_1 .v-text-align {{ text-align: center !important; }} #u_content_heading_2 .v-container-padding-padding {{ padding: 20px 10px 27px !important; }} #u_content_heading_3 .v-container-padding-padding {{ padding: 51px 10px 50px !important; }} #u_content_text_1 .v-container-padding-padding {{ padding: 10px !important; }} #u_content_button_1 .v-container-padding-padding {{ padding: 10px !important; }} #u_content_button_1 .v-size-width {{ width: 80% !important; }} #u_content_text_2 .v-container-padding-padding {{ padding: 10px 10px 40px !important; }} #u_content_heading_4 .v-container-padding-padding {{ padding: 40px 10px 10px !important; }} #u_content_heading_4 .v-text-align {{ text-align: center !important; }} #u_content_text_3 .v-container-padding-padding {{ padding: 10px 40px 0px !important; }} #u_content_text_3 .v-text-align {{ text-align: center !important; }} }}\r\n    </style>\r\n  \r\n  \r\n<link href=\"https://fonts.googleapis.com/css?family=Raleway:400,700&display=swap\" rel=\"stylesheet\" type=\"text/css\">\r\n\r\n</head>\r\n\r\n<body class=\"clean-body u_body\" style=\"margin: 0;padding: 0;-webkit-text-size-adjust: 100%;background-color: #ecf0f1;color: #000000\">\r\n  <table id=\"u_body\" style=\"border-collapse: collapse;table-layout: fixed;border-spacing: 0;mso-table-lspace: 0pt;mso-table-rspace: 0pt;vertical-align: top;min-width: 320px;Margin: 0 auto;background-color: #ecf0f1;width:100%\" cellpadding=\"0\" cellspacing=\"0\">\r\n  <tbody>\r\n  <tr style=\"vertical-align: top\">\r\n    <td style=\"word-break: break-word;border-collapse: collapse !important;vertical-align: top\">\r\n<div class=\"u-row-container\" style=\"margin-top: -100px; padding: 0px;background-image: url('https://cdn.templates.unlayer.com/assets/1712173422382-bg.png');background-repeat: no-repeat;background-position: center top;background-color: transparent\">\r\n  <div class=\"u-row\" style=\"margin: 0 auto;min-width: 320px;max-width: 600px;overflow-wrap: break-word;word-wrap: break-word;word-break: break-word;background-color: transparent;\">\r\n    <div style=\"border-collapse: collapse;display: table;width: 100%;height: 100%;background-color: transparent;\">\r\n      \r\n\r\n\r\n<table id=\"u_content_heading_2\" style=\" font-family:'Raleway',sans-serif;\" role=\"presentation\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" border=\"0\">\r\n  <tbody>\r\n    <tr>\r\n        <img src=\"https://simepciwebb.azurewebsites.net/imgs/landing/Logo.png\" alt=\"\" style=\"max-width: 100px; display: flex; margin-bottom: -50px; margin-top: 100px; \">\r\n      \r\n\r\n      </td>\r\n    </tr>\r\n  </tbody>\r\n</table>\r\n\r\n<table id=\"u_content_heading_3\" style=\"font-family:'Raleway',sans-serif;\" role=\"presentation\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" border=\"0\">\r\n  <tbody>\r\n    <tr>\r\n      <td class=\"v-container-padding-padding\" style=\"overflow-wrap:break-word;word-break:break-word;padding:51px 10px 50px 43px;font-family:'Raleway',sans-serif;\" align=\"left\">\r\n        \r\n  \r\n    <h1 class=\"v-text-align\" style=\"margin-bottom: 100px; color: #c9c9c9; line-height: 120%; text-align: left; word-wrap: break-word; font-size: 29px; font-weight: 400;\"><span><span><span><span><span style=\"line-height: 34.8px;\"><strong><span style=\"line-height: 34.8px;\">Recordatorio de cita</span></strong></span></span></span></span></span></h1>\r\n  \r\n\r\n      </td>\r\n    </tr>\r\n  </tbody>\r\n</table>\r\n\r\n<table id=\"u_content_text_1\" style=\"font-family:'Raleway',sans-serif;\" role=\"presentation\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" border=\"0\">\r\n  <tbody>\r\n    <tr>\r\n      <td class=\"v-container-padding-padding\" style=\"overflow-wrap:break-word;word-break:break-word;padding:10px 43px;font-family:'Raleway',sans-serif;\" align=\"left\">\r\n        \r\n  <div class=\"v-text-align\" style=\"font-size: 14px; line-height: 160%; text-align: left; word-wrap: break-word;\">\r\n    <br>\r\n<p style=\"line-height: 160%; color: #000000 !important; \">Se le recuerda al paciente que tiene una cita agendada para la fecha: <b>{cita.horaInicio}</b> </p>\r\n<ol>\r\n</ol>\r\n\r\n  </div>\r\n\r\n      </td>\r\n    </tr>\r\n  </tbody>\r\n</table>\r\n\r\n<table id=\"u_content_button_1\" style=\"font-family:'Raleway',sans-serif;\" role=\"presentation\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" border=\"0\">\r\n  <tbody>\r\n    <tr>\r\n      <td class=\"v-container-padding-padding\" style=\"overflow-wrap:break-word;word-break:break-word;padding:10px 10px 10px 43px;font-family:'Raleway',sans-serif;\" align=\"left\">\r\n        \r\n<div class=\"v-text-align\" align=\"left\">\r\n\r\n</div>\r\n\r\n      </td>\r\n    </tr>\r\n  </tbody>\r\n</table>\r\n\r\n<table id=\"u_content_text_2\" style=\"font-family:'Raleway',sans-serif;\" role=\"presentation\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" border=\"0\">\r\n  <tbody>\r\n    <tr>\r\n      <td class=\"v-container-padding-padding\" style=\"overflow-wrap:break-word;word-break:break-word;padding:10px 43px 30px;font-family:'Raleway',sans-serif;\" align=\"left\">\r\n        \r\n \r\n\r\n      </td>\r\n    </tr>\r\n  </tbody>\r\n</table>\r\n</div>\r\n  </div>\r\n</div>\r\n    </div>\r\n  </div>\r\n  </div>\r\n  \r\n   \r\n\r\n\r\n  \r\n  \r\n<div class=\"u-row-container\" style=\"padding: 0px;background-color: transparent\">\r\n  <div class=\"u-row\" style=\"margin: 0 auto;min-width: 320px;max-width: 600px;overflow-wrap: break-word;word-wrap: break-word;word-break: break-word;background-color: transparent;\">\r\n    <div style=\"border-collapse: collapse;display: table;width: 100%;height: 100%;background-color: transparent;\">\r\n      \r\n\r\n<div class=\"u-col u-col-100\" style=\"max-width: 320px;min-width: 600px;display: table-cell;vertical-align: top;\">\r\n  <div style=\"background-color: #AAD9BB;height: 100%;width: 100% !important;border-radius: 0px;-webkit-border-radius: 0px; -moz-border-radius: 0px;\">\r\n <div style=\"box-sizing: border-box; height: 100%; padding: 0px;border-top: 0px solid transparent;border-left: 0px solid transparent;border-right: 0px solid transparent;border-bottom: 0px solid transparent;border-radius: 0px;-webkit-border-radius: 0px; -moz-border-radius: 0px;\">\r\n  \r\n<table id=\"u_content_heading_4\" style=\"font-family:'Raleway',sans-serif;\" role=\"presentation\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" border=\"0\">\r\n  <tbody>\r\n    <tr>\r\n      <td class=\"v-container-padding-padding\" style=\"overflow-wrap:break-word;word-break:break-word;padding:40px 10px 10px 40px;font-family:'Raleway',sans-serif;\" align=\"left\">\r\n        \r\n\r\n    <h1 class=\"v-text-align\" style=\"margin: 0px; color: #000000; line-height: 140%; text-align: left; word-wrap: break-word; font-size: 22px; font-weight: 400;\"><span><span><span>Gracias por preferirnos!!</span></span></span></h1>\r\n  \r\n\r\n      </td>\r\n    </tr>\r\n  </tbody>\r\n</table>\r\n\r\n<table id=\"u_content_text_3\" style=\"font-family:'Raleway',sans-serif;\" role=\"presentation\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" border=\"0\">\r\n  <tbody>\r\n    <tr>\r\n      <td class=\"v-container-padding-padding\" style=\"overflow-wrap:break-word;word-break:break-word;padding:10px 60px 25px 40px;font-family:'Raleway',sans-serif;\" align=\"left\">\r\n        \r\n  <div class=\"v-text-align\" style=\"font-size: 14px; color: #000000; line-height: 170%; text-align: left; word-wrap: break-word;\">\r\n    <p style=\"line-height: 170%;\"><span data-metadata=\"&lt;!--(figmeta)eyJmaWxlS2V5IjoiaUdkbDhwdlZkTlBQU2x2aTlkeXpHRyIsInBhc3RlSUQiOjMxOTY0Mjc2NCwiZGF0YVR5cGUiOiJzY2VuZSJ9Cg==(/figmeta)--&gt;\" style=\"line-height: 23.8px;\"></span>En el hospital Su salud Primero agradecemos su confianza en nuestros servicios. </p>\r\n  </div>\r\n\r\n      </td>\r\n    </tr>\r\n  </tbody>\r\n</table>\r\n\r\n<table style=\"font-family:'Raleway',sans-serif;\" role=\"presentation\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" border=\"0\">\r\n  <tbody>\r\n    <tr>\r\n      <td class=\"v-container-padding-padding\" style=\"overflow-wrap:break-word;word-break:break-word;padding:10px 10px 0px;font-family:'Raleway',sans-serif;\" align=\"left\">\r\n        \r\n  <table height=\"0px\" align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"92%\" style=\"border-collapse: collapse;table-layout: fixed;border-spacing: 0;mso-table-lspace: 0pt;mso-table-rspace: 0pt;vertical-align: top;border-top: 1px solid #BBBBBB;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%\">\r\n    <tbody>\r\n      <tr style=\"vertical-align: top\">\r\n        <td style=\"word-break: break-word;border-collapse: collapse !important;vertical-align: top;font-size: 0px;line-height: 0px;mso-line-height-rule: exactly;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%\">\r\n          <span>&#160;</span>\r\n        </td>\r\n      </tr>\r\n    </tbody>\r\n  </table>\r\n\r\n      </td>\r\n    </tr>\r\n  </tbody>\r\n</table>\r\n\r\n  </div>\r\n  </div>\r\n</div>\r\n    </div>\r\n  </div>\r\n  </div>\r\n  \r\n    </td>\r\n  </tr>\r\n  </tbody>\r\n  </table>\r\n</body>\r\n\r\n</html>\r\n"; //Contenido del correo
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
                emailContent.Html = $"<!DOCTYPE HTML PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional //EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\">\r\n<html xmlns=\"http://www.w3.org/1999/xhtml\" xmlns:v=\"urn:schemas-microsoft-com:vml\" xmlns:o=\"urn:schemas-microsoft-com:office:office\">\r\n<head>\r\n<!--[if gte mso 9]>\r\n<xml>\r\n  <o:OfficeDocumentSettings>\r\n    <o:AllowPNG/>\r\n    <o:PixelsPerInch>96</o:PixelsPerInch>\r\n  </o:OfficeDocumentSettings>\r\n</xml>\r\n<![endif]-->\r\n  <meta http-equiv=\"Content-Type\" content=\"text/html; charset=UTF-8\">\r\n  <meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\">\r\n  <meta name=\"x-apple-disable-message-reformatting\">\r\n  <!--[if !mso]><!--><meta http-equiv=\"X-UA-Compatible\" content=\"IE=edge\"><!--<![endif]-->\r\n  <title></title>\r\n  \r\n    <style type=\"text/css\">\r\n      @media only screen and (min-width: 620px) {{\r\n  .u-row {{\r\n    width: 600px !important;\r\n  }}\r\n  .u-row .u-col {{\r\n    vertical-align: top;\r\n  }}\r\n\r\n  .u-row .u-col-100 {{\r\n    width: 600px !important;\r\n  }}\r\n\r\n}}\r\n\r\n@media (max-width: 620px) {{\r\n  .u-row-container {{\r\n    max-width: 100% !important;\r\n    padding-left: 0px !important;\r\n    padding-right: 0px !important;\r\n  }}\r\n  .u-row .u-col {{\r\n    min-width: 320px !important;\r\n    max-width: 100% !important;\r\n    display: block !important;\r\n  }}\r\n  .u-row {{\r\n    width: 100% !important;\r\n  }}\r\n  .u-col {{\r\n    width: 100% !important;\r\n  }}\r\n  .u-col > div {{\r\n    margin: 0 auto;\r\n  }}\r\n}}\r\nbody {{\r\n  margin: 0;\r\n  padding: 0;\r\n}}\r\n\r\ntable,\r\ntr,\r\ntd {{\r\n  vertical-align: top;\r\n  border-collapse: collapse;\r\n}}\r\n\r\np {{\r\n  margin: 0;\r\n}}\r\n\r\n.ie-container table,\r\n.mso-container table {{\r\n  table-layout: fixed;\r\n}}\r\n\r\n* {{\r\n  line-height: inherit;\r\n}}\r\n\r\na[x-apple-data-detectors='true'] {{\r\n  color: inherit !important;\r\n  text-decoration: none !important;\r\n}}\r\n\r\n.black-text{{\r\n  color: #030303 !important;\r\n}}\r\n\r\ntable, td {{ color: #000000; }} #u_body a {{ color: #0000ee; text-decoration: underline; }} @media (max-width: 480px) {{ #u_content_heading_1 .v-container-padding-padding {{ padding: 51px 10px 30px !important; }} #u_content_heading_1 .v-text-align {{ text-align: center !important; }} #u_content_heading_2 .v-container-padding-padding {{ padding: 20px 10px 27px !important; }} #u_content_heading_3 .v-container-padding-padding {{ padding: 51px 10px 50px !important; }} #u_content_text_1 .v-container-padding-padding {{ padding: 10px !important; }} #u_content_button_1 .v-container-padding-padding {{ padding: 10px !important; }} #u_content_button_1 .v-size-width {{ width: 80% !important; }} #u_content_text_2 .v-container-padding-padding {{ padding: 10px 10px 40px !important; }} #u_content_heading_4 .v-container-padding-padding {{ padding: 40px 10px 10px !important; }} #u_content_heading_4 .v-text-align {{ text-align: center !important; }} #u_content_text_3 .v-container-padding-padding {{ padding: 10px 40px 0px !important; }} #u_content_text_3 .v-text-align {{ text-align: center !important; }} }}\r\n    </style>\r\n  \r\n  \r\n\r\n<!--[if !mso]><!--><link href=\"https://fonts.googleapis.com/css?family=Raleway:400,700&display=swap\" rel=\"stylesheet\" type=\"text/css\"><!--<![endif]-->\r\n\r\n</head>\r\n\r\n<body class=\"clean-body u_body\" style=\"margin: 0;padding: 0;-webkit-text-size-adjust: 100%;background-color: #ecf0f1;color: #000000\">\r\n  <!--[if IE]><div class=\"ie-container\"><![endif]-->\r\n  <!--[if mso]><div class=\"mso-container\"><![endif]-->\r\n  <table id=\"u_body\" style=\"border-collapse: collapse;table-layout: fixed;border-spacing: 0;mso-table-lspace: 0pt;mso-table-rspace: 0pt;vertical-align: top;min-width: 320px;Margin: 0 auto;background-color: #ecf0f1;width:100%\" cellpadding=\"0\" cellspacing=\"0\">\r\n  <tbody>\r\n  <tr style=\"vertical-align: top\">\r\n    <td style=\"word-break: break-word;border-collapse: collapse !important;vertical-align: top\">\r\n    <!--[if (mso)|(IE)]><table width=\"100%\" cellpadding=\"0\" cellspacing=\"0\" border=\"0\"><tr><td align=\"center\" style=\"background-color: #ecf0f1;\"><![endif]-->\r\n    \r\n\r\n    <!--[if gte mso 9]>\r\n      <table cellpadding=\"0\" cellspacing=\"0\" border=\"0\" style=\"margin: 0 auto;min-width: 320px;max-width: 600px;\">\r\n        <tr>\r\n          <td background=\"https://cdn.templates.unlayer.com/assets/1712173422382-bg.png\" valign=\"top\" width=\"100%\">\r\n      <v:rect xmlns:v=\"urn:schemas-microsoft-com:vml\" fill=\"true\" stroke=\"false\" style=\"width: 600px;\">\r\n        <v:fill type=\"frame\" src=\"https://cdn.templates.unlayer.com/assets/1712173422382-bg.png\" /><v:textbox style=\"mso-fit-shape-to-text:true\" inset=\"0,0,0,0\">\r\n      <![endif]-->\r\n  \r\n<div class=\"u-row-container\" style=\"margin-top: -100px; padding: 0px;background-image: url('https://cdn.templates.unlayer.com/assets/1712173422382-bg.png');background-repeat: no-repeat;background-position: center top;background-color: transparent\">\r\n  <div class=\"u-row\" style=\"margin: 0 auto;min-width: 320px;max-width: 600px;overflow-wrap: break-word;word-wrap: break-word;word-break: break-word;background-color: transparent;\">\r\n    <div style=\"border-collapse: collapse;display: table;width: 100%;height: 100%;background-color: transparent;\">\r\n      <!--[if (mso)|(IE)]><table width=\"100%\" cellpadding=\"0\" cellspacing=\"0\" border=\"0\"><tr><td style=\"padding: 0px;background-image: url('images/image-1.png');background-repeat: no-repeat;background-position: center top;background-color: transparent;\" align=\"center\"><table cellpadding=\"0\" cellspacing=\"0\" border=\"0\" style=\"width:600px;\"><tr style=\"background-color: transparent;\"><![endif]-->\r\n      \r\n<!--[if (mso)|(IE)]><td align=\"center\" width=\"600\" style=\"width: 600px;padding: 0px;border-top: 0px solid transparent;border-left: 0px solid transparent;border-right: 0px solid transparent;border-bottom: 0px solid transparent;\" valign=\"top\"><![endif]-->\r\n<div class=\"u-col u-col-100\" style=\"max-width: 320px;min-width: 600px;display: table-cell;vertical-align: top;\">\r\n  <div style=\"height: 100%;width: 100% !important;\">\r\n  <!--[if (!mso)&(!IE)]><!--><div style=\"box-sizing: border-box; height: 100%; padding: 0px;border-top: 0px solid transparent;border-left: 0px solid transparent;border-right: 0px solid transparent;border-bottom: 0px solid transparent;\"><!--<![endif]-->\r\n  \r\n\r\n<table id=\"u_content_heading_2\" style=\"font-family:'Raleway',sans-serif;\" role=\"presentation\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" border=\"0\">\r\n  <tbody>\r\n    <tr>\r\n        <img src=\"https://simepciwebb.azurewebsites.net/imgs/landing/Logo.png\" alt=\"\" style=\"max-width: 100px; display: flex; margin-bottom: -50px; margin-top: 100px; \">\r\n        \r\n  <!--[if mso]><table width=\"100%\"><tr><td><![endif]-->\r\n  <!--[if mso]></td></tr></table><![endif]-->\r\n\r\n      </td>\r\n    </tr>\r\n  </tbody>\r\n</table>\r\n\r\n<table id=\"u_content_heading_3\" style=\"font-family:'Raleway',sans-serif;\" role=\"presentation\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" border=\"0\">\r\n  <tbody>\r\n    <tr>\r\n      <td class=\"v-container-padding-padding\" style=\"overflow-wrap:break-word;word-break:break-word;padding:51px 10px 50px 43px;font-family:'Raleway',sans-serif;\" align=\"left\">\r\n        \r\n  <!--[if mso]><table width=\"100%\"><tr><td><![endif]-->\r\n    <h1 class=\"v-text-align\" style=\"margin-bottom: 100px; color: #c9c9c9; line-height: 120%; text-align: left; word-wrap: break-word; font-size: 29px; font-weight: 400;\"><span><span><span><span><span style=\"line-height: 34.8px;\"><strong><span style=\"line-height: 34.8px;\">Confirmacion de Pago</span></strong></span></span></span></span></span></h1>\r\n  <!--[if mso]></td></tr></table><![endif]-->\r\n\r\n      </td>\r\n    </tr>\r\n  </tbody>\r\n</table>\r\n\r\n<table id=\"u_content_text_1\" style=\"font-family:'Raleway',sans-serif; \" role=\"presentation\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" border=\"0\" >\r\n  <tbody>\r\n    <tr>\r\n      <td class=\"v-container-padding-padding\" style=\"overflow-wrap:break-word;word-break:break-word;padding:10px 43px;font-family:'Raleway',sans-serif;\" align=\"left\">\r\n        \r\n  <div class=\"v-text-align\" style=\"font-size: 14px; line-height: 160%; text-align: left; word-wrap: break-word;\">\r\n    <br>\r\n    <p class=\"black-text\">Estimado/a  {usuarioPaciente.nombre}</p>\r\n    <p class=\"black-text\">Su pago se ha completado de forma exitosa por el monto de {factura.monto}</p>\r\n<ol>\r\n</ol>\r\n\r\n  </div>\r\n\r\n      </td>\r\n    </tr>\r\n  </tbody>\r\n</table>\r\n\r\n<table id=\"u_content_button_1\" style=\"font-family:'Raleway',sans-serif;\" role=\"presentation\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" border=\"0\">\r\n  <tbody>\r\n    <tr>\r\n      <td class=\"v-container-padding-padding\" style=\"overflow-wrap:break-word;word-break:break-word;padding:10px 10px 10px 43px;font-family:'Raleway',sans-serif;\" align=\"left\">\r\n        \r\n  <!--[if mso]><style>.v-button {{background: transparent !important;}}</style><![endif]-->\r\n<div class=\"v-text-align\" align=\"left\">\r\n  <!--[if mso]><table border=\"0\" cellspacing=\"0\" cellpadding=\"0\"><tr><td align=\"center\" bgcolor=\"#117c5d\" style=\"padding:10px 20px;\" valign=\"top\"><![endif]-->\r\n   \r\n    <!--[if mso]></td></tr></table><![endif]-->\r\n</div>\r\n\r\n      </td>\r\n    </tr>\r\n  </tbody>\r\n</table>\r\n\r\n<table id=\"u_content_text_2\" style=\"font-family:'Raleway',sans-serif;\" role=\"presentation\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" border=\"0\">\r\n  <tbody>\r\n    <tr>\r\n      <td class=\"v-container-padding-padding\" style=\"overflow-wrap:break-word;word-break:break-word;padding:10px 43px 30px;font-family:'Raleway',sans-serif;\" align=\"left\">\r\n        \r\n \r\n\r\n      </td>\r\n    </tr>\r\n  </tbody>\r\n</table>\r\n\r\n  <!--[if (!mso)&(!IE)]><!--></div><!--<![endif]-->\r\n  </div>\r\n</div>\r\n<!--[if (mso)|(IE)]></td><![endif]-->\r\n      <!--[if (mso)|(IE)]></tr></table></td></tr></table><![endif]-->\r\n    </div>\r\n  </div>\r\n  </div>\r\n  \r\n    <!--[if gte mso 9]>\r\n      </v:textbox></v:rect>\r\n    </td>\r\n    </tr>\r\n    </table>\r\n    <![endif]-->\r\n    \r\n\r\n\r\n  \r\n  \r\n<div class=\"u-row-container\" style=\"padding: 0px;background-color: transparent\">\r\n  <div class=\"u-row\" style=\"margin: 0 auto;min-width: 320px;max-width: 600px;overflow-wrap: break-word;word-wrap: break-word;word-break: break-word;background-color: transparent;\">\r\n    <div style=\"border-collapse: collapse;display: table;width: 100%;height: 100%;background-color: transparent;\">\r\n      <!--[if (mso)|(IE)]><table width=\"100%\" cellpadding=\"0\" cellspacing=\"0\" border=\"0\"><tr><td style=\"padding: 0px;background-color: transparent;\" align=\"center\"><table cellpadding=\"0\" cellspacing=\"0\" border=\"0\" style=\"width:600px;\"><tr style=\"background-color: transparent;\"><![endif]-->\r\n      \r\n<!--[if (mso)|(IE)]><td align=\"center\" width=\"600\" style=\"background-color: #117c5d;width: 600px;padding: 0px;border-top: 0px solid transparent;border-left: 0px solid transparent;border-right: 0px solid transparent;border-bottom: 0px solid transparent;border-radius: 0px;-webkit-border-radius: 0px; -moz-border-radius: 0px;\" valign=\"top\"><![endif]-->\r\n<div class=\"u-col u-col-100\" style=\"max-width: 320px;min-width: 600px;display: table-cell;vertical-align: top;\">\r\n  <div style=\"background-color: #AAD9BB;height: 100%;width: 100% !important;border-radius: 0px;-webkit-border-radius: 0px; -moz-border-radius: 0px;\">\r\n  <!--[if (!mso)&(!IE)]><!--><div style=\"box-sizing: border-box; height: 100%; padding: 0px;border-top: 0px solid transparent;border-left: 0px solid transparent;border-right: 0px solid transparent;border-bottom: 0px solid transparent;border-radius: 0px;-webkit-border-radius: 0px; -moz-border-radius: 0px;\"><!--<![endif]-->\r\n  \r\n<table id=\"u_content_heading_4\" style=\"font-family:'Raleway',sans-serif;\" role=\"presentation\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" border=\"0\">\r\n  <tbody>\r\n    <tr>\r\n      <td class=\"v-container-padding-padding\" style=\"overflow-wrap:break-word;word-break:break-word;padding:40px 10px 10px 40px;font-family:'Raleway',sans-serif;\" align=\"left\">\r\n        \r\n  <!--[if mso]><table width=\"100%\"><tr><td><![endif]-->\r\n    <h1 class=\"v-text-align\" style=\"margin: 0px; color: #000000; line-height: 140%; text-align: left; word-wrap: break-word; font-size: 22px; font-weight: 400;\"><span><span><span>Gracias por preferirnos!!</span></span></span></h1>\r\n  <!--[if mso]></td></tr></table><![endif]-->\r\n\r\n      </td>\r\n    </tr>\r\n  </tbody>\r\n</table>\r\n\r\n<table id=\"u_content_text_3\" style=\"font-family:'Raleway',sans-serif;\" role=\"presentation\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" border=\"0\">\r\n  <tbody>\r\n    <tr>\r\n      <td class=\"v-container-padding-padding\" style=\"overflow-wrap:break-word;word-break:break-word;padding:10px 60px 25px 40px;font-family:'Raleway',sans-serif;\" align=\"left\">\r\n        \r\n  <div class=\"v-text-align\" style=\"font-size: 14px; color: #000000; line-height: 170%; text-align: left; word-wrap: break-word;\">\r\n    <p style=\"line-height: 170%;\"><span data-metadata=\"&lt;!--(figmeta)eyJmaWxlS2V5IjoiaUdkbDhwdlZkTlBQU2x2aTlkeXpHRyIsInBhc3RlSUQiOjMxOTY0Mjc2NCwiZGF0YVR5cGUiOiJzY2VuZSJ9Cg==(/figmeta)--&gt;\" style=\"line-height: 23.8px;\"></span>En el hospital Su salud Primero agradecemos su confianza en nuestros servicios. </p>\r\n  </div>\r\n\r\n      </td>\r\n    </tr>\r\n  </tbody>\r\n</table>\r\n\r\n<table style=\"font-family:'Raleway',sans-serif;\" role=\"presentation\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" border=\"0\">\r\n  <tbody>\r\n    <tr>\r\n      <td class=\"v-container-padding-padding\" style=\"overflow-wrap:break-word;word-break:break-word;padding:10px 10px 0px;font-family:'Raleway',sans-serif;\" align=\"left\">\r\n        \r\n  <table height=\"0px\" align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"92%\" style=\"border-collapse: collapse;table-layout: fixed;border-spacing: 0;mso-table-lspace: 0pt;mso-table-rspace: 0pt;vertical-align: top;border-top: 1px solid #BBBBBB;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%\">\r\n    <tbody>\r\n      <tr style=\"vertical-align: top\">\r\n        <td style=\"word-break: break-word;border-collapse: collapse !important;vertical-align: top;font-size: 0px;line-height: 0px;mso-line-height-rule: exactly;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%\">\r\n          <span>&#160;</span>\r\n        </td>\r\n      </tr>\r\n    </tbody>\r\n  </table>\r\n\r\n      </td>\r\n    </tr>\r\n  </tbody>\r\n</table>\r\n\r\n  <!--[if (!mso)&(!IE)]><!--></div><!--<![endif]-->\r\n  </div>\r\n</div>\r\n<!--[if (mso)|(IE)]></td><![endif]-->\r\n      <!--[if (mso)|(IE)]></tr></table></td></tr></table><![endif]-->\r\n    </div>\r\n  </div>\r\n  </div>\r\n  \r\n\r\n\r\n    <!--[if (mso)|(IE)]></td></tr></table><![endif]-->\r\n    </td>\r\n  </tr>\r\n  </tbody>\r\n  </table>\r\n  <!--[if mso]></div><![endif]-->\r\n  <!--[if IE]></div><![endif]-->\r\n</body>\r\n\r\n</html>\r\n"; //Contenido del correo
                
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
            emailContent.Html = @$"<!DOCTYPE HTML PUBLIC ""-//W3C//DTD XHTML 1.0 Transitional //EN"" ""http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd"">
<html xmlns=""http://www.w3.org/1999/xhtml"" xmlns:v=""urn:schemas-microsoft-com:vml"" xmlns:o=""urn:schemas-microsoft-com:office:office"">
<head>
  <meta http-equiv=""Content-Type"" content=""text/html; charset=UTF-8"">
  <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
  <meta name=""x-apple-disable-message-reformatting"">
 <meta http-equiv=""X-UA-Compatible"" content=""IE=edge""><!--<![endif]-->
  <title></title>
  
    <style type=""text/css"">
      @media only screen and (min-width: 620px) {{
  .u-row {{
    width: 600px !important;
  }}
  .u-row .u-col {{
    vertical-align: top;
  }}

  .u-row .u-col-100 {{
    width: 600px !important;
  }}

}}

@media (max-width: 620px) {{
  .u-row-container {{
    max-width: 100% !important;
    padding-left: 0px !important;
    padding-right: 0px !important;
  }}
  .u-row .u-col {{
    min-width: 320px !important;
    max-width: 100% !important;
    display: block !important;
  }}
  .u-row {{
    width: 100% !important;
  }}
  .u-col {{
    width: 100% !important;
  }}
  .u-col > div {{
    margin: 0 auto;
  }}
}}
body {{
  margin: 0;
  padding: 0;
}}

table,
tr,
td {{
  vertical-align: top;
  border-collapse: collapse;
}}

p {{
  margin: 0;
}}

.ie-container table,
.mso-container table {{
  table-layout: fixed;
}}

* {{
  line-height: inherit;
}}

a[x-apple-data-detectors='true'] {{
  color: inherit !important;
  text-decoration: none !important;
}}

table, td {{ color: #000000; }} #u_body a {{ color: #0000ee; text-decoration: underline; }} @media (max-width: 480px) {{ #u_content_heading_1 .v-container-padding-padding {{ padding: 51px 10px 30px !important; }} #u_content_heading_1 .v-text-align {{ text-align: center !important; }} #u_content_heading_2 .v-container-padding-padding {{ padding: 20px 10px 27px !important; }} #u_content_heading_3 .v-container-padding-padding {{ padding: 51px 10px 50px !important; }} #u_content_text_1 .v-container-padding-padding {{ padding: 10px !important; }} #u_content_button_1 .v-container-padding-padding {{ padding: 10px !important; }} #u_content_button_1 .v-size-width {{ width: 80% !important; }} #u_content_text_2 .v-container-padding-padding {{ padding: 10px 10px 40px !important; }} #u_content_heading_4 .v-container-padding-padding {{ padding: 40px 10px 10px !important; }} #u_content_heading_4 .v-text-align {{ text-align: center !important; }} #u_content_text_3 .v-container-padding-padding {{ padding: 10px 40px 0px !important; }} #u_content_text_3 .v-text-align {{ text-align: center !important; }} }}
    </style>
  
  

<link href=""https://fonts.googleapis.com/css?family=Raleway:400,700&display=swap"" rel=""stylesheet"" type=""text/css"">

</head>

<body class=""clean-body u_body"" style=""margin: 0;padding: 0;-webkit-text-size-adjust: 100%;background-color: #ecf0f1;color: #000000"">
 
  <table id=""u_body"" style=""border-collapse: collapse;table-layout: fixed;border-spacing: 0;mso-table-lspace: 0pt;mso-table-rspace: 0pt;vertical-align: top;min-width: 320px;Margin: 0 auto;background-color: #ecf0f1;width:100%"" cellpadding=""0"" cellspacing=""0"">
  <tbody>
  <tr style=""vertical-align: top"">
    <td style=""word-break: break-word;border-collapse: collapse !important;vertical-align: top"">
 
    


  
<div class=""u-row-container"" style="" margin-top: -100px; padding: 0px;background-image: url('https://cdn.templates.unlayer.com/assets/1712173422382-bg.png');background-repeat: no-repeat;background-position: center top;background-color: transparent"">
  <div class=""u-row"" style=""margin: 0 auto;min-width: 320px;max-width: 600px;overflow-wrap: break-word;word-wrap: break-word;word-break: break-word;background-color: transparent;"">
    <div style=""border-collapse: collapse;display: table;width: 100%;height: 100%;background-color: transparent;"">
    
      

<div class=""u-col u-col-100"" style=""max-width: 320px;min-width: 600px;display: table-cell;vertical-align: top;"">
  <div style=""height: 100%;width: 100% !important;"">
  <div style=""box-sizing: border-box; height: 100%; padding: 0px;border-top: 0px solid transparent;border-left: 0px solid transparent;border-right: 0px solid transparent;border-bottom: 0px solid transparent;""><!--<![endif]-->
  

<table id=""u_content_heading_2"" style=""font-family:'Raleway',sans-serif;"" role=""presentation"" cellpadding=""0"" cellspacing=""0"" width=""100%"" border=""0"">
  <tbody>
    <tr>
        <img src=""https://simepciwebb.azurewebsites.net/imgs/landing/Logo.png"" alt="""" style=""max-width: 100px; display: flex; margin-bottom: -50px; margin-top: 100px; "">
        
 

      </td>
    </tr>
  </tbody>
</table>

<table id=""u_content_heading_3"" style=""font-family:'Raleway',sans-serif;"" role=""presentation"" cellpadding=""0"" cellspacing=""0"" width=""100%"" border=""0"">
  <tbody>
    <tr>
      <td class=""v-container-padding-padding"" style=""overflow-wrap:break-word;word-break:break-word;padding:51px 10px 50px 43px;font-family:'Raleway',sans-serif;"" align=""left"">
        
 
    <h1 class=""v-text-align"" style=""margin-bottom: 100px; color: #c9c9c9; line-height: 120%; text-align: left; word-wrap: break-word; font-size: 29px; font-weight: 400;""><span><span><span><span><span style=""line-height: 34.8px;""><strong><span style=""line-height: 34.8px;"">Confirmacion de Cita</span></strong></span></span></span></span></span></h1>
 

      </td>
    </tr>
  </tbody>
</table>

<table id=""u_content_text_1"" style=""font-family:'Raleway',sans-serif;"" role=""presentation"" cellpadding=""0"" cellspacing=""0"" width=""100%"" border=""0"">
  <tbody>
    <tr>
      <td class=""v-container-padding-padding"" style=""overflow-wrap:break-word;word-break:break-word;padding:10px 43px;font-family:'Raleway',sans-serif;"" align=""left"">
        
  <div class=""v-text-align"" style=""font-size: 14px; line-height: 160%; text-align: left; word-wrap: break-word;"">
    <br>
    <p style=""line-height: 160%;"">Estimado/a  {usuarioPaciente.nombre}</p>
    <p style=""line-height: 160%;"">Su cita para la fecha {horaInicioCr} se ha confirmado</p>
<ol>
</ol>

  </div>

      </td>
    </tr>
  </tbody>
</table>

<table id=""u_content_button_1"" style=""font-family:'Raleway',sans-serif;"" role=""presentation"" cellpadding=""0"" cellspacing=""0"" width=""100%"" border=""0"">
  <tbody>
    <tr>
      <td class=""v-container-padding-padding"" style=""overflow-wrap:break-word;word-break:break-word;padding:10px 10px 10px 43px;font-family:'Raleway',sans-serif;"" align=""left"">
        
  
<div class=""v-text-align"" align=""left"">
 
   
   
</div>

      </td>
    </tr>
  </tbody>
</table>

<table id=""u_content_text_2"" style=""font-family:'Raleway',sans-serif;"" role=""presentation"" cellpadding=""0"" cellspacing=""0"" width=""100%"" border=""0"">
  <tbody>
    <tr>
      <td class=""v-container-padding-padding"" style=""overflow-wrap:break-word;word-break:break-word;padding:10px 43px 30px;font-family:'Raleway',sans-serif;"" align=""left"">
        
 

      </td>
    </tr>
  </tbody>
</table>

 </div>
  </div>
</div>

    </div>
  </div>
  </div>
  
    


  
  
<div class=""u-row-container"" style=""padding: 0px;background-color: transparent"">
  <div class=""u-row"" style=""margin: 0 auto;min-width: 320px;max-width: 600px;overflow-wrap: break-word;word-wrap: break-word;word-break: break-word;background-color: transparent;"">
    <div style=""border-collapse: collapse;display: table;width: 100%;height: 100%;background-color: transparent;"">
      
<div class=""u-col u-col-100"" style=""max-width: 320px;min-width: 600px;display: table-cell;vertical-align: top;"">
  <div style=""background-color: #AAD9BB;height: 100%;width: 100% !important;border-radius: 0px;-webkit-border-radius: 0px; -moz-border-radius: 0px;"">
  <div style=""box-sizing: border-box; height: 100%; padding: 0px;border-top: 0px solid transparent;border-left: 0px solid transparent;border-right: 0px solid transparent;border-bottom: 0px solid transparent;border-radius: 0px;-webkit-border-radius: 0px; -moz-border-radius: 0px;""><!--<![endif]-->
  
<table id=""u_content_heading_4"" style=""font-family:'Raleway',sans-serif;"" role=""presentation"" cellpadding=""0"" cellspacing=""0"" width=""100%"" border=""0"">
  <tbody>
    <tr>
      <td class=""v-container-padding-padding"" style=""overflow-wrap:break-word;word-break:break-word;padding:40px 10px 10px 40px;font-family:'Raleway',sans-serif;"" align=""left"">
        
  
    <h1 class=""v-text-align"" style=""margin: 0px; color: #000000; line-height: 140%; text-align: left; word-wrap: break-word; font-size: 22px; font-weight: 400;""><span><span><span>Gracias por preferirnos!!</span></span></span></h1>
  

      </td>
    </tr>
  </tbody>
</table>

<table id=""u_content_text_3"" style=""font-family:'Raleway',sans-serif;"" role=""presentation"" cellpadding=""0"" cellspacing=""0"" width=""100%"" border=""0"">
  <tbody>
    <tr>
      <td class=""v-container-padding-padding"" style=""overflow-wrap:break-word;word-break:break-word;padding:10px 60px 25px 40px;font-family:'Raleway',sans-serif;"" align=""left"">
        
  <div class=""v-text-align"" style=""font-size: 14px; color: #000000; line-height: 170%; text-align: left; word-wrap: break-word;"">
    <p style=""line-height: 170%;""><span data-metadata=""&lt;!--(figmeta)eyJmaWxlS2V5IjoiaUdkbDhwdlZkTlBQU2x2aTlkeXpHRyIsInBhc3RlSUQiOjMxOTY0Mjc2NCwiZGF0YVR5cGUiOiJzY2VuZSJ9Cg==(/figmeta)--&gt;"" style=""line-height: 23.8px;""></span>En el hospital Su salud Primero agradecemos su confianza en nuestros servicios. </p>
  </div>

      </td>
    </tr>
  </tbody>
</table>

<table style=""font-family:'Raleway',sans-serif;"" role=""presentation"" cellpadding=""0"" cellspacing=""0"" width=""100%"" border=""0"">
  <tbody>
    <tr>
      <td class=""v-container-padding-padding"" style=""overflow-wrap:break-word;word-break:break-word;padding:10px 10px 0px;font-family:'Raleway',sans-serif;"" align=""left"">
        
  <table height=""0px"" align=""center"" border=""0"" cellpadding=""0"" cellspacing=""0"" width=""92%"" style=""border-collapse: collapse;table-layout: fixed;border-spacing: 0;mso-table-lspace: 0pt;mso-table-rspace: 0pt;vertical-align: top;border-top: 1px solid #BBBBBB;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%"">
    <tbody>
      <tr style=""vertical-align: top"">
        <td style=""word-break: break-word;border-collapse: collapse !important;vertical-align: top;font-size: 0px;line-height: 0px;mso-line-height-rule: exactly;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%"">
          <span>&#160;</span>
        </td>
      </tr>
    </tbody>
  </table>

      </td>
    </tr>
  </tbody>
</table>
  </div>
</div>
    </div>
  </div>
  </div>
    </td>
  </tr>
  </tbody>
  </table>
</body>

</html>

"; //Contenido del correo

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
