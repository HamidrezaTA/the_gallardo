namespace Application.Utilities.Mapper;

public interface IMapper
{
    TDestination MapSourceToDestination<TSource, TDestination>(TSource source);
    TDestination MapSourceToDestination<TSource, TDestination>(TSource source, TDestination destination);
}
