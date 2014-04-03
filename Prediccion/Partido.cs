using Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prediccion
{
    public class Partido
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public Equipo Ganador
        {
            get
            {
                if (IdEquipoGanador == null)
                {
                    return null;
                }
                return Equipos.Find((equi) => { return equi.Id == IdEquipoGanador.GetValueOrDefault(); });

            }
        }
        public string Geolocalizacion { get; set; }
        public int Ponderado { get; set; }
        public int? IdEquipoGanador { get; set; }
        public List<Equipo> Equipos { get; set; }

        public UsuarioPrediccion EquipoPrediccion { get; set; }
        private Partido(int id, IEnumerable<Equipo> equipos, DateTime fecha, string geo, int ponderado, int? idEquiporGanador, UsuarioPrediccion equipoPrediccion = null)
        {
            Id = id;
            Equipos = new List<Equipo>();
            Equipos.AddRange(equipos);
            if (!Validations.Validar(Validations.ValidationTypes.EqualTo, Equipos.Count, 2))
            {
                throw new Exception(Mensajes.LaCantidadDeEquiposDebeSer2);
            }
            Fecha = fecha;
            Geolocalizacion = geo;
            IdEquipoGanador = IdEquipoGanador;
            Ponderado = ponderado;
            EquipoPrediccion = equipoPrediccion;
        }

        public static Partido Create(int id, IEnumerable<Equipo> equipos, DateTime fecha, string geo, int ponderado, int? idEquiporGanador, UsuarioPrediccion equipoPrediccion = null)
        {
            return new Partido(id, equipos, fecha, geo, ponderado, idEquiporGanador, equipoPrediccion);
        }
    }
}
