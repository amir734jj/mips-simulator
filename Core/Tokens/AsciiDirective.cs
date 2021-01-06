using Core.Interfaces;

namespace Core.Tokens
{
    public class AsciiDirective : IInstruction
    {
        public override string ToString()
        {
            return ".asciiz";
        }
    }
}