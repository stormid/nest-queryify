namespace Nest.Queryify.Queries.Common
{
    public class GetByIdQuery<T> : GetQueryObject<T> where T : class
    {
        private readonly string _id;

        public GetByIdQuery(string id)
        {
            _id = id;
        }

        protected override IGetResponse<T> ExecuteCore(IElasticClient client, string index)
        {
            return client.Get<T>(desc => BuildQuery(desc).Index(index));
        }

        protected override GetDescriptor<T> BuildQuery(GetDescriptor<T> descriptor)
        {
            return descriptor.Id(_id);
        }
    }
}