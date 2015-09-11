using System;
using System.Linq;
using Nest.Queryify.Abstractions.Queries;
using Nest.Queryify.Queries;
using Nest.Queryify.Tests.Queries.Fixtures;
using Nest.Queryify.Tests.TestData;

namespace Nest.Queryify.Tests.Queries.Specs
{
    public class BulkIndexDocumentQueryRefreshSpecs : QuerySpec<IBulkResponse>
    {
        public BulkIndexDocumentQueryRefreshSpecs(ElasticClientQueryObjectTestFixture fixture) : base(fixture)
        {
        }

        protected override void AssertExpectations()
        {
            Fixture.ShouldUseHttpMethod("POST");
            Fixture.ShouldUseUri(new Uri("http://localhost:9200/_bulk?refresh=true"));
        }

        protected override ElasticClientQueryObject<IBulkResponse> Query()
        {
            var list = Enumerable.Repeat(new Person(), 10);
            return new BulkIndexDocumentQuery<Person>(list, true);
        }
    }
}