using System.Collections.Generic;

namespace HRM.Services.Converts
{
    public interface IConverter
    {
        TDestination Map<TSource, TDestination>(TSource source, TDestination destination);
        TDestination Map<TSource, TDestination>(TSource source);
        IEnumerable<TDestination> Map<TSource, TDestination>(IEnumerable<TSource> source);
        IEnumerable<TDestination> Map<TSource, TDestination>(IEnumerable<TSource> source, IEnumerable<TDestination> destination);
    }
}