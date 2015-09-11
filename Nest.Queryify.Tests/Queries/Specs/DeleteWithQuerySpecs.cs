using System;
using FluentAssertions;
using Nest.Queryify.Abstractions.Queries;
using Nest.Queryify.Queries;
using Nest.Queryify.Tests.Queries.Fixtures;
using Nest.Queryify.Tests.TestData;

namespace Nest.Queryify.Tests.Queries.Specs
{
    public class DeleteWithQuerySpecs : QuerySpec<IDeleteResponse>
    {
        public class StubQuery : DeleteWithQueryObject<Person>
        {
            protected override DeleteByQueryDescriptor<Person> BuildQuery(DeleteByQueryDescriptor<Person> descriptor)
            {
                return descriptor.MatchAll();
            }
        }

        public DeleteWithQuerySpecs(ElasticClientQueryObjectTestFixture fixture) : base(fixture)
        {
        }

        protected override void AssertExpectations()
        {
            Fixture.ShouldUseHttpMethod("DELETE");
            Fixture.ShouldUseUri(new Uri("http://localhost:9200/my-application/person/_query"));
            Fixture.RespondsWith<DeleteResponse>().Should().NotBeNull();
        }

        protected override ElasticClientQueryObject<IDeleteResponse> Query()
        {
            return new StubQuery();
        }
    }
}