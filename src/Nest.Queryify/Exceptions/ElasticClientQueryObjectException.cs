using System;
using System.Runtime.Serialization;
using Elasticsearch.Net;

namespace Nest.Queryify.Exceptions
{
    [Serializable]
    public class ElasticClientQueryObjectException : ElasticsearchException
    {
        public ElasticClientQueryObjectException(string message, string exceptionType, int status) : base(message, exceptionType, status)
        {
        }

        public ElasticClientQueryObjectException(string message, string exceptionType, int status, Exception inner) : base(message, exceptionType, status, inner)
        {
        }

        public ElasticClientQueryObjectException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}