using UmlautSearchEngine.Application.DTO;
using UmlautSearchEngine.Application.Interfaces;
using UmlautSearchEngine.Domain.Interfaces;

namespace UmlautSearchEngine.Application.Services
{
    public class NameProcessingService : INameProcessingService
    {
        private readonly INameConverter _converter;
        private readonly IVariationGenerator _generator;
        public NameProcessingService(
            INameConverter converter,
            IVariationGenerator generator)
        {
            _converter = converter;
            _generator = generator;
        }

        public NameResult Process(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                throw new ArgumentException("Input name cannot be empty.");

            input = input.ToUpperInvariant();

            var converted = _converter.Convert(input);

            var variations = _generator.Generate(input)
                                       .OrderBy(x => x)
                                       .ToList();

            return new NameResult
            {
                Original = input,
                Converted = converted,
                Variations = variations
            };
        }
    }
}
