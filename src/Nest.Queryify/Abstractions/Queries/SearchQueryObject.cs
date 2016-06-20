namespace Nest.Queryify.Abstractions.Queries
{
	/// <summary>
	/// Base search query object
	/// </summary>
	/// <typeparam name="TDocument"></typeparam>
	public abstract class SearchQueryObject<TDocument> : ElasticClientQueryObject<ISearchResponse<TDocument>> where TDocument : class
	{
	}
}