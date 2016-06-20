using System.Threading.Tasks;

namespace Nest.Queryify.Queries.Common
{
    /// <summary>
    /// Returns a document for a given id
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class GetByIdQuery<T> : GetQuery<T> where T : class
    {
        private readonly string _id;

        /// <summary>
        /// Returns a document for the specified id
        /// </summary>
        /// <param name="id"></param>
        public GetByIdQuery(string id)
        {
            _id = id;
        }

        /// <summary>
        /// Implement to build query
        /// </summary>
        /// <param name="descriptor"></param>
        /// <returns></returns>
        protected override GetDescriptor<T> BuildQuery(GetDescriptor<T> descriptor)
        {
            return descriptor.Id(_id);
        }
    }
}