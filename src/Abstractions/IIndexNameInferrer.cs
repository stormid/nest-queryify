namespace Nest.Queryify
{
    public interface IIndexNameInferrer
    {
        string GetIndexName(string index = null);
    }
}