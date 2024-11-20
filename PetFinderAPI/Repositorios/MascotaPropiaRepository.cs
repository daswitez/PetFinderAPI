namespace PetFinderAPI.Repositorios
{
    using MongoDB.Driver;
    using PetFinderAPI.Models;

    public class MascotaPropiaRepository
    {
        private readonly IMongoCollection<MascotaPropia> _mascotaPropia;

        public MascotaPropiaRepository(IMongoDatabase database)
        {
            _mascotaPropia = database.GetCollection<MascotaPropia>("MascotasPropia");
        }

        public async Task<List<MascotaPropia>> GetAllAsync() =>
            await _mascotaPropia.Find(_ => true).ToListAsync();

        public async Task<MascotaPropia> GetByIdAsync(string id) =>
            await _mascotaPropia.Find(m => m.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(MascotaPropia mascota) =>
            await _mascotaPropia.InsertOneAsync(mascota);

        public async Task UpdateAsync(string id, MascotaPropia mascota) =>
            await _mascotaPropia.ReplaceOneAsync(m => m.Id == id, mascota);

        public async Task DeleteAsync(string id) =>
            await _mascotaPropia.DeleteOneAsync(m => m.Id == id);
    }

}
