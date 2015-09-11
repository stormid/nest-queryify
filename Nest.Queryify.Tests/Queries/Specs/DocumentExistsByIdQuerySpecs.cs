using System;
using Nest.Queryify.Abstractions.Queries;
using Nest.Queryify.Queries;
using Nest.Queryify.Tests.Queries.Fixtures;
using Nest.Queryify.Tests.TestData;

namespace Nest.Queryify.Tests.Queries.Specs
{
    public class DocumentExistsByIdQuerySpecs : QuerySpec<IExistsResponse>
    {
        public DocumentExistsByIdQuerySpecs(ElasticClientQueryObjectTestFixture fixture) : base(fixture)
        {
        }

        protected override void AssertExpectations()
        {
            Fixture.ShouldUseHttpMethod("HEAD");
            Fixture.ShouldUseUri(new Uri("http://localhost:9200/my-application/person/1"));
        }

        protected override ElasticClientQueryObject<IExistsResponse> Query()
        {
            return new DocumentExistsByIdQuery<Person>("1");
        }
    }
}