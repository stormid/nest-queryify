using System;
using System.Threading.Tasks;
using Elasticsearch.Net.Connection;
using Nest.Queryify.Abstractions;
using Nest.Queryify.Tests.Extensions;
using Nest.Queryify.Tests.Queries.Fixtures;
using Nest.Queryify.Tests.TestData;
using Xunit;

namespace Nest.Queryify.Tests.Repository
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