using System.Threading.Tasks;
using Elasticsearch.Net.Connection;
using FluentAssertions;
using Nest.Queryify.Abstractions;
using Nest.Queryify.Tests.TestData;
using Xunit;

namespace Nest.Queryify.Tests.Repository
{
    public class ElasticsearchRepositoryExistsSpecs
    {
        private readonly IElasticsearchRepository _repository;
        private IElasticClient _client;

        public ElasticsearchRepositoryExistsSpecs()
        {
            var settings = new ConnectionSettings();

            _client = new ElasticClient(settings, new InMemoryConnection(settings, string.Empty, 200));
            _repository = new ElasticsearchRepository(_client);
        }

        [Fact]
        public void ShouldRun()
        {
            var result = _repository.Exists(new Person() { Id = 1 });

            result.Should().BeTrue();
        }

        [Fact]
        public async Task ShouldRunAsync()
        {
            var result = await _repository.ExistsAsync(new Person() { Id = 1 });

            result.Should().BeTrue();
        }
    }
}