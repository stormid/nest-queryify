using System.Threading.Tasks;

namespace Nest.Queryify.Abstractions.Queries
{
	/// <summary>
	/// Base search descriptor query object
	/// </summary>
	/// <typeparam name="TDocument"></typeparam>
	public abstract class SearchDescriptorQuery<TDocument> : SearchQueryObject<TDocument>
		where TDocument : class
	{
	    /// <summary>
	    /// Implement and write your operation
	    /// </summary>
	    /// <param name="client">The elastic client</param>
	    /// <param name="index">The index to operate on</param>
	    /// <returns></returns>
	    protected override ISearchResponse<TDocument> ExecuteCore(IElasticClient client, string index)
		{
			return client.Search<TDocument, TDocument>(desc => BuildQuery(desc).Index(index));
		}

	    /// <summary>
	    /// Implement and write your operation
	    /// </summary>
	    /// <param name="client">The elastic client</param>
	    /// <param name="index">The index to operate on</param>
	    /// <returns></returns>
	    protected override Task<ISearchResponse<TDocument>> ExecuteCoreAsync(IElasticClient client, string index)
	    {
            return client.SearchAsync<TDocument, TDocument>(desc => BuildQuery(desc).Index(index));
        }

	    /// <summary>
	    /// Query implementation
	    /// </summary>
	    /// <param name="descriptor"></param>
	    /// <returns></returns>
	    protected abstract SearchDescriptor<TDocument> BuildQuery(SearchDescriptor<TDocument> descriptor);
	}

    /// <summary>
    /// Base search descriptor query object with alternate return document
    /// </summary>
    /// <typeparam name="TDocument"></typeparam>
    /// <typeparam name="TReturnDocument"></typeparam>
    public abstract class SearchDescriptorQuery<TDocument, TReturnDocument> : SearchQueryObject<TReturnDocument>
		where TDocument : class
		where TReturnDocument : class
    {
	    /// <summary>
	    /// Implement and write your operation
	    /// </summary>
	    /// <param name="client">The elastic client</param>
	    /// <param name="index">The index to operate on</param>
	    /// <returns></returns>
	    protected override ISearchResponse<TReturnDocument> ExecuteCore(IElasticClient client, string index)
        {
            return client.Search<TDocument, TReturnDocument>(desc => BuildQuery(desc).Index(index));
        }

	    /// <summary>
	    /// Implement and write your operation
	    /// </summary>
	    /// <param name="client">The elastic client</param>
	    /// <param name="index">The index to operate on</param>
	    /// <returns></returns>
	    protected override Task<ISearchResponse<TReturnDocument>> ExecuteCoreAsync(IElasticClient client, string index)
	    {
            return client.SearchAsync<TDocument, TReturnDocument>(desc => BuildQuery(desc).Index(index));
        }

	    /// <summary>
	    /// Query implementation
	    /// </summary>
	    /// <param name="descriptor"></param>
	    /// <returns></returns>
	    protected abstract SearchDescriptor<TDocument> BuildQuery(SearchDescriptor<TDocument> descriptor);
    }
}