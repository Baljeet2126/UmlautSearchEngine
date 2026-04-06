using UmlautSearchEngine.Application.Interfaces;

namespace UmlautSearchEngine.Application.Builders
{
    public class QueryBuilder : IQueryBuilder
    {
        public string BuildQuery(IEnumerable<string> variations)
        {
            if (variations == null)
                throw new ArgumentNullException(nameof(variations));

            var distinctVariations = variations
                .Where(v => !string.IsNullOrWhiteSpace(v))
                .Distinct()
                .ToList();

            if (!distinctVariations.Any())
                throw new ArgumentException("Variations cannot be empty.");

            var values = string.Join(", ", distinctVariations.Select(v => $"'{v}'"));

            return $"SELECT * FROM tbl_phonebook WHERE lastname IN ({values});";
        }
    }
}
