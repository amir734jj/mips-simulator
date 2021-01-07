using Core.Interfaces;

namespace Core.Tokens
{
    public class JumpAndLink : IInstruction
    {
        public string Value { get; }

        public JumpAndLink(string value)
        {
            Value = value;
        }
        
        public override string ToString()
        {
            return $"jal {Value}";
        }
    }
}