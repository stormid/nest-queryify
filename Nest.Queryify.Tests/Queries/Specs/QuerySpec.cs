using System;
using System.Threading.Tasks;
using Elasticsearch.Net;
using Nest.Queryify.Abstractions.Queries;
using Nest.Queryify.Exceptions;
using Nest.Queryify.Tests.Queries.Fixtures;
using Nest.Queryify.Tests.TestData;
using Xunit;

namespace Nest.Queryify.Tests.Queries.Specs
{
    public abstract class QuerySpec<TResponse> : IClassFixture<ElasticClientQueryObjectTestFixture>
        where TResponse: class
    {
        protected ElasticClientQueryObjectTestFixture Fixture { get; }

        protected QuerySpec(ElasticClientQueryObjectTestFixture fixture)
        {
            Fixture = fixture;
        }

        [Fact]
        public virtual void ShouldExecute()
        {
            var query = Query();
            query.Execute(Fixture.Client);
            AssertExpectations();
        }

        [Fact]
        public virtual async Task ShouldExecuteAsync()
        {
            var query = Query();
            await query.ExecuteAsync(Fixture.Client);
            AssertExpectations();
        }

        protected abstract void AssertExpectations();
        protected abstract ElasticClientQueryObject<TResponse> Query();

    }

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

        protected override void AssertExpectations()
        {
        }

        protected override ElasticClientQueryObject<ISearchResponse<Person>> Query()
        {
            return new ExceptionQuery();
        }
    }

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

    public class QueryThrowsElasticClientQueryObjectException : QuerySpec<ISearchResponse<Person>>
    {
        public class ExceptionQuery : ElasticClientQueryObject<ISearchResponse<Person>>
        {
            protected override ISearchResponse<Person> ExecuteCore(IElasticClient client, string index)
            {
                throw new ElasticClientQueryObjectException("Bang!", "error", 500);
            }

            protected override Task<ISearchResponse<Person>> ExecuteCoreAsync(IElasticClient client, string index)
            {
                throw new ElasticClientQueryObjectException("Bang!", "error", 500);
            }
        }

        public QueryThrowsElasticClientQueryObjectException(ElasticClientQueryObjectTestFixture fixture) : base(fixture)
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