using AutoMapper;
using Escola.Api.Config.AutoMapper;


namespace Escola.Api.Config
{
    public static class AutoMapperConfig
    {
        public static IMapper Configure(){
            var configMap = new MapperConfiguration( config => {
                config.AddProfile(new AlunoAutoMapper());
            });
            return configMap.CreateMapper();
        }
    }
}