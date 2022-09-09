using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Escola.Domain.Interfaces.Repositories;
using Escola.Infra.DataBase.Repositories;

namespace Escola.Api.Config.IoC
{
    public class RepositoryIoC
    {
        public static void RegisterServices(IServiceCollection builder){
            builder.AddScoped<IAlunoRepositorio,AlunoRepositorio>();
        }
    }
}