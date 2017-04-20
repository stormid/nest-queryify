using System.Threading.Tasks;
using FluentAssertions;
using Nest.Queryify.Abstractions;
using Nest.Queryify.UnitTests.Extensions;
using Nest.Queryify.UnitTests.Queries.Fixtures;
using Nest.Queryify.UnitTests.TestData;
using Xunit;

namespace Nest.Queryify.UnitTests.Repository
{
    public class ElasticsearchRepositoryGetByIdNotFoundSpecs
    {
        private readonly IElasticsearchRepository _repository;
        private IElasticClient _client;

        public ElasticsearchRepositoryGetByIdNotFoundSpecs()
        {
            var settings = new ConnectionSettings();

            var output =
                "Nest.Queryify.Tests.TestData.NotFoundGetResponse.json".ReadAsStringFromEmbeddedResource<ElasticClientQueryObjectTestFixture>();
            _client = new ElasticClient(settings, new InMemoryConnection(settings, output, 404));
            _repository = new ElasticsearchRepository(_client);
        }

        [Fact]
        public void ShouldRun()
        {
            var response = _repository.GetById<Person>("1");
            response.Found.Should().BeFalse();
            response.Id.Should().Be("1");
        }

        [Fact]
        public async Task ShouldRunAsync()
        {
            var response = await _repository.GetByIdAsync<Person>("1");
            response.Found.Should().BeFalse();
            response.Id.Should().Be("1");
        }
    }
}