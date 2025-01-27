﻿using AppLogic;
using DataAccess.Crud;
using DTO;
using DTO.Doctores;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;

namespace SIMEPCI_API.Controllers
{
    [EnableCors("Simepci-web-policy")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DoctorController : ControllerBase
    {
        DoctorManager manager = new DoctorManager();

        [HttpGet]

        public List<DoctorOutput> GetAllDoctores()
        {
            return manager.GetAllDoctors();
        }
        [HttpPatch]
        public string CambiarHorarioDoctror(int idDoctor, int horario)
        {
            return manager.UpdateHorarioDoctor(idDoctor, horario);
        }

        [HttpPatch]
        public string CambiarSedeDoctror(int idDoctor, int idSede)
        {
            return manager.UpdateSedeDoctor(idDoctor, idSede);
        }
        [HttpGet]
        public List<DoctorOutput> getDoctoresBySedeAndEspecialidad(int idSede, int idEspecialidad)
        {
            return manager.GetDoctoresByEspecialidadAndSede(idSede, idEspecialidad);
        }

    }
}
