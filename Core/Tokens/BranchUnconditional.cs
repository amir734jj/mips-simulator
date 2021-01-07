using Core.Interfaces;

namespace Core.Tokens
{
    public class BranchUnconditional : IInstruction
    {
        public string Value { get; }

        public BranchUnconditional(string value)
        {
            Value = value;
        }
        
        public override string ToString()
        {
            return $"b {Value}";
        }
    }
}