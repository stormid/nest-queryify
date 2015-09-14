using System.Threading.Tasks;

namespace Nest.Queryify.Abstractions.Queries
{
	public abstract class SearchDescriptorQuery<TDocument> : SearchQueryObject<TDocument>
		where TDocument : class
	{
		protected override ISearchResponse<TDocument> ExecuteCore(IElasticClient client, string index)
		{
			return client.Search<TDocument, TDocument>(desc => BuildQuery(desc).Index(index));
		}

	    protected override Task<ISearchResponse<TDocument>> ExecuteCoreAsync(IElasticClient client, string index)
	    {
            return client.SearchAsync<TDocument, TDocument>(desc => BuildQuery(desc).Index(index));
        }

	    protected abstract SearchDescriptor<TDocument> BuildQuery(SearchDescriptor<TDocument> descriptor);
	}

	public abstract class SearchDescriptorQuery<TDocument, TReturnDocument> : SearchQueryObject<TReturnDocument>
		where TDocument : class
		where TReturnDocument : class
    {
        protected override ISearchResponse<TReturnDocument> ExecuteCore(IElasticClient client, string index)
        {
            return client.Search<TDocument, TReturnDocument>(desc => BuildQuery(desc).Index(index));
        }

	    protected override Task<ISearchResponse<TReturnDocument>> ExecuteCoreAsync(IElasticClient client, string index)
	    {
            return client.SearchAsync<TDocument, TReturnDocument>(desc => BuildQuery(desc).Index(index));
        }

	    protected abstract SearchDescriptor<TDocument> BuildQuery(SearchDescriptor<TDocument> descriptor);
    }
}