using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Elasticsearch.Net.Connection;
using FluentAssertions;
using Moq;
using Nest.Queryify.Abstractions;
using Nest.Queryify.Exceptions;
using Nest.Queryify.Tests.Extensions;
using Nest.Queryify.Tests.Queries.Fixtures;
using Nest.Queryify.Tests.TestData;
using Newtonsoft.Json;
using Xunit;

namespace Nest.Queryify.Tests
{
    public class ElasticsearchRepositoryGetByIdSpecs
    {
        private readonly IElasticsearchRepository _repository;
        private IElasticClient _client;

        public ElasticsearchRepositoryGetByIdSpecs()
        {
            var settings = new ConnectionSettings();

            var output =
                "Nest.Queryify.Tests.TestData.ValidGetResponse.json".ReadAsStringFromEmbeddedResource<ElasticClientQueryObjectTestFixture>();
            _client = new ElasticClient(settings, new InMemoryConnection(settings, output));
            _repository = new ElasticsearchRepository(_client);
        }
        
        [Fact]
        public void GetById()
        {
            var response = _repository.GetById<Person>("1");
            response.Found.Should().BeTrue();
            response.Id.Should().Be("1");
        }
    }

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
        public void GetById()
        {
            var response = _repository.GetById<Person>("1");
            response.Found.Should().BeFalse();
            response.Id.Should().Be("1");
        }
    }

    public class ElasticsearchRepositoryFindByIdSpecs
    {
        private readonly IElasticsearchRepository _repository;
        private IElasticClient _client;

        public ElasticsearchRepositoryFindByIdSpecs()
        {
            var settings = new ConnectionSettings();

            var output =
                "Nest.Queryify.Tests.TestData.ValidGetResponse.json".ReadAsStringFromEmbeddedResource<ElasticClientQueryObjectTestFixture>();
            _client = new ElasticClient(settings, new InMemoryConnection(settings, output));
            _repository = new ElasticsearchRepository(_client);
        }

        [Fact]
        public void GetById()
        {
            var person = _repository.FindById<Person>("1");
            person.Should().NotBeNull();
        }
    }

    public class ElasticsearchRepositoryFindByIdNotFoundSpecs
    {
        private readonly IElasticsearchRepository _repository;
        private IElasticClient _client;

        public ElasticsearchRepositoryFindByIdNotFoundSpecs()
        {
            var settings = new ConnectionSettings();

            var output =
                "Nest.Queryify.Tests.TestData.NotFoundGetResponse.json".ReadAsStringFromEmbeddedResource<ElasticClientQueryObjectTestFixture>();
            _client = new ElasticClient(settings, new InMemoryConnection(settings, output));
            _repository = new ElasticsearchRepository(_client);
        }

        [Fact]
        public void GetById()
        {
            var person = _repository.FindById<Person>("1");
            person.Should().BeNull();
        }
    }

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
        public void GetById()
        {
            var person = _repository.Save(new Person() { Id = 2, Name = "Tom", Email = "tom@cruise.com"});
            person.Created.Should().BeTrue();
        }
    }

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
        public void GetById()
        {
            Assert.Throws<ArgumentNullException>(() => _repository.Save<Person>(null));
        }
    }

    public class ElasticsearchRepositoryBulkSpecs
    {
        private readonly IElasticsearchRepository _repository;
        private IElasticClient _client;

        public ElasticsearchRepositoryBulkSpecs()
        {
            var settings = new ConnectionSettings();

            var output =
                "Nest.Queryify.Tests.TestData.ValidBulkResponse.json".ReadAsStringFromEmbeddedResource<ElasticClientQueryObjectTestFixture>();
            _client = new ElasticClient(settings, new InMemoryConnection(settings, output));
            _repository = new ElasticsearchRepository(_client);
        }

        [Fact]
        public void GetById()
        {
            var response = _repository.Bulk(new [] {
                new Person() { Id = 1 },
                new Person() { Id = 2 },
                new Person() { Id = 3 }
                });

            response.Took.Should().Be(7);
            response.Items.Count().Should().Be(3);
        }
    }

    public class ElasticsearchRepositoryDeleteSpecs
    {
        private readonly IElasticsearchRepository _repository;
        private IElasticClient _client;

        public ElasticsearchRepositoryDeleteSpecs()
        {
            var settings = new ConnectionSettings();

            var output =
                "Nest.Queryify.Tests.TestData.ValidDeleteResponse.json".ReadAsStringFromEmbeddedResource<ElasticClientQueryObjectTestFixture>();
            _client = new ElasticClient(settings, new InMemoryConnection(settings, output));
            _repository = new ElasticsearchRepository(_client);
        }

        [Fact]
        public void GetById()
        {
            var response = _repository.Delete<Person>("1");

            response.Found.Should().BeTrue();
        }
    }

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
        public void GetById()
        {
            var response = _repository.Delete(new Person() { Id = 1});

            response.Found.Should().BeTrue();
        }
    }

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
        public void GetById()
        {
            var result = _repository.Exists(new Person() { Id = 1 });

            result.Should().BeTrue();
        }
    }

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
        public void GetById()
        {
            var result = _repository.Exists(new Person() { Id = 1 });

            result.Should().BeFalse();
        }
    }

    public class ElasticsearchRepositoryDocumentExistsSpecs
    {
        private readonly IElasticsearchRepository _repository;
        private IElasticClient _client;

        public ElasticsearchRepositoryDocumentExistsSpecs()
        {
            var settings = new ConnectionSettings();

            _client = new ElasticClient(settings, new InMemoryConnection(settings, string.Empty));
            _repository = new ElasticsearchRepository(_client);
        }

        [Fact]
        public void GetById()
        {
            var result = _repository.Exists<Person>("1");

            result.Should().BeTrue();
        }
    }

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
        public void GetById()
        {
            var result = _repository.Exists<Person>("1");

            result.Should().BeFalse();
        }
    }
}
