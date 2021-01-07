using Core.Interfaces;

namespace Core.Tokens
{
    public class DataDirective : IInstruction
    {
        public override string ToString()
        {
            return ".data";
        }
    }
}