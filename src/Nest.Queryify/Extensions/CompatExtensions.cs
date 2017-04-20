namespace Nest.Queryify.Extensions
{
    internal static class CompatExtensions
    {
        internal static IndexDescriptor<T> Refresh<T>(this IndexDescriptor<T> descriptor, bool refresh = true) where T : class
        {
            return descriptor.Refresh(refresh ? Elasticsearch.Net.Refresh.True : Elasticsearch.Net.Refresh.False);
        }

        internal static BulkDescriptor Refresh(this BulkDescriptor descriptor, bool refresh = true)
        {
            return descriptor.Refresh(refresh ? Elasticsearch.Net.Refresh.True : Elasticsearch.Net.Refresh.False);
        }

        internal static DeleteDescriptor<T> Refresh<T>(this DeleteDescriptor<T> descriptor, bool refresh = true) where T : class
        {
            return descriptor.Refresh(refresh ? Elasticsearch.Net.Refresh.True : Elasticsearch.Net.Refresh.False);
        }
    }
}