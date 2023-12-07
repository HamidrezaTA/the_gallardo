namespace Application.Utilities.Mapper;

public interface IApplicationMapper
{
    TDestination Map<TSource, TDestination>(TSource source);
    TDestination Map<TSource, TDestination>(TSource source, TDestination destination);
}
