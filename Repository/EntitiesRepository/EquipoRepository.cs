using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.EntitiesRepository
{
    public class EquipoRepository
    {
        public List<Equipo> GetAll()
        {
            var equipos = new List<Equipo>();
            using (var ctx = new PredictionSQLEntities())
            {
                var response = (from e in ctx.Equipo select e);
                equipos.AddRange(response.ToList());
            }
            return equipos;
        }

        public Equipo GetById(int idEquipo)
        {
            var equipo = new Equipo();
            using (var ctx = new PredictionSQLEntities())
            {
                var response = (from e in ctx.Equipo where e.Id == idEquipo select e);
                equipo = response.ToList().FirstOrDefault();
            }
            return equipo;
        }

        public Equipo Update(int idequipo, string nombre, string nombreExtendido)
        {
            Equipo equipo = null;
            using (var ctx = new PredictionSQLEntities())
            {
                var response = (from e in ctx.Equipo where e.Id == idequipo select e);
                equipo = response.ToList().FirstOrDefault();
                if (equipo != null)
                {
                    equipo.Nombre = nombre;
                    equipo.NombreExtendido = nombreExtendido;
                    ctx.SaveChanges();
                }
                else
                {
                    var e = new Equipo();
                    e.Nombre = nombre;
                    e.NombreExtendido = nombreExtendido;
                    ctx.Equipo.AddObject(e);
                    ctx.SaveChanges();
                    equipo = e;
                }
            }
            return equipo;
        }


    }
}
