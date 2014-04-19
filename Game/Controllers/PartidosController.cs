using Prediccion;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web.Http;
using Repository.EntitiesRepository;
using PartidoPrediccion = Prediccion.Partido;
using Game.RepositoryMap;
namespace Game.Controllers
{
    public class PartidosController : ApiController
    {

        public HttpResponseMessage Get()
        {
            var response = new HttpResponseMessage(HttpStatusCode.OK);
            try
            {
                var repoPartidos = new PartidoRepository();
                var partidoMapper = new PartidoMap();
                //obtengo todos los partidos de la base de datos
                var listaDePartidos = partidoMapper.MapPartidos(repoPartidos.GetAll());
                var partidosPorFecha = new List<PartidosPorFecha>();
                //los agrupo por fecha a los objetos de modelo
                var listas = listaDePartidos.GroupBy((item) => { return item.Fecha.ToShortDateString(); });

                foreach (var lista in listas)
                {
                    //por cada grupo de partidos por fecha creo un objeto de retorno
                    var partidos = lista.ToList().ConvertAll((item) => { return new Controllers.Partido() { Equipos = item.Equipos, Ganador = item.Ganador, Ponderado = item.Ponderado, Geolocalizacion = item.Geolocalizacion, Hora = item.Fecha.ToString("MM:ss tt"), Id = item.Id }; });
                    //creo un objeto que contenga la fecha y sus partidos
                    var ppf = new PartidosPorFecha() { Fecha = lista.Key, Partidos = partidos };
                    partidosPorFecha.Add(ppf);
                }

                response.Content = new ObjectContent(typeof(List<PartidosPorFecha>), partidosPorFecha, new JsonMediaTypeFormatter());
            }
            catch (Exception ex)
            {
                response.Content = new ObjectContent(typeof(string), ex.Message, new JsonMediaTypeFormatter());
            }
            return response;
        }

        public HttpResponseMessage Post(PartidoEquiposUpload partidoToUpload)
        {
            //actualiza los goles por equipo del partido
            var response = new HttpResponseMessage(HttpStatusCode.OK);
            try
            {
                if (partidoToUpload == null)
                {
                    throw new Exception("Debe enviar informacion válida");
                }
                var list = partidoToUpload.Equipos.ConvertAll<int>((item) => { return item.IdEquipo; });

                var repoPartidos = new PartidoRepository();
                repoPartidos.UpdateEquiposAndGolesFromPartido(partidoToUpload.IdPartido
                                                                ,partidoToUpload.IdEquipoGanador
                                                                ,list);

                response.Content = new ObjectContent(typeof(string), "Partido actualizado con exito", new JsonMediaTypeFormatter());
            }
            catch (Exception ex)
            {
                response.Content = new ObjectContent(typeof(string), ex.Message, new JsonMediaTypeFormatter());
            }
            return response;
        }

        /// <summary>
        /// Partidos y predicciones del usuario, devuelve el partido aunque el usuario no lo haya predicho
        /// </summary>
        /// <param name="idUsuario"></param>
        /// <returns></returns>
        public HttpResponseMessage Get(int idUsuario)
        {
            var response = new HttpResponseMessage(HttpStatusCode.OK);
            try
            {
                var repoPartidos = new PartidoRepository();
                var partidoMapper = new PartidoMap();
                //obtengo todos los partidos de la base de datos
                var listaDePartidos = partidoMapper.MapPartidos(repoPartidos.GetAllAndUserPrediction(idUsuario));
                var partidosPorFecha = new List<PartidosPorFecha>();
                //los agrupo por fecha a los objetos de modelo
                var listas = listaDePartidos.GroupBy((item) => { return item.Fecha.ToShortDateString(); });

                foreach (var lista in listas)
                {
                    //por cada grupo de partidos por fecha creo un objeto de retorno
                    var partidos = lista.ToList().ConvertAll((item) =>
                    {
                        var predicciones = new List<PrediccionUsuario>();
                        if (item.EquipoPrediccion != null)
                        {
                            predicciones.Add(new PrediccionUsuario() { EquipoGanador = item.EquipoPrediccion.Ganador, IdUsuario = item.EquipoPrediccion.IdUsuario });
                        }
                        return new Controllers.Partido()
                        {
                            Equipos = item.Equipos,
                            Ganador = item.Ganador,
                            Ponderado = item.Ponderado,
                            Geolocalizacion = item.Geolocalizacion,
                            Hora = item.Fecha.ToString("MM:ss tt"),
                            Id = item.Id,
                            Prediccion = predicciones
                        };
                    });
                    //creo un objeto que contenga la fecha y sus partidos
                    var ppf = new PartidosPorFecha() { Fecha = lista.Key, Partidos = partidos };
                    partidosPorFecha.Add(ppf);
                }

                response.Content = new ObjectContent(typeof(List<PartidosPorFecha>), partidosPorFecha, new JsonMediaTypeFormatter());
            }
            catch (Exception ex)
            {
                response.Content = new ObjectContent(typeof(string), ex.Message, new JsonMediaTypeFormatter());
            }
            return response;
        }

