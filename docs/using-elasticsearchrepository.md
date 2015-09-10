# Using the ElasticsearchRepository

For those new to Nest, or even new to Elasticsearch itself you can use the ```ElasticsearchRepository``` to not only provide an entrypoint into executing queries, but also as an abstraction over the ```ElasticClient``` itself.  Unsurprisingly this class follows the repository pattern to give you a simple interface to interaction with Elasticsearch that will feel more closely associated with a typical data store.

## Basic Example

```
var uri = new Uri("http://localhost:9200");
var defaultIndex = "my-application";
IElasticsearchRepository repository = new ElasticsearchRepository(uri, defaultIndex);

var query = new GetByIdQuery<MyDocument>("my-identifier");
IGetResponse<MyDocument> queryResponse = repository.Query(query);
var document = queryResponse.Source;
// use your document
```