using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;
using MongoDB.Driver;
using PetFinderAPI.Models;
using PetFinderAPI.Repositorios;
using HotChocolate;
using PetFinderAPI.input;

namespace PetFinderAPI.Resolvers
{
    public class PetFinderResolver
    {
        private readonly UsuarioRepository _usuarioRepository;
        private readonly MascotaPropiaRepository _mascotaRepository;
        private readonly PublicacionRepository _publicacionRepository;
        private readonly UbicacionRepository _ubicacionRepository;
        private readonly RecordatorioRepository _recordatorioRepository;
        private readonly HistorialRepository _historialRepository;

        private const string JwtSecretKey = "ClaveSuperSeguraYExtendida1234567890"; 
        private const string JwtIssuer = "PetFinderAPI"; 
        private const string JwtAudience = "PetFinderApp"; 

        public PetFinderResolver(
            UsuarioRepository usuarioRepository,
            MascotaPropiaRepository mascotaRepository,
            PublicacionRepository publicacionRepository,
            UbicacionRepository ubicacionRepository,
            RecordatorioRepository recordatorioRepository,
            HistorialRepository historialRepository)
        {
            _usuarioRepository = usuarioRepository;
            _mascotaRepository = mascotaRepository;
            _publicacionRepository = publicacionRepository;
            _ubicacionRepository = ubicacionRepository;
            _recordatorioRepository = recordatorioRepository;
            _historialRepository = historialRepository;
        }

        // --- Consultas ---

        [UseFiltering]
        [UseSorting]
        public async Task<List<Usuario>> GetUsuarios() =>
            await _usuarioRepository.GetAllAsync();

        public async Task<Usuario> GetUsuarioById(string id) =>
            await _usuarioRepository.GetByIdAsync(id);

        [UseFiltering]
        [UseSorting]
        public async Task<List<MascotaPropia>> GetMascotas() =>
            await _mascotaRepository.GetAllAsync();

        public async Task<MascotaPropia> GetMascotaById(string id) =>
            await _mascotaRepository.GetByIdAsync(id);

        [UseFiltering]
        [UseSorting]
        public async Task<List<Publicacion>> GetPublicaciones() =>
            await _publicacionRepository.GetAllAsync();

        public async Task<Publicacion> GetPublicacionById(string id) =>
            await _publicacionRepository.GetByIdAsync(id);

        public async Task<List<Publicacion>> GetPublicacionesByUsuarioId(string usuarioId) =>
            await _publicacionRepository.GetByUsuarioIdAsync(usuarioId);

        [UseFiltering]
        [UseSorting]
        public async Task<List<Ubicacion>> GetUbicaciones() =>
            await _ubicacionRepository.GetAllAsync();

        public async Task<Ubicacion> GetUbicacionById(string id) =>
            await _ubicacionRepository.GetByIdAsync(id);

        [UseFiltering]
        [UseSorting]
        public async Task<List<Recordatorio>> GetRecordatorios() =>
            await _recordatorioRepository.GetAllAsync();

        public async Task<Recordatorio> GetRecordatorioById(string id) =>
            await _recordatorioRepository.GetByIdAsync(id);

        [UseFiltering]
        [UseSorting]
        public async Task<List<Historial>> GetHistoriales() =>
            await _historialRepository.GetAllAsync();

        public async Task<Historial> GetHistorialById(string id) =>
            await _historialRepository.GetByIdAsync(id);

        // --- Mutaciones ---

        public async Task<Usuario> CreateUsuario(UsuarioInput input)
        {
            var usuario = new Usuario
            {
                Nombre = input.Nombre,
                Apellido = input.Apellido,
                Email = input.Email,
                Telefono = input.Telefono,
                Contraseña = input.Contraseña,
                tipo = input.tipo
            };
            await _usuarioRepository.CreateAsync(usuario);
            return usuario;
        }

