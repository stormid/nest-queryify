using System.Threading.Tasks;
using Nest.Queryify.Abstractions.Queries;

namespace Nest.Queryify.Queries
{
    /// <summary>
    /// Checks when a given document exists in the index by issuing a HEAD request for the document and expecting a 200 response
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DocumentExistsByIdQuery<T> : ElasticClientQueryObject<IExistsResponse> where T : class
	{
		private readonly string _id;

        /// <summary>
        /// Check if the given document id exists for type
        /// </summary>
        /// <param name="id"></param>
        public DocumentExistsByIdQuery(string id)
		{
			_id = id;
		}

        /// <summary>
        /// Implement and write your operation
        /// </summary>
        /// <param name="client">The elastic client</param>
        /// <param name="index">The index to operate on</param>
        /// <returns></returns>
        protected override IExistsResponse ExecuteCore(IElasticClient client, string index)
		{
			return client.DocumentExists<T>(desc => BuildQueryCore(desc).Index(index));
		}

        /// <summary>
        /// Implement and write your operation
        /// </summary>
        /// <param name="client">The elastic client</param>
        /// <param name="index">The index to operate on</param>
        /// <returns></returns>
        protected override Task<IExistsResponse> ExecuteCoreAsync(IElasticClient client, string index)
	    {
            return client.DocumentExistsAsync<T>(desc => BuildQueryCore(desc).Index(index));
        }

	    /// <summary>
	    /// 
	    /// </summary>
	    /// <param name="descriptor"></param>
	    /// <returns></returns>
	    protected virtual DocumentExistsDescriptor<T> BuildQueryCore(DocumentExistsDescriptor<T> descriptor)
		{
			descriptor = descriptor.Id(_id);
			return BuildQuery(descriptor);
		}

		/// <summary>
		/// Query implementation
		/// </summary>
		/// <param name="descriptor"></param>
		/// <returns></returns>
		protected virtual DocumentExistsDescriptor<T> BuildQuery(DocumentExistsDescriptor<T> descriptor)
		{
			return descriptor;
		}
	}
}