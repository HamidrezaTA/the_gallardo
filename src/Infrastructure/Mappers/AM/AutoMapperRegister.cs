using Application.Utilities.Mapper;
using Infrastructure.Mappers;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Mappers.AM;
public static class AutoMapperRegister
{
    public static void RegisterAutoMapper(this IServiceCollection services)
    {
        services.AddSingleton<IApplicationMapper, AutoMapperService>();
        services.AddAutoMapper(typeof(AutoMapperService));
    }
}
