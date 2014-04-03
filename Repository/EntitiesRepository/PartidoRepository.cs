using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Objects;
using System.Data.Objects.DataClasses;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.Repositories;

namespace Repository.EntitiesRepository
{
    public class PartidoRepository
    {
        private const int DefaultIdPartido = 0;

        public List<Partido> GetAll()
        {
            var all = new List<Partido>();
            using (var ctx = new PredictionSQLEntities())
            {
                var partidos = (from a in ctx.Partido.Include("Equipo").Include("PartidoEquipo").Include("PartidoEquipo.Equipo") select a);
                all.AddRange(partidos.ToList());
            }
            return all;
        }

        public void UpdateEquiposAndGolesFromPartido(int idPartido, int? idEquipoGanador, List<int> equipos)
        {
            if (idPartido != DefaultIdPartido)
            {
                using (var ctx = new PredictionSQLEntities())
                {
                    var toUpdate = (from p
                                    in ctx.PartidoEquipo
                                    where p.IdPartido == idPartido
                                    select p);
                    var partidos = toUpdate.ToList();

                    //eliminino primero las relaciones de los equipos con el partido actual
                    partidos.ForEach((partido) => { ctx.PartidoEquipo.DeleteObject(partido); });
                    if (partidos.Exists((partido) => { return partido.EntityState == EntityState.Deleted; }))
                    {
                        ctx.SaveChanges();
                    }

                    //creo las relaciones nuevas del partido con los equipos
                    PartidoEquipo newPartido;
                    foreach (var idEquipo in equipos)
                    {
                        newPartido = new PartidoEquipo();
                        newPartido.IdEquipo = idEquipo;
                        newPartido.IdPartido = idPartido;
                        ctx.PartidoEquipo.AddObject(newPartido);
                    }

                    var toUpdatePartido = (from p
                                                in ctx.Partido
                                           where p.Id == idPartido
                                           select p).ToList().FirstOrDefault();
                    if (toUpdatePartido != null)
                    {
                        toUpdatePartido.IdEquipoGanador = idEquipoGanador;
                    }
                    ctx.SaveChanges();
                }

                //actualizo los puntajes de los usuarios cuyas predicciones coincidan con el resultado del partido
                UpdateUsuariosPuntajesByPartido(idPartido);
            }
        }

        public Partido Insert(DateTime fecha
            , string geo
            , int ponderado
            , List<int> equipos
            , int? idequipoganador)
        {
            var rtnPartido = new Partido();
            using (var ctx = new PredictionSQLEntities())
            {
                var newPartido = new Partido();
                newPartido.Fecha = fecha;
                newPartido.Geolocalizacion = geo;
                newPartido.Ponderado = ponderado;
                newPartido.IdEquipoGanador = idequipoganador;
                ctx.Partido.AddObject(newPartido);
                PartidoEquipo newPartidoEquipo;
                foreach (var item in equipos)
                {
                    newPartidoEquipo = new PartidoEquipo();
                    newPartidoEquipo.IdEquipo = item;
                    newPartidoEquipo.IdPartido = newPartido.Id;

                    ctx.PartidoEquipo.AddObject(newPartidoEquipo);
                }
                ctx.SaveChanges();

                rtnPartido = (from p in ctx.Partido.Include("PartidoEquipo").Include("PartidoEquipo.Equipo") where p.Id == newPartido.Id select p).ToList().FirstOrDefault();
            }
            return rtnPartido;
        }

        public List<Partido> GetAllAndUserPrediction(int idUsuario)
        {
            //obtengo todos los partidos y a su vez las predicciones hechas por el usuario
            var par = new List<Partido>();
            using (var ctx = new PredictionSQLEntities())
            {
                var pbes = (from s in ctx.Partido.Include("PartidoEquipo").Include("PartidoEquipo.Equipo")//.Include("UsuarioPrediccion").Include("UsuarioPrediccion.Equipo")
                            select s);
                par = pbes.ToList();
                par.ForEach((item) =>
                                {
                                    var usupred = (from up in ctx.UsuarioPrediccion.Include("Equipo") where up.IdUsuario == idUsuario && up.IdPartido == item.Id select up).ToList().FirstOrDefault();
                                    if (usupred != null)
                                    {
                                        item.UsuarioPrediccion.Load(MergeOption.OverwriteChanges);
                                        item.UsuarioPrediccion.Add(new UsuarioPrediccion() { Equipo = usupred.Equipo, IdEquipoGanador = usupred.IdEquipoGanador, IdUsuario = usupred.IdUsuario, IdPartido = usupred.IdPartido, Predecido = usupred.Predecido });
                                    }
                                });
            }

            return par;
        }

        public List<Partido> GetAllByUserPrediction(int idUsuario, bool onlyuser)
        {
            //obtengo todos los partidos que coincidan con las predicciones hechas por el usuario
            var par = new List<Partido>();
            using (var ctx = new PredictionSQLEntities())
            {
                var pbes = (from s in ctx.Partido.Include("PartidoEquipo")
                                                    .Include("PartidoEquipo.Equipo")
                                                    .Include("UsuarioPrediccion")
                                                    .Include("UsuarioPrediccion.Equipo")
                            where s.Id == 1 && s.UsuarioPrediccion.Any(item =>

                                                                               item.IdUsuario ==
                                                                                      idUsuario
                                                                           )
                            select s);
                par = pbes.ToList();
            }

            return par;
        }

        private void UpdateUsuariosPuntajesByPartido(int idPartido)
        {
            using (var ctx = new PredictionSQLEntities())
            {
                //selecciono todos los usuarios que tengan el mismo idequipoganador 
                //que el partido que se esta actualizando
                //y les sumo puntaje
                var usuarios = (
                                from usu in ctx.UsuarioPrediccion.Include("Usuario")
                                join par in ctx.Partido on usu.IdEquipoGanador equals par.IdEquipoGanador
                                where par.Id == idPartido
                                select usu.Usuario
                                ).ToList();
                foreach (var usuario in usuarios)
                {
                    usuario.Puntaje += 3;
                }

                ctx.SaveChanges();
            }
        }
    }
}
