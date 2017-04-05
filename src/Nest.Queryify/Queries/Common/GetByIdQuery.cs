using System;

namespace Nest.Queryify.Queries.Common
{
    public class GetByIdQuery<T> : GetQuery<T> where T : class
    {
        [Obsolete(ObsoleteMessage)]
        public GetByIdQuery(string id) : base(id)
        {
        }

        public GetByIdQuery(DocumentPath<T> documentPath) : base(documentPath)
        {
            
        }

        protected override GetDescriptor<T> BuildQuery(GetDescriptor<T> descriptor)
        {
            return descriptor;
        }
    }
}