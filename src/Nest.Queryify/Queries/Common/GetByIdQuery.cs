using System.Threading.Tasks;

namespace Nest.Queryify.Queries.Common
{
    public class GetByIdQuery<T> : GetQuery<T> where T : class
    {
        private readonly string _id;

        public GetByIdQuery(string id)
        {
            _id = id;
        }

        protected override GetDescriptor<T> BuildQuery(GetDescriptor<T> descriptor)
        {
            return descriptor.Id(_id);
        }
    }
}