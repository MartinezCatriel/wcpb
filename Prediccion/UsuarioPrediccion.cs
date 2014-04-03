using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prediccion
{
    public class UsuarioPrediccion
    {
        public int IdPartido { get; set; }
        public int IdUsuario { get; set; }

        public int Predecido { get; set; }
        /// <summary>
        /// key=equipo
        /// value=goles
        /// </summary>

        public int? IdEquipoGanador { get; set; }

        public Equipo Ganador { get; private set; }

        private UsuarioPrediccion(int idPartido, int idUsuario, int? idEquipoGanador, int predecido, Equipo equi)
        {
            IdUsuario = idUsuario;
            IdPartido = idPartido;
            IdEquipoGanador = idEquipoGanador;
            Ganador = equi;
            Predecido = predecido;
        }

        public static UsuarioPrediccion Create(int idPartido, int idUsuario, int? idEquipoGanador, int predecido, Equipo equi)
        {
            return new UsuarioPrediccion(idPartido, idUsuario, idEquipoGanador, predecido, equi);
        }
    }
}
