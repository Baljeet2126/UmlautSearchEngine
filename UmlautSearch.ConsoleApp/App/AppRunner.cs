using UmlautSearchEngine.Application.DTO;
using UmlautSearchEngine.Application.Interfaces;

namespace UmlautSearch.ConsoleApp.App
{
    public class AppRunner
    {
        private readonly INameProcessingService _processor;
        private readonly IQueryBuilder _sqlBuilder;
        private readonly IDataRepository _repository;

        public AppRunner(
            INameProcessingService processor,
            IQueryBuilder sqlBuilder,
            IDataRepository dataRepository
            )
        {
            _processor = processor;
            _sqlBuilder = sqlBuilder;
            _repository = dataRepository;
        }

        public async Task RunAsync()
        {
            var names = new[]
            {
            "KOESTNER",
            "RUESSWURM",
            "DUERMUELLER",
            "JAEAESKELAEINEN",
            "GROSSSCHAEDL"
        };

            foreach (var name in names)
            {
                try
                {
                    var result = _processor.Process(name);

                    _repository.SaveVariants(result.Original, result.Variations);

                    var sql = _sqlBuilder.BuildQuery(result.Variations);

                    var matches = _repository.Search(sql);

                    Print(result, sql, matches);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }

            await Task.CompletedTask;
        }

        private void Print(NameResult result, string sql, List<string> matches)
        {
            Console.WriteLine("========================================");
            Console.WriteLine($"Input: {result.Original}");

            Console.WriteLine("\nConverted:");
            Console.WriteLine(result.Converted);

            Console.WriteLine("\nVariations:");
            foreach (var v in result.Variations)
                Console.WriteLine($"- {v}");

            // ✅ Variation count
            Console.WriteLine($"\nTotal Variations: {result.Variations.Count}");

            Console.WriteLine("\nSQL:");
            Console.WriteLine(sql);

            // ✅ DB results
            Console.WriteLine("\nMatches from DB:");
            if (matches.Any())
            {
                foreach (var match in matches)
                    Console.WriteLine($"- {match}");
            }
            else
            {
                Console.WriteLine("No matches found.");
            }

            // ✅ Result count
            Console.WriteLine($"\nTotal Matches: {matches.Count}");

            Console.WriteLine("========================================\n");
        }
    }
}