        public async Task<Usuario> UpdateUsuario(string id, UsuarioInput input)
        {
            var existingUsuario = await _usuarioRepository.GetByIdAsync(id);
            if (existingUsuario == null)
            {
                throw new Exception("Usuario no encontrado");
            }

            var usuario = new Usuario
            {
                Id = existingUsuario.Id,
                Nombre = input.Nombre,
                Apellido = input.Apellido,
                Email = input.Email,
                Telefono = input.Telefono,
                Contraseña = input.Contraseña,
                tipo = input.tipo
            };

            await _usuarioRepository.UpdateAsync(id, usuario);
            return usuario;
        }

        public async Task<bool> DeleteUsuario(string id)
        {
            await _usuarioRepository.DeleteAsync(id);
            return true;
        }

        public async Task<MascotaPropia> CreateMascota(MascotaPropiaInput input)
        {
            var mascota = new MascotaPropia
            {
                Nombre = input.Nombre,
                Raza = input.Raza,
                Sexo = input.Sexo,
                UsuarioId = input.UsuarioId
            };
            await _mascotaRepository.CreateAsync(mascota);
            return mascota;
        }

        public async Task<MascotaPropia> UpdateMascota(string id, MascotaPropiaInput input)
        {
            var existingMascota = await _mascotaRepository.GetByIdAsync(id);
            if (existingMascota == null)
            {
                throw new Exception("Mascota no encontrada");
            }

            var mascota = new MascotaPropia
            {
                Id = existingMascota.Id,
                Nombre = input.Nombre,
                Raza = input.Raza,
                Sexo = input.Sexo,
                UsuarioId = input.UsuarioId
            };

            await _mascotaRepository.UpdateAsync(id, mascota);
            return mascota;
        }

        public async Task<bool> DeleteMascota(string id)
        {
            await _mascotaRepository.DeleteAsync(id);
            return true;
        }

        public async Task<Publicacion> CreatePublicacion(PublicacionInput input)
        {
            var publicacion = new Publicacion
            {
                Estado = input.Estado,
                Nombre = input.Nombre,
                Especie = input.Especie,
                Raza = input.Raza,
                Color = input.Color,
                Tamanho = input.Tamanho,
                Sexo = input.Sexo,
                Foto = input.Foto,
                Descripcion = input.Descripcion,
                UsuarioId = input.UsuarioId,
                UbicacionId = input.UbicacionId,
                FechaPublicacion = DateTime.UtcNow
            };
            await _publicacionRepository.CreateAsync(publicacion);
            return publicacion;
        }

        public async Task<Publicacion> UpdatePublicacion(string id, PublicacionInput input)
        {
            var existingPublicacion = await _publicacionRepository.GetByIdAsync(id);
            if (existingPublicacion == null)
            {
                throw new Exception("Publicación no encontrada");
            }

            var publicacion = new Publicacion
            {
                Id = existingPublicacion.Id,
                Estado = input.Estado,
                Nombre = input.Nombre,
                Especie = input.Especie,
                Raza = input.Raza,
                Color = input.Color,
                Tamanho = input.Tamanho,
                Sexo = input.Sexo,
                Foto = input.Foto,
                Descripcion = input.Descripcion,
                UsuarioId = input.UsuarioId,
                UbicacionId = input.UbicacionId,
                FechaPublicacion = existingPublicacion.FechaPublicacion
            };

            await _publicacionRepository.UpdateAsync(id, publicacion);
            return publicacion;
        }

        public async Task<bool> DeletePublicacion(string id)
        {
            await _publicacionRepository.DeleteAsync(id);
            return true;
        }

        // --- Mutación para Login ---
        public async Task<AuthPayload> Login(LoginInput input)
        {
            var usuario = await _usuarioRepository.GetByEmailAsync(input.Email);
            if (usuario == null || usuario.Contraseña != input.Password)
            {
                throw new Exception("Credenciales inválidas");
            }

            var token = GenerateJwtToken(usuario);

            return new AuthPayload
            {
                Token = token,
                Usuario = usuario
            };
        }

        private string GenerateJwtToken(Usuario usuario)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtSecretKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, usuario.Id),
                new Claim(JwtRegisteredClaimNames.Email, usuario.Email),
                new Claim("tipo", usuario.tipo)
            };

            var token = new JwtSecurityToken(
                issuer: JwtIssuer,
                audience: JwtAudience,
                claims: claims,
                expires: DateTime.UtcNow.AddHours(2),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
