using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web.Http;
using Prediccion;
using Repository.EntitiesRepository;
using Game.RepositoryMap;

namespace Game.Controllers
{
    public class EquiposController : ApiController
    {
        public HttpResponseMessage Get(string id)
        {
            var response = new HttpResponseMessage(HttpStatusCode.OK);
            try
            {
                var repoEquipo = new EquipoRepository();
                var equipoMapper = new EquipoMap();
                var equipo = equipoMapper.MapEquipo(repoEquipo.GetById(Convert.ToInt32(id)));
                response.Content = new ObjectContent(typeof(Equipo), equipo, new JsonMediaTypeFormatter());
            }
            catch (Exception ex)
            {
                response.Content = new ObjectContent(typeof(string), "Error al obtener el equipo. Error:" + ex.Message + "Stack:" + ex.StackTrace, new JsonMediaTypeFormatter());
                return response;
            }
            return response;
        }

        public HttpResponseMessage Get()
        {
            var response = new HttpResponseMessage(HttpStatusCode.OK);
            try
            {
                var repoEquipo = new EquipoRepository();
                var equipoMapper = new EquipoMap();
                var equipos = equipoMapper.MapEquipos(repoEquipo.GetAll());
                response.Content = new ObjectContent(typeof(List<Equipo>), equipos, new JsonMediaTypeFormatter());
            }
            catch (Exception ex)
            {
                response.Content = new ObjectContent(typeof(string), "Error al obtener los equipos. Error:" + ex.Message + "Stack:" + ex.StackTrace, new JsonMediaTypeFormatter());
                return response;
            }
            return response;
        }

        public HttpResponseMessage Post(EquipoToSave equipo)
        {
            var response = new HttpResponseMessage(HttpStatusCode.OK);
            try
            {
                var repoEquipo = new EquipoRepository();
                var equipoMapper = new EquipoMap();
                var equipoMapped = equipoMapper.MapEquipo(repoEquipo.Update(equipo.id, equipo.nombre, equipo.nombreExtendido));
                response.Content = new ObjectContent(typeof(Equipo), equipoMapped, new JsonMediaTypeFormatter());
            }
            catch (Exception ex)
            {
                response.Content = new ObjectContent(typeof(string), "Error al obtener los equipos. Error:" + ex.Message + "Stack:" + ex.StackTrace, new JsonMediaTypeFormatter());
                return response;
            }
            return response;
        }
    }

    public class EquipoToSave
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public string nombreExtendido { get; set; }
    }
}
