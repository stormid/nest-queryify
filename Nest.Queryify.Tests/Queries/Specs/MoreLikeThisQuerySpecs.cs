using System;
using Nest.Queryify.Abstractions.Queries;
using Nest.Queryify.Queries;
using Nest.Queryify.Tests.Queries.Fixtures;
using Nest.Queryify.Tests.TestData;

namespace Nest.Queryify.Tests.Queries.Specs
{
    public class MoreLikeThisQuerySpecs : QuerySpec<ISearchResponse<Person>>
    {
        public class StubQuery : MoreLikeThisQuery<Person>
        {
            protected override MoreLikeThisDescriptor<Person> BuildQuery(MoreLikeThisDescriptor<Person> descriptor)
            {
                return descriptor.Search(p => p.MatchAll()).Id("1");
            }
        }

        public MoreLikeThisQuerySpecs(ElasticClientQueryObjectTestFixture fixture) : base(fixture)
        {
        }

        protected override void AssertExpectations()
        {
            Fixture.ShouldUseHttpMethod("POST");
            Fixture.ShouldUseUri(new Uri("http://localhost:9200/my-application/person/1/_mlt"));
            Fixture.RespondsWith<SearchResponse<Person>>();
        }

        protected override ElasticClientQueryObject<ISearchResponse<Person>> Query()
        {
            return new StubQuery();
        }
    }
}