using AutoMapper;
using Domain.Entities;
using Application.DTOes;
using Application.Utilities.Mapper;

namespace Infrastructure.Mappers.AM
{
    public class AutoMapperService : Profile, IApplicationMapper
    {
        private readonly IMapper _mapper;

        public AutoMapperService(IMapper mapper)
        {
            _mapper = mapper;

            CreateMap<Sample, SampleDto>().ReverseMap();
        }
        public TDestination Map<TSource, TDestination>(TSource source)
        {
            return _mapper.Map<TDestination>(source);
        }

        public TDestination Map<TSource, TDestination>(TSource source, TDestination destination)
        {
            return _mapper.Map(source, destination);
        }
    }
}