﻿using AppLogic;
using DTO.EspecialidadesMedicas;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SIMEPCI_API.Controllers
{
    [EnableCors("Simepci-web-policy")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class EspecialidadMedicaController : ControllerBase
    {
        EspecialidadMedicaManager manager = new EspecialidadMedicaManager();


        [HttpGet]
        public EspecialidadMedica GetEspecialidadById(int idEspecialidadMedica)
        {
            return manager.GetEspecialidadById(idEspecialidadMedica);
        }

        [HttpPost]
        public string CrearEspecialidadMedica(EspecialidadMedicaInsert especialidadMedicaInsert)
        {
            return manager.CreateEspecialidad(especialidadMedicaInsert);
        }
    }
}
