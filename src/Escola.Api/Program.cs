using Escola.Domain.Interfaces.Services;
using Escola.Domain.Interfaces.Repositories;
using Escola.Infra.DataBase.Repositories;
using Escola.Domain.Services;
using Escola.Infra.DataBase;
using Escola.Api.Config;
using Escola.Api.Config.IoC;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<EscolaDBContexto>();

RepositoryIoC.RegisterServices(builder.Services);

builder.Services.AddScoped<IAlunoServico,AlunoServico>();

builder.Services.AddMemoryCache();
builder.Services.AddScoped(typeof(CacheService<>));

builder.Services.AddSingleton(AutoMapperConfig.Configure());

builder.Services.AddControllers()
                .AddNewtonsoftJson();
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
