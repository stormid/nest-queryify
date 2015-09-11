using Elasticsearch.Net.Connection;
using FluentAssertions;
using Nest.Queryify.Abstractions;
using Nest.Queryify.Tests.TestData;
using Xunit;

namespace Nest.Queryify.Tests.Repository
{
    public class ElasticsearchRepositoryDocumentNotExistsSpecs
    {
        private readonly IElasticsearchRepository _repository;
        private IElasticClient _client;

        public ElasticsearchRepositoryDocumentNotExistsSpecs()
        {
            var settings = new ConnectionSettings();

            _client = new ElasticClient(settings, new InMemoryConnection(settings, string.Empty, 404));
            _repository = new ElasticsearchRepository(_client);
        }

        [Fact]
        public void ShouldRun()
        {
            var result = _repository.Exists<Person>("1");

            result.Should().BeFalse();
        }
    }
}