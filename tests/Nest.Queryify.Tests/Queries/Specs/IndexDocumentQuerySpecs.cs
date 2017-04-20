using System;
using Nest.Queryify.Abstractions.Queries;
using Nest.Queryify.Queries;
using Nest.Queryify.Tests.Queries.Fixtures;
using Nest.Queryify.Tests.TestData;

namespace Nest.Queryify.Tests.Queries.Specs
{
    public class IndexDocumentQuerySpecs : QuerySpec<IIndexResponse>
    {
        public IndexDocumentQuerySpecs(ElasticClientQueryObjectTestFixture fixture) : base(fixture)
        {
        }

        protected override void AssertExpectations()
        {
            Fixture.ShouldUseHttpMethod("PUT");
            Fixture.ShouldUseUri(new Uri("http://localhost:9200/my-application/person/1?refresh=false"));
            // Fixture.RespondsWith<IndexResponse>().Should().NotBeNull();
        }

        protected override ElasticClientQueryObject<IIndexResponse> Query()
        {
            return new IndexDocumentQuery<Person>(new Person() { Id = 1, Name = "Tom", Email = "tom@cruise.com" });
        }
    }
}