using System.Threading.Tasks;
using Nest.Queryify.Abstractions.Queries;

namespace Nest.Queryify.Queries
{
    /// <summary>
    /// Base Get query
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class GetQuery<T> : ElasticClientQueryObject<IGetResponse<T>> where T : class
    {
        /// <summary>
        /// Implement and write your operation
        /// </summary>
        /// <param name="client">The elastic client</param>
        /// <param name="index">The index to operate on</param>
        /// <returns></returns>
        protected override IGetResponse<T> ExecuteCore(IElasticClient client, string index)
        {
            return client.Get<T>(desc => BuildQuery(desc).Index(index));
        }

        /// <summary>
        /// Implement and write your operation
        /// </summary>
        /// <param name="client">The elastic client</param>
        /// <param name="index">The index to operate on</param>
        /// <returns></returns>
        protected override Task<IGetResponse<T>> ExecuteCoreAsync(IElasticClient client, string index)
        {
            return client.GetAsync<T>(desc => BuildQuery(desc).Index(index));
        }

        /// <summary>
        /// Implement to build query
        /// </summary>
        /// <param name="descriptor"></param>
        /// <returns></returns>
        protected abstract GetDescriptor<T> BuildQuery(GetDescriptor<T> descriptor);
    }
}