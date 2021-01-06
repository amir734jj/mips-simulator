using Core.Interfaces;

namespace Core.Tokens
{
    public class CodeDirective : IInstruction
    {
        public override string ToString()
        {
            return ".code";
        }
    }
}