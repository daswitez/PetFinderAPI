using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace PetFinderAPI.Models
{
    public class Ubicacion
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = null!;
        public string Latitud { get; set; } = null!;
        public string Longitud { get; set;} = null!;
    }
}
