using Academia.Inventario.API._Common;
using Academia.Inventario.API._Features.EstadosCiviles.Dtos;
using Academia.Inventario.API._Features.Lotes.Dtos;
using Academia.Inventario.API._Features.Productos.Dtos;
using Academia.Inventario.API._Features.Roles.Dtos;
using Academia.Inventario.API.Infrastructure;
using Academia.Inventario.API.Infrastructure.InventarioDB.Entities;
using Academia.Inventario.API.Infrastructure.InventarioDB.Maps;
using AutoMapper;
using Farsiman.Application.Core.Standard.DTOs;
using Farsiman.Domain.Core.Standard.Repositories;
using FluentValidation;
using FluentValidation.Results;
using static Academia.Inventario.API.Infrastructure.InventarioDB.Entities.EstadosCivile;
using static Academia.Inventario.API.Infrastructure.InventarioDB.Entities.Producto;

namespace Academia.Inventario.API._Features.Productos
{
    public class ProductosService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public ProductosService(IMapper mapper, UnitOfWorkBuilder unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork.BuilderProyectoInventario();
        }

        public Respuesta<List<ProductosDto>> ListarProductos()
        {
            var productosList = (from productos in _unitOfWork.Repository<Producto>().AsQueryable()
                                 where productos.Activo == true && productos.Activo == true
                                 select new ProductosDto
                                 {
                                     ProductoId = productos.ProductoId,
                                     Nombre = productos.Nombre,

                                 }).ToList();

            return Respuesta.Success(productosList, Mensajes.Proceso_Exitoso, Codigos.Success);
        }

        public Respuesta<ProductosDto> Insertar(ProductosDto productosDto)
        {
            var productosMap = _mapper.Map<Producto>(productosDto);

            productosMap.UsuarioCreacionId = 1;
            productosMap.FechaCreacion = DateTime.Now;
            productosMap.UsuarioModificacionId = null;
            productosMap.FechaModificacion = null;

            _unitOfWork.Repository<Producto>().Add(productosMap);
            _unitOfWork.SaveChanges();


            return Respuesta.Success(productosDto, Mensajes.Proceso_Exitoso, Codigos.Success);
        }

        public Respuesta<ProductosDto> Editar(ProductosDto productosDto)
        {
            Producto? productosMap = _unitOfWork.Repository<Producto>().FirstOrDefault(x => x.ProductoId == productosDto.ProductoId);

            if (productosMap == null)
                return Respuesta.Fault<ProductosDto>(Mensajes.Registro_No_Existe("ProductoId"), Codigos.BadRequest);

            productosMap.ProductoId = productosDto.ProductoId;
            productosMap.Nombre = productosDto.Nombre;
            productosMap.UsuarioModificacionId = 1;
            productosMap.FechaModificacion = DateTime.Now;

            _unitOfWork.SaveChanges();
            productosMap.ProductoId = productosDto.ProductoId;

            return Respuesta.Success(productosDto, Mensajes.Proceso_Exitoso, Codigos.Success);
        }
    }
}
