using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Prediccion;
using UsuarioPrediccionMod = Prediccion.UsuarioPrediccion;
using UsuarioPrediccionSql = Repository.UsuarioPrediccion;
namespace Game.RepositoryMap
{
    public class UsuarioPrediccionMap
    {
        public UsuarioPrediccionMod MapUsuarioPrediccion(UsuarioPrediccionSql toMap)
        {
            Equipo equi = null;
            if (toMap.EquipoReference.IsLoaded)
            {
                equi = (new EquipoMap()).MapEquipo(toMap.Equipo);
            }
            return UsuarioPrediccionMod.Create(toMap.IdPartido, toMap.IdUsuario, toMap.IdEquipoGanador, toMap.Predecido, equi);

        }

        public List<UsuarioPrediccionMod> MapUsuarioPredicciones(List<UsuarioPrediccionSql> toMap)
        {
            return toMap.ConvertAll<UsuarioPrediccionMod>((item) =>
                                                              {
                                                                  return MapUsuarioPrediccion(item);
                                                              });
        }
    }
}