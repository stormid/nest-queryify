using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Elasticsearch.Net.Connection;
using FluentAssertions;
using Nest.Queryify.Abstractions;
using Nest.Queryify.Exceptions;
using Xunit;

namespace Nest.Queryify.Tests
{
    public abstract class ElasticsearchRepositorySpecs
    {
        public class MyDocument
        {
            
        }

        [ElasticType(IdProperty = "Id")]
        public class MyDocumentWithId
        {
            public string Id { get; set; }
        }

        protected readonly IElasticClient _client;
        protected readonly IConnectionSettingsValues _connectionSettings;
        protected readonly InMemoryConnection _inMemoryConnection;
        protected readonly string _defaultIndex;
        protected readonly IElasticsearchRepository _repository;

        protected ElasticsearchRepositorySpecs()
        {
            _defaultIndex = "my-application";
            _connectionSettings = new ConnectionSettings(defaultIndex: _defaultIndex);
            _inMemoryConnection = new InMemoryConnection(_connectionSettings);
            _inMemoryConnection.Requests.AddRange(ExpectedRequests());

            _client = new ElasticClient(_connectionSettings, _inMemoryConnection);
            _repository = new ElasticsearchRepository(_client);

        }

        protected virtual IEnumerable<Tuple<string, Uri, byte[]>> ExpectedRequests()
        {
            _inMemoryConnection.RecordRequests = true;
            return Enumerable.Empty<Tuple<string, Uri, byte[]>>();
        }

        protected void ShouldUseHttpMethod(string method, int requestIndex = 0)
        {
            _inMemoryConnection.Requests.Count.Should().BeGreaterOrEqualTo(requestIndex + 1);
            _inMemoryConnection.Requests[requestIndex].Item1.Should().Be(method);
        }

        protected void ShouldUseUri(Uri uri, int requestIndex = 0)
        {
            _inMemoryConnection.Requests.Count.Should().BeGreaterOrEqualTo(requestIndex + 1);
            _inMemoryConnection.Requests[requestIndex].Item2.Should().Be(uri);
        }

        protected void ShouldRespondWith(byte[] bytes, int requestIndex = 0)
        {
            _inMemoryConnection.Requests.Count.Should().BeGreaterOrEqualTo(requestIndex + 1);
            _inMemoryConnection.Requests[requestIndex].Item3.Should().Equal(bytes);
        }

        public class GetById : ElasticsearchRepositorySpecs
        {
            public GetById()
            {
                _repository.GetById<MyDocument>("id");
            }
            
            [Fact]
            public void ShouldCallElasticsearch()
            {
                ShouldUseHttpMethod("GET");
                ShouldUseUri(new Uri("http://localhost:9200/my-application/mydocument/id"));
            }
        }

        public class DeleteById : ElasticsearchRepositorySpecs
        {
            public DeleteById()
            {
                _repository.Delete<MyDocument>("id");
            }
            
            [Fact]
            public void ShouldCallElasticsearch()
            {
                ShouldUseHttpMethod("DELETE");
                ShouldUseUri(new Uri("http://localhost:9200/my-application/mydocument/id"));
            }
        }

        public class DeleteByDocument : ElasticsearchRepositorySpecs
        {
            public DeleteByDocument()
            {
                _repository.Delete(new MyDocumentWithId() {Id = "id"});
            }

            [Fact]
            public void ShouldExecuteQuery()
            {
                ShouldUseHttpMethod("DELETE");
                ShouldUseUri(new Uri("http://localhost:9200/my-application/mydocumentwithid/id"));
            }
        }

        public class DeleteByDocumentWithoutIdDefinition : ElasticsearchRepositorySpecs
        {
            private readonly ElasticClientQueryObjectException ex;

            public DeleteByDocumentWithoutIdDefinition()
            {
                ex = Assert.Throws<ElasticClientQueryObjectException>(() => _repository.Delete(new MyDocument()));
            }

            [Fact]
            public void ShouldThrowQueryException()
            {
                ex.Should().NotBeNull();
            }
        }
    }
}
