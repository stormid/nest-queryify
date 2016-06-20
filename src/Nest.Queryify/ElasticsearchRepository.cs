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
    [DebuggerStepThrough]
    public class ElasticsearchRepository : IElasticsearchRepository
    {
        private readonly IElasticClient _client;
        
        public ElasticsearchRepository(IElasticClient client)
        {
            _client = client;
        }

        protected virtual string GetIndexName(IElasticClient client, string index = null)
        {
            return index ?? client.Infer.DefaultIndex;
        }

        public T FindById<T>(string id, string index = null) where T : class
        {
            var response = GetById<T>(id, GetIndexName(_client, index));
            if (response.IsValid && response.Found)
            {
                return response.Source;
            }
            return null;
        }

        public IGetResponse<T> GetById<T>(string id, string index = null) where T : class
        {
            return Query(new GetByIdQuery<T>(id), index);
        }

        public TResponse Query<TResponse>(IElasticClientQueryObject<TResponse> query, string index = null)
            where TResponse : class
        {
            return _client.Query(query, GetIndexName(_client, index));
        }

		/// <exception cref="NullReferenceException">indexed document can not be null</exception>
		public IIndexResponse Save<T>(T document, string index = null, bool? refreshOnSave = null) where T : class
        {
			if(document == null) throw new ArgumentNullException(nameof(document), "indexed document can not be null");

	        return Query(new IndexDocumentQuery<T>(document, refreshOnSave.GetValueOrDefault(false)), GetIndexName(_client, index));
        }

		public IBulkResponse Bulk<T>(IEnumerable<T> documents, string index = null, bool? refreshOnSave = null) where T : class
        {
	        return Query(new BulkIndexDocumentQuery<T>(documents, refreshOnSave.GetValueOrDefault(false)), GetIndexName(_client, index));
        }

        public IDeleteResponse Delete<T>(T document, string index = null, bool? refreshOnDelete = null) where T : class
        {
	        return Query(new DeleteDocumentQuery<T>(document, refreshOnDelete.GetValueOrDefault(false)), GetIndexName(_client, index));
        }

		public IDeleteResponse Delete<T>(string id, string index = null, bool? refreshOnDelete = null) where T : class
		{
			return Query(new DeleteByIdQuery<T>(id, refreshOnDelete.GetValueOrDefault(false)), GetIndexName(_client, index));
		}

        public bool Exists<T>(string id, string index = null) where T : class
        {
	        var response = Query(new DocumentExistsByIdQuery<T>(id), GetIndexName(_client, index));
	        return response.IsValid && response.Exists;
        }

        public bool Exists<T>(T document, string index = null) where T : class
        {
			var response = Query(new DocumentExistsQuery<T>(document), GetIndexName(_client, index));
			return response.IsValid && response.Exists;
		}
    }
}