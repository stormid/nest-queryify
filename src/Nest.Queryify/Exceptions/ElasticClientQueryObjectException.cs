using System;

namespace Nest.Queryify.Exceptions
{
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