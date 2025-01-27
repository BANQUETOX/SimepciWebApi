﻿using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using DataAccess.Dao;
using DataAccess.Mapper;
using DTO;
using DTO.Usuarios;

namespace DataAccess.Crud
{
    public class UsuarioCrud 
    {

        UsuarioMapper usuarioMapper;
        SqlDao dao;

        public UsuarioCrud() 
        {
            usuarioMapper = new UsuarioMapper();
            dao = SqlDao.GetInstance();
        }

        public  void Create(BaseClass dto)
        {
            SqlOperation operation = usuarioMapper.GetCreateStatement(dto);
            dao.ExecuteStoredProcedure(operation);

        }
        public void Update(UsuarioUpdate usuario) { 
            SqlOperation operation = usuarioMapper.GetUpdateStatement(usuario);
            dao.ExecuteStoredProcedure(operation);
        }

        public  void Delete(BaseClass dto)
        {
            throw new NotImplementedException();
        }

        public  List<Usuario> RetrieveAll()
        {

            List<Usuario> resultList = new List<Usuario>();
            SqlOperation operation = usuarioMapper.GetRetrieveAllStatement();

            List<Dictionary<string, object>> dataResults = dao.ExecuteStoredProcedureWithQuery(operation);

            if (dataResults.Count > 0)
            {
                var dtoList = usuarioMapper.BuildObjects(dataResults);
                foreach (var dto in dtoList)
                {
                    resultList.Add((Usuario)Convert.ChangeType(dto, typeof(Usuario)));
                }
            }
            return resultList;
        }

        public  Usuario RetrieveById(int id)
        {
            
            SqlOperation operation = usuarioMapper.GetRetrieveByIdStatement(id);
            var result = dao.ExecuteStoredProcedureWithQuery(operation);
            Usuario usuario = (Usuario)usuarioMapper.BuildObject(result[0]);
            return usuario;
            
        }

        public Usuario RetrieveByDoctorId(int idDoctor)
        {

            SqlOperation operation = usuarioMapper.GetUsuarioByDoctorId(idDoctor);
            var result = dao.ExecuteStoredProcedureWithQuery(operation);
            Usuario usuario = (Usuario)usuarioMapper.BuildObject(result[0]);
            return usuario;

        }
        public Usuario RetrieveByPacienteId(int idPaciente)
        {

            SqlOperation operation = usuarioMapper.GetUsuarioByPacienteId(idPaciente);
            var result = dao.ExecuteStoredProcedureWithQuery(operation);
            Usuario usuario = new Usuario();
            if (result.Count> 0) { 
                usuario = (Usuario)usuarioMapper.BuildObject(result[0]);
            }
            return usuario;

        }

        public Usuario RetrieveByCedula(string cedula)
        {
            Usuario usuario = new Usuario();
            SqlOperation operation = usuarioMapper.GetUsuarioByCedula(cedula);
            var result = dao.ExecuteStoredProcedureWithQuery(operation);
            if (result.Count> 0)
            {
                 usuario = (Usuario)usuarioMapper.BuildObject(result[0]);
            }
            return usuario;

        }


        public Usuario RetrieveByFacturaId(int idFactura)
        {

            SqlOperation operation = usuarioMapper.GetUsuarioByFacturaId(idFactura);
            var result = dao.ExecuteStoredProcedureWithQuery(operation);
            Usuario usuario = new Usuario();
            if (result.Count > 0)
            {
                usuario = (Usuario)usuarioMapper.BuildObject(result[0]);
            }
            return usuario;

        }


        public  void Update(BaseClass dto)
        {
            throw new NotImplementedException();
        }


        public Usuario GetUsuarioByEmail(string correo)
        {
            SqlOperation operation = usuarioMapper.GetUserByEmailStatement(correo);
            List<Dictionary<string, object>> dataResults = dao.ExecuteStoredProcedureWithQuery(operation);
            Usuario usuario = null;
            if (dataResults.Count > 0)
            {
                var dtoList = usuarioMapper.BuildObjects(dataResults);
                foreach (var dto in dtoList)
                {
                    usuario = (Usuario)Convert.ChangeType(dto, typeof(Usuario));
                }
            }
            return usuario;
        }


        public Usuario Login(string correo, string password)
        {
            SqlOperation operation = usuarioMapper.Login(correo,password);
            List<Dictionary<string, object>> dataResults = dao.ExecuteStoredProcedureWithQuery(operation);
            Usuario usuario = new Usuario();

            if (dataResults.Count > 0)
            {
                 usuario = (Usuario)usuarioMapper.BuildObject(dataResults[0]);
                
            }
            else
            {
                usuario = (Usuario)usuarioMapper.BuildEmptyObject();
                
            }

            return usuario;
        }

        public string DesactivarUsuario(string correo)
        {
            SqlOperation operation = usuarioMapper.GetDesactivarUsuarioStatement(correo);
            dao.ExecuteStoredProcedureWithQuery(operation);
            return "El usuario a sido desactivado";
        }

        public string ActivarUsuario(string correo)
        {
            SqlOperation operation = usuarioMapper.GetActivarUsuarioStatement(correo);
            dao.ExecuteStoredProcedureWithQuery(operation);
            return "El usuario a sido activado";
        }


        public void UpdatePassword(string correoUsuario, string newPassword)
        {
            SqlOperation operation = usuarioMapper.GetUpdatePasswordStatement(correoUsuario,newPassword);
            dao.ExecuteStoredProcedure(operation);
        }



        public bool verificarCorreo(string correo)
        {
            string patronCorreo = @"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$";
            return Regex.IsMatch(correo, patronCorreo);
        }

    }
}
