using System.Threading.Tasks;
using Nest.Queryify.Abstractions.Queries;

namespace Nest.Queryify.Queries
{
	public class DocumentExistsByIdQuery<T> : ElasticClientQueryObject<IExistsResponse> where T : class
	{
		private readonly string _id;

		public DocumentExistsByIdQuery(string id)
		{
			_id = id;
		}

		protected override IExistsResponse ExecuteCore(IElasticClient client, string index)
		{
			return client.DocumentExists<T>(desc => BuildQueryCore(desc).Index(index));
		}

	    protected override Task<IExistsResponse> ExecuteCoreAsync(IElasticClient client, string index)
	    {
            return client.DocumentExistsAsync<T>(desc => BuildQueryCore(desc).Index(index));
        }

	    protected virtual DocumentExistsDescriptor<T> BuildQueryCore(DocumentExistsDescriptor<T> descriptor)
		{
			descriptor = descriptor.Id(_id);
			return BuildQuery(descriptor);
		}

		protected virtual DocumentExistsDescriptor<T> BuildQuery(DocumentExistsDescriptor<T> descriptor)
		{
			return descriptor;
		}
	}
}