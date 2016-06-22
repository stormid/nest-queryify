using System;
using System.Linq;
using FluentAssertions;
using Nest.Queryify.Abstractions.Queries;
using Nest.Queryify.Tests.Queries.Fixtures;
using Nest.Queryify.Tests.TestData;

namespace Nest.Queryify.Tests.Queries.Specs
{
    public class SearchQueryWithReturnDocumentSpecs : QuerySpec<ISearchResponse<PersonModel>>
    {
        public class StubQuery : SearchDescriptorQuery<Person, PersonModel>
        {
            protected override SearchDescriptor<Person> BuildQuery(SearchDescriptor<Person> descriptor)
            {
                return descriptor.MatchAll();
            }
        }

        public SearchQueryWithReturnDocumentSpecs(ElasticClientQueryObjectTestFixture fixture) : base(fixture)
        {
        }

        protected override void AssertExpectations()
        {
            Fixture.ShouldUseHttpMethod("POST");
            Fixture.ShouldUseUri(new Uri("http://localhost:9200/my-application/person/_search"));
            Fixture.RespondsWith<SearchResponse<PersonModel>>().Should().NotBeNull();
            Fixture.RespondsWith<SearchResponse<PersonModel>>().Documents.Any().Should().BeFalse();
        }

        protected override ElasticClientQueryObject<ISearchResponse<PersonModel>> Query()
        {
            return new StubQuery();
        }
    }
}