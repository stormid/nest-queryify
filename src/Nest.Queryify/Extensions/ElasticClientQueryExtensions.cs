using Nest.Queryify.Abstractions.Queries;

namespace Nest.Queryify.Extensions
{
    /// <summary>
    /// Client extensions methods
    /// </summary>
    public static class ElasticClientQueryExtensions
    {
        /// <summary>
        /// Execute a given query object against the default or given index and return the response defined by the query
        /// </summary>
        /// <param name="client">Elastic client</param>
        /// <param name="query">query object</param>
        /// <param name="index">(optional) index to operate on</param>
        /// <typeparam name="TResponse"></typeparam>
        /// <returns></returns>
        public static TResponse Query<TResponse>(this IElasticClient client, IElasticClientQueryObject<TResponse> query, string index = null) where TResponse : class
        {
            return query.Execute(client, index);
        }
    }
}
