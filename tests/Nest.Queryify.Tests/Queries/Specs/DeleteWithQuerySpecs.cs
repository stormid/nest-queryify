using System;
using Nest.Queryify.Abstractions.Queries;
using Nest.Queryify.Queries;
using Nest.Queryify.Tests.Queries.Fixtures;
using Nest.Queryify.Tests.TestData;

namespace Nest.Queryify.Tests.Queries.Specs
{
    public class DeleteWithQuerySpecs : QuerySpec<IDeleteByQueryResponse>
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
            Fixture.ShouldUseHttpMethod("POST");
            Fixture.ShouldUseUri(new Uri("http://localhost:9200/my-application/person/_delete_by_query"));
            // Fixture.RespondsWith<DeleteResponse>().Should().NotBeNull();
        }

        protected override ElasticClientQueryObject<IDeleteByQueryResponse> Query()
        {
            return new StubQuery();
        }
    }
}