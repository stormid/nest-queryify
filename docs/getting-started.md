# Getting started with Nest-Queryify

## Using Nest-Queryify
Once [installed](installation.md) you will be able to create "queries" to execute against your Elasticsearch cluster.

### Pre-defined queries
There are a number of pre-defined queries you can use for common queries, such as ```GetByIdQuery``` or ```DeleteByIdQuery```.  Each of these queries are ready to use and will perform a specific task when executed.  See the [query reference list](queries/index.md) for a list of available pre-defined queries.

### Custom queries
If there isnt a pre-defined query that suits your needs you can extend the underlying queries with your own logic.  To do this you should try and use one of the available abstract queries available to you, doing this will give you the cleanest access to the specific Nest descriptor needed for your task.  We will discuss custom queries in more detail within the [Writing Custom Queries](queries/writing-custom-queries.md).

Depending on your knowledge of Nest and your experience with Elasticsearch you can execute your queries in one (or both) of the following ways.

### Executing your query via IElasticClient
Good those with past experience of Nest, or those that want to use queries as well as other core features of the Nest library directly.  See [Querying via ElasticClient](using-elasticclient).

### Executing your query via ElasticsearchRepository
Great for those new to Nest and/or Elasticsearch.  See [Using the ElasticsearchRepository](using-elasticsearchrepository).
