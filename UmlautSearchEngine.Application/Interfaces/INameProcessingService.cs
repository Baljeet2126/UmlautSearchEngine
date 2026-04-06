using UmlautSearchEngine.Application.DTO;

namespace UmlautSearchEngine.Application.Interfaces
{
    public interface INameProcessingService
    {
        NameResult Process(string input);
    }
}
