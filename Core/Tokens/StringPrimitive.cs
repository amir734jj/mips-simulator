using Core.Interfaces;

namespace Core.Tokens
{
    public class StringPrimitive : IInstruction
    {
        public string Value { get; }

        public StringPrimitive(string value)
        {
            Value = value;
        }
    }
}