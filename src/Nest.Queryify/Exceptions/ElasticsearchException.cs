using System;
using System.Runtime.Serialization;
using Elasticsearch.Net;

namespace Nest.Queryify.Exceptions
{
    /// <summary>
    /// Base elastic search exception
    /// </summary>
    [Serializable]
    public class ElasticsearchException : Exception
    {
        /// <summary>
        /// Exception occurring
        /// </summary>
        public string ExceptionType { get; private set; }
        
        /// <summary>
        /// Status returned
        /// </summary>
        public int Status { get; private set; }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="exceptionType"></param>
        /// <param name="status"></param>
        public ElasticsearchException(string message, string exceptionType, int status) : base(message)
        {
            ExceptionType = exceptionType;
            Status = status;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="exceptionType"></param>
        /// <param name="status"></param>
        /// <param name="inner"></param>
        public ElasticsearchException(string message, string exceptionType, int status, Exception inner)
            : base(message, inner)
        {
            ExceptionType = exceptionType;
            Status = status;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="inner"></param>
        public ElasticsearchException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}