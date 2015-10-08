# Writing custom queries

This is where the real power of the query object pattern is shown, you are free to derive your own queries from the available base queries or from the ```ElasticClientQueryObject```, your query will represent a unit of logic executed against your Elasticsearch cluster.

For example, you could create your own query to perform a complex search within an index, the complexity of the query is contained within the query object.

The example below shows how you can build a query to execute a search and return the response.  NB: there are pre-defined search query implementations within Nest-Queryify so you would never _need_ to drop to this level to perform a search (see [Search queries](search-queries/index.md)).

```
public class MySearchQuery : ElasticClientQueryObject<ISearchResponse<MyDocument>> {
	protected override TResponse ExecuteCore(IElasticClient client, string index) {
		return client.Search<MyDocument>(desc => desc.MatchAll().Index(index));
	}

	protected override Task<TResponse> ExecuteCoreAsync(IElasticClient client, string index) {
		return client.SearchAsync<MyDocument>(desc => desc.MatchAll().Index(index));
	}
}
```