using MongoDB.Driver;
using PetFinderAPI.Models;

namespace PetFinderAPI.Repositorios
{
    using MongoDB.Driver;

    public class PublicacionRepository
    {
        private readonly IMongoCollection<Publicacion> _publicaciones;

        public PublicacionRepository(IMongoDatabase database)
        {
            _publicaciones = database.GetCollection<Publicacion>("Publicaciones");
        }

        public async Task<List<Publicacion>> GetAllAsync() =>
            await _publicaciones.Find(_ => true).ToListAsync();

        public async Task<Publicacion> GetByIdAsync(string id) =>
            await _publicaciones.Find(p => p.Id == id).FirstOrDefaultAsync();

        public async Task<List<Publicacion>> GetByUsuarioIdAsync(string usuarioId) =>
            await _publicaciones.Find(p => p.UsuarioId == usuarioId).ToListAsync();

        public async Task CreateAsync(Publicacion publicacion) =>
            await _publicaciones.InsertOneAsync(publicacion);

        public async Task UpdateAsync(string id, Publicacion publicacion) =>
            await _publicaciones.ReplaceOneAsync(p => p.Id == id, publicacion);

        public async Task DeleteAsync(string id) =>
            await _publicaciones.DeleteOneAsync(p => p.Id == id);
    }


}
