using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prediccion
{
    public interface Animal
    {
        int Id { get; set; }
        string Nombre { get; set; }
        TipoAnimal Tipo { get; set; }
        void Prediccion();
    }

    public enum TipoAnimal
    {
        Pulpo = 0,
        Leon = 1
    }
}
