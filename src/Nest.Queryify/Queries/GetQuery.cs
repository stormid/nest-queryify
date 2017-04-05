using System;
using System.Threading.Tasks;
using Nest.Queryify.Abstractions.Queries;

namespace Nest.Queryify.Queries
{
    public abstract class GetQuery<T> : ElasticClientQueryObject<IGetResponse<T>> where T : class
    {
        internal const string ObsoleteMessage = "Use alternate constructor accepting DocumentPath";
        private readonly DocumentPath<T> documentPath;

        [Obsolete(ObsoleteMessage)]
        protected GetQuery(string id)
        {
            documentPath = DocumentPath<T>.Id(id);
        }

        protected GetQuery(DocumentPath<T> documentPath)
        {
            this.documentPath = documentPath;
        }

        protected override IGetResponse<T> ExecuteCore(IElasticClient client, string index)
        {
            return client.Get(documentPath, desc => BuildQuery(desc).Index(index));
        }

        protected override Task<IGetResponse<T>> ExecuteCoreAsync(IElasticClient client, string index)
        {
            return client.GetAsync(documentPath, desc => BuildQuery(desc).Index(index));
        }

        protected abstract GetDescriptor<T> BuildQuery(GetDescriptor<T> descriptor);
    }
}