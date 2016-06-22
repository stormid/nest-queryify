using System;
using System.Runtime.Serialization;
using Elasticsearch.Net;

namespace Nest.Queryify.Exceptions
{
    [Serializable]
    public class ElasticsearchException : Exception
    {
        public string ExceptionType { get; private set; }
        public int Status { get; private set; }

        public ElasticsearchException(string message, string exceptionType, int status) : base(message)
        {
            ExceptionType = exceptionType;
            Status = status;
        }

        public ElasticsearchException(string message, string exceptionType, int status, Exception inner)
            : base(message, inner)
        {
            ExceptionType = exceptionType;
            Status = status;
        }

        public ElasticsearchException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}