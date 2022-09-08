using Escola.Domain.Interfaces.Services;
using Escola.Domain.Interfaces.Repositories;
using Escola.Infra.DataBase.Repositories;
using Escola.Domain.Services;
using Escola.Infra.DataBase;
using Escola.Api.Config; 
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<EscolaDBContexto>();

builder.Services.AddScoped<IAlunoRepositorio,AlunoRepositorio>();
builder.Services.AddScoped<IAlunoServico,AlunoServico>();
builder.Services.AddScoped<IMateriaRepositorio,MateriaRepositorio>();
builder.Services.AddScoped<IMateriaServico, MateriaServico>();

builder.Services.AddMemoryCache();
builder.Services.AddScoped(typeof(CacheService<>));

builder.Services.AddApiVersioning(options =>
            {
                // Retorna os headers "api-supported-versions" e "api-deprecated-versions"
                // indicando versões suportadas pela API e o que está como deprecated
                options.ReportApiVersions = true;

                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = new ApiVersion(1, 0);
            });

builder.Services.AddVersionedApiExplorer(p =>
            {
                p.GroupNameFormat = "'v'VVV";
                p.SubstituteApiVersionInUrl = true;
            });

builder.Services.AddControllers();
builder.Services.AddSwaggerGen(config => { 
    config.SwaggerDoc("v1", new OpenApiInfo {
                               Title = "Escola API", 
                               Version = "v1.0", 
                               Contact = new OpenApiContact{
                                Name = "Vitor Lassen",
                                Email = "vitor@lassen.com.br" 
                                }
                            });
    config.SwaggerDoc("v2", new OpenApiInfo {
                               Title = "Escola API", 
                               Version = "v2.0", 
                               Contact = new OpenApiContact{
                                Name = "Vitor Lassen",
                                Email = "vitor@lassen.com.br" 
                                }
                            });
});

var app = builder.Build();

var provider = app.Services.GetService<IApiVersionDescriptionProvider>();

app.MapControllers();
app.UseMiddleware<ErrorMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        foreach(var description in provider.ApiVersionDescriptions){
            options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json",
                                    description.GroupName);
        }
        options.RoutePrefix = string.Empty;
    });
}

app.Run();
