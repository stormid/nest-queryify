using System;
using FluentAssertions;
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
            Fixture.ShouldUseUri(new Uri("http://localhost:9200/_msearch"));
            Fixture.RespondsWith<MultiSearchResponse>().Should().NotBeNull();
        }

        protected override ElasticClientQueryObject<IMultiSearchResponse> Query()
        {
            return new StubQuery();
        }
    }

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
            Fixture.ShouldUseUri(new Uri("http://localhost:9200/my-application/person/_search"));
            Fixture.RespondsWith<SearchResponse<Person>>().Should().NotBeNull();
        }

        protected override ElasticClientQueryObject<ISearchResponse<Person>> Query()
        {
            return new StubQuery();
        }
    }

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
        }

        protected override ElasticClientQueryObject<ISearchResponse<PersonModel>> Query()
        {
            return new StubQuery();
        }
    }
}