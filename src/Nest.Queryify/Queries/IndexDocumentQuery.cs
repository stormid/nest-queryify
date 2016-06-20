using System.Threading.Tasks;
using Nest.Queryify.Abstractions.Queries;

namespace Nest.Queryify.Queries
{
	/// <summary>
	/// Index a document
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class IndexDocumentQuery<T> : ElasticClientQueryObject<IIndexResponse> where T : class
	{
		private readonly T _document;
		private readonly bool _refreshOnSave;

		/// <summary>
		/// Creates a query for a given document
		/// </summary>
		/// <param name="document"></param>
		/// <param name="refreshOnSave">should the index be refreshed after indexing completes</param>
		public IndexDocumentQuery(T document, bool refreshOnSave = false)
		{
			_document = document;
			_refreshOnSave = refreshOnSave;
		}

	    /// <summary>
	    /// Implement and write your operation
	    /// </summary>
	    /// <param name="client">The elastic client</param>
	    /// <param name="index">The index to operate on</param>
	    /// <returns></returns>
	    protected override IIndexResponse ExecuteCore(IElasticClient client, string index)
		{
			return client.Index(_document, desc => BuildQueryCore(desc, _refreshOnSave).Index(index));
		}

	    /// <summary>
	    /// Implement and write your operation
	    /// </summary>
	    /// <param name="client">The elastic client</param>
	    /// <param name="index">The index to operate on</param>
	    /// <returns></returns>
	    protected override Task<IIndexResponse> ExecuteCoreAsync(IElasticClient client, string index)
	    {
            return client.IndexAsync(_document, desc => BuildQueryCore(desc, _refreshOnSave).Index(index));
        }

	    /// <summary>
	    /// 
	    /// </summary>
	    /// <param name="descriptor"></param>
	    /// <param name="refreshOnSave"></param>
	    /// <returns></returns>
	    protected virtual IndexDescriptor<T> BuildQueryCore(IndexDescriptor<T> descriptor, bool refreshOnSave)
		{
			descriptor = descriptor
				.Type<T>()
				.Refresh(refreshOnSave);
			return BuildQuery(descriptor);
		}

		/// <summary>
		/// Query implementation
		/// </summary>
		/// <param name="descriptor"></param>
		/// <returns></returns>
		protected virtual IndexDescriptor<T> BuildQuery(IndexDescriptor<T> descriptor)
		{
			return descriptor;
		}
	}
}