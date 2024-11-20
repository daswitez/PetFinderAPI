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
            var usuario = new Usuario
            {
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
                Sexo = input.Sexo
            };
            await _mascotaRepository.CreateAsync(mascota);
            return mascota;
        }

        public async Task<MascotaPropia> UpdateMascota(string id, MascotaPropiaInput input)
        {
            var mascota = new MascotaPropia
            {
                Nombre = input.Nombre,
                Raza = input.Raza,
                Sexo = input.Sexo
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
                FechaPublicacion = DateTime.UtcNow
            };
            await _publicacionRepository.CreateAsync(publicacion);
            return publicacion;
        }

        public async Task<Publicacion> UpdatePublicacion(string id, PublicacionInput input)
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
                FechaPublicacion = DateTime.UtcNow
            };
            await _publicacionRepository.UpdateAsync(id, publicacion);
            return publicacion;
        }

        public async Task<bool> DeletePublicacion(string id)
        {
            await _publicacionRepository.DeleteAsync(id);
            return true;
        }

        public async Task<Ubicacion> CreateUbicacion(UbicacionInput input)
        {
            var ubicacion = new Ubicacion
            {
                Latitud = input.Latitud,
                Longitud = input.Longitud
            };
            await _ubicacionRepository.CreateAsync(ubicacion);
            return ubicacion;
        }

        public async Task<Ubicacion> UpdateUbicacion(string id, UbicacionInput input)
        {
            var ubicacion = new Ubicacion
            {
                Latitud = input.Latitud,
                Longitud = input.Longitud
            };
            await _ubicacionRepository.UpdateAsync(id, ubicacion);
            return ubicacion;
        }

        public async Task<bool> DeleteUbicacion(string id)
        {
            await _ubicacionRepository.DeleteAsync(id);
            return true;
        }

        public async Task<Recordatorio> CreateRecordatorio(RecordatorioInput input)
        {
            var recordatorio = new Recordatorio
            {
                Suministrar = input.Suministrar,
                Estado = input.Estado,
                FechaSuministrar = input.FechaSuministrar
            };
            await _recordatorioRepository.CreateAsync(recordatorio);
            return recordatorio;
        }

        public async Task<Recordatorio> UpdateRecordatorio(string id, RecordatorioInput input)
        {
            var recordatorio = new Recordatorio
            {
                Suministrar = input.Suministrar,
                Estado = input.Estado,
                FechaSuministrar = input.FechaSuministrar
            };
            await _recordatorioRepository.UpdateAsync(id, recordatorio);
            return recordatorio;
        }

        public async Task<bool> DeleteRecordatorio(string id)
        {
            await _recordatorioRepository.DeleteAsync(id);
            return true;
        }

        public async Task<Historial> CreateHistorial(Historialnput input)
        {
            var historial = new Historial
            {
                Suministrado = input.Suministrado,
                FechaSuministrado = input.FechaSuministrado
            };
            await _historialRepository.CreateAsync(historial);
            return historial;
        }

        public async Task<Historial> UpdateHistorial(string id, Historialnput input)
        {
            var historial = new Historial
            {
                Suministrado = input.Suministrado,
                FechaSuministrado = input.FechaSuministrado
            };
            await _historialRepository.UpdateAsync(id, historial);
            return historial;
        }

        public async Task<bool> DeleteHistorial(string id)
        {
            await _historialRepository.DeleteAsync(id);
            return true;
        }
    }
}
