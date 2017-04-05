using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Nest.Queryify.Abstractions;
using Nest.Queryify.Abstractions.Queries;
using Nest.Queryify.Extensions;
using Nest.Queryify.Queries;
using Nest.Queryify.Queries.Common;

namespace Nest.Queryify
{
    public partial class ElasticsearchRepository
    {
        [Obsolete("Use implementation accepting DocumentPath")]
        public async Task<T> FindByIdAsync<T>(string id, string index = null) where T : class
        {
            var documentPath = DocumentPath<T>.Id(id);
            return await FindByIdAsync(documentPath, GetIndexName(client, index));
        }

        public async Task<T> FindByIdAsync<T>(DocumentPath<T> documentPath, string index = null) where T : class
        {
            var response = await GetByIdAsync(documentPath, GetIndexName(client, index));
            if (response.IsValid && response.Found)
            {
                return response.Source;
            }
            return null;
        }

        [Obsolete("Use implementation accepting DocumentPath")]
        public async Task<IGetResponse<T>> GetByIdAsync<T>(string id, string index = null) where T : class
        {
            var documentPath = DocumentPath<T>.Id(id);
            return await QueryAsync(new GetByIdQuery<T>(documentPath), GetIndexName(client, index));
        }

        public async Task<IGetResponse<T>> GetByIdAsync<T>(DocumentPath<T> documentPath, string index = null) where T : class
        {
            return await QueryAsync(new GetByIdQuery<T>(documentPath), GetIndexName(client, index));
        }

        public async Task<TResponse> QueryAsync<TResponse>(IElasticClientQueryObject<TResponse> query, string index = null) where TResponse : class
        {
            return await client.QueryAsync(query, GetIndexName(client, index));
        }

        public async Task<IIndexResponse> SaveAsync<T>(T document, string index = null, bool? refreshOnSave = null) where T : class
        {
            if (document == null) throw new ArgumentNullException(nameof(document), "indexed document can not be null");

            return await QueryAsync(new IndexDocumentQuery<T>(document, refreshOnSave.GetValueOrDefault(false)), GetIndexName(client, index));
        }

        public async Task<IBulkResponse> BulkAsync<T>(IEnumerable<T> documents, string index = null, bool? refreshOnSave = null) where T : class
        {
            return await QueryAsync(new BulkIndexDocumentQuery<T>(documents, refreshOnSave.GetValueOrDefault(false)), GetIndexName(client, index));

        }

        public async Task<IDeleteResponse> DeleteAsync<T>(T document, string index = null, bool? refreshOnDelete = null) where T : class
        {
            return await QueryAsync(new DeleteDocumentQuery<T>(document, refreshOnDelete.GetValueOrDefault(false)), GetIndexName(client, index));
        }

        public async Task<IDeleteResponse> DeleteAsync<T>(string id, string index = null, bool? refreshOnDelete = null) where T : class
        {
            return await QueryAsync(new DeleteByIdQuery<T>(id, refreshOnDelete.GetValueOrDefault(false)), GetIndexName(client, index));
        }

        public async Task<bool> ExistsAsync<T>(T document, string index = null) where T : class
        {
            var response = await QueryAsync(new DocumentExistsQuery<T>(document), GetIndexName(client, index));
            return response.IsValid && response.Exists;
        }

        public async Task<bool> ExistsAsync<T>(string id, string index = null) where T : class
        {
            var response = await QueryAsync(new DocumentExistsByIdQuery<T>(id), GetIndexName(client, index));
            return response.IsValid && response.Exists;
        }
    }

    [DebuggerStepThrough]
    public partial class ElasticsearchRepository : IElasticsearchRepository
    {
        private readonly IElasticClient client;

        public ElasticsearchRepository(IElasticClient client)
        {
            this.client = client;
        }

        protected virtual string GetIndexName(IElasticClient client, string index = null)
        {
            return index ?? client.GetDefaultIndex();
        }

        [Obsolete("Use implementation accepting DocumentPath")]
        public T FindById<T>(string id, string index = null) where T : class
        {
            var documentPath = DocumentPath<T>.Id(id);
            return FindById(documentPath, GetIndexName(client, index));
        }

        public T FindById<T>(DocumentPath<T> documentPath, string index = null) where T : class
        {
            var response = GetById(documentPath, GetIndexName(client, index));
            if (response.IsValid && response.Found)
            {
                return response.Source;
            }
            return null;
        }

        [Obsolete("Use implementation accepting DocumentPath")]
        public IGetResponse<T> GetById<T>(string id, string index = null) where T : class
        {
            var documentPath = DocumentPath<T>.Id(id);
            return GetById(documentPath, index);
        }

        public IGetResponse<T> GetById<T>(DocumentPath<T> documentPath, string index = null) where T : class
        {
            return Query(new GetByIdQuery<T>(documentPath), GetIndexName(client, index));
        }

        public TResponse Query<TResponse>(IElasticClientQueryObject<TResponse> query, string index = null)
            where TResponse : class
        {
            return client.Query(query, GetIndexName(client, index));
        }

        /// <exception cref="NullReferenceException">indexed document can not be null</exception>
        public IIndexResponse Save<T>(T document, string index = null, bool? refreshOnSave = null) where T : class
        {
            if (document == null) throw new ArgumentNullException(nameof(document), "indexed document can not be null");

            return Query(new IndexDocumentQuery<T>(document, refreshOnSave.GetValueOrDefault(false)), GetIndexName(client, index));
        }

        public IBulkResponse Bulk<T>(IEnumerable<T> documents, string index = null, bool? refreshOnSave = null) where T : class
        {
            return Query(new BulkIndexDocumentQuery<T>(documents, refreshOnSave.GetValueOrDefault(false)), GetIndexName(client, index));
        }

        public IDeleteResponse Delete<T>(T document, string index = null, bool? refreshOnDelete = null) where T : class
        {
            return Query(new DeleteDocumentQuery<T>(document, refreshOnDelete.GetValueOrDefault(false)), GetIndexName(client, index));
        }

        public IDeleteResponse Delete<T>(string id, string index = null, bool? refreshOnDelete = null) where T : class
        {
            return Query(new DeleteByIdQuery<T>(id, refreshOnDelete.GetValueOrDefault(false)), GetIndexName(client, index));
        }

        public bool Exists<T>(string id, string index = null) where T : class
        {
            var response = Query(new DocumentExistsByIdQuery<T>(id), GetIndexName(client, index));
            return response.IsValid && response.Exists;
        }

        public bool Exists<T>(T document, string index = null) where T : class
        {
            var response = Query(new DocumentExistsQuery<T>(document), GetIndexName(client, index));
            return response.IsValid && response.Exists;
        }
    }
}