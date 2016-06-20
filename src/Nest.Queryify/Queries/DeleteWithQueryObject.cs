using System.Threading.Tasks;
using Nest.Queryify.Abstractions.Queries;

namespace Nest.Queryify.Queries
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class DeleteWithQueryObject<T> : ElasticClientQueryObject<IDeleteResponse> where T : class
    {
        /// <summary>
        /// Implement and write your operation
        /// </summary>
        /// <param name="client">The elastic client</param>
        /// <param name="index">The index to operate on</param>
        /// <returns></returns>
        protected override IDeleteResponse ExecuteCore(IElasticClient client, string index)
        {
            return client.DeleteByQuery<T>(desc => BuildQuery(desc).Index(index));
        }

        /// <summary>
        /// Implement and write your operation
        /// </summary>
        /// <param name="client">The elastic client</param>
        /// <param name="index">The index to operate on</param>
        /// <returns></returns>
        protected override Task<IDeleteResponse> ExecuteCoreAsync(IElasticClient client, string index)
        {
            return client.DeleteByQueryAsync<T>(desc => BuildQuery(desc).Index(index));
        }

        /// <summary>
        /// Query implementation
        /// </summary>
        /// <param name="descriptor"></param>
        /// <returns></returns>
        protected abstract DeleteByQueryDescriptor<T> BuildQuery(DeleteByQueryDescriptor<T> descriptor);
    }
}