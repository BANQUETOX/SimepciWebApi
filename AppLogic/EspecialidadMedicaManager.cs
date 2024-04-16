﻿using DataAccess.Crud;
using DTO.EspecialidadesMedicas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppLogic
{
    public class EspecialidadMedicaManager
    {
        EspecialidadMedicaCrud crud = new EspecialidadMedicaCrud();


        public EspecialidadMedica GetEspecialidadById(int idEspecialidadMedica)
        {
            return crud.GetEspecialidadById(idEspecialidadMedica);
        }

        public string CreateEspecialidad(EspecialidadMedicaInsert especialidadMedicaInsert)
        {
            string result;
            try
            {
                crud.CreateEspecialidadMedica(castEspecialidadMedicaInsert(especialidadMedicaInsert));
                result = "Especialidad Creada";
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return result;
        }

        public EspecialidadMedica castEspecialidadMedicaInsert(EspecialidadMedicaInsert insert) { 
            EspecialidadMedica especialidad = new EspecialidadMedica();
            especialidad.costoCita = insert.costoCita;
            especialidad.nombre = insert.nombre;
            return especialidad;
        }
    }
}
