using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Nest.Queryify.Abstractions.Queries;
using Nest.Queryify.Tests.Queries.Fixtures;
using Xunit;

namespace Nest.Queryify.Tests.Queries.Specs
{
    [DebuggerStepThrough]
    public abstract class QuerySpec<TResponse> : IClassFixture<ElasticClientQueryObjectTestFixture>
        where TResponse: class
    {
        protected ElasticClientQueryObjectTestFixture Fixture { get; }

        protected QuerySpec(ElasticClientQueryObjectTestFixture fixture)
        {
            Fixture = fixture;
        }

        private void DisplayParameters()
        {
            Console.WriteLine($"Uri: {Fixture.HostUri}");
            Console.WriteLine($"Index: Expected -> {Fixture.DefaultIndex} || Inferred -> {Fixture.Client.Infer.DefaultIndex}");
            if(Fixture.CalledUriList.Any()) Console.WriteLine($"Called Uri's: {string.Join(" | ", Fixture.CalledUriList)}");
        }

        [Fact]
        public virtual void ShouldExecute()
        {
            DisplayParameters();
            var query = Query();
            query.Execute(Fixture.Client, Fixture.DefaultIndex);
            AssertExpectations();
        }

        [Fact]
        public virtual async Task ShouldExecuteAsync()
        {
            var query = Query();
            await query.ExecuteAsync(Fixture.Client, Fixture.DefaultIndex);
            AssertExpectations();
        }

        protected virtual void AssertExpectations()
        {
            
        }

        protected abstract ElasticClientQueryObject<TResponse> Query();

    }
}