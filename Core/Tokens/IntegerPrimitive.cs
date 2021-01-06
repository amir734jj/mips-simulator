using Core.Interfaces;

namespace Core.Tokens
{
    public class IntegerPrimitive : IInstruction
    {
        public int Value { get; }

        public IntegerPrimitive(int value)
        {
            Value = value;
        }
        
        public override string ToString()
        {
            return Value.ToString();
        }
    }
}