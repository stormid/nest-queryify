using System.Threading.Tasks;

namespace Nest.Queryify.Abstractions.Queries
{
	public abstract class SearchDescriptorQueryObject<T> : SearchQueryObject<T, T>
		where T : class
	{
		protected override ISearchResponse<T> ExecuteCore(IElasticClient client, string index)
		{
			return client.Search<T, T>(desc => BuildQuery(desc).Index(index));
		}

	    protected override Task<ISearchResponse<T>> ExecuteCoreAsync(IElasticClient client, string index)
	    {
            return client.SearchAsync<T, T>(desc => BuildQuery(desc).Index(index));
        }

	    protected abstract SearchDescriptor<T> BuildQuery(SearchDescriptor<T> descriptor);
	}

	public abstract class SearchDescriptorQueryObject<T, TReturnAs> : SearchQueryObject<T, TReturnAs>
		where T : class
		where TReturnAs : class
    {
        protected override ISearchResponse<TReturnAs> ExecuteCore(IElasticClient client, string index)
        {
            return client.Search<T, TReturnAs>(desc => BuildQuery(desc).Index(index));
        }

	    protected override Task<ISearchResponse<TReturnAs>> ExecuteCoreAsync(IElasticClient client, string index)
	    {
            return client.SearchAsync<T, TReturnAs>(desc => BuildQuery(desc).Index(index));
        }

	    protected abstract SearchDescriptor<T> BuildQuery(SearchDescriptor<T> descriptor);
    }
}