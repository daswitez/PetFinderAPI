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
        public string Estado { get; set; } = null!; // "Perdida" o "Adopcion"
        public string Nombre { get; set; } = null!;
        public string Especie {  get; set; } = null!;
        public string Raza { get; set; } = null!;
        public string Color { get; set; } = null!;
        public string Tamanho { get; set; } = null!;
        public string Sexo { get; set; } = null!;
        public string Foto { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
        public DateTime FechaPublicacion { get; set; } = DateTime.UtcNow;
    }
}
