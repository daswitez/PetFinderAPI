using MongoDB.Driver;
using PetFinderAPI.Repositorios;
using PetFinderAPI.Resolvers;
using HotChocolate;
using HotChocolate.AspNetCore;
using HotChocolate.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IMongoClient>(serviceProvider =>
{
    var connectionString = builder.Configuration.GetValue<string>("MongoDbSettings:ConnectionString");

    // Validar que la cadena de conexión no esté vacía
    if (string.IsNullOrEmpty(connectionString))
    {
        throw new ArgumentNullException("MongoDb connection string is null or empty.");
    }

    // Crear y devolver el cliente de MongoDB
    return new MongoClient(connectionString);
});

// Obtener la base de datos desde el cliente de MongoDB
builder.Services.AddScoped(serviceProvider =>
{
    var client = serviceProvider.GetRequiredService<IMongoClient>();

    // Extraer la base de datos de la cadena de conexión
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

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.MapGraphQL(); 

app.Run();
