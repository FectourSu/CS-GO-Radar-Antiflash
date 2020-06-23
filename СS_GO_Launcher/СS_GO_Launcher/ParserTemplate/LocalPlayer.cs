using Parser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace СS_GO_Launcher.ParserTemplate
{
    class LocalPlayer : ParserObject
    {
        public override string Uri => base.Uri + "/blob/master/csgo.hpp";

        public override string Pattern => "dwLocalPlayer";

        public override string ClassName => "pl-c1";
    }
}
