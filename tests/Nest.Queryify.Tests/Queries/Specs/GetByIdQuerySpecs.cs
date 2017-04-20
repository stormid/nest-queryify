using System;
using Nest.Queryify.Abstractions.Queries;
using Nest.Queryify.Queries.Common;
using Nest.Queryify.Tests.Queries.Fixtures;
using Nest.Queryify.Tests.TestData;

namespace Nest.Queryify.Tests.Queries.Specs
{
    public class GetByIdQuerySpecs : QuerySpec<IGetResponse<Person>>
    {
        public GetByIdQuerySpecs(ElasticClientQueryObjectTestFixture fixture) : base(fixture)
        {
        }

        protected override void AssertExpectations()
        {
            Fixture.ShouldUseHttpMethod("GET");
            Fixture.ShouldUseUri(new Uri("http://localhost:9200/my-application/person/1"));
        }

        protected override ElasticClientQueryObject<IGetResponse<Person>> Query()
        {
            return new GetByIdQuery<Person>(DocumentPath<Person>.Id("1"));
        }
    }
}
