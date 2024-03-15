namespace Interfaces;

public interface IConverter<TSource, TDestination>
{
    TDestination Convert(TSource source);
    IEnumerable<TDestination> Convert(IEnumerable<TSource> source);
}
