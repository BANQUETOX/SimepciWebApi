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

        public RecuperarPasswordOtp GetRecuperarPasswordOtpByCode(string code)
        {
            return otpCrud.GetPasswordOtpByCode(code);
        }
    }
}
