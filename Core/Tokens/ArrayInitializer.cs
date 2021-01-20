using Core.Interfaces;

namespace Core.Tokens
{
    public class ArrayInitializer : IInstruction
    {
        public int DefaultValue { get; }
        public int Count { get; }

        public ArrayInitializer(int defaultValue, int count)
        {
            DefaultValue = defaultValue;
            Count = count;
        }

        public override string ToString()
        {
            return $"{DefaultValue}:{Count}";
        }
    }
}