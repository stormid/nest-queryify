using System;
using System.Collections.Generic;
using System.Diagnostics;
using Nest.Queryify.Abstractions;
using Nest.Queryify.Abstractions.Queries;
using Nest.Queryify.Extensions;
using Nest.Queryify.Queries;
using Nest.Queryify.Queries.Common;

namespace Nest.Queryify
{
    /// <summary>
    /// The elastic search repository encapsulates standard CRUD operations performed against an index
    /// </summary>
    [DebuggerStepThrough]
    public class ElasticsearchRepository : IElasticsearchRepository
    {
        private readonly IElasticClient _client;
        
        /// <summary>
        /// Creates an instance of the repository
        /// </summary>
        /// <param name="client"></param>
        public ElasticsearchRepository(IElasticClient client)
        {
            _client = client;
        }

        /// <summary>
        /// When overriden can be used to alter the index name returned for repository operations
        /// </summary>
        /// <param name="client">The Elastic client</param>
        /// <param name="index">The original index name supplied</param>
        /// <returns></returns>
        protected virtual string GetIndexName(IElasticClient client, string index = null)
        {
            return index ?? client.Infer.DefaultIndex;
        }

        /// <summary>
        /// Get individual item for <paramref name="id"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public T FindById<T>(string id, string index = null) where T : class
        {
            var response = GetById<T>(id, GetIndexName(_client, index));
            if (response.IsValid && response.Found)
            {
                return response.Source;
            }
            return null;
        }

        /// <summary>
        /// Get individual item for <paramref name="id"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public IGetResponse<T> GetById<T>(string id, string index = null) where T : class
        {
            return Query(new GetByIdQuery<T>(id), index);
        }

        /// <summary>
        /// Execute a query against the given or derived index
        /// </summary>
        /// <typeparam name="TResponse">The response associated with the query</typeparam>
        /// <param name="query">query object to execute</param>
        /// <param name="index">(optional) index on which to execute the query, if not supplied the index default index will be used</param>
        /// <returns>the response associated with the query</returns>
        public TResponse Query<TResponse>(IElasticClientQueryObject<TResponse> query, string index = null)
            where TResponse : class
        {
            return _client.Query(query, GetIndexName(_client, index));
        }

        /// <summary>
        /// Saves an individual item
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="document">the document to save</param>
        /// <param name="index">(optional) index on which to execute the query, if not supplied the index default index will be used</param>
        /// <param name="refreshOnSave">specifies whether to refresh the search index after completing the save operation, this will make the document immediately available to search, only use when you understand the impact</param>
        /// <returns></returns>
        public IIndexResponse Save<T>(T document, string index = null, bool? refreshOnSave = null) where T : class
        {
			if(document == null) throw new ArgumentNullException(nameof(document), "indexed document can not be null");

	        return Query(new IndexDocumentQuery<T>(document, refreshOnSave.GetValueOrDefault(false)), GetIndexName(_client, index));
        }

        /// <summary>
        /// Issues a bulk insert operation for all items in a single bulk batch
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="documents">the document batch to save</param>
        /// <param name="index">(optional) index on which to execute the query, if not supplied the index default index will be used</param>
        /// <param name="refreshOnSave">specifies whether to refresh the search index after completing the save operation, this will make the document immediately available to search, only use when you understand the impact</param>
        /// <returns></returns>
        /// <remarks>Manage your bulk batch size, do not flood the bulk api</remarks>
        public IBulkResponse Bulk<T>(IEnumerable<T> documents, string index = null, bool? refreshOnSave = null) where T : class
        {
	        return Query(new BulkIndexDocumentQuery<T>(documents, refreshOnSave.GetValueOrDefault(false)), GetIndexName(_client, index));
        }

        /// <summary>
        /// Removes an individual item by deriving the id from the given <paramref name="document"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="document"></param>
        /// <param name="index">(optional) index on which to execute the query, if not supplied the index default index will be used</param>
        /// <param name="refreshOnDelete">specifies whether to refresh the search index after completing the delete operation, only use when you understand the impact</param>
        /// <returns></returns>
        public IDeleteResponse Delete<T>(T document, string index = null, bool? refreshOnDelete = null) where T : class
        {
	        return Query(new DeleteDocumentQuery<T>(document, refreshOnDelete.GetValueOrDefault(false)), GetIndexName(_client, index));
        }

        /// <summary>
        /// Removes an individual item identified by <paramref name="id"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <param name="index">(optional) index on which to execute the query, if not supplied the index default index will be used</param>
        /// <param name="refreshOnDelete">specifies whether to refresh the search index after completing the delete operation, only use when you understand the impact</param>
        /// <returns></returns>
        public IDeleteResponse Delete<T>(string id, string index = null, bool? refreshOnDelete = null) where T : class
		{
			return Query(new DeleteByIdQuery<T>(id, refreshOnDelete.GetValueOrDefault(false)), GetIndexName(_client, index));
		}

        /// <summary>
        /// Determines whether an item identified by <paramref name="id"/> exists in the given index
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id">the identifier of the document whose existence you wish to verify</param>
        /// <param name="index">(optional) index on which to execute the query, if not supplied the index default index will be used</param>
        /// <returns></returns>
        public bool Exists<T>(string id, string index = null) where T : class
        {
	        var response = Query(new DocumentExistsByIdQuery<T>(id), GetIndexName(_client, index));
	        return response.IsValid && response.Exists;
        }

        /// <summary>
        /// Determines whether <paramref name="document"/> exists in the given index
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="document">the document whose existence you wish to verify</param>
        /// <param name="index">(optional) index on which to execute the query, if not supplied the index default index will be used</param>
        /// <returns></returns>
        public bool Exists<T>(T document, string index = null) where T : class
        {
			var response = Query(new DocumentExistsQuery<T>(document), GetIndexName(_client, index));
			return response.IsValid && response.Exists;
		}
    }
}