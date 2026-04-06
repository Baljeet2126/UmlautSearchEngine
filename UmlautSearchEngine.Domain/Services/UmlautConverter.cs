using UmlautSearchEngine.Domain.Interfaces;
using UmlautSearchEngine.Domain.Model;

namespace UmlautSearchEngine.Domain.Services
{
    public class UmlautConverter : INameConverter
    {
        private readonly List<UmlautRule> _rules;

        public UmlautConverter(IUmlautRuleProvider ruleProvider)
        {
            _rules = ruleProvider.GetRules()?.ToList()
                     ?? throw new InvalidOperationException("No umlaut rules provided.");
        }

        public string Convert(string input)
        {
            var result = input;

            foreach (var rule in _rules)
            {
                result = result.Replace(rule.Source, rule.Target);
            }

            return result;
        }
    }
}
