namespace Nest.Queryify.Abstractions
{
    public interface IIndexNameInferrer
    {
        string GetIndexName(string index = null);
    }
}