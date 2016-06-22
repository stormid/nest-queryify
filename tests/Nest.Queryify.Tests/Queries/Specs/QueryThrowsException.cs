using System;
using System.Threading.Tasks;
using Nest.Queryify.Abstractions.Queries;
using Nest.Queryify.Exceptions;
using Nest.Queryify.Tests.Queries.Fixtures;
using Nest.Queryify.Tests.TestData;
using Xunit;

namespace Nest.Queryify.Tests.Queries.Specs
{
    public class QueryThrowsException : QuerySpec<ISearchResponse<Person>>
    {
        public class ExceptionQuery : ElasticClientQueryObject<ISearchResponse<Person>>
        {
            protected override ISearchResponse<Person> ExecuteCore(IElasticClient client, string index)
            {
                throw new Exception("Bang!");
            }

            protected override Task<ISearchResponse<Person>> ExecuteCoreAsync(IElasticClient client, string index)
            {
                throw new Exception("Bang!");
            }
        }

        public QueryThrowsException(ElasticClientQueryObjectTestFixture fixture) : base(fixture)
        {
            
        }

        public override void ShouldExecute()
        {
            Assert.Throws<ElasticClientQueryObjectException>(() => base.ShouldExecute());
        }

        public override async Task ShouldExecuteAsync()
        {
            await Assert.ThrowsAsync<ElasticClientQueryObjectException>(async () => await base.ShouldExecuteAsync());
        }
        
        protected override ElasticClientQueryObject<ISearchResponse<Person>> Query()
        {
            return new ExceptionQuery();
        }
    }
}