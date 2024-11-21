namespace PetFinderAPI.Repositorios
{
    using MongoDB.Driver;
    using PetFinderAPI.Models;

    public class UsuarioRepository
    {
        private readonly IMongoCollection<Usuario> _usuarios;

        public UsuarioRepository(IMongoDatabase database)
        {
            _usuarios = database.GetCollection<Usuario>("Usuarios");
        }

        public async Task<List<Usuario>> GetAllAsync() =>
            await _usuarios.Find(_ => true).ToListAsync();

        public async Task<Usuario> GetByIdAsync(string id) =>
            await _usuarios.Find(u => u.Id == id).FirstOrDefaultAsync();

        public async Task<Usuario> GetByEmailAsync(string email) =>
            await _usuarios.Find(u => u.Email == email).FirstOrDefaultAsync();

        public async Task CreateAsync(Usuario usuario) =>
            await _usuarios.InsertOneAsync(usuario);

        public async Task UpdateAsync(string id, Usuario usuario) =>
            await _usuarios.ReplaceOneAsync(u => u.Id == id, usuario);

        public async Task DeleteAsync(string id) =>
            await _usuarios.DeleteOneAsync(u => u.Id == id);
    }
}
