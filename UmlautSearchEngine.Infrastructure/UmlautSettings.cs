namespace UmlautSearchEngine.Infrastructure
{
    public class UmlautSettings
    {
        public Dictionary<string, string> UmlautRules { get; set; } = new();
        public int MaxVariations { get; set; } = 1000;
    }
}

