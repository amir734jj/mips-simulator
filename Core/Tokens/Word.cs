using Core.Interfaces;

namespace Core.Tokens
{
    public class Word : IInstruction
    {
        public override string ToString()
        {
            return ".word";
        }
    }
}