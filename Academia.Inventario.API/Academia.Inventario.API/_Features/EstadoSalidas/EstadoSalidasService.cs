using Academia.Inventario.API._Common;
using Academia.Inventario.API._Features.EstadoSalidas.Dtos;
using Academia.Inventario.API.Infrastructure;
using Academia.Inventario.API.Infrastructure.InventarioDB.Entities;
using AutoMapper;
using Farsiman.Application.Core.Standard.DTOs;
using Farsiman.Domain.Core.Standard.Repositories;

namespace Academia.Inventario.API._Features.EstadoSalidas
{
    public class EstadoSalidasService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public EstadoSalidasService(IMapper mapper, UnitOfWorkBuilder unitOfWork)
        {
            _mapper     = mapper;
            _unitOfWork = unitOfWork.BuilderProyectoInventario();
        }

        public Respuesta<List<EstadoSalidasDto>> ListarEstadoSalidas()
        {
            var estadosalidasList = (from  estados in _unitOfWork.Repository<EstadoSalida>().AsQueryable()
                                     where estados.Activo == true
                                     select new EstadoSalidasDto
                                     {
                                         EstadoId       = estados.EstadoId,
                                         Descripcion    = estados.Descripcion,

                                     }).ToList();

            return Respuesta.Success(estadosalidasList, Mensajes.Proceso_Exitoso, Codigos.Success);
        }

        public Respuesta<EstadoSalidasDto> Insertar(EstadoSalidasDto estadoSalidasDto)
        {
            var estadosmapeados = _mapper.Map<EstadoSalida>(estadoSalidasDto);

            estadosmapeados.UsuarioCreacionId       = 1;
            estadosmapeados.FechaCreacion           = DateTime.Now;
            estadosmapeados.UsuarioModificacionId   = null;
            estadosmapeados.FechaModificacion       = null;

            _unitOfWork.Repository<EstadoSalida>().Add(estadosmapeados);
            _unitOfWork.SaveChanges();


            return Respuesta.Success(estadoSalidasDto, Mensajes.Proceso_Exitoso, Codigos.Success);
        }
        public Respuesta<EstadoSalidasDto> Editar(EstadoSalidasDto estadoSalidasDto)
        {
            EstadoSalida? estadoMapeado = _unitOfWork.Repository<EstadoSalida>().FirstOrDefault(x => x.EstadoId == estadoSalidasDto.EstadoId);

            if (estadoMapeado == null)
                return Respuesta.Fault<EstadoSalidasDto>(Mensajes.Registro_No_Existe("EstadoId"), Codigos.BadRequest);

            estadoMapeado.EstadoId              = estadoSalidasDto.EstadoId;
            estadoMapeado.Descripcion           = estadoSalidasDto.Descripcion;
            estadoMapeado.UsuarioModificacionId = 1;
            estadoMapeado.FechaModificacion     = DateTime.Now;

            _unitOfWork.SaveChanges();
            estadoSalidasDto.EstadoId = estadoMapeado.EstadoId;

            return Respuesta.Success(estadoSalidasDto, Mensajes.Proceso_Exitoso, Codigos.Success);
        }
    }
}
