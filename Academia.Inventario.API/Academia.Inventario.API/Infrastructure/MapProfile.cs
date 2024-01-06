using Academia.Inventario.API._Features.Empleados.Dtos;
using Academia.Inventario.API._Features.EstadoSalidas.Dtos;
using Academia.Inventario.API._Features.EstadosCiviles.Dtos;
using Academia.Inventario.API._Features.Lotes.Dtos;
using Academia.Inventario.API._Features.Permisos.Dtos;
using Academia.Inventario.API._Features.Productos.Dtos;
using Academia.Inventario.API._Features.Roles.Dtos;
using Academia.Inventario.API._Features.RolesPorPermisos.Dtos;
using Academia.Inventario.API._Features.SalidasDeInventario.Dtos;
using Academia.Inventario.API._Features.SalidasInventarioDetalles.Dtos;
using Academia.Inventario.API._Features.Sucursales.Dtos;
using Academia.Inventario.API._Features.Usuarios.Dtos;
using Academia.Inventario.API.Infrastructure.InventarioDB.Entities;
using AutoMapper;

namespace Academia.Inventario.API.Infrastructure
{
    public class MapProfile : Profile
    {
        public MapProfile() {
            CreateMap<Empleado, EmpleadosDto>().ReverseMap();
            CreateMap<EstadoSalida, EstadoSalidasDto>().ReverseMap();
            CreateMap<EstadosCivile, EstadosCivilesDto>().ReverseMap();
            CreateMap<Lote, LotesDto>().ReverseMap();
            CreateMap<Permiso, PermisosDto>().ReverseMap();
            CreateMap<Producto, ProductosDto>().ReverseMap();
            CreateMap<Role, RolesDto>().ReverseMap();
            CreateMap<RolesPorPermiso, RolesPorPermisosDto>().ReverseMap();
            CreateMap<SalidasInventario, SalidasInventarioDto>().ReverseMap();
            CreateMap<SalidasInventarioDetalle, SalidasInventarioDetallesDto>().ReverseMap();
            CreateMap<Sucursale, SucursalesDto>().ReverseMap();
            CreateMap<Usuario, UsuariosDto>().ReverseMap();
        }
    }
}
