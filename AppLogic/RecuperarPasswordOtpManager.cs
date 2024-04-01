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
            string otp = GetLastPasswordOtpByEmail(correo);
            return otp.Equals(otpInput);
        }


        public string GetLastPasswordOtpByEmail(string correo) {

            return otpCrud.GetPasswordOtpByEmail(correo);
        }

        
    }
}
