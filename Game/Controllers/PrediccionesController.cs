using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web.Http;
using Game.RepositoryMap;
using Prediccion;
using Repository.EntitiesRepository;

namespace Game.Controllers
{
    public class PrediccionesController : ApiController
    {
        public HttpResponseMessage Get(int user)
        {
            var response = new HttpResponseMessage(HttpStatusCode.OK);

            try
            {
                var repoPredicciones = new UsuarioPrediccionRepository();
                var usuarioMapper = new UsuarioPrediccionMap();
                var listaDePrediccionesPorUsuario = usuarioMapper.MapUsuarioPredicciones(repoPredicciones.GetByUsuarioId(user));

                response.Content = new ObjectContent(typeof(List<UsuarioPrediccion>), listaDePrediccionesPorUsuario, new JsonMediaTypeFormatter());
            }
            catch (Exception ex)
            {

                response.Content = new ObjectContent(typeof(string), string.Format("Error. Msg:{0} Stack:{1}", ex.Message, ex.StackTrace), new JsonMediaTypeFormatter());
            }
            return response;
        }

        public HttpResponseMessage Get(int user, int match)
        {
            var response = new HttpResponseMessage(HttpStatusCode.OK);

            try
            {
                var repoPredicciones = new UsuarioPrediccionRepository();
                var usuarioMapper = new UsuarioPrediccionMap();
                var prediccionByUserAndMatch = usuarioMapper.MapUsuarioPredicciones(repoPredicciones.GetByUsuarioId(user));

                response.Content = new ObjectContent(typeof(List<UsuarioPrediccion>), prediccionByUserAndMatch, new JsonMediaTypeFormatter());
            }
            catch (Exception ex)
            {

                response.Content = new ObjectContent(typeof(string), string.Format("Error. Msg:{0} Stack:{1}", ex.Message, ex.StackTrace), new JsonMediaTypeFormatter());
            }
            return response;
        }

        public HttpResponseMessage Post(int partido, int usuario, int? idequipoganador = null)
        {
            var response = new HttpResponseMessage(HttpStatusCode.OK);
            try
            {
                var repoPredicciones = new UsuarioPrediccionRepository();

                repoPredicciones.Update(partido, usuario, idequipoganador);

                response.Content = new ObjectContent(typeof(string), "Operacion realizada con exito", new JsonMediaTypeFormatter());
            }
            catch (Exception ex)
            {

                response.Content = new ObjectContent(typeof(string), string.Format("Error. Msg:{0} Stack:{1}", ex.Message, ex.StackTrace), new JsonMediaTypeFormatter());
            }
            return response;
        }
    }
}
