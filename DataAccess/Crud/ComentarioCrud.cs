using DataAccess.Dao;
using DataAccess.Mapper;
using DTO.Comentarios;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Crud
{
    public class ComentarioCrud
    {
        ComentarioMapper mapper;
        SqlDao dao;

        public ComentarioCrud()
        {
            mapper = new ComentarioMapper();
            dao = new SqlDao();
        }

        public void Create(Comentario comentario)
        {
            SqlOperation operation = mapper.GetCreateStatement(comentario);
            dao.ExecuteStoredProcedure(operation);
        }

       /* public List<Comentario> GetAll()
        {
            SqlOperation operation = mapper.GetRetrieveAllStatement();
        }*/


    }
}
