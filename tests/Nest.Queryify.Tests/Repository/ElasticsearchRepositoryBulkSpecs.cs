using System.Linq;
using Elasticsearch.Net.Connection;
using FluentAssertions;
using Nest.Queryify.Abstractions;
using Nest.Queryify.Tests.Extensions;
using Nest.Queryify.Tests.Queries.Fixtures;
using Nest.Queryify.Tests.TestData;
using Xunit;

namespace Nest.Queryify.Tests.Repository
{
    public class ElasticsearchRepositoryBulkSpecs
    {
        private readonly IElasticsearchRepository _repository;
        private readonly IElasticClient _client;

        public ElasticsearchRepositoryBulkSpecs()
        {
            var settings = new ConnectionSettings();
            settings.SetDefaultIndex("test-index");

            var output =
                "Nest.Queryify.Tests.TestData.ValidBulkResponse.json".ReadAsStringFromEmbeddedResource<ElasticClientQueryObjectTestFixture>();
            _client = new ElasticClient(settings, new InMemoryConnection(settings, output));
            _repository = new ElasticsearchRepository(_client);
        }

        [Fact]
        public void ShouldRun()
        {
            var response = _repository.Bulk(new [] {
                new Person() { Id = 1 },
                new Person() { Id = 2 },
                new Person() { Id = 3 }
            });

            response.Took.Should().Be(7);
            response.Items.Count().Should().Be(3);
            response.Infer.DefaultIndex.Should().Be("test-index");
        }
    }
}