using Microsoft.Extensions.Options;
using UmlautSearchEngine.Domain.Interfaces;
using UmlautSearchEngine.Domain.Model;

namespace UmlautSearchEngine.Infrastructure.Providers
{
    public class UmlautRuleProvider : IUmlautRuleProvider
    {
        private readonly UmlautSettings _settings;

        public UmlautRuleProvider(IOptions<UmlautSettings> options)
        {
            _settings = options.Value;
        }
        public IEnumerable<Domain.Model.UmlautRule> GetRules()
        {
            return _settings.UmlautRules
            .Select(x => new UmlautRule(x.Key, x.Value));
        }
    }
}
