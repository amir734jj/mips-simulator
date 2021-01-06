using Core.Interfaces;

namespace Core.Tokens
{
    public class Semicolon : IInstruction
    {
        public override string ToString()
        {
            return ";";
        }
    }
}