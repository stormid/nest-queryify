using System;
using System.Runtime.Serialization;
using Elasticsearch.Net;

namespace Nest.Queryify.Exceptions
{
    /// <summary>
    /// Query object exception
    /// </summary>
    [Serializable]
    public class ElasticClientQueryObjectException : ElasticsearchException
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="exceptionType"></param>
        /// <param name="status"></param>
        public ElasticClientQueryObjectException(string message, string exceptionType, int status) : base(message, exceptionType, status)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="exceptionType"></param>
        /// <param name="status"></param>
        /// <param name="inner"></param>
        public ElasticClientQueryObjectException(string message, string exceptionType, int status, Exception inner) : base(message, exceptionType, status, inner)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="inner"></param>
        public ElasticClientQueryObjectException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}