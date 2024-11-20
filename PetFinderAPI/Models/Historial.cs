using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace PetFinderAPI.Models
{
    public class Historial
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Suministrado { get; set; }
        public string MascotaPropiaId { get; set; } = null!;
        public DateOnly FechaSuministrado { get; set; } 
    }
}
