namespace UmlautSearchEngine.Application.Interfaces
{
    public interface IDataRepository
    {
        List<string> Search(string sql);

        void SaveVariants(string originalName, IEnumerable<string> variants);
    }
}
