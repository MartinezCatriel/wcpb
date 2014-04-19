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
                case TipoAnimal.Dragon:
                    return Dragon.Create();
                case TipoAnimal.Canguro:
                    return Canguro.Create();
                case TipoAnimal.Pulpo:
                    return Pulpo.Create();
                case TipoAnimal.Papagayo:
                    return Papagayo.Create();
                case TipoAnimal.Mantis:
                    return Mantis.Create();
            }
            return null;
        }
    }
}
