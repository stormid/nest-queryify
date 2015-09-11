using System.Threading.Tasks;
using Nest.Queryify.Abstractions.Queries;
using Nest.Queryify.Tests.Queries.Fixtures;
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
}