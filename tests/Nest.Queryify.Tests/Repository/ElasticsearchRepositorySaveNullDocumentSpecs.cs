using System;
using System.Threading.Tasks;
using Nest.Queryify.Abstractions;
using Nest.Queryify.UnitTests.Extensions;
using Nest.Queryify.UnitTests.Queries.Fixtures;
using Nest.Queryify.UnitTests.TestData;
using Xunit;

namespace Nest.Queryify.UnitTests.Repository
{
    public class ElasticsearchRepositorySaveNullDocumentSpecs
    {
        private readonly IElasticsearchRepository _repository;
        private IElasticClient _client;

        public ElasticsearchRepositorySaveNullDocumentSpecs()
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
            Assert.Throws<ArgumentNullException>(() => _repository.Save<Person>(null));
        }

        [Fact]
        public async Task ShouldRunAsync()
        {
            await Assert.ThrowsAsync<ArgumentNullException>(() => _repository.SaveAsync<Person>(null));
        }
    }
}