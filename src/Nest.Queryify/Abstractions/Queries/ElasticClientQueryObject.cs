using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Elasticsearch.Net;
using Nest.Queryify.Exceptions;

namespace Nest.Queryify.Abstractions.Queries
{
    [DebuggerStepThrough]
    public abstract class ElasticClientQueryObject<TResponse> : IElasticClientQueryObject<TResponse> where TResponse : class
    {
        private static string GetDefaultIndex(IElasticClient client)
        {
            return client.ConnectionSettings.DefaultIndex;
        }

        public TResponse Execute(IElasticClient client, string index = null)
        {
            try
            {
                return ExecuteCore(client, index ?? GetDefaultIndex(client));
            }
            catch (ElasticClientQueryObjectException)
            {
                throw;
            }
            catch (Exception exception)
            {
                throw new ElasticClientQueryObjectException($"An unexpected query execution error occurred: {exception.Message}", exception);
            }
        }

        public async Task<TResponse> ExecuteAsync(IElasticClient client, string index = null)
        {
            try
            {
                return await ExecuteCoreAsync(client, index ?? GetDefaultIndex(client)).ConfigureAwait(false);
            }
            catch (ElasticClientQueryObjectException)
            {
                throw;
            }
            catch (Exception exception)
            {
                throw new ElasticClientQueryObjectException($"An unexpected query execution error occurred: {exception.Message}", exception);
            }
        }

        protected abstract TResponse ExecuteCore(IElasticClient client, string index);

        protected abstract Task<TResponse> ExecuteCoreAsync(IElasticClient client, string index);
    }
}