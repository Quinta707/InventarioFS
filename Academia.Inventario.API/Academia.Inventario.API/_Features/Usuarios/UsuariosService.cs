using Academia.Inventario.API._Common;
using Academia.Inventario.API._Features.Productos.Dtos;
using Academia.Inventario.API._Features.Usuarios.Dtos;
using Academia.Inventario.API.Infrastructure;
using Academia.Inventario.API.Infrastructure.InventarioDB.Entities;
using AutoMapper;
using Farsiman.Application.Core.Standard.DTOs;
using Farsiman.Domain.Core.Standard.Repositories;

namespace Academia.Inventario.API._Features.Usuarios
{
    public class UsuariosService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public UsuariosService(IMapper mapper, UnitOfWorkBuilder unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork.BuilderProyectoInventario();
        }

        public Respuesta<List<UsuariosListDto>> ListarUsuarios()
        {
            var usuariosList = (from usuarios in _unitOfWork.Repository<Usuario>().AsQueryable()
                                join empleados in _unitOfWork.Repository<Empleado>().AsQueryable()
                                on usuarios.EmpleadoId equals empleados.EmpleadoId
                                join roles in _unitOfWork.Repository<Role>().AsQueryable()
                                on usuarios.RolId equals roles.RolId
                                 where usuarios.Activo == true
                                 select new UsuariosListDto
                                 {
                                     UsuarioId = usuarios.UsuarioId,
                                     Nombre = usuarios.Nombre,
                                     EmpleadoId = usuarios.EmpleadoId,
                                     EmpleadoNombre = empleados.Nombre + ' ' + empleados.Apellido,
                                     RolId = usuarios.RolId,
                                     RolNombre = roles.Nombre,
                                     UsuarioCreacionId = usuarios.UsuarioCreacionId,
                                     FechaCreacion = usuarios.FechaCreacion,
                                     UsuarioModificacionId = usuarios.UsuarioModificacionId,
                                     FechaModificacion = usuarios.FechaModificacion,
                                     Activo = usuarios.Activo,

                                 }).ToList();

            return Respuesta.Success(usuariosList, Mensajes.Proceso_Exitoso, Codigos.Success);
        }
    }
}
