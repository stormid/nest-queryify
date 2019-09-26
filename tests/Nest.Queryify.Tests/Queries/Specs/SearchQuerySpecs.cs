using System;
using Nest.Queryify.Abstractions.Queries;
using Nest.Queryify.Tests.Queries.Fixtures;
using Nest.Queryify.Tests.TestData;

namespace Nest.Queryify.Tests.Queries.Specs
{
    public class SearchQuerySpecs : QuerySpec<ISearchResponse<Person>>
    {
        public class StubQuery : SearchDescriptorQuery<Person>
        {
            protected override SearchDescriptor<Person> BuildQuery(SearchDescriptor<Person> descriptor)
            {
                return descriptor.MatchAll();
            }
        }

        public SearchQuerySpecs(ElasticClientQueryObjectTestFixture fixture) : base(fixture)
        {
        }

        protected override void AssertExpectations()
        {
            Fixture.ShouldUseHttpMethod("POST");
            Fixture.ShouldUseUri(new Uri("http://localhost:9200/my-application/person/_search?typed_keys=true"));
        }

        protected override ElasticClientQueryObject<ISearchResponse<Person>> Query()
        {
            return new StubQuery();
        }
    }
}