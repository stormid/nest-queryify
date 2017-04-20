# Querying via ElasticClient

If you have previous experience using Nest or have an existing application using Nest you can introduce Nest-Queryify very easily via a single ```Query``` extension method added to ```IElasticClient```.

## Basic Example

```
var singleNode = new SingleNodeConnectionPool(new Uri("http://localhost:9200"));
var settings = new ConnectionSettings(node);
settings.DefaultIndex("my-application");
IElasticClient client = new ElasticClient(settings);

var query = new GetByIdQuery(DocumentPath.Id<MyDocument>("my-identifier"));
IGetResponse<MyDocument> queryResponse = client.Query(query);
var document = queryResponse.Source;
// use your document
```