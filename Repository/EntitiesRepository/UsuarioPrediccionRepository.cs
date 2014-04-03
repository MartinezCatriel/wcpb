using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.EntitiesRepository
{
    public class UsuarioPrediccionRepository
    {
        public List<UsuarioPrediccion> GetByUsuarioId(int idUsuario)
        {
            var rtn = new List<UsuarioPrediccion>();
            using (var ctx = new PredictionSQLEntities())
            {
                var response = (from up in ctx.UsuarioPrediccion.Include("Equipo").Include("Partido").Include("Usuario") where up.IdUsuario == idUsuario select up);
                rtn.AddRange(response.ToList());
            }
            return rtn;
        }

        public List<UsuarioPrediccion> GetByUsuarioIdAndPartidoId(int idUsuario, int idPartido)
        {
            var rtn = new List<UsuarioPrediccion>();
            using (var ctx = new PredictionSQLEntities())
            {
                var response = (from up in ctx.UsuarioPrediccion.Include("Equipo").Include("Partido").Include("Usuario") where up.IdUsuario == idUsuario && up.IdPartido == idPartido select up);
                rtn.AddRange(response.ToList());
            }
            return rtn;
        }

        public void Update(int idpartido, int idusaurio, int? idequipoganador)
        {
            using (var ctx = new PredictionSQLEntities())
            {
                var response = (from up
                                    in ctx.UsuarioPrediccion
                                where up.IdUsuario == idusaurio
                                        && up.IdPartido == idpartido
                                select up);
                if (response.ToList().FirstOrDefault() != null)
                {
                    var toUpdate = response.ToList().FirstOrDefault();
                    if (toUpdate != null)
                    {
                        toUpdate.Predecido = 1;
                        toUpdate.IdEquipoGanador = idequipoganador;
                    }
                }
                else
                {
                    var newPred = new Repository.UsuarioPrediccion();
                    newPred.IdEquipoGanador = idequipoganador;
                    newPred.IdPartido = idpartido;
                    newPred.IdUsuario = idusaurio;
                    newPred.Predecido = 0;

                    ctx.UsuarioPrediccion.AddObject(newPred);
                }
                ctx.SaveChanges();
            }
            UpdateUsuarioPuntaje(idusaurio);
        }

        private void UpdateUsuarioPuntaje(int idusuario)
        {
            using (var ctx = new PredictionSQLEntities())
            {
                var prediccionesAcertadas = (from up
                                               in ctx.UsuarioPrediccion
                                             join par in ctx.Partido on up.IdPartido equals par.Id
                                             where up.Predecido == 1
                                                  && up.IdUsuario == idusuario
                                                  && par.IdEquipoGanador == up.IdEquipoGanador
                                             select up).Count();
                var usuario = (from usu in ctx.Usuario where usu.Id == idusuario select usu).ToList().FirstOrDefault();
                if (usuario != null)
                {
                    usuario.Puntaje = prediccionesAcertadas * 3;
                    ctx.SaveChanges();
                }


            }
        }
    }
}
