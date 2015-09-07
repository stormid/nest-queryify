using System.Threading.Tasks;
using Nest.Queryify.Abstractions.Queries;

namespace Nest.Queryify.Queries
{
    public abstract class DeleteWithQueryObject<T> : ElasticClientQueryObject<IDeleteResponse> where T : class
    {
        protected override IDeleteResponse ExecuteCore(IElasticClient client, string index)
        {
            return client.DeleteByQuery<T>(desc => BuildQuery(desc).Index(index));
        }

        protected override Task<IDeleteResponse> ExecuteCoreAsync(IElasticClient client, string index)
        {
            return client.DeleteByQueryAsync<T>(desc => BuildQuery(desc).Index(index));
        }

        protected abstract DeleteByQueryDescriptor<T> BuildQuery(DeleteByQueryDescriptor<T> descriptor);
    }
}