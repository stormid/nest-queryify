using Nest.Queryify.Abstractions.Queries;

namespace Nest.Queryify.Extensions
{
    public static class ElasticClientQueryExtensions
    {
        public static TResponse Query<TResponse>(this IElasticClient client, ElasticClientQueryObject<TResponse> query, string index = null) where TResponse : class
        {
            return query.Execute(client, index);
        }
    }
}
