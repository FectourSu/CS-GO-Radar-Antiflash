using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Parser;

namespace СS_GO_Launcher.Models.ParserObjects
{
    public class ClientDll : ParserObject
    {
        public override string Uri => base.Uri + "blob/master/config.json";

        public override string Pattern => "dwEntityList";

        public override string ClassName => "pl-s";

        public override int SkipAfterLines => 3;
    }
}
