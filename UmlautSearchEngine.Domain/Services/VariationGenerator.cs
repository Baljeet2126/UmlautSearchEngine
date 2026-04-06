using UmlautSearchEngine.Domain.Configuration;
using UmlautSearchEngine.Domain.Interfaces;
using UmlautSearchEngine.Domain.Model;

namespace UmlautSearchEngine.Domain.Services
{
 
    namespace UmlautSearchEngine.Domain.Services
    {
        public class VariationGenerator : IVariationGenerator
        {
            private readonly List<UmlautRule> _rules;
            private readonly int _maxVariations;
            private readonly Dictionary<string, List<string>> _cache = new();

            public VariationGenerator(
               IUmlautRuleProvider ruleProvider,
               VariationConfig config
               )
            {
                _rules = ruleProvider.GetRules()?.ToList()
                ?? throw new InvalidOperationException("No rules provided.");

                _maxVariations = config.MaxVariations;
            }

            public IEnumerable<string> Generate(string input)
            {
                if (string.IsNullOrWhiteSpace(input))
                    throw new ArgumentException("Input cannot be null or empty.");

                if (_cache.TryGetValue(input, out var cached))
                    return cached;

                var results = GenerateVariations(input);

                _cache[input] = results;
                    return results;
            }

            private List<string> GenerateVariations(string input)
            {
                var results = new HashSet<string>();
                var queue = new Queue<string>();

                queue.Enqueue(input);
                results.Add(input);

                while (queue.Count > 0)
                {
                    var current = queue.Dequeue();

                    foreach (var rule in _rules)
                    {
                        int index = current.IndexOf(rule.Source);

                        while (index != -1)
                        {
                            var replaced =
                                current.Substring(0, index) +
                                rule.Target +
                                current.Substring(index + rule.Source.Length);

                            if (results.Add(replaced))
                            {
                                // LIMIT number of variations allowed.
                                if (results.Count >= _maxVariations)
                                {
                                    throw new InvalidOperationException(
                                        $"Too many variations generated (>{_maxVariations}).");
                                }

                                queue.Enqueue(replaced);
                            }


                            index = current.IndexOf(rule.Source, index + 1);
                        }
                    }
                }
                return [.. results];
            }
        }
    }
}
