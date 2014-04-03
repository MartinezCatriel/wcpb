using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prediccion
{
    public static class AnimalCreator
    {
        public static Animal GetAnimalInstanceByType(TipoAnimal tipo, int id, string nombre)
        {
            switch (tipo)
            {
                case TipoAnimal.Leon:
                    return Leon.Create(id, nombre, tipo);
                case TipoAnimal.Pulpo:
                    return Pulpo.Create(id, nombre, tipo);
            }
            return null;
        }
    }
}
