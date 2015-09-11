using System;
using Nest.Queryify.Abstractions.Queries;
using Nest.Queryify.Queries.Common;
using Nest.Queryify.Tests.Queries.Fixtures;
using Nest.Queryify.Tests.TestData;

namespace Nest.Queryify.Tests.Queries.Specs
{
    public class DeleteByIdQuerySpecs : QuerySpec<IDeleteResponse>
    {
        public DeleteByIdQuerySpecs(ElasticClientQueryObjectTestFixture fixture) : base(fixture)
        {
        }

        protected override void AssertExpectations()
        {
            Fixture.ShouldUseHttpMethod("DELETE");
            Fixture.ShouldUseUri(new Uri("http://localhost:9200/my-application/person/1"));
        }

        protected override ElasticClientQueryObject<IDeleteResponse> Query()
        {
            return new DeleteByIdQuery<Person>("1");
        }
    }
}