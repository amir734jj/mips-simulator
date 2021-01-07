using Core.Interfaces;

namespace Core.Tokens
{
    public class JumpUnconditional : IInstruction
    {
        public string Value { get; }

        public JumpUnconditional(string value)
        {
            Value = value;
        }
        
        public override string ToString()
        {
            return $"j {Value}";
        }
    }
}