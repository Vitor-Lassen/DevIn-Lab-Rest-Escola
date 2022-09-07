using Escola.Domain.Interfaces.Services;
using Escola.Domain.Interfaces.Repositories;
using Escola.Infra.DataBase.Repositories;
using Escola.Domain.Services;
using Escola.Infra.DataBase;
using Escola.Api.Config;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<EscolaDBContexto>();

builder.Services.AddScoped<IAlunoRepositorio,AlunoRepositorio>();
builder.Services.AddScoped<IAlunoServico,AlunoServico>();
builder.Services.AddScoped<IMateriaRepositorio,MateriaRepositorio>();
builder.Services.AddScoped<IMateriaServico, MateriaServico>();

builder.Services.AddMemoryCache();
builder.Services.AddScoped(typeof(CacheService<>));


builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.MapControllers();
app.UseMiddleware<ErrorMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        options.RoutePrefix = string.Empty;
    });
}

app.Run();
