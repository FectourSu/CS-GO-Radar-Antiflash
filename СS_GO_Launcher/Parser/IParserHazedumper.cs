using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parser
{
    public interface IParserHazedumper
    {
        event EventHandler<string> Working;

        Task<IEnumerable<ParserResult>> RunAsync(params ParserObject[] setings);
    }
}
