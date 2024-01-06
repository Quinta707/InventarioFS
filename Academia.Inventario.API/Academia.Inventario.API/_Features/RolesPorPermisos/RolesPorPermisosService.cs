using Academia.Inventario.API._Common;
using Academia.Inventario.API._Features.Empleados.Dtos;
using Academia.Inventario.API._Features.RolesPorPermisos.Dtos;
using Academia.Inventario.API.Infrastructure;
using Academia.Inventario.API.Infrastructure.InventarioDB.Entities;
using AutoMapper;
using Farsiman.Application.Core.Standard.DTOs;
using Farsiman.Domain.Core.Standard.Repositories;
using System.Globalization;

namespace Academia.Inventario.API._Features.RolesPorPermisos
{
    public class RolesPorPermisosService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly RolesPorPermisoDomain _rolesPorPermisoDomain;

        public RolesPorPermisosService(IMapper mapper, UnitOfWorkBuilder unitOfWork, RolesPorPermisoDomain rolesPorPermisoDomain)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork.BuilderProyectoInventario();
            _rolesPorPermisoDomain = rolesPorPermisoDomain;
        }

        public Respuesta<List<RolesPorPermisosListDto>> ListarRolesPorPermiso()
        {
            var rolporpermisoList = (from rolporpermiso in _unitOfWork.Repository<RolesPorPermiso>().AsQueryable()
                                     join rol in _unitOfWork.Repository<Role>().AsQueryable()
                                     on rolporpermiso.RolId equals rol.RolId
                                     join permiso in _unitOfWork.Repository<Permiso>().AsQueryable()
                                     on rolporpermiso.PermisoId equals permiso.PermisoId
                                     where rolporpermiso.Activo == true
                                     select new RolesPorPermisosListDto
                                     {
                                         RolPorPermisoId = rolporpermiso.RolPorPermisoId,
                                         PermisoId = rolporpermiso.PermisoId,
                                         PermisoDescripcion = permiso.Nombre,
                                         RolId = rolporpermiso.RolId,
                                         RolDescripcion = rol.Nombre,

                                     }).ToList();

            return Respuesta.Success(rolporpermisoList, Mensajes.Proceso_Exitoso, Codigos.Success);
        }

        public Respuesta<RolesPorPermisosDto> Insertar(RolesPorPermisosDto rolesporpermisoDto)
        {
            var rolesporpermisoMap = _mapper.Map<RolesPorPermiso>(rolesporpermisoDto);

            var rolList = _unitOfWork.Repository<Role>().AsQueryable().AsQueryable().Where(x => x.Activo == true).ToList();
            var permisoList = _unitOfWork.Repository<Permiso>().AsQueryable().AsQueryable().Where(x => x.Activo == true).ToList();
            string Validacion = _rolesPorPermisoDomain.AgregarRolesPorPermiso(rolList, rolesporpermisoDto.RolId, permisoList, rolesporpermisoDto.PermisoId);
            
            if(Validacion != "200")
                return Respuesta.Fault<RolesPorPermisosDto>(Validacion.ToString(), Codigos.Error);

            rolesporpermisoMap.UsuarioCreacionId = 1;
            rolesporpermisoMap.FechaCreacion = DateTime.Now;
            rolesporpermisoMap.UsuarioModificacionId = null;
            rolesporpermisoMap.FechaModificacion = null;

            _unitOfWork.Repository<RolesPorPermiso>().Add(rolesporpermisoMap);
            _unitOfWork.SaveChanges();


            return Respuesta.Success(rolesporpermisoDto, Mensajes.Proceso_Exitoso, Codigos.Success);
        }

        public Respuesta<RolesPorPermisosDto> Editar(RolesPorPermisosDto rolesporpermisoDto)
        {
            RolesPorPermiso? rolesporpermisoMap = _unitOfWork.Repository<RolesPorPermiso>().FirstOrDefault(x => x.RolPorPermisoId == rolesporpermisoDto.RolPorPermisoId);

            if (rolesporpermisoMap == null)
                return Respuesta.Fault<RolesPorPermisosDto>(Mensajes.Registro_No_Existe("RolPorPermisoId"), Codigos.BadRequest);

            rolesporpermisoMap.RolPorPermisoId = rolesporpermisoDto.RolPorPermisoId;
            rolesporpermisoMap.PermisoId = rolesporpermisoDto.PermisoId;
            rolesporpermisoMap.RolId = rolesporpermisoDto.RolId;
            rolesporpermisoMap.UsuarioModificacionId = 1;
            rolesporpermisoMap.FechaModificacion = DateTime.Now;

            var rolList = _unitOfWork.Repository<Role>().AsQueryable().AsQueryable().Where(x => x.Activo == true).ToList();
            var permisoList = _unitOfWork.Repository<Permiso>().AsQueryable().AsQueryable().Where(x => x.Activo == true).ToList();
            string Validacion = _rolesPorPermisoDomain.AgregarRolesPorPermiso(rolList, rolesporpermisoDto.RolId, permisoList, rolesporpermisoDto.PermisoId);

            if (Validacion != "200")
                return Respuesta.Fault<RolesPorPermisosDto>(Validacion.ToString(), Codigos.Error);

            _unitOfWork.SaveChanges();
            rolesporpermisoMap.RolPorPermisoId = rolesporpermisoDto.RolPorPermisoId;

            return Respuesta.Success(rolesporpermisoDto, Mensajes.Proceso_Exitoso, Codigos.Success);
        }
    }
}
