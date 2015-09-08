namespace Nest.Queryify.Abstractions.Queries
{
	public abstract class SearchQueryObject<TDocument, TReturnDocument> : ElasticClientQueryObject<ISearchResponse<TReturnDocument>>
		where TDocument : class 
        where TReturnDocument : class
	{
	}
}