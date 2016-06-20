using System.Collections.Generic;
using System.Threading.Tasks;
using Nest.Queryify.Abstractions.Queries;

namespace Nest.Queryify.Queries
{
	/// <summary>
	/// 
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class BulkIndexDocumentQuery<T> : ElasticClientQueryObject<IBulkResponse> where T : class
	{
		private readonly IEnumerable<T> _documents;
		private readonly bool _refreshOnSave;

		/// <summary>
		/// Will index a set of documents
		/// </summary>
		/// <param name="documents"></param>
		/// <param name="refreshOnSave">refresh the index on completion</param>
		public BulkIndexDocumentQuery(IEnumerable<T> documents, bool refreshOnSave = false)
		{
			_documents = documents;
			_refreshOnSave = refreshOnSave;
		}

	    /// <summary>
	    /// Implement and write your operation
	    /// </summary>
	    /// <param name="client">The elastic client</param>
	    /// <param name="index">The index to operate on</param>
	    /// <returns></returns>
	    protected override IBulkResponse ExecuteCore(IElasticClient client, string index)
		{
			return client.Bulk(desc => BuildQueryCore(desc, index, _refreshOnSave));
		}

	    /// <summary>
	    /// Implement and write your operation
	    /// </summary>
	    /// <param name="client">The elastic client</param>
	    /// <param name="index">The index to operate on</param>
	    /// <returns></returns>
	    protected override Task<IBulkResponse> ExecuteCoreAsync(IElasticClient client, string index)
	    {
            return client.BulkAsync(desc => BuildQueryCore(desc, index, _refreshOnSave));
        }

	    /// <summary>
	    /// 
	    /// </summary>
	    /// <param name="descriptor"></param>
	    /// <param name="index"></param>
	    /// <param name="refreshOnSave"></param>
	    /// <returns></returns>
	    protected virtual BulkDescriptor BuildQueryCore(BulkDescriptor descriptor,
			string index, bool refreshOnSave)
		{
			descriptor = descriptor
				.IndexMany(_documents, (d, i) => d
                    .Type(i.GetType())
					.Index(index)
				)
				.Refresh(refreshOnSave);
			return BuildQuery(descriptor);
		}

		/// <summary>
		/// Query implementation
		/// </summary>
		/// <param name="descriptor"></param>
		/// <returns></returns>
		protected virtual BulkDescriptor BuildQuery(BulkDescriptor descriptor)
		{
			return descriptor;
		}
	}
}