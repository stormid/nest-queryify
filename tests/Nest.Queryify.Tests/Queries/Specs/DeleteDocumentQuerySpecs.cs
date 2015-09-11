using System;
using Nest.Queryify.Abstractions.Queries;
using Nest.Queryify.Exceptions;
using Nest.Queryify.Extensions;
using Nest.Queryify.Queries.Common;
using Nest.Queryify.Tests.Queries.Fixtures;
using Nest.Queryify.Tests.TestData;
using Xunit;

namespace Nest.Queryify.Tests.Queries.Specs
{
    public class DeleteDocumentQuerySpecs : QuerySpec<IDeleteResponse>
    {
        public DeleteDocumentQuerySpecs(ElasticClientQueryObjectTestFixture fixture) : base(fixture)
        {
        }

        protected override void AssertExpectations()
        {
            Fixture.ShouldUseHttpMethod("DELETE");
            Fixture.ShouldUseUri(new Uri("http://localhost:9200/my-application/person/1"));
        }

        protected override ElasticClientQueryObject<IDeleteResponse> Query()
        {
            var person = new Person() {Id = 1};
            return new DeleteDocumentQuery<Person>(person);
        }

        [Fact]
        public void WhenNoDocumentIsSpecified()
        {
            var query = new DeleteDocumentQuery<Person>(null);
            var ex = Assert.Throws<ElasticClientQueryObjectException>(() => Fixture.Client.Query(query));
        }        
    }
}