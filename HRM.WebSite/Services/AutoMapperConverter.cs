using System.Collections.Generic;
using AutoMapper;
using HRM.Services.Converts;

namespace HRM.WebSite.Services
{
    public class AutoMapperConverter : IConverter
    {
        public AutoMapperConverter()
        {
            
        }

        public TDestination Map<TSource, TDestination>(TSource source, TDestination destination)
        {
            return Mapper.Map(source, destination);
        }

        public TDestination Map<TSource, TDestination>(TSource source)
        {
            return Mapper.Map<TSource, TDestination>(source);
        }

        public IEnumerable<TDestination> Map<TSource, TDestination>(IEnumerable<TSource> source, IEnumerable<TDestination> destination)
        {
            return Mapper.Map(source, destination);
        }

        public IEnumerable<TDestination> Map<TSource, TDestination>(IEnumerable<TSource> source)
        {
            return Mapper.Map<IEnumerable<TSource>, IEnumerable<TDestination>>(source);
        }
    }
}