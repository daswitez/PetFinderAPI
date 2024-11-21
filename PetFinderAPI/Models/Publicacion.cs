using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace PetFinderAPI.Models
{
    public class Publicacion
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = null!;

        public string UsuarioId { get; set; } = null!; // Relación con Usuario
        public string UbicacionId { get; set; } = null!; // Relación con Ubicacion
        public string? Estado { get; set; } // "Perdida" o "Adopcion"
        public string? Nombre { get; set; }
        public string? Especie {  get; set; } 
        public string? Raza { get; set; } 
        public string? Color { get; set; } 
        public string? Tamanho { get; set; } 
        public string? Sexo { get; set; } 
        public string? Foto { get; set; } 
        public string? Descripcion { get; set; } 
        public DateTime FechaPublicacion { get; set; } = DateTime.UtcNow;
    }
}
