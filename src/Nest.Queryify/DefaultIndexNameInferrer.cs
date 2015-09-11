using System;
using Nest.Queryify.Abstractions;

namespace Nest.Queryify
{
    public class DefaultIndexNameInferrer : IIndexNameInferrer
    {
        private readonly IElasticClient _client;
        public DefaultIndexNameInferrer(IElasticClient client)
        {
            _client = client;
        }

        public string GetIndexName(string index = null)
        {
            return GetIndexNameCore(index ?? _client.Infer.DefaultIndex.ToLowerInvariant());
        }

        protected virtual string GetIndexNameCore(string index)
        {
            return index;
        }
    }
}