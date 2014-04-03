using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class TestSQLRepo
    {
        public TestSQLRepo()
        {
            using (var ctx = new PredictionSQLEntities())
            {
                var repo = new SQLRepository(ctx);
                var equipos = repo.GetAll<Equipo>();
                var listdeequipos = equipos.ToList();
                foreach (var listdeequipo in listdeequipos)
                {
                    int id = listdeequipo.Id;
                    if (id != 0)
                    {

                    }
                }
            }
            /*
            //equipo
            using (var ctx = new PrediccionesSQLContainer())
            {
                var repo = new SQLRepository(ctx);



                //var repoEquipos = new SQLRepositoryByEntityType<Equipo>(ctx);
                var newequipo = new Equipo();
                newequipo.Nombre = "Brasil";
                //repoEquipos.Insert(newequipo);
                repo.Insert(newequipo);
                //var equipoById = repo.SearchFor(equipo => (equipo.Id == 1));
                //equipoById.ToList();
                newequipo = new Equipo();
                newequipo.Nombre = "Croacia";
                repo.Insert(newequipo);
                //repoEquipos.Insert(newequipo);



                ctx.SaveChanges();
            }

            //partido
            using (var ctx = new PrediccionesSQLContainer())
            {
                var repo = new SQLRepository(ctx);
                var newpartido = new Partido();
                newpartido.Fecha = Convert.ToDateTime("2014-06-12 17:00");
                newpartido.Geolocalizacion = "San Pablo";
                newpartido.Ponderado = 60;
                repo.Insert(newpartido);
                var newPartidoEquipo = new PartidoEquipo();
                newPartidoEquipo.IdPartido = 1;
                newPartidoEquipo.IdEquipo = 6;
                newPartidoEquipo.Goles = 0;
                repo.Insert(newPartidoEquipo);
                newPartidoEquipo = new PartidoEquipo();
                newPartidoEquipo.IdPartido = 1;
                newPartidoEquipo.IdEquipo = 7;
                newPartidoEquipo.Goles = 0;
                repo.Insert(newPartidoEquipo);

                ctx.SaveChanges(SaveOptions.DetectChangesBeforeSave);
            }
            */

            using (var ctx = new PredictionSQLEntities())
            {
                var repo = new SQLRepository(ctx);
                var newPartidoEquipo = new PartidoEquipo();
                newPartidoEquipo.IdPartido = 1;
                newPartidoEquipo.IdEquipo = 6;
                repo.Insert(newPartidoEquipo);


                ctx.SaveChanges(SaveOptions.DetectChangesBeforeSave);
            }
            /*
            //partidoequipo
            using (var ctx = new PrediccionesSQLContainer())
            {
                var repo = new SQLRepository<PartidoEquipo>(ctx);
                var newpartidoequipo = new PartidoEquipo();
                newpartidoequipo.IdPartido = 1;
                newpartidoequipo.IdEquipo = 1;
                repo.Insert(newpartidoequipo);
                ctx.SaveChanges(SaveOptions.DetectChangesBeforeSave);
            }

            //usuario
            using (var ctx = new PrediccionesSQLContainer())
            {
                var repo = new SQLRepository<Usuario>(ctx);
                var newequipo = new Usuario();
                newequipo.Nombre = "NewTeam";
                repo.Insert(newequipo);
                ctx.SaveChanges(SaveOptions.DetectChangesBeforeSave);
            }

            //usuarioprediccion
            using (var ctx = new PrediccionesSQLContainer())
            {
                var repo = new SQLRepository<UsuarioPrediccion>(ctx);
                var newequipo = new UsuarioPrediccion();
                newequipo.Nombre = "NewTeam";
                repo.Insert(newequipo);
                ctx.SaveChanges(SaveOptions.DetectChangesBeforeSave);
            }*/
        }
    }
}
