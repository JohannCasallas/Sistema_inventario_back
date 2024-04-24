namespace Sistema_inventario_backend.Models
{
    public class Respuesta<T>
    {
        public string? Mensaje { get; set; }
        public bool Exitoso { get; set; }
        public T? Datos { get; set; }
    }
}
