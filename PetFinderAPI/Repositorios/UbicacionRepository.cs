using MongoDB.Driver;
using PetFinderAPI.Models;

namespace PetFinderAPI.Repositorios
{
    public class UbicacionRepository
    {
        private readonly IMongoCollection<Ubicacion> _ubicaciones;

        public UbicacionRepository(IMongoDatabase database)
        {
            _ubicaciones = database.GetCollection<Ubicacion>("Ubicacion");
        }

        public async Task<List<Ubicacion>> GetAllAsync() =>
            await _ubicaciones.Find(_ => true).ToListAsync();

        public async Task<Ubicacion> GetByIdAsync(string id) =>
            await _ubicaciones.Find(u => u.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(Ubicacion ubicacion) =>
            await _ubicaciones.InsertOneAsync(ubicacion);

        public async Task UpdateAsync(string id, Ubicacion ubicacion) =>
            await _ubicaciones.ReplaceOneAsync(u => u.Id == id, ubicacion);

        public async Task DeleteAsync(string id) =>
            await _ubicaciones.DeleteOneAsync(u => u.Id == id);
    }
}
