using Academia.Inventario.API._Features.Empleados;
using Academia.Inventario.API._Features.EstadoSalidas;
using Academia.Inventario.API._Features.EstadosCiviles;
using Academia.Inventario.API._Features.Lotes;
using Academia.Inventario.API._Features.Permisos;
using Academia.Inventario.API._Features.Productos;
using Academia.Inventario.API._Features.Roles;
using Academia.Inventario.API._Features.RolesPorPermisos;
using Academia.Inventario.API._Features.SalidaInventario;
using Academia.Inventario.API._Features.SalidasDeInventario;
using Academia.Inventario.API._Features.SalidasInventarioDetalles;
using Academia.Inventario.API._Features.Sucursales;
using Academia.Inventario.API._Features.Usuarios;
using Academia.Inventario.API.Infrastructure;
using Academia.Inventario.API.Infrastructure.InventarioDB;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

var connectionString = builder.Configuration.GetConnectionString("ProyectoInventario");
builder.Services.AddDbContext<InventarioSeqpContext>(opciones => opciones.UseSqlServer(connectionString));

builder.Services.AddTransient<UnitOfWorkBuilder, UnitOfWorkBuilder>();

builder.Services.AddAutoMapper(typeof(MapProfile));

//SERVICES
builder.Services.AddTransient<EmpleadosService>();
builder.Services.AddTransient<EstadoSalidasService>();
builder.Services.AddTransient<EstadosCivilesService>();
builder.Services.AddTransient<LotesService>();
builder.Services.AddTransient<PermisosService>();
builder.Services.AddTransient<ProductosService>();
builder.Services.AddTransient<RolesService>();
builder.Services.AddTransient<RolesPorPermisosService>();
builder.Services.AddTransient<SalidasInventarioService>();
builder.Services.AddTransient<SalidasInventarioDetallesService>();
builder.Services.AddTransient<SucursalesService>();
builder.Services.AddTransient<UsuariosService>();

//DOMAINS
builder.Services.AddTransient<EmpleadosDomain>();
builder.Services.AddTransient<EstadoSalidasDomain>();
builder.Services.AddTransient<EstadosCivilesDomain>();
builder.Services.AddTransient<LotesDomain>();
builder.Services.AddTransient<PermisosDomain>();
builder.Services.AddTransient<ProductosDomain>();
builder.Services.AddTransient<RolesDomain>();
builder.Services.AddTransient<RolesPorPermisoDomain>();
builder.Services.AddTransient<SalidasInventarioDomain>();
builder.Services.AddTransient<SalidasInventarioDetallesDomain>();
builder.Services.AddTransient<SucursalesDomain>();
builder.Services.AddTransient<UsuariosDomain>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// builder.Services.AddSwaggerForFsIdentityServer(opt =>
//{
//    opt.Title = "AcademiaFS";
//    opt.Description = "Proyecto de inventario";
//    opt.Version = "v1.0";
//});

//builder.Services.AddFsAuthService(configureOptions =>
//{
//    configureOptions.Username = builder.Configuration.GetFromENV("Configurations:FsIdentityServer:Username");
//    configureOptions.Password = builder.Configuration.GetFromENV("Configurations:FsIdentityServer:Password");
//});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
     app.UseSwagger();
    //app.UseSwaggerWithFsIdentityServer();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
//app.UseFsAuthService();

app.MapControllers();

app.Run();
