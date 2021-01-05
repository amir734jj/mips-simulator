using Core.Interfaces;

namespace Core.Tokens
{
    public class SystemCall : IInstruction
    {
        public int Number { get; }
        
        public SystemCall()
        {
            Number = 0;
        }
    }
}