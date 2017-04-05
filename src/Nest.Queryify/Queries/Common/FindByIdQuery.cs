using System.Threading.Tasks;
using Nest.Queryify.Abstractions.Queries;

namespace Nest.Queryify.Queries.Common
{
    public class FindByIdQuery<T> : ElasticClientQueryObject<T> where T : class
    {
        private readonly string _id;

        public FindByIdQuery(string id)
        {
            _id = id;
        }

        protected override T ExecuteCore(IElasticClient client, string index)
        {
            return client.Get(DocumentPath<T>.Id(_id), desc => desc.Index(index)).Source;
        }

        protected override Task<T> ExecuteCoreAsync(IElasticClient client, string index)
        {
            return client.GetAsync(DocumentPath<T>.Id(_id), desc => desc.Index(index)).ContinueWith(t => t.Result.Source);
        }
    }
}