using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace PetFinderAPI.Models
{
    public class Recordatorio
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Suministrar { get; set; } = null!;
        public string Estado { get; set; } = null!;
        public string MascotaPropiaId { get; set; } = null!;
        public string HistorialId { get; set; } = null!;
        public DateOnly FechaSuministrar { get; set; }

    }
}
