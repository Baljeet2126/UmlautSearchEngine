using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UmlautSearchEngine.Domain.Model;

namespace UmlautSearchEngine.Domain.Interfaces
{
    public interface IUmlautRuleProvider
    {
        IEnumerable<UmlautRule> GetRules();
    }
}
