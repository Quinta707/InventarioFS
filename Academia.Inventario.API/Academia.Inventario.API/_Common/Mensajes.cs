namespace Academia.Inventario.API._Common
{
    public class Mensajes
    {
        public const string Proceso_Exitoso = "El proceso ha sido ejecutado exitosamente";

        public const string Proceso_Fallido = "El proceso ha fallado, arregle su codigo plz";

        public const string No_Hay_Registros = "No hay registros con las caracteristicas solicitadas";

        public const string Peticion_Fallida = "Los datos enviados son erróneos";

        public const string Genero_Invalido = "El género es inválido";

        public const string Id_Nulo = "El ID es requerido";

        public const string Inventario_Insuficiente = "El inventario es insuficiente";

        public const string Lotes_No_Disponibles = "No hay lotes con el producto seleccionado";

        public const string Sucursal_Limite = "No se pueden realizar mas salidas a la sucursal seleccionada. Reciba Salidas para liberar espacio.";

        public const string Permisos_Insuficientes = "El usuario no tiene los permisos suficientes";
        public static string Campos_Vacios(string? cadena)
        {
            return $"El campo '{cadena}' es requerido";
        }
        public static string Longitd_Excedida(string? cadena, int? numero)
        {
            return $"El campo '{cadena}' no puede ser mayor a '{numero}' caracteres";
        }
        public static string Fuera_De_Rango(string? cadena, int? numero, int? numero2)
        {
            return $"El campo '{cadena}' no puede ser menor a '{numero}' y mayor a '{numero2}' caracteres";
        }
        public static string Campo_Repetido(string? cadena)
        {
            return $"El registro '{cadena}' ya existe en la base de datos";
        }
        public static string Registro_No_Existe(string? cadena)
        {
            return $"El registro '{cadena}' no existe en la base de datos";
        }
        public static string Debe_Ser_Int(string? cadena)
        {
            return $"El campo '{cadena}' debe ser un numero";
        }
        public static string Formato_Incorrecto(string? cadena)
        {
            return $"El campo '{cadena}' no tiene el formato correcto";
        }
    }
}
