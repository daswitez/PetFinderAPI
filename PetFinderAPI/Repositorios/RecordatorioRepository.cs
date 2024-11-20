using MongoDB.Driver;
using PetFinderAPI.Models;

namespace PetFinderAPI.Repositorios
{
    public class RecordatorioRepository
    {
        private readonly IMongoCollection<Recordatorio> _recordatorios;

        public RecordatorioRepository(IMongoDatabase database)
        {
            _recordatorios = database.GetCollection<Recordatorio>("Recordatorio");
        }

        public async Task<List<Recordatorio>> GetAllAsync() =>
            await _recordatorios.Find(_ => true).ToListAsync();

        public async Task<Recordatorio> GetByIdAsync(string id) =>
            await _recordatorios.Find(r => r.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(Recordatorio recordatorio) =>
            await _recordatorios.InsertOneAsync(recordatorio);

        public async Task UpdateAsync(string id, Recordatorio recordatorio) =>
            await _recordatorios.ReplaceOneAsync(r => r.Id == id, recordatorio);

        public async Task DeleteAsync(string id) =>
            await _recordatorios.DeleteOneAsync(r => r.Id == id);
    }
}
