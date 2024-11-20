using MongoDB.Driver;

namespace PetFinderAPI
{
    public class DatabaseInitializer
    {
        private readonly IMongoDatabase _database;

        public DatabaseInitializer(IMongoDatabase database)
        {
            _database = database;
        }

        public void Initialize()
        {
            var collections = _database.ListCollectionNames().ToList();

            if (!collections.Contains("Usuarios"))
            {
                _database.CreateCollection("Usuarios");
            }

            if (!collections.Contains("Recordatorio"))
            {
                _database.CreateCollection("Recordatorio");
            }

            if (!collections.Contains("Historial"))
            {
                _database.CreateCollection("Historial");
            }

            if (!collections.Contains("Ubicacion"))
            {
                _database.CreateCollection("Ubicacion");
            }

            if (!collections.Contains("MascotaPropia"))
            {
                _database.CreateCollection("MascotaPropia");
            }

            if (!collections.Contains("Publicaciones"))
            {
                _database.CreateCollection("Publicaciones");
            }
        }
    }
}
