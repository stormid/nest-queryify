using System.Threading.Tasks;
using FluentAssertions;
using Nest.Queryify.Abstractions;
using Nest.Queryify.UnitTests.Extensions;
using Nest.Queryify.UnitTests.Queries.Fixtures;
using Nest.Queryify.UnitTests.TestData;
using Xunit;

namespace Nest.Queryify.UnitTests.Repository
{
    public class ElasticsearchRepositoryGetByIdSpecs
    {
        private readonly IElasticsearchRepository _repository;
        private readonly IElasticClient _client;

        public ElasticsearchRepositoryGetByIdSpecs()
        {
            var settings = new ConnectionSettings();

            var output =
                "Nest.Queryify.Tests.TestData.ValidGetResponse.json".ReadAsStringFromEmbeddedResource<ElasticClientQueryObjectTestFixture>();
            _client = new ElasticClient(settings, new InMemoryConnection(settings, output));
            _repository = new ElasticsearchRepository(_client);
        }
        
        [Fact]
        public void ShouldRun()
        {
            var response = _repository.GetById<Person>("1");
            response.Found.Should().BeTrue();
            response.Id.Should().Be("1");
        }

        [Fact]
        public async Task ShouldRunAsync()
        {
            var response = await _repository.GetByIdAsync<Person>("1");
            response.Found.Should().BeTrue();
            response.Id.Should().Be("1");
        }
    }
}
