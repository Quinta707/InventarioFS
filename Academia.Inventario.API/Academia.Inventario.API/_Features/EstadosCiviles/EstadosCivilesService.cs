using Academia.Inventario.API._Common;
using Academia.Inventario.API._Features.EstadosCiviles.Dtos;
using Academia.Inventario.API.Infrastructure;
using Academia.Inventario.API.Infrastructure.InventarioDB.Entities;
using AutoMapper;
using Farsiman.Application.Core.Standard.DTOs;
using Farsiman.Domain.Core.Standard.Repositories;
namespace Academia.Inventario.API._Features.EstadosCiviles
{
    public class EstadosCivilesService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public EstadosCivilesService(IMapper mapper, UnitOfWorkBuilder unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork.BuilderProyectoInventario();
        }
        public Respuesta<List<EstadosCivilesDto>> ListarEstadosCiviles()
        {
            var estadoscivlesList = (from  estados in _unitOfWork.Repository<EstadosCivile>().AsQueryable()
                                     where estados.Activo == true
                                     select new EstadosCivilesDto
                                     {
                                         EstadoCivilId  = estados.EstadoCivilId,
                                         Descripcion    = estados.Descripcion,

                                     }).ToList();

            return Respuesta.Success(estadoscivlesList, Mensajes.Proceso_Exitoso, Codigos.Success);
        }

        public Respuesta<EstadosCivilesDto> InsertarEstadosCiviles(EstadosCivilesDto estadosCivilesDto)
        {

            var estadosmapeados = _mapper.Map<EstadosCivile>(estadosCivilesDto);

            estadosmapeados.UsuarioCreacionId       = 1;
            estadosmapeados.FechaCreacion           = DateTime.Now;
            estadosmapeados.UsuarioModificacionId   = null;
            estadosmapeados.FechaModificacion       = null;

            _unitOfWork.Repository<EstadosCivile>().Add(estadosmapeados);
            _unitOfWork.SaveChanges();


            return Respuesta.Success(estadosCivilesDto, Mensajes.Proceso_Exitoso, Codigos.Success);
        }


        public Respuesta<EstadosCivilesDto> EditarEstadoCivil(EstadosCivilesDto estadosCivilesDto)
        {
            EstadosCivile? estadoMapeado = _unitOfWork.Repository<EstadosCivile>().FirstOrDefault(x => x.EstadoCivilId == estadosCivilesDto.EstadoCivilId);

            if (estadoMapeado == null)
                return Respuesta.Fault<EstadosCivilesDto>(Mensajes.Registro_No_Existe("EstadoCivilId"), Codigos.BadRequest);

            estadoMapeado.EstadoCivilId         = estadosCivilesDto.EstadoCivilId;
            estadoMapeado.Descripcion           = estadosCivilesDto.Descripcion;
            estadoMapeado.UsuarioModificacionId = 1;
            estadoMapeado.FechaModificacion     = DateTime.Now;

            _unitOfWork.SaveChanges();
            estadosCivilesDto.EstadoCivilId = estadoMapeado.EstadoCivilId;

            return Respuesta.Success(estadosCivilesDto, Mensajes.Proceso_Exitoso, Codigos.Success);
        }

        public string DesactivarEstadoCivil(EstadosCivilesDto estadosCivilesDto)
        {

            EstadosCivile? estadoMapeado = _unitOfWork.Repository<EstadosCivile>().FirstOrDefault(x => x.EstadoCivilId == estadosCivilesDto.EstadoCivilId);

            estadoMapeado.Activo                = false;
            estadoMapeado.UsuarioModificacionId = 1;
            estadoMapeado.FechaModificacion     = DateTime.Now;

            _unitOfWork.SaveChanges();

            estadoMapeado.EstadoCivilId = estadosCivilesDto.EstadoCivilId;

            return Mensajes.Proceso_Exitoso;
        }

        public string ActivarEstadoCivil(EstadosCivilesDto estadosCivilesDto)
        {
            EstadosCivile? estadoMapeado = _unitOfWork.Repository<EstadosCivile>().FirstOrDefault(x => x.EstadoCivilId == estadosCivilesDto.EstadoCivilId);

            estadoMapeado.Activo                = true;
            estadoMapeado.UsuarioModificacionId = 1;
            estadoMapeado.FechaModificacion     = DateTime.Now;

            _unitOfWork.SaveChanges();

            estadoMapeado.EstadoCivilId = estadosCivilesDto.EstadoCivilId;

            return Mensajes.Proceso_Exitoso;
        }

    }
}
