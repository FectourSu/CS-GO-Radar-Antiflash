using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Parser;

namespace СS_GO_Launcher.Models.ParserObjects
{
    public class Spostted : ParserObject
    {
        public override string Uri => base.Uri + "/blob/master/csgo.hpp";

        public override string Pattern => "m_bSpotted";

        public override string ClassName => "pl-c1";
    }
}
