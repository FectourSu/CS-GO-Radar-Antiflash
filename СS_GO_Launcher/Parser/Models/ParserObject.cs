using System;

namespace Parser
{
    public abstract class ParserObject
    {
        public virtual string Uri => "https://github.com/frk1/hazedumper/";

        public abstract string Pattern { get; }

        public abstract string ClassName { get; }

        public virtual int SkipAfterLines { get; }
    }
}