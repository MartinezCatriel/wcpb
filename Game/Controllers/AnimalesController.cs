using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web.Http;
using Common;
using Prediccion;

namespace Game.Controllers
{
    public class AnimalesController : ApiController
    {

        public HttpResponseMessage Get()
        {
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
            var animales = new List<Animal>();
            animales.Add(AnimalCreator.GetAnimalInstanceByType(TipoAnimal.Leon, 1, "leon choton"));
            animales.Add(AnimalCreator.GetAnimalInstanceByType(TipoAnimal.Pulpo, 2, "pulpo paul"));
            response.Content = new ObjectContent(typeof(List<Animal>), animales, new JsonMediaTypeFormatter()); // new StringContent("{equipoid:1, equiponombre:argentina}");
            return response;
        }

        public HttpResponseMessage Get(string id)
        {
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
            string error = string.Empty;
            if (!Validations.Validar(Validations.ValidationTypes.IsInt, id))
            {
                error += Mensajes.ElValorDebeSerEntero;
                response.Content = new StringContent(error);
                return response;
            }


            var animales = new List<Animal>();
            animales.Add(AnimalCreator.GetAnimalInstanceByType(TipoAnimal.Leon, 1, "leon choton"));
            animales.Add(AnimalCreator.GetAnimalInstanceByType(TipoAnimal.Pulpo, 2, "pulpo paul"));

            Animal an = (from a in animales where a.Id == Convert.ToInt32(id) select a).FirstOrDefault();
            response.Content = new ObjectContent(typeof(List<Animal>), animales, new JsonMediaTypeFormatter()); // new StringContent("{equipoid:1, equiponombre:argentina}");
            return response;
        }

        public HttpResponseMessage Post(string nombre, string tipo)
        {
            return new HttpResponseMessage(HttpStatusCode.OK);
        }
    }
}
