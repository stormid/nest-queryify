using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Elasticsearch.Net;

namespace Nest.Queryify.Tests.Queries.Fixtures
{
    public class ElasticClientQueryObjectTestFixture : IDisposable
    {
        public class CallDetails
        {
            public string HttpMethod { get; set; }
            public Uri Uri { get; set; }
        }

        public Uri HostUri { get; }
        public string DefaultIndex { get; }

        public IList<CallDetails> ApiCallsList = new List<CallDetails>();

        public IElasticClient Client { get; private set; }

        public ElasticClientQueryObjectTestFixture() : this("my-application") { }

        private void SetupElasticClient()
        {
            var connection = new InMemoryConnection();
            IConnectionSettingsValues connectionSettings = new ConnectionSettings(new SingleNodeConnectionPool(HostUri), connection)
                .DefaultIndex(DefaultIndex)
                .DisableDirectStreaming()
                .OnRequestCompleted(details =>
                {
                    ApiCallsList.Add(new CallDetails
                    {
                        Uri = details.Uri,
                        HttpMethod = details.HttpMethod.GetStringValue()
                    });
                });

            Client = new ElasticClient(connectionSettings);
        }

        protected ElasticClientQueryObjectTestFixture(string defaultIndex, string uri = null)
        {
            HostUri = new Uri(uri ?? "http://localhost:9200");
            DefaultIndex = defaultIndex;

            SetupElasticClient();
        }

        public void ShouldUseHttpMethod(string method, int requestIndex = 0)
        {
            ApiCallsList.Count.Should().BeGreaterOrEqualTo(requestIndex + 1);
            ApiCallsList.ElementAt(requestIndex).HttpMethod.Should().Be(method);
        }

        public void ShouldUseUri(Uri uri, int requestIndex = 0)
        {
            ApiCallsList.Count.Should().BeGreaterOrEqualTo(requestIndex + 1);
            ApiCallsList.ElementAt(requestIndex).Uri.Should().Be(uri);
        }

        public void Dispose()
        {
        }
    }
}