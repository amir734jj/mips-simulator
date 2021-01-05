using Core.Interfaces;

namespace Core.Tokens
{
    public class Label : IInstruction
    {
        public string Value { get; }

        public Label(string value)
        {
            Value = value;
        }
    }
}