using System;
using System.Runtime.Serialization;
using Elasticsearch.Net;

namespace Nest.Queryify.Exceptions
{
    public class ElasticsearchException : Exception
    {
        public string ExceptionType { get; }
        public int Status { get; }

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