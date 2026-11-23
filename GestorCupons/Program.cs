using GestorCupons.Interfaces;
using GestorCupons.Repositories;
using GestorCupons.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<ICupomRepository, CupomRepository>();
builder.Services.AddScoped<ICupomService, CupomService>();

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

app.Run();