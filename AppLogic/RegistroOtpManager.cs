using Azure;
using DataAccess.Crud;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppLogic
{
    public class RegistroOtpManager
    {
        RegistroOtpCrud registroOtpCrud;
        public RegistroOtpManager() {
             registroOtpCrud = new RegistroOtpCrud();
        }
        public void CrearOtp(RegistroOtp loginOtp)
        {
            registroOtpCrud.Delete(loginOtp.correoUsuario);
            registroOtpCrud.Create(loginOtp);

        }

        public RegistroOtp GetRegistroOtpByEmail(string correoUsuario) {
            return registroOtpCrud.GetOtpByEmail(correoUsuario);
            
        }

        public bool ValidarOtp(string correoUsuario, string otpInput)
        {
            RegistroOtp otpEncontrado = GetRegistroOtpByEmail(correoUsuario);
            DateTime horaActual = DateTime.Now;
          
            TimeSpan diferencia = horaActual - otpEncontrado.fechaCreacion.AddHours(-6);
            Console.WriteLine(diferencia);
            Console.WriteLine(diferencia.Minutes);
            if (diferencia.Minutes > 1 )
            {
                return false;
            }
            return otpEncontrado.codigoOtp == otpInput;


        }
    }
}
