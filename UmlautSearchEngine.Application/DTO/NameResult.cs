using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UmlautSearchEngine.Application.DTO
{
    public class NameResult
    {
        public string Original { get; init; }
        public string Converted { get; init; }
        public List<string> Variations { get; init; } = new();
    }
}
