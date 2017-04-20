namespace Nest.Queryify.Extensions
{
    internal static class ElasticClientExtensions
    {
        internal static string GetDefaultIndex(this IElasticClient client)
        {
            return client.ConnectionSettings.DefaultIndex;
        }
    }
}