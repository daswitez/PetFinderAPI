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

        [UseFiltering]
        [UseSorting]
        public async Task<Publicacion> GetPublicacionById(string id)
        {
            var publicacion = await _publicacionRepository.GetByIdAsync(id);
            if (publicacion == null)
            {
                throw new Exception("Publicación no encontrada");
            }

            // Relacionar la ubicación usando el ID
            if (!string.IsNullOrEmpty(publicacion.UbicacionId))
            {
                publicacion.Ubicacion = await _ubicacionRepository.GetByIdAsync(publicacion.UbicacionId);
            }

            return publicacion;
        }


        [UseFiltering]
        [UseSorting]
        public async Task<List<Publicacion>> GetPublicacionesByUsuarioId(string usuarioId)
        {
            var publicaciones = await _publicacionRepository.GetByUsuarioIdAsync(usuarioId);
            if (publicaciones == null || !publicaciones.Any())
            {
                throw new Exception("No se encontraron publicaciones para el usuario");
            }

            foreach (var publicacion in publicaciones)
            {
                if (!string.IsNullOrEmpty(publicacion.UbicacionId))
                {
                    publicacion.Ubicacion = await _ubicacionRepository.GetByIdAsync(publicacion.UbicacionId);
                }
            }

            return publicaciones;
        }
        [UseFiltering]
        [UseSorting]
        public async Task<Ubicacion> GetUbicacionByPublicacionId(string publicacionId)
        {
            // Obtener la publicación por su ID
            var publicacion = await _publicacionRepository.GetByIdAsync(publicacionId);
            if (publicacion == null)
            {
                throw new Exception("Publicación no encontrada");
            }

            // Validar si tiene una ubicación asociada
            if (string.IsNullOrEmpty(publicacion.UbicacionId))
            {
                throw new Exception("La publicación no tiene una ubicación asociada");
            }

            // Obtener la ubicación usando el UbicacionId de la publicación
            var ubicacion = await _ubicacionRepository.GetByIdAsync(publicacion.UbicacionId);
            if (ubicacion == null)
            {
                throw new Exception("Ubicación no encontrada");
            }

            return ubicacion;
        }



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

        [UseFiltering]
        [UseSorting]
        public async Task<List<MascotaPropia>> GetMascotaByUsuarioId(string usuarioId)
        {
            var mascotas = await _mascotaRepository.GetAllAsync();
            var mascotasUsuario = mascotas.Where(m => m.UsuarioId == usuarioId).ToList();

            if (!mascotasUsuario.Any())
            {
                throw new Exception("No se encontraron mascotas para el usuario");
            }

            return mascotasUsuario;
        }

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

        public async Task<Usuario> UpdateUsuario([ID] string id, UsuarioInput input)
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
            var existingUbicacion = await _ubicacionRepository.GetByIdAsync(id);
            if (existingUbicacion == null)
            {
                throw new Exception("Ubicación no encontrada");
            }

            var ubicacion = new Ubicacion
            {
                Id = existingUbicacion.Id,
                Latitud = input.Latitud,
                Longitud = input.Longitud
            };

            await _ubicacionRepository.UpdateAsync(id, ubicacion);
            return ubicacion;
        }
        public async Task<UbicacionWithPublicacion> GetUbicacionByPublicacion(string publicacionId)
        {
            // Obtener la publicación por su ID
            var publicacion = await _publicacionRepository.GetByIdAsync(publicacionId);
            if (publicacion == null)
            {
                throw new Exception("Publicación no encontrada");
            }

            // Obtener la ubicación asociada a la publicación
            if (string.IsNullOrEmpty(publicacion.UbicacionId))
            {
                throw new Exception("La publicación no tiene una ubicación asociada");
            }

            var ubicacion = await _ubicacionRepository.GetByIdAsync(publicacion.UbicacionId);
            if (ubicacion == null)
            {
                throw new Exception("Ubicación no encontrada");
            }

            // Retornar ambas entidades
            return new UbicacionWithPublicacion
            {
                Ubicacion = ubicacion,
                Publicacion = publicacion
            };
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
                FechaSuministrar = input.FechaSuministrar,
                MascotaPropiaId = input.MascotaPropiaId,
                HistorialId = input.HistorialId
            };
            await _recordatorioRepository.CreateAsync(recordatorio);
            return recordatorio;
        }

        public async Task<Recordatorio> UpdateRecordatorio(string id, RecordatorioInput input)
        {
            var existingRecordatorio = await _recordatorioRepository.GetByIdAsync(id);
            if (existingRecordatorio == null)
            {
                throw new Exception("Recordatorio no encontrado");
            }

            var recordatorio = new Recordatorio
            {
                Id = existingRecordatorio.Id,
                Suministrar = input.Suministrar,
                Estado = input.Estado,
                FechaSuministrar = input.FechaSuministrar,
                MascotaPropiaId = input.MascotaPropiaId,
                HistorialId = input.HistorialId
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
                FechaSuministrado = input.FechaSuministrado,
                MascotaPropiaId = input.MascotaPropiaId

            };
            await _historialRepository.CreateAsync(historial);
            return historial;
        }

        public async Task<Historial> UpdateHistorial(string id, Historialnput input)
        {
            var existingHistorial = await _historialRepository.GetByIdAsync(id);
            if (existingHistorial == null)
            {
                throw new Exception("Historial no encontrado");
            }

            var historial = new Historial
            {
                Id = existingHistorial.Id,
                Suministrado = input.Suministrado,
                FechaSuministrado = input.FechaSuministrado,
                MascotaPropiaId = input.MascotaPropiaId
            };

            await _historialRepository.UpdateAsync(id, historial);
            return historial;
        }

        public async Task<bool> DeleteHistorial(string id)
        {
            await _historialRepository.DeleteAsync(id);
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
        public async Task<Ubicacion?> GetUbicacion([Parent] Publicacion publicacion)
        {
            // Usa el UbicacionId de la publicación para buscar la ubicación asociada.
            return await _ubicacionRepository.GetByIdAsync(publicacion.UbicacionId);
        }


    }
}
