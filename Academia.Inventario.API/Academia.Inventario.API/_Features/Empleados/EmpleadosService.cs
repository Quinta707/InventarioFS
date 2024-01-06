using Academia.Inventario.API._Common;
using Academia.Inventario.API._Features.Empleados.Dtos;
using Academia.Inventario.API.Infrastructure;
using Academia.Inventario.API.Infrastructure.InventarioDB.Entities;
using AutoMapper;
using Farsiman.Application.Core.Standard.DTOs;
using Farsiman.Domain.Core.Standard.Repositories;
using FluentValidation.Results;
using static Academia.Inventario.API.Infrastructure.InventarioDB.Entities.Empleado;

namespace Academia.Inventario.API._Features.Empleados
{
    public class EmpleadosService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly EmpleadosDomain _empleadosDomain;

        public EmpleadosService(IMapper mapper, UnitOfWorkBuilder unitOfWork, EmpleadosDomain empleadosDomain)
        {
            _mapper             = mapper;
            _unitOfWork         = unitOfWork.BuilderProyectoInventario();
            _empleadosDomain    = empleadosDomain;
        }

        public Respuesta<List<EmpleadosListDto>> ListarEmpleados()
        {
            var list = (from  empleados         in _unitOfWork.Repository<Empleado>().AsQueryable()
                        join  estadosciviles    in _unitOfWork.Repository<EstadosCivile>().AsQueryable()
                        on    empleados.EstadoCivilId equals estadosciviles.EstadoCivilId
                        where empleados.Activo == true
                        select new EmpleadosListDto
                        {
                            EmpleadoId              = empleados.EmpleadoId,
                            Nombre                  = empleados.Nombre,
                            Apellido                = empleados.Apellido,
                            Telefono                = empleados.Telefono,
                            Genero                  = empleados.Genero,
                            Direccion               = empleados.Direccion,
                            Identidad               = empleados.Identidad,
                            EstadoCivilId           = empleados.EstadoCivilId,
                            EstadoCivilDescripcion  = estadosciviles.Descripcion,
                        }).ToList();
            return Respuesta.Success(list, Mensajes.Proceso_Exitoso, Codigos.Success);
        }


        public Respuesta<EmpleadosDto> InsertarEmpleados(EmpleadosDto empleadosDto)
        {
            var empleadosMap = _mapper.Map<Empleado>(empleadosDto);

            EmpleadoValidator validator         = new EmpleadoValidator();
            ValidationResult validationResult   = validator.Validate(empleadosMap);

            if (!validationResult.IsValid)
            {
                IEnumerable<string> errores = validationResult.Errors.Select(s => s.ErrorMessage);
                string menssageValidation   = string.Join(Environment.NewLine, errores);
                return Respuesta.Fault<EmpleadosDto>(menssageValidation, Codigos.BadRequest);
            }
            var empleadosList   = _unitOfWork.Repository<Empleado>().AsQueryable().ToList();
            var estadocivilList = _unitOfWork.Repository<EstadosCivile>().AsQueryable().ToList();

            string Validacion = _empleadosDomain.AgregarEmpleados(empleadosList, estadocivilList, empleadosMap.Identidad, empleadosMap.EstadoCivilId);
            if (Validacion != "200")
                return Respuesta.Fault<EmpleadosDto>(Validacion.ToString());

            empleadosMap.UsuarioCreacionId      = 1;
            empleadosMap.FechaCreacion          = DateTime.Now;
            empleadosMap.UsuarioModificacionId  = null;
            empleadosMap.FechaModificacion      = null;

            _unitOfWork.Repository<Empleado>().Add(empleadosMap);
            _unitOfWork.SaveChanges();

            return Respuesta.Success(empleadosDto, Mensajes.Proceso_Exitoso, Codigos.Success);
        }

        public Respuesta<EmpleadosDto> EditarEmpleados(EmpleadosDto empleadosDto)
        {
            Empleado? empleadoMapeado = _unitOfWork.Repository<Empleado>().FirstOrDefault(x => x.EmpleadoId == empleadosDto.EmpleadoId);

            if (empleadoMapeado == null)
                return Respuesta.Fault<EmpleadosDto>(Mensajes.Registro_No_Existe("EmpleadoId"), Codigos.BadRequest);

            empleadoMapeado.EmpleadoId              = empleadosDto.EmpleadoId;
            empleadoMapeado.Nombre                  = empleadosDto.Nombre;
            empleadoMapeado.Apellido                = empleadosDto.Apellido;
            empleadoMapeado.Telefono                = empleadosDto.Telefono;
            empleadoMapeado.Identidad               = empleadosDto.Identidad;
            empleadoMapeado.Genero                  = empleadosDto.Genero;
            empleadoMapeado.EstadoCivilId           = empleadosDto.EstadoCivilId;
            empleadoMapeado.Direccion               = empleadosDto.Direccion;
            empleadoMapeado.UsuarioModificacionId   = 1;
            empleadoMapeado.FechaModificacion       = DateTime.Now;

            EmpleadoValidator validator         = new EmpleadoValidator();
            ValidationResult validationResult   = validator.Validate(empleadoMapeado);

            if (!validationResult.IsValid)
            {
                IEnumerable<string> errores = validationResult.Errors.Select(s => s.ErrorMessage);
                string menssageValidation   = string.Join(Environment.NewLine, errores);
                return Respuesta.Fault<EmpleadosDto>(menssageValidation, Codigos.BadRequest);
            }

            var empleadosList   = _unitOfWork.Repository<Empleado>().AsQueryable().ToList();
            var estadocivilList = _unitOfWork.Repository<EstadosCivile>().AsQueryable().ToList();

            string Validacion = _empleadosDomain.EditarEmpleados(empleadosList, empleadoMapeado.Identidad, empleadoMapeado.EmpleadoId, estadocivilList, empleadoMapeado.EstadoCivilId);
            if (Validacion != "200")
                return Respuesta.Fault<EmpleadosDto>(Validacion.ToString());

            _unitOfWork.SaveChanges();
            empleadosDto.EmpleadoId = empleadoMapeado.EmpleadoId;

            return Respuesta.Success(empleadosDto, Mensajes.Proceso_Exitoso, Codigos.Success);

        }
    }
}
