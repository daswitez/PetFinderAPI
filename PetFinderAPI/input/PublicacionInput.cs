namespace PetFinderAPI.input
{
    public class PublicacionInput
    {
        public string UsuarioId { get; set; } = null!;  
        public string UbicacionId { get; set; } = null!;  
        public string? Estado { get; set; } // "Perdida" o "Adopcion"
        public string? Nombre { get; set; }
        public string? Especie { get; set; } 
        public string? Raza { get; set; } 
        public string? Color { get; set; } 
        public string? Tamanho { get; set; }
        public string? Telefono { get; set; }
        public string? Sexo { get; set; } 
        public string? Foto { get; set; } 
        public string? Descripcion { get; set; } 
    }
}