        /// <summary>
        /// Devuelve solamente los partidos que haya predicho
        /// </summary>
        /// <param name="idUsuario"></param>
        /// <param name="onlyuser"></param>
        /// <returns></returns>
        public HttpResponseMessage Get(int idUsuario, bool onlyuser)
        {
            var response = new HttpResponseMessage(HttpStatusCode.OK);
            try
            {
                var repoPartidos = new PartidoRepository();
                var partidoMapper = new PartidoMap();
                //obtengo todos los partidos de la base de datos
                var listaDePartidos = partidoMapper.MapPartidos(repoPartidos.GetAllByUserPrediction(idUsuario, onlyuser));
                var partidosPorFecha = new List<PartidosPorFecha>();
                //los agrupo por fecha a los objetos de modelo
                var listas = listaDePartidos.GroupBy((item) => { return item.Fecha.ToShortDateString(); });

                foreach (var lista in listas)
                {
                    //por cada grupo de partidos por fecha creo un objeto de retorno
                    var partidos = lista.ToList().ConvertAll((item) =>
                                                                 {
                                                                     var predicciones = new List<PrediccionUsuario>();
                                                                     if (item.EquipoPrediccion != null)
                                                                     {
                                                                         predicciones.Add(new PrediccionUsuario() { EquipoGanador = item.EquipoPrediccion.Ganador, IdUsuario = item.EquipoPrediccion.IdUsuario });
                                                                     }
                                                                     return new Controllers.Partido() { Equipos = item.Equipos, Ganador = item.Ganador, Ponderado = item.Ponderado, Geolocalizacion = item.Geolocalizacion, Hora = item.Fecha.ToString("MM:ss tt"), Id = item.Id, Prediccion = predicciones };
                                                                 });
                    //creo un objeto que contenga la fecha y sus partidos
                    var ppf = new PartidosPorFecha() { Fecha = lista.Key, Partidos = partidos };
                    partidosPorFecha.Add(ppf);
                }

                response.Content = new ObjectContent(typeof(List<PartidosPorFecha>), partidosPorFecha, new JsonMediaTypeFormatter());
            }
            catch (Exception ex)
            {
                response.Content = new ObjectContent(typeof(string), ex.Message, new JsonMediaTypeFormatter());
            }
            return response;
        }
    }

    public class PartidoEquiposUpload
    {
        public int IdPartido { get; set; }
        public int? IdEquipoGanador { get; set; }
        public List<EquipoToUpload> Equipos { get; set; }
    }

    public class EquipoToUpload
    {
        public int IdEquipo { get; set; }
    }

    public class PartidosPorFecha
    {
        public string Fecha { get; set; }
        public List<Game.Controllers.Partido> Partidos { get; set; }
    }

    public class Partido
    {
        public string Hora { get; set; }
        public int Id { get; set; }
        public Equipo Ganador { get; set; }
        public string Geolocalizacion { get; set; }
        public int Ponderado { get; set; }
        public List<Equipo> Equipos { get; set; }
        public List<PrediccionUsuario> Prediccion { get; set; }
    }

    public class PrediccionUsuario
    {
        public int IdUsuario { get; set; }
        public Equipo EquipoGanador { get; set; }
    }
}
