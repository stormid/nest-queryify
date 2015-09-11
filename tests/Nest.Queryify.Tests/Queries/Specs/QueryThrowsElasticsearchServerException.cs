using System.Threading.Tasks;
using Elasticsearch.Net;
using Nest.Queryify.Abstractions.Queries;
using Nest.Queryify.Exceptions;
using Nest.Queryify.Tests.Queries.Fixtures;
using Nest.Queryify.Tests.TestData;
using Xunit;

namespace Nest.Queryify.Tests.Queries.Specs
{
    public class QueryThrowsElasticsearchServerException : QuerySpec<ISearchResponse<Person>>
    {
        public class ExceptionQuery : ElasticClientQueryObject<ISearchResponse<Person>>
        {
            protected override ISearchResponse<Person> ExecuteCore(IElasticClient client, string index)
            {
                throw new ElasticsearchServerException(new ElasticsearchServerError());
            }

            protected override Task<ISearchResponse<Person>> ExecuteCoreAsync(IElasticClient client, string index)
            {
                throw new ElasticsearchServerException(new ElasticsearchServerError());
            }
        }

        public QueryThrowsElasticsearchServerException(ElasticClientQueryObjectTestFixture fixture) : base(fixture)
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

        protected override void AssertExpectations()
        {
        }

        protected override ElasticClientQueryObject<ISearchResponse<Person>> Query()
        {
            return new ExceptionQuery();
        }
    }
}