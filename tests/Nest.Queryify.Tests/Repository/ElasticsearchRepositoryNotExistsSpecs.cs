using System.Threading.Tasks;
using Elasticsearch.Net.Connection;
using FluentAssertions;
using Nest.Queryify.Abstractions;
using Nest.Queryify.Tests.TestData;
using Xunit;

namespace Nest.Queryify.Tests.Repository
{
    public class ElasticsearchRepositoryNotExistsSpecs
    {
        private readonly IElasticsearchRepository _repository;
        private IElasticClient _client;

        public ElasticsearchRepositoryNotExistsSpecs()
        {
            var settings = new ConnectionSettings();

            _client = new ElasticClient(settings, new InMemoryConnection(settings, string.Empty, 404));
            _repository = new ElasticsearchRepository(_client);
        }

        [Fact]
        public void ShouldRun()
        {
            var result = _repository.Exists(new Person() { Id = 1 });

            result.Should().BeFalse();
        }

        [Fact]
        public async Task ShouldRunAsync()
        {
            var result = await _repository.ExistsAsync(new Person() { Id = 1 });

            result.Should().BeFalse();
        }
    }
}