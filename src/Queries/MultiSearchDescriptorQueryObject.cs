using System.Threading.Tasks;
using Nest.Queryify.Abstractions.Queries;

namespace Nest.Queryify.Queries
{
    public abstract class MultiSearchDescriptorQueryObject : ElasticClientQueryObject<IMultiSearchResponse>
    {
        protected override IMultiSearchResponse ExecuteCore(IElasticClient client, string index)
        {
			return client.MultiSearch(desc => BuildQueries(desc, index));
        }

        protected override Task<IMultiSearchResponse> ExecuteCoreAsync(IElasticClient client, string index)
        {
            return client.MultiSearchAsync(desc => BuildQueries(desc, index));
        }

        protected abstract MultiSearchDescriptor BuildQueries(MultiSearchDescriptor descriptor, string index);
    }
}