using System.Collections.Generic;
using System.Threading.Tasks;
using Nest.Queryify.Abstractions.Queries;

namespace Nest.Queryify.Queries
{
    /// <summary>
    /// The multi search API allows to execute several search requests within the same API. The endpoint for it is _msearch.
    /// https://www.elastic.co/guide/en/elasticsearch/reference/1.7/search-multi-search.html
    /// </summary>
    public abstract class MultiSearchQuery : ElasticClientQueryObject<IMultiSearchResponse>
    {
        /// <summary>
        /// Implement and write your operation
        /// </summary>
        /// <param name="client">The elastic client</param>
        /// <param name="index">The index to operate on</param>
        /// <returns></returns>
        protected override IMultiSearchResponse ExecuteCore(IElasticClient client, string index)
        {
			return client.MultiSearch(desc => BuildQueryCore(desc, index));
        }

        /// <summary>
        /// Implement and write your operation
        /// </summary>
        /// <param name="client">The elastic client</param>
        /// <param name="index">The index to operate on</param>
        /// <returns>Task of response</returns>
        protected override Task<IMultiSearchResponse> ExecuteCoreAsync(IElasticClient client, string index)
        {
            return client.MultiSearchAsync(desc => BuildQueryCore(desc, index));
        }

        /// <summary>
        /// Wraps the BuildQuery virtual method and its derived implementation
        /// </summary>
        /// <param name="descriptor">Search descriptor</param>
        /// <param name="index">inde on which to operate</param>
        /// <returns>descriptor</returns>
        protected MultiSearchDescriptor BuildQueryCore(MultiSearchDescriptor descriptor, string index)
        {
            descriptor = BuildQuery(descriptor, index);
            return descriptor;
        }

        /// <summary>
        /// Query implementation
        /// </summary>
        /// <param name="descriptor">Search descriptor</param>
        /// <param name="index">index to operate on</param>
        /// <returns>descriptor</returns>
        protected abstract MultiSearchDescriptor BuildQuery(MultiSearchDescriptor descriptor, string index);
    }
}