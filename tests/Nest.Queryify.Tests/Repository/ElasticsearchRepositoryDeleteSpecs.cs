using Elasticsearch.Net.Connection;
using FluentAssertions;
using Nest.Queryify.Abstractions;
using Nest.Queryify.Tests.Extensions;
using Nest.Queryify.Tests.Queries.Fixtures;
using Nest.Queryify.Tests.TestData;
using Xunit;

namespace Nest.Queryify.Tests.Repository
{
    public class ElasticsearchRepositoryDeleteSpecs
    {
        private readonly IElasticsearchRepository _repository;
        private IElasticClient _client;

        public ElasticsearchRepositoryDeleteSpecs()
        {
            var settings = new ConnectionSettings();

            var output =
                "Nest.Queryify.Tests.TestData.ValidDeleteResponse.json".ReadAsStringFromEmbeddedResource<ElasticClientQueryObjectTestFixture>();
            _client = new ElasticClient(settings, new InMemoryConnection(settings, output));
            _repository = new ElasticsearchRepository(_client);
        }

        [Fact]
        public void ShouldRun()
        {
            var response = _repository.Delete<Person>("1");

            response.Found.Should().BeTrue();
        }
    }
}