using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Elasticsearch.Net.Connection;
using FluentAssertions;
using Nest.Queryify.Tests.Extensions;
using Newtonsoft.Json.Linq;

namespace Nest.Queryify.Tests.Queries.Fixtures
{
    public class ElasticClientQueryObjectTestFixture : IDisposable
    {
        private readonly IConnectionSettingsValues _connectionSettingsValues;
        private readonly InMemoryConnection _connection;

        public Uri HostUri { get; }
        public string DefaultIndex { get; }

        public IEnumerable<Uri> CalledUriList => _connection?.Requests?.Select(x => x.Item2) ?? Enumerable.Empty<Uri>();

        public IElasticClient Client { get; }

        public ElasticClientQueryObjectTestFixture() : this("my-application", captureRequests: true) { }

        protected ElasticClientQueryObjectTestFixture(string defaultIndex, string uri = null, bool captureRequests = false)
        {
            HostUri = new Uri(uri ?? "http://localhost:9200");
            DefaultIndex = defaultIndex;

            _connectionSettingsValues = new ConnectionSettings(HostUri, DefaultIndex);
            _connection = new InMemoryConnection(_connectionSettingsValues)
            {
                RecordRequests = captureRequests
            };
            _connection.Requests = ExpectedRequests().ToList();

            Client = new ElasticClient(_connectionSettingsValues, _connection);
        }

        public void SetExpectedRequest<TResponse>(string method, Uri uri, TResponse response)
        {
            var bytes = Client.Serializer.Serialize(response);
            _connection.Requests.Add(new Tuple<string, Uri, byte[]>(method, uri, bytes));
        }

        public void SetExpectedRequest<TResponse>(string method, Uri uri, string jsonOutput)
        {
            var response = Client.Serializer.Deserialize<TResponse>(new MemoryStream(Encoding.UTF8.GetBytes(jsonOutput)));

            SetExpectedRequest(method, uri, response);
        }

        public void SetExpectedRequestFromResource<TResponse>(string method, Uri uri, string embeddedResourcePath)
        {
            SetExpectedRequest<TResponse>(method, uri, embeddedResourcePath.ReadAsStringFromEmbeddedResource<ElasticClientQueryObjectTestFixture>());
        }

        protected virtual IEnumerable<Tuple<string, Uri, byte[]>> ExpectedRequests()
        {
            return Enumerable.Empty<Tuple<string, Uri, byte[]>>();
        }

        public void ShouldUseHttpMethod(string method, int requestIndex = 0)
        {
            _connection.Requests.Count.Should().BeGreaterOrEqualTo(requestIndex + 1);
            _connection.Requests[requestIndex].Item1.Should().Be(method);
        }

        public void ShouldUseUri(Uri uri, int requestIndex = 0)
        {
            _connection.Requests.Count.Should().BeGreaterOrEqualTo(requestIndex + 1);
            _connection.Requests[requestIndex].Item2.Should().Be(uri);
        }

        public void ShouldRespondWith(byte[] bytes, int requestIndex = 0)
        {
            _connection.Requests.Count.Should().BeGreaterOrEqualTo(requestIndex + 1);
            _connection.Requests[requestIndex].Item3.Should().Equal(bytes);
        }

        public T RespondsWith<T>(int requestIndex = 0)
        {
            _connection.Requests.Count.Should().BeGreaterOrEqualTo(requestIndex + 1);
            var bytes = _connection.Requests[requestIndex].Item3;
            return Client.Serializer.Deserialize<T>(new MemoryStream(bytes));
        }

        public void ShouldRespondWith<T>(Action<T> response, int requestIndex = 0)
        {
            _connection.Requests.Count.Should().BeGreaterOrEqualTo(requestIndex + 1);
            var bytes = _connection.Requests[requestIndex].Item3;
            response(Client.Serializer.Deserialize<T>(new MemoryStream(bytes)));
        }

        public void Dispose()
        {
        }

        public void ToggleRecordingRequests()
        {
            _connection.RecordRequests = !_connection.RecordRequests;
        }
    }
}