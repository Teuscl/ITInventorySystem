using ITInventorySystem.Data;
using ITInventorySystem.Repositories.Implementations;
using ITInventorySystem.Repositories.Interfaces;
using ITInventorySystem.Repositories.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Adicionar os servi�os ao cont�iner.
builder.Services.AddControllers();

// Configura��o de CORS para permitir a origem do frontend
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        // Ajuste o URL do seu frontend. Aqui est� configurado para localhost:3000
        policy.WithOrigins("http://localhost:5173") // URL do seu frontend (ajuste conforme necess�rio)
              .AllowAnyMethod()   // Permite qualquer m�todo HTTP (GET, POST, PUT, DELETE, etc.)
              .AllowAnyHeader()   // Permite qualquer cabe�alho
              .AllowCredentials(); // Permite enviar cookies ou credenciais
    });
});

// Configura��o do Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Inje��o de depend�ncias para os servi�os
builder.Services.AddScoped<IUserInterface, UserService>();
builder.Services.AddScoped<IClientInterface, ClientService>();
builder.Services.AddScoped<IWorkOrderInterface, WorkOrderService>();
builder.Services.AddScoped<IProductInterface, ProductService>();

// Configura��o do banco de dados com SQL Server
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

var app = builder.Build();

// Configura��o do pipeline HTTP.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Habilitar CORS com a pol�tica definida
app.UseCors("AllowFrontend");

// Configura��o de autoriza��o
app.UseAuthorization();

// Mapear os controllers
app.MapControllers();

// Inicia a aplica��o
app.Run();
