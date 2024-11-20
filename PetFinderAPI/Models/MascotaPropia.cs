using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace PetFinderAPI.Models
{
    public class MascotaPropia
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = null!;
        public string Nombre { get; set; } = null!;
        public string Raza { get; set; } = null!;
        public string Sexo { get; set; } = null!;
        public string UsuarioId { get; set; } = null!;
    }
}
