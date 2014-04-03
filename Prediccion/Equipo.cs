using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace Prediccion
{
    public class Equipo
    {

        private Equipo(int id, string nombre, string nombreExtendido)
        {
            if (!Validations.Validar(Validations.ValidationTypes.GreatherThanZero, id))
            {
                throw new Exception(Mensajes.IdIncorrecto);
            }

            if (!Validations.Validar(Validations.ValidationTypes.IsNotNullOrEmpyAndWhiteSpaces, nombre))
            {
                throw new Exception(Mensajes.NombreIncorrecto);
            }
            Id = id;
            Nombre = nombre;
            NombreExtendido = nombreExtendido;
        }

        public string Nombre
        {
            get;
            set;
        }

        public int Id
        {
            get;
            set;
        }
        public string NombreExtendido
        {
            get;
            set;
        }
        public static Equipo Create(int id, string nombre, string nombreExtendido)
        {
            return new Equipo(id, nombre, nombreExtendido);
        }
    }
}
