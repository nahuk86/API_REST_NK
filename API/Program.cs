using DAL;
using Negocio;
using Negocio.Workers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Registrar servicios de persistencia (DAL)
builder.Services.AddPersistenceServices();

// Registrar servicios de negocio
builder.Services.AddBusinessServices();

// Registrar el worker como servicio hosted
builder.Services.AddHostedService<PrintWorkerHostedService>();

// Configuración de CORS si es necesario
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Print Jobs API v1");
        c.RoutePrefix = string.Empty; // Para que Swagger esté en la raíz
    });
}

app.UseHttpsRedirection();

app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

app.Run();