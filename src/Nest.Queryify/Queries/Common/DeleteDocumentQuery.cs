using System.Threading.Tasks;
using Nest.Queryify.Abstractions.Queries;

namespace Nest.Queryify.Queries.Common
{
	/// <summary>
	/// Removes a document from an index
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class DeleteDocumentQuery<T> : ElasticClientQueryObject<IDeleteResponse> where T : class
	{
		private readonly T _document;
	    private readonly bool _refreshOnDelete;

	    /// <summary>
	    /// Removes given document from index if it exists
	    /// </summary>
	    /// <param name="document"></param>
	    /// <param name="refreshOnDelete"></param>
	    public DeleteDocumentQuery(T document, bool refreshOnDelete = false)
	    {
	        _document = document;
	        _refreshOnDelete = refreshOnDelete;
	    }

	    /// <summary>
	    /// Implement and write your operation
	    /// </summary>
	    /// <param name="client">The elastic client</param>
	    /// <param name="index">The index to operate on</param>
	    /// <returns></returns>
	    protected override IDeleteResponse ExecuteCore(IElasticClient client, string index)
		{
			return client.Delete<T>(descriptor => BuildQueryCore(descriptor).Index(index));
		}

	    /// <summary>
	    /// Implement and write your operation
	    /// </summary>
	    /// <param name="client">The elastic client</param>
	    /// <param name="index">The index to operate on</param>
	    /// <returns></returns>
	    protected override Task<IDeleteResponse> ExecuteCoreAsync(IElasticClient client, string index)
	    {
            return client.DeleteAsync<T>(descriptor => BuildQueryCore(descriptor).Index(index));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="descriptor"></param>
        /// <returns></returns>
        protected virtual DeleteDescriptor<T> BuildQueryCore(DeleteDescriptor<T> descriptor)
        {
            descriptor = descriptor
                .IdFrom(_document)
                .Refresh(_refreshOnDelete);
            return BuildQuery(descriptor);
        }

        /// <summary>
        /// Queryt implementation
        /// </summary>
        /// <param name="descriptor"></param>
        /// <returns></returns>
        protected virtual DeleteDescriptor<T> BuildQuery(DeleteDescriptor<T> descriptor)
        {
            return descriptor;
        }
    }
}