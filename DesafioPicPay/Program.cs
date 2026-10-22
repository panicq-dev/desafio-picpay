using DesafioPicPay.Infra;
using DesafioPicPay.Infra.Repositorios.Carteiras;
using DesafioPicPay.Infra.Repositorios.Transferencias;
using DesafioPicPay.Services.Autorizador;
using DesafioPicPay.Services.Carteiras;
using DesafioPicPay.Services.Transferencias;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Swagger Container.
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

// Conexão.
builder.Services.AddDbContext<AplicacaoDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));
// Repositorios.
builder.Services.AddScoped<ICarteiraRepositorio, CarteiraRepositorio>(); 
builder.Services.AddScoped<ITransferenciaRepositorio, TransferenciaRepositorio>();
// AutoMapper.
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies()); 
// Services
builder.Services.AddScoped<ICarteiraService, CarteiraService>();
builder.Services.AddScoped<ITransferenciaService, TransferenciaService>();
builder.Services.AddScoped<IAutorizadorService, AutorizadorService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.MapControllers();
app.UseHttpsRedirection();
app.Run();
