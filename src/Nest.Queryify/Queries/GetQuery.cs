using System.Threading.Tasks;
using Nest.Queryify.Abstractions.Queries;

namespace Nest.Queryify.Queries
{
    public abstract class GetQuery<T> : ElasticClientQueryObject<IGetResponse<T>> where T : class
    {
        protected override IGetResponse<T> ExecuteCore(IElasticClient client, string index)
        {
            return client.Get<T>(desc => BuildQuery(desc).Index(index));
        }

        protected override Task<IGetResponse<T>> ExecuteCoreAsync(IElasticClient client, string index)
        {
            return client.GetAsync<T>(desc => BuildQuery(desc).Index(index));
        }

        protected abstract GetDescriptor<T> BuildQuery(GetDescriptor<T> descriptor);
    }
}