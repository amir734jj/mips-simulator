using Core.Interfaces;

namespace Core.Tokens
{
    public class AsciizDirective : IInstruction
    {
        public override string ToString()
        {
            return ".asciiz";
        }
    }
}