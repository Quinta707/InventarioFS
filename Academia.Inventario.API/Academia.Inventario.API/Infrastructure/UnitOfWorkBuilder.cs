using Academia.Inventario.API.Infrastructure.InventarioDB;
using Farsiman.Domain.Core.Standard.Repositories;
using Farsiman.Infraestructure.Core.Entity.Standard;
using Microsoft.EntityFrameworkCore;

namespace Academia.Inventario.API.Infrastructure
{
    public class UnitOfWorkBuilder
    {
        readonly IServiceProvider _serviceProvider;

        public UnitOfWorkBuilder(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IUnitOfWork BuilderProyectoInventario()
        {
            DbContext dbContext = _serviceProvider.GetService<InventarioSeqpContext>() ?? throw new NullReferenceException();
            return new UnitOfWork(dbContext);
        }
    }
}
