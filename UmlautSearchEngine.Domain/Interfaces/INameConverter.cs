using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UmlautSearchEngine.Domain.Interfaces
{
    public interface INameConverter
    {
        string Convert(string input);
    }
}
