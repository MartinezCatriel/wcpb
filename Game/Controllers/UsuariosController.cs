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
using Common;

namespace Game.Controllers
{

    public class UsuariosController : ApiController
    {
        public HttpResponseMessage Get(int id)
        {
            var response = new HttpResponseMessage(HttpStatusCode.OK);
            try
            {

                var repoUsu = new UsuarioRepository();
                var usuMapper = new UsuarioMap();
                var usuRTN = usuMapper.MapperUsuario(repoUsu.GetById(id));
                response.Content = new ObjectContent(typeof(Usuario), usuRTN, new JsonMediaTypeFormatter());
            }
            catch (Exception ex)
            {
                response.Content = new ObjectContent(typeof(string), "Error al obtener el usuario. Error:" + ex.Message + "Stack:" + ex.StackTrace, new JsonMediaTypeFormatter());
                return response;
            }
            return response;
        }

        public HttpResponseMessage Get()
        {
            var response = new HttpResponseMessage(HttpStatusCode.OK);
            try
            {
                var repoUsu = new UsuarioRepository();
                var usuMapper = new UsuarioMap();
                var listaDeUsuarios = usuMapper.MapperUsuarios(repoUsu.GetAll());
                response.Content = new ObjectContent(typeof(List<Usuario>), listaDeUsuarios, new JsonMediaTypeFormatter());
            }
            catch (Exception ex)
            {
                response.Content = new ObjectContent(typeof(string), "Error al obtener los usuarios. Error:" + ex.Message + "Stack:" + ex.StackTrace, new JsonMediaTypeFormatter());
                return response;
            }
            return response;
        }

        public HttpResponseMessage Post(NewUsuario newU)
        {
            var response = new HttpResponseMessage(HttpStatusCode.OK);

            try
            {
                var repoUsu = new UsuarioRepository();
                var usuMapper = new UsuarioMap();
                var fbk = new FacebookInformation();
                fbk.GetByTokenAuth(newU.token);
                var email = fbk.Email;
                var id = fbk.Id;

                var usufromrepo = repoUsu.Insert(email, "FBK", newU.token, id);
                var usuRTN = usuMapper.MapperUsuario(usufromrepo);
                response.Content = new ObjectContent(typeof(Usuario), usuRTN, new JsonMediaTypeFormatter());
            }
            catch (FacebookException ex)
            {
                response.Content = new ObjectContent(typeof(string), "Facebook error login. Error:" + ex.Message + "Stack:" + ex.StackTrace, new JsonMediaTypeFormatter());
                return response;
            }
            catch (Exception ex)
            {
                response.Content = new ObjectContent(typeof(string), "Error al obtener el usuario. Error:" + ex.Message + "Stack:" + ex.StackTrace, new JsonMediaTypeFormatter());
                return response;
            }

            return response;
        }


        /*public HttpResponseMessage Post(int id, string token)
        {
            var response = new HttpResponseMessage(HttpStatusCode.OK);

            try
            {
                var repoUsu = new UsuarioRepository();
                var usuMapper = new UsuarioMap();
                var usufromrepo = repoUsu.UploadTokenById(id, token);
                response.Content = new ObjectContent(typeof(string), "Usuario actualizado correctamente", new JsonMediaTypeFormatter());
            }
            catch (Exception ex)
            {

                response.Content = new ObjectContent(typeof(string), "Error al obtener los usuarios. Error:" + ex.Message + "Stack:" + ex.StackTrace, new JsonMediaTypeFormatter());
                return response;
            }
            return response;
        }
        */

    }

    public class NewUsuario
    {
        public string token { get; set; }
    }
}
