using MongoDB.Driver;
using PetFinderAPI.Models;

namespace PetFinderAPI.Repositorios
{
    public class HistorialRepository
    {
        private readonly IMongoCollection<Historial> _historial;

        public HistorialRepository(IMongoDatabase database)
        {
            _historial = database.GetCollection<Historial>("Historial");
        }

        public async Task<List<Historial>> GetAllAsync() =>
            await _historial.Find(_ => true).ToListAsync();

        public async Task<Historial> GetByIdAsync(string id) =>
            await _historial.Find(m => m.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(Historial historial) =>
            await _historial.InsertOneAsync(historial);

        public async Task UpdateAsync(string id, Historial historial) =>
            await _historial.ReplaceOneAsync(m => m.Id == id, historial);

        public async Task DeleteAsync(string id) =>
            await _historial.DeleteOneAsync(m => m.Id == id);
    }
}
