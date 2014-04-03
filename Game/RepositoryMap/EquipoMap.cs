using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EquipoMod = Prediccion.Equipo;
using EquipoSql = Repository.Equipo;

namespace Game.RepositoryMap
{
    public class EquipoMap
    {
        public EquipoMod MapEquipo(EquipoSql toMap)
        {
            return EquipoMod.Create(toMap.Id, toMap.Nombre, toMap.NombreExtendido);
        }

        public List<EquipoMod> MapEquipos(List<EquipoSql> toMap)
        {
            return toMap.ConvertAll<EquipoMod>((item) => { return MapEquipo(item); });
        }
    }
}