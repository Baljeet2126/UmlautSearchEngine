namespace UmlautSearchEngine.Application.Interfaces
{
    public interface IQueryBuilder
    {
        string BuildQuery(IEnumerable<string> variations);
    }
}
