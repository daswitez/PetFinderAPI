namespace PetFinderAPI.input
{
    public class PublicacionInput
    {
        public string UsuarioId { get; set; } = null!;  
        public string UbicacionId { get; set; } = null!;  
        public string Estado { get; set; } = null!; // "Perdida" o "Adopcion"
        public string? Nombre { get; set; }
        public string Especie { get; set; } = null!;
        public string Raza { get; set; } = null!;
        public string Color { get; set; } = null!;
        public string Tamanho { get; set; } = null!;
        public string Sexo { get; set; } = null!;
        public string Foto { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
    }
}
