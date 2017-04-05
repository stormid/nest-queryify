using System.Threading.Tasks;
using Nest.Queryify.Abstractions.Queries;
using Nest.Queryify.Extensions;

namespace Nest.Queryify.Queries.Common
{
	public class DeleteDocumentQuery<T> : ElasticClientQueryObject<IDeleteResponse> where T : class
	{
		private readonly T _document;
	    private readonly bool _refreshOnDelete;

	    public DeleteDocumentQuery(T document, bool refreshOnDelete = false)
	    {
	        _document = document;
	        _refreshOnDelete = refreshOnDelete;
	    }

	    protected override IDeleteResponse ExecuteCore(IElasticClient client, string index)
	    {
            var doc = DocumentPath<T>.Id(_document);
            return client.Delete(doc, descriptor => BuildQueryCore(descriptor).Index(index));
		}

	    protected override Task<IDeleteResponse> ExecuteCoreAsync(IElasticClient client, string index)
	    {
            var doc = DocumentPath<T>.Id(_document);
            return client.DeleteAsync(doc, descriptor => BuildQueryCore(descriptor).Index(index));
        }

        protected virtual DeleteDescriptor<T> BuildQueryCore(DeleteDescriptor<T> descriptor)
        {
            descriptor = descriptor
                .Refresh(_refreshOnDelete);
            return BuildQuery(descriptor);
        }

        protected virtual DeleteDescriptor<T> BuildQuery(DeleteDescriptor<T> descriptor)
        {
            return descriptor;
        }
    }
}