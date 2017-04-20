# Using the ElasticsearchRepository

For those new to Nest, or even new to Elasticsearch itself you can use the ```ElasticsearchRepository``` to not only provide an entrypoint into executing queries, but also as an abstraction over the ```ElasticClient``` itself.  Unsurprisingly this class follows the repository pattern to give you a simple interface to interaction with Elasticsearch that will feel more closely associated with a typical data store.

## Basic Example

```
var singleNode = new SingleNodeConnectionPool(new Uri("http://localhost:9200"));
var settings = new ConnectionSettings(node);
settings.DefaultIndex("my-application");
IElasticClient client = new ElasticClient(settings);

IElasticsearchRepository repository = new ElasticsearchRepository(client);

var query = new GetByIdQuery(DocumentPath.Id<MyDocument>("my-identifier"));
IGetResponse<MyDocument> queryResponse = repository.Query(query);
var document = queryResponse.Source;

// or

var queryResponse = repository.GetById(DocumentPath.Id<MyDocument>("my-identifier"));
var document = queryResponse.Source;

// or

var document = repository.FindById(DocumentPath.Id<MyDocument>("my-identifier"));

// use your document
```