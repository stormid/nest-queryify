using System;
using System.Runtime.Serialization;
using Elasticsearch.Net;

namespace Nest.Queryify.Exceptions
{
    [Serializable]
    public class ElasticClientQueryObjectException : ElasticsearchException
    {
        public ElasticClientQueryObjectException(ElasticsearchServerError error) : base(error)
        {
        }

        public ElasticClientQueryObjectException(string message, string exceptionType, int status) : base(message, exceptionType, status)
        {
        }

        public ElasticClientQueryObjectException(string message, string exceptionType, int status, Exception inner) : base(message, exceptionType, status, inner)
        {
        }

        public ElasticClientQueryObjectException(string message, Exception inner) : base(message, inner)
        {
        }

	    /// <exception cref="SerializationException">The class name is null or <see cref="P:System.Exception.HResult" /> is zero (0). </exception>
	    /// <exception cref="ArgumentNullException">The <paramref name="info" /> parameter is null. </exception>
	    protected ElasticClientQueryObjectException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}