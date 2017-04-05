using System.Threading.Tasks;
using FluentAssertions;
using Nest.Queryify.Abstractions;
using Nest.Queryify.UnitTests.Extensions;
using Nest.Queryify.UnitTests.Queries.Fixtures;
using Nest.Queryify.UnitTests.TestData;
using Xunit;

namespace Nest.Queryify.UnitTests.Repository
{
    public class ElasticsearchRepositorySaveSpecs
    {
        private readonly IElasticsearchRepository _repository;
        private IElasticClient _client;

        public ElasticsearchRepositorySaveSpecs()
        {
            var settings = new ConnectionSettings();

            var output =
                "Nest.Queryify.Tests.TestData.ValidIndexResponse.json".ReadAsStringFromEmbeddedResource<ElasticClientQueryObjectTestFixture>();
            _client = new ElasticClient(settings, new InMemoryConnection(settings, output));
            _repository = new ElasticsearchRepository(_client);
        }

        [Fact]
        public void ShouldRun()
        {
            var person = _repository.Save(new Person() { Id = 2, Name = "Tom", Email = "tom@cruise.com"});
            person.Created.Should().BeTrue();
        }

        [Fact]
        public async Task ShouldRunAsync()
        {
            var person = await _repository.SaveAsync(new Person() { Id = 2, Name = "Tom", Email = "tom@cruise.com" });
            person.Created.Should().BeTrue();
        }
    }
}