using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prediccion
{
    public class Pulpo : Animal
    {
        private Pulpo(int id, string nombre, TipoAnimal tipo)
        {
            Id = id;
            Nombre = nombre;
            Tipo = tipo;
        }

        public static Pulpo Create(int id, string nombre, TipoAnimal tipo)
        {
            return new Pulpo(id, nombre, tipo);
        }

        public int Id
        {
            get;
            set;
        }

        public string Nombre
        {
            get;
            set;
        }

        public TipoAnimal Tipo
        {
            get;
            set;
        }

        public void Prediccion()
        {

        }
    }
}
