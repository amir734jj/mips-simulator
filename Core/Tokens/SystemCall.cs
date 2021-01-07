using Core.Interfaces;

namespace Core.Tokens
{
    public class SystemCall : IInstruction
    {
        public override string ToString()
        {
            return "syscall";
        }
    }
}