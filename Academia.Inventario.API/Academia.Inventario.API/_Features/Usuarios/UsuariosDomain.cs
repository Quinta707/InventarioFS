using Academia.Inventario.API.Infrastructure.InventarioDB.Entities;

namespace Academia.Inventario.API._Features.Usuarios
{
    public class UsuariosDomain
    {
        public bool UsuariosExisteId(List<Usuario> usuariosList, int? id)
        {
            if (usuariosList.Where(x => x.UsuarioId == id && x.Activo == true).Any())
                return true;
            return false;
        }
    }
}
