﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DTO;
using AppLogic;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Cors;
using DTO.Usuarios;



namespace SIMEPCI_API.Controllers
{
    [EnableCors("Simepci-web-policy")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class RolController : ControllerBase
    {
        RolManager rolManager = new RolManager();
        [HttpGet]
        public List<Rol> GetAllRols()
        {
            List<Rol> result = new List<Rol>();
            

            try
            {
                result = rolManager.GetAllRols(); 

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());   
            }
            return result;
        }

        [HttpGet]
        public List<string> GetRolesUsuario(string correoUsuario)
        {
            List<string> result = new List<string>();
            try
            {
                result = rolManager.GetRolesUsuario(correoUsuario);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return result;
        }

        [HttpPost]
        public string AsignarRolUsuario(Usuario usuario, int idRol)
        {
            string result;
            try
            {
                result = rolManager.AsignarRolUsuario(usuario, idRol);
            }
            catch (Exception ex)
            {
                result = ex.ToString();
            }
            return result;
        }

        [HttpPost]
        public string RemoverRolUsuario(Usuario usuario, int idRol)
        {
            string result;
            try
            {
                result = rolManager.RemoverRolUsuario(usuario, idRol);
            }
            catch (Exception ex)
            {
                result = ex.ToString();
            }
            return result;
        }

      

    }
}
