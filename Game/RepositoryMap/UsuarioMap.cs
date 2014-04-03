using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RepoUsu = Repository.Usuario;
using ModelUsu = Prediccion.Usuario;
namespace Game.RepositoryMap
{
    public class UsuarioMap
    {
        public ModelUsu MapperUsuario(RepoUsu toMap)
        {
            return ModelUsu.Create(toMap.Id, toMap.Procedencia, toMap.Email, toMap.Token, toMap.ExternalId);
        }

        public List<ModelUsu> MapperUsuarios(List<RepoUsu> toMap)
        {
            return toMap.ConvertAll<ModelUsu>((item) => { return MapperUsuario(item); });
        }
    }
}