using System.Threading.Tasks;
using Nest.Queryify.Abstractions.Queries;

namespace Nest.Queryify.Queries.Common
{
    /// <summary>
    /// Removes a document with a given id
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DeleteByIdQuery<T> : ElasticClientQueryObject<IDeleteResponse> where T : class
    {
        private readonly string _id;
        private readonly bool _refreshOnDelete;

        /// <summary>
        /// Removes a document with the specified id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="refreshOnDelete"></param>
        public DeleteByIdQuery(string id, bool refreshOnDelete = false)
        {
            _id = id;
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
                .Id(_id)
                .Refresh(_refreshOnDelete);
            return BuildQuery(descriptor);
        }

        /// <summary>
        /// Query implementation
        /// </summary>
        /// <param name="descriptor"></param>
        /// <returns></returns>
        protected virtual DeleteDescriptor<T> BuildQuery(DeleteDescriptor<T> descriptor)
        {
            return descriptor;
        }
    }
}