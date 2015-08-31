using System.Threading.Tasks;
using Nest.Queryify.Abstractions.Queries;

namespace Nest.Queryify.Queries.Common
{
	public class DeleteDocumentQueryObject<T> : ElasticClientQueryObject<IDeleteResponse> where T : class
	{
		private readonly T _document;

		public DeleteDocumentQueryObject(T document)
		{
			_document = document;
		}

		protected override IDeleteResponse ExecuteCore(IElasticClient client, string index)
		{
			return client.Delete<T>(descriptor => descriptor.IdFrom(_document).Index(index));
		}

	    protected override Task<IDeleteResponse> ExecuteCoreAsync(IElasticClient client, string index)
	    {
            return client.DeleteAsync<T>(descriptor => descriptor.IdFrom(_document).Index(index));
        }
	}
}