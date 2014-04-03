using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Prediccion;
using PartidoMod = Prediccion.Partido;
using PartidoSql = Repository.Partido;

namespace Game.RepositoryMap
{
    public class PartidoMap
    {
        public PartidoMod MapPartido(PartidoSql toMap)
        {
            var equipos = new List<Prediccion.Equipo>();
            var equipoMap = new EquipoMap();
            if (toMap.PartidoEquipo.IsLoaded)
            {
                foreach (var item in toMap.PartidoEquipo)
                {
                    if (item.EquipoReference.IsLoaded)
                    {
                        equipos.Add(equipoMap.MapEquipo(item.Equipo));
                    }

                }
            }

            UsuarioPrediccion equipoPrediccion = null;
            if (toMap.UsuarioPrediccion.IsLoaded)
            {
                var usuariopredMap = new UsuarioPrediccionMap();
                foreach (var pred in toMap.UsuarioPrediccion)
                {
                    equipoPrediccion = (new UsuarioPrediccionMap()).MapUsuarioPrediccion(pred);
                }

            }

            return PartidoMod.Create(toMap.Id,
                equipos,
                toMap.Fecha,
                toMap.Geolocalizacion,
                toMap.Ponderado,
                toMap.IdEquipoGanador,
                equipoPrediccion);
        }

        public List<PartidoMod> MapPartidos(List<PartidoSql> toMap)
        {
            return toMap.ConvertAll<PartidoMod>((item) => { return MapPartido(item); });
        }
    }
}