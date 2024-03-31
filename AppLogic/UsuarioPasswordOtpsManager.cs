using DataAccess.Crud;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppLogic
{
    public class UsuarioPasswordOtpsManager
    {
        UsuarioPasswordOtpsCrud crud = new UsuarioPasswordOtpsCrud();

        public void CrearUsuarioPasswordOtps(int idUsuario, int idRecuperarPasswordOtp)
        {
            UsuarioPasswordsOtps usuarioPasswordsOtps = new UsuarioPasswordsOtps();
            usuarioPasswordsOtps.idUsuario = idUsuario;
            usuarioPasswordsOtps.idRecuperarPasswordOtp = idRecuperarPasswordOtp;
            crud.Create(usuarioPasswordsOtps);
        }

        public List<UsuarioPasswordsOtps> GetUsuarioPasswordOtps (int idUsuario, int idRecuperarPasswordOtp){

            return crud.RetrieveByIds(idUsuario,idRecuperarPasswordOtp);
        }
    }
}
