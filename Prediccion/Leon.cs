using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prediccion
{
    public class Leon : Animal
    {
        private Leon(int id, string nombre, TipoAnimal tipo)
        {
            Id = id;
            Nombre = nombre;
            Tipo = tipo;
        }

        public static Leon Create(int id, string nombre, TipoAnimal tipo)
        {
            return new Leon(id, nombre, tipo);
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
