using Core.Interfaces;

namespace Core.Tokens
{
    public class TextDirective : IInstruction
    {
        public override string ToString()
        {
            return ".text";
        }
    }
}