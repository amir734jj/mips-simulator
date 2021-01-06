using Core.Interfaces;

namespace Core.Tokens
{
    public class Comment : IInstruction
    {
        public string Value { get; }

        public Comment(string value)
        {
            Value = value;
        }

        public override string ToString()
        {
            return $"#{Value}";
        }
    }
}