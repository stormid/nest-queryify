using System.Threading.Tasks;
using Nest.Queryify.Abstractions.Queries;

namespace Nest.Queryify.Queries.Common
{
    public class DeleteByIdQueryObject<T> : ElasticClientQueryObject<IDeleteResponse> where T : class
    {
        private readonly string _id;

        public DeleteByIdQueryObject(string id)
        {
            _id = id;
        }

        protected override IDeleteResponse ExecuteCore(IElasticClient client, string index)
        {
            return client.Delete<T>(descriptor => descriptor.Id(_id).Index(index));
        }

        protected override Task<IDeleteResponse> ExecuteCoreAsync(IElasticClient client, string index)
        {
            return client.DeleteAsync<T>(descriptor => descriptor.Id(_id).Index(index));
        }
    }
}