using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess;
using DataAccess.Crud;
using DTO;

namespace AppLogic
{
    public class RecuperarPasswordOtpManager
    {
        RecuperarPasswordOtpCrud otpCrud; 
        public RecuperarPasswordOtpManager() {
            otpCrud = new RecuperarPasswordOtpCrud();
        }


        public int CrearPasswordOtp(string codigo)
        {
            RecuperarPasswordOtp recuperarPasswordOtp = new RecuperarPasswordOtp();
            recuperarPasswordOtp.codigo = codigo;
            return otpCrud.CreateWithRetrieve(recuperarPasswordOtp);
            
        }

        public bool ValidarPasswordOtp(string correo, string otpInput)
        {
            RecuperarPasswordOtp otp = GetLastPasswordOtpByEmail(correo);
            DateTime horaActual = DateTime.Now;

            TimeSpan diferencia = horaActual - otp.fechaCreacion.AddHours(-6);
            Console.WriteLine(diferencia);
            Console.WriteLine(diferencia.Minutes);
            if (diferencia.Minutes > 1)
            {
                return false;
            }
           
            return otp.codigo.Equals(otpInput);
        }


        public RecuperarPasswordOtp GetLastPasswordOtpByEmail(string correo) {

            return otpCrud.GetPasswordOtpByEmail(correo);
        }

        
    }
}
