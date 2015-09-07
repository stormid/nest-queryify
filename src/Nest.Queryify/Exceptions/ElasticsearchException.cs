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

        public ElasticsearchException(ElasticsearchServerError error) : this(error.Error, error.ExceptionType, error.Status)
        {
        }

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

	    /// <exception cref="SerializationException">The class name is null or <see cref="P:System.Exception.HResult" /> is zero (0). </exception>
	    /// <exception cref="ArgumentNullException">The <paramref name="info" /> parameter is null. </exception>
	    protected ElasticsearchException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }    }
}