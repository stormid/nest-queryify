using System.Threading.Tasks;
using FluentAssertions;
using Nest.Queryify.Abstractions;
using Nest.Queryify.Tests.Extensions;
using Nest.Queryify.Tests.Queries.Fixtures;
using Nest.Queryify.Tests.TestData;
using Xunit;

namespace Nest.Queryify.UnitTests.Repository
{
    public class ElasticsearchRepositoryDeleteDocumentSpecs
    {
        private readonly IElasticsearchRepository _repository;
        private IElasticClient _client;

        public ElasticsearchRepositoryDeleteDocumentSpecs()
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
            var response = _repository.Delete(new Person() { Id = 1});

            response.Found.Should().BeTrue();
        }

        [Fact]
        public async Task ShouldRunAsync()
        {
            var response = await _repository.DeleteAsync(new Person() { Id = 1 });

            response.Found.Should().BeTrue();
        }
    }
}