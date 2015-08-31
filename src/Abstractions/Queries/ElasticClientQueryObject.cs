using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Elasticsearch.Net;
using Nest.Queryify.Exceptions;

namespace Nest.Queryify.Abstractions.Queries
{
    [DebuggerStepThrough]
    public abstract class ElasticClientQueryObject<TResponse> where TResponse : class
    {
	    public TResponse Execute(IElasticClient client, string index = null)
        {
            return WrapQueryResponse(() => ExecuteCore(client, index));
        }

        public Task<TResponse> ExecuteAsync(IElasticClient client, string index = null)
        {
            return WrapQueryResponse(() => ExecuteCoreAsync(client, index));
        }

        protected abstract TResponse ExecuteCore(IElasticClient client, string index);

        protected abstract Task<TResponse> ExecuteCoreAsync(IElasticClient client, string index);

        private static TQueryResponse WrapQueryResponse<TQueryResponse>(Func<TQueryResponse> execute) where TQueryResponse : class
        {
            try
            {
                return execute();
            }
            catch (ElasticClientQueryObjectException)
            {
                throw;
            }
            catch (ElasticsearchServerException exception)
            {
                throw new ElasticClientQueryObjectException("Query execution failed", exception.ExceptionType, exception.Status, exception);
            }
            catch (Exception exception)
            {
                throw new ElasticClientQueryObjectException("An unexpected query execution error occurred", exception);
            }
        }
    }
}