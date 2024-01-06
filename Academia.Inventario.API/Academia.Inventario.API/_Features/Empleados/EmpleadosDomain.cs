using Academia.Inventario.API._Common;
using Academia.Inventario.API._Features.EstadosCiviles;
using Academia.Inventario.API.Infrastructure.InventarioDB.Entities;

namespace Academia.Inventario.API._Features.Empleados
{
    public class EmpleadosDomain
    {
        private readonly EstadosCivilesDomain _estadosCivilesDomain;

        public EmpleadosDomain(EstadosCivilesDomain estadosCivilesDomain)
        {
            _estadosCivilesDomain = estadosCivilesDomain;
        }

        public bool EmpleadoExisteId(List<Empleado> empleadoList, int? id)
        {
            if (empleadoList.Where(x => x.EmpleadoId == id && x.Activo == true).Any())
                return true;
            return false;
        }
        public bool EmpleadoExiste(List<Empleado> empleadoList, string? Identidad)
        {
            if (empleadoList.Where(x => x.Identidad == Identidad && x.Activo == true).Any())
                return true;
            return false;
        }
        public string AgregarEmpleados(List<Empleado> empleadoList, List<EstadosCivile> estadosCivilesList, string? identidad, int? estadocivilID)
        {
            if (EmpleadoExiste(empleadoList, identidad))
                return Mensajes.Campo_Repetido("Identidad");

            if (!_estadosCivilesDomain.EstadoCivilExisteId(estadosCivilesList, estadocivilID))
                return Mensajes.Registro_No_Existe("EstadosCivilID");

            return "200";
        }

        public string EditarEmpleados(List<Empleado> empleadoList, string? identidad, int? EmpleadoId, List<EstadosCivile> estadosCivilesList, int? estadocivilID)
        {
            if (EmpleadoExiste(empleadoList.Where(X => X.EmpleadoId != EmpleadoId).ToList(), identidad))
                return Mensajes.Campo_Repetido("Identidad");

            if (!_estadosCivilesDomain.EstadoCivilExisteId(estadosCivilesList, estadocivilID))
                return Mensajes.Registro_No_Existe("EstadosCivilID");

            return "200";
        }
    }
}
