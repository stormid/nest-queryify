# Queries

A query is a unit of logic that will execute against an Elasticsearch cluster, a query could be to return a single document or search an entire index.

All queries within Nest-Queryify are derived from a base ```ElasticClientQueryObject```, this is the lowest level query, all other queries build on this.