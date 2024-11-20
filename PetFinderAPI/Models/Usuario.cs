using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace PetFinderAPI.Models
{
    public class Usuario
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = null!;
        public string Nombre { get; set; } = null!;
        public string Apellido { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Telefono { get; set; } = null!;
        public string Contraseña { get; set; } = null!;
        public string tipo { get; set; } = null!; // "Admin", "Usuario", etc.
    }
}
