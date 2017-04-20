using System.Threading.Tasks;
using FluentAssertions;
using Nest.Queryify.Abstractions;
using Nest.Queryify.UnitTests.TestData;
using Xunit;

namespace Nest.Queryify.UnitTests.Repository
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

        [Fact]
        public async Task ShouldRunAsync()
        {
            var result = await _repository.ExistsAsync<Person>("1");

            result.Should().BeFalse();
        }
    }
}