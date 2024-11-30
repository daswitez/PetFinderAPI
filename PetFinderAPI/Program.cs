using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;
using PetFinderAPI.Repositorios;
using PetFinderAPI.Resolvers;
using HotChocolate;
using HotChocolate.AspNetCore;
using HotChocolate.Data;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Configuración de CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()  // Permite cualquier origen
              .AllowAnyMethod()  // Permite cualquier método (GET, POST, etc.)
              .AllowAnyHeader();  // Permite cualquier cabecera
    });
});

builder.Services.AddSingleton<IMongoClient>(serviceProvider =>
{
    var connectionString = builder.Configuration.GetValue<string>("MongoDbSettings:ConnectionString");

    if (string.IsNullOrEmpty(connectionString))
    {
        throw new ArgumentNullException("MongoDb connection string is null or empty.");
    }

    return new MongoClient(connectionString);
});

builder.Services.AddScoped(serviceProvider =>
{
    var client = serviceProvider.GetRequiredService<IMongoClient>();

    var databaseName = new MongoUrl(builder.Configuration.GetValue<string>("MongoDbSettings:ConnectionString")).DatabaseName;

    if (string.IsNullOrEmpty(databaseName))
    {
        throw new ArgumentNullException("MongoDb database name is null or empty.");
    }

    return client.GetDatabase(databaseName);
});

builder.Services.AddScoped<UsuarioRepository>();
builder.Services.AddScoped<MascotaPropiaRepository>();
builder.Services.AddScoped<PublicacionRepository>();
builder.Services.AddScoped<UbicacionRepository>();
builder.Services.AddScoped<RecordatorioRepository>();
builder.Services.AddScoped<HistorialRepository>();
builder.Services.AddScoped<PetFinderResolver>();

builder.Services
    .AddGraphQLServer()
    .AddQueryType<PetFinderResolver>()
    .AddMutationType<PetFinderResolver>()
    .AddFiltering()
    .AddSorting();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Habilitar Swagger y SwaggerUI en desarrollo
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Habilitar CORS antes del middleware de autorización
app.UseCors("AllowAll");

app.UseMiddleware<JwtMiddleware>();

app.UseAuthorization();

app.MapControllers();
app.MapGraphQL();

app.Run();
