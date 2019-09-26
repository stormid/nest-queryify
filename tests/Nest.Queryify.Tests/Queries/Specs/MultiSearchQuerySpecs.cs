using System;
using Nest.Queryify.Abstractions.Queries;
using Nest.Queryify.Queries;
using Nest.Queryify.Tests.Queries.Fixtures;
using Nest.Queryify.Tests.TestData;

namespace Nest.Queryify.Tests.Queries.Specs
{
    public class MultiSearchQuerySpecs : QuerySpec<IMultiSearchResponse>
    {
        public class StubQuery : MultiSearchQuery
        {
            protected override MultiSearchDescriptor BuildQuery(MultiSearchDescriptor descriptor, string index)
            {
                return descriptor.Search<Person>("search1", s => s.MatchAll());
            }
        }

        public MultiSearchQuerySpecs(ElasticClientQueryObjectTestFixture fixture) : base(fixture)
        {
        }

        protected override void AssertExpectations()
        {
            Fixture.ShouldUseHttpMethod("POST");
            Fixture.ShouldUseUri(new Uri("http://localhost:9200/_msearch?typed_keys=true"));
        }

        protected override ElasticClientQueryObject<IMultiSearchResponse> Query()
        {
            return new StubQuery();
        }
    }
}