using System.Threading.Tasks;
using Nest.Queryify.Abstractions.Queries;

namespace Nest.Queryify.Queries
{
    /// <summary>
    /// The More Like This Query (MLT Query) finds documents that are "like" a given set of documents.
    /// https://www.elastic.co/guide/en/elasticsearch/reference/1.7/query-dsl-mlt-query.html
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class MoreLikeThisQuery<T> : SearchQueryObject<T>
		where T : class
	{
	    /// <summary>
	    /// Implement and write your operation
	    /// </summary>
	    /// <param name="client">The elastic client</param>
	    /// <param name="index">The index to operate on</param>
	    /// <returns></returns>
	    protected override ISearchResponse<T> ExecuteCore(IElasticClient client, string index)
		{
			return client.MoreLikeThis<T>(desc => BuildQuery(desc).Index(index));
		}

	    /// <summary>
	    /// Implement and write your operation
	    /// </summary>
	    /// <param name="client">The elastic client</param>
	    /// <param name="index">The index to operate on</param>
	    /// <returns></returns>
	    protected override Task<ISearchResponse<T>> ExecuteCoreAsync(IElasticClient client, string index)
	    {
            return client.MoreLikeThisAsync<T>(desc => BuildQuery(desc).Index(index));
        }

        /// <summary>
        /// Query implementation
        /// </summary>
        /// <param name="descriptor">MLT descriptor</param>
        /// <returns>descriptor</returns>
        protected abstract MoreLikeThisDescriptor<T> BuildQuery(MoreLikeThisDescriptor<T> descriptor);
	}
}