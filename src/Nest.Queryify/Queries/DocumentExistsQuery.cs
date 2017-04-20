using System.Threading.Tasks;
using Nest.Queryify.Abstractions.Queries;

namespace Nest.Queryify.Queries
{
	public class DocumentExistsQuery<T> : ElasticClientQueryObject<IExistsResponse> where T : class
	{
		private readonly T _document;

		public DocumentExistsQuery(T document)
		{
			_document = document;
		}

		protected override IExistsResponse ExecuteCore(IElasticClient client, string index)
		{
		    return client.DocumentExists<T>(DocumentPath<T>.Id(_document), desc => BuildQueryCore(desc).Index(index));
        }

	    protected override Task<IExistsResponse> ExecuteCoreAsync(IElasticClient client, string index)
	    {
	        return client.DocumentExistsAsync<T>(DocumentPath<T>.Id(_document), desc => BuildQueryCore(desc).Index(index));
        }

        protected virtual DocumentExistsDescriptor<T> BuildQueryCore(DocumentExistsDescriptor<T> descriptor)
		{
			return BuildQuery(descriptor);
		}

		protected virtual DocumentExistsDescriptor<T> BuildQuery(DocumentExistsDescriptor<T> descriptor)
		{
			return descriptor;
		}
	}
}