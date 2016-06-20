using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Elasticsearch.Net;
using Nest.Queryify.Exceptions;

namespace Nest.Queryify.Abstractions.Queries
{
    /// <summary>
    /// Implement this class to create a new query object to execute an elasticsearch operation
    /// </summary>
    /// <typeparam name="TResponse"></typeparam>
    [DebuggerStepThrough]
    public abstract class ElasticClientQueryObject<TResponse> : IElasticClientQueryObject<TResponse> where TResponse : class
    {
        /// <exception cref="ElasticClientQueryObjectException">will be thrown on any query error</exception>
        public TResponse Execute(IElasticClient client, string index = null)
	    {
	        return WrapQueryResponse(() => ExecuteCore(client, index ?? client.Infer.DefaultIndex));
	    }

        /// <returns></returns>        
        /// <exception cref="ElasticClientQueryObjectException">will be thrown on any query error</exception>
        public Task<TResponse> ExecuteAsync(IElasticClient client, string index = null)
        {
            return WrapQueryResponse(() => ExecuteCoreAsync(client, index ?? client.Infer.DefaultIndex));
        }

        /// <summary>
        /// Implement and write your operation
        /// </summary>
        /// <param name="client">The elastic client</param>
        /// <param name="index">The index to operate on</param>
        /// <returns></returns>
        protected abstract TResponse ExecuteCore(IElasticClient client, string index);

        /// <summary>
        /// Implement and write your operation
        /// </summary>
        /// <param name="client">The elastic client</param>
        /// <param name="index">The index to operate on</param>
        /// <returns></returns>
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
                throw new ElasticClientQueryObjectException($"An unexpected query execution error occurred: {exception.Message}", exception);
            }
        }
    }
}