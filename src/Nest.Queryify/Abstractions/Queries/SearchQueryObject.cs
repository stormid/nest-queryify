namespace Nest.Queryify.Abstractions.Queries
{
	public abstract class SearchQueryObject<T, TReturnAs> : ElasticClientQueryObject<ISearchResponse<TReturnAs>>
		where T : class where TReturnAs : class
	{
	}
}