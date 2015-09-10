using System.Threading.Tasks;
using Nest.Queryify.Abstractions.Queries;

namespace Nest.Queryify.Queries
{
	public abstract class MoreLikeThisDescriptorQuery<T> : SearchQueryObject<T, T>
		where T : class
	{
		protected override ISearchResponse<T> ExecuteCore(IElasticClient client, string index)
		{
			return client.MoreLikeThis<T>(desc => BuildQuery(desc).Index(index));
		}

	    protected override Task<ISearchResponse<T>> ExecuteCoreAsync(IElasticClient client, string index)
	    {
            return client.MoreLikeThisAsync<T>(desc => BuildQuery(desc).Index(index));
        }

	    protected abstract MoreLikeThisDescriptor<T> BuildQuery(MoreLikeThisDescriptor<T> descriptor);
	}
}