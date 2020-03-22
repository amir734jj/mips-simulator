namespace Core.Models
{
    public abstract class Instruction
    {
        public virtual Context Pipeline(Context context)
        {
            return context;
        }
    }
}